using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClumsySanta.Redist.Controls
{
    public class CollectionFlowPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));

        public static readonly DependencyProperty AnimationFunctionProperty = DependencyProperty.Register("AnimationFunction", typeof(IEasingFunction), typeof(CollectionFlowPanel), null);
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(CollectionFlowPanel), null);

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register("ItemWidth", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty ItemRelativeSizeProperty = DependencyProperty.Register("ItemRelativeSize", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty UseRelativeSizeProperty = DependencyProperty.Register("UseRelativeSize", typeof(bool), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));

        public static readonly DependencyProperty ItemVisibilityProperty = DependencyProperty.Register("ItemVisibility", typeof(int), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty ItemFadingProperty = DependencyProperty.Register("ItemFading", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));

        public static readonly DependencyProperty FocusedItemDistanceProperty = DependencyProperty.Register("FocusedItemDistance", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemDistanceProperty = DependencyProperty.Register("UnfocusedItemDistance", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty FocusedItemOffsetProperty = DependencyProperty.Register("FocusedItemOffset", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemOffsetProperty = DependencyProperty.Register("UnfocusedItemOffset", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty FocusedItemAngleProperty = DependencyProperty.Register("FocusedItemAngle", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemAngleProperty = DependencyProperty.Register("UnfocusedItemAngle", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));

        public static readonly DependencyProperty FocusedItemIndexProperty = DependencyProperty.Register("FocusedItemIndex", typeof(int), typeof(CollectionFlowPanel), new PropertyMetadata(CollectionFlowPanel.PropertyChanged));

        public CollectionFlowPanel()
        {
            Orientation = Orientation.Horizontal;

            AnimationFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            AnimationDuration = TimeSpan.FromSeconds(0.4);

            ItemWidth = 100;
            ItemHeight = 100;
            ItemRelativeSize = 0.8;
            UseRelativeSize = false;

            ItemFading = 0.2;
            ItemVisibility = 3;

            FocusedItemDistance = 0;
            UnfocusedItemDistance = 200;
            FocusedItemOffset = 50;
            UnfocusedItemOffset = 10;
            FocusedItemAngle = 0;
            UnfocusedItemAngle = 45;

            FocusedItemIndex = -1;
        }

        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CollectionFlowPanel)sender).InvalidateArrange(); }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement child in Children)
            {
                if (child.RenderTransform.GetType() != typeof(TranslateTransform))
                    child.RenderTransform = new TranslateTransform();
                if (child.Projection == null)
                    child.Projection = new PlaneProjection();

                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            double isw = ItemWidth;
            double ish = ItemHeight;

            if (UseRelativeSize)
            {
                if (Orientation == Orientation.Vertical)
                    ish = isw = finalSize.Width * ItemRelativeSize;
                else
                    ish = isw = finalSize.Height * ItemRelativeSize;
            }

            Point center = new Point((finalSize.Width - isw) / 2, (finalSize.Height - ish) / 2);

            for (int i = 0; i < Children.Count; i++)
            {
                FrameworkElement child = (FrameworkElement)Children[i];
                child.Width = isw;
                child.Height = ish;
                child.Arrange(new Rect(center.X, center.Y, isw, ish));

                Storyboard storyboard = new Storyboard();

                double dist = i - FocusedItemIndex;
                double disti = Math.Abs(dist);

                Canvas.SetZIndex(child, -(int)disti);

                TranslateTransform transform = (TranslateTransform)child.RenderTransform;

                DoubleAnimation tanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(tanim, transform);

                if (Orientation == Orientation.Vertical)
                    Storyboard.SetTargetProperty(tanim, new PropertyPath(TranslateTransform.YProperty));
                else
                    Storyboard.SetTargetProperty(tanim, new PropertyPath(TranslateTransform.XProperty));
                tanim.To = disti > 1 ? (dist < 0 ? -FocusedItemOffset : FocusedItemOffset) + dist * UnfocusedItemOffset : dist * FocusedItemOffset;

                PlaneProjection projection = (PlaneProjection)child.Projection;

                DoubleAnimation danim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(danim, projection);
                Storyboard.SetTargetProperty(danim, new PropertyPath(PlaneProjection.GlobalOffsetZProperty));

                DoubleAnimation aanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(aanim, projection);

                if (dist != 0)
                {
                    danim.To = -disti * UnfocusedItemDistance;

                    if (Orientation == Orientation.Vertical)
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(PlaneProjection.RotationXProperty));
                    else
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(PlaneProjection.RotationYProperty));
                    aanim.To = dist < 0 ? -UnfocusedItemAngle : UnfocusedItemAngle;
                }
                else
                {
                    danim.To = FocusedItemDistance;
                    if (Orientation == Orientation.Vertical)
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(PlaneProjection.RotationXProperty));
                    else
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(PlaneProjection.RotationYProperty));
                    aanim.To = FocusedItemAngle;
                }

                DoubleAnimation fanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(fanim, child);
                Storyboard.SetTargetProperty(fanim, new PropertyPath(FrameworkElement.OpacityProperty));

                if (disti > ItemVisibility)
                    fanim.To = 0.0;
                else
                    fanim.To = Math.Max(0.0, 1.0 - disti * ItemFading);

                storyboard.Children.Add(tanim);
                storyboard.Children.Add(danim);
                storyboard.Children.Add(aanim);
                storyboard.Children.Add(fanim);
                storyboard.Begin();
            }

            return base.ArrangeOverride(finalSize);
        }

        [Category("CollectionFlow Panel")]
        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(CollectionFlowPanel.OrientationProperty); }
            set { this.SetValue(CollectionFlowPanel.OrientationProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public IEasingFunction AnimationFunction
        {
            get { return (IEasingFunction)this.GetValue(CollectionFlowPanel.AnimationFunctionProperty); }
            set { this.SetValue(CollectionFlowPanel.AnimationFunctionProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)this.GetValue(CollectionFlowPanel.AnimationDurationProperty); }
            set { this.SetValue(CollectionFlowPanel.AnimationDurationProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double ItemWidth
        {
            get { return (double)this.GetValue(CollectionFlowPanel.ItemWidthProperty); }
            set { this.SetValue(CollectionFlowPanel.ItemWidthProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double ItemHeight
        {
            get { return (double)this.GetValue(CollectionFlowPanel.ItemHeightProperty); }
            set { this.SetValue(CollectionFlowPanel.ItemHeightProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double ItemRelativeSize
        {
            get { return (double)this.GetValue(CollectionFlowPanel.ItemRelativeSizeProperty); }
            set { this.SetValue(CollectionFlowPanel.ItemRelativeSizeProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public bool UseRelativeSize
        {
            get { return (bool)this.GetValue(CollectionFlowPanel.UseRelativeSizeProperty); }
            set { this.SetValue(CollectionFlowPanel.UseRelativeSizeProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double ItemFading
        {
            get { return (double)this.GetValue(CollectionFlowPanel.ItemFadingProperty); }
            set { this.SetValue(CollectionFlowPanel.ItemFadingProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public int ItemVisibility
        {
            get { return (int)this.GetValue(CollectionFlowPanel.ItemVisibilityProperty); }
            set { this.SetValue(CollectionFlowPanel.ItemVisibilityProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double FocusedItemDistance
        {
            get { return (double)this.GetValue(CollectionFlowPanel.FocusedItemDistanceProperty); }
            set { this.SetValue(CollectionFlowPanel.FocusedItemDistanceProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemDistance
        {
            get { return (double)this.GetValue(CollectionFlowPanel.UnfocusedItemDistanceProperty); }
            set { this.SetValue(CollectionFlowPanel.UnfocusedItemDistanceProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double FocusedItemOffset
        {
            get { return (double)this.GetValue(CollectionFlowPanel.FocusedItemOffsetProperty); }
            set { this.SetValue(CollectionFlowPanel.FocusedItemOffsetProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemOffset
        {
            get { return (double)this.GetValue(CollectionFlowPanel.UnfocusedItemOffsetProperty); }
            set { this.SetValue(CollectionFlowPanel.UnfocusedItemOffsetProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double FocusedItemAngle
        {
            get { return (double)this.GetValue(CollectionFlowPanel.FocusedItemAngleProperty); }
            set { this.SetValue(CollectionFlowPanel.FocusedItemAngleProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemAngle
        {
            get { return (double)this.GetValue(CollectionFlowPanel.UnfocusedItemAngleProperty); }
            set { this.SetValue(CollectionFlowPanel.UnfocusedItemAngleProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public int FocusedItemIndex
        {
            get { return (int)this.GetValue(CollectionFlowPanel.FocusedItemIndexProperty); }
            set { this.SetValue(CollectionFlowPanel.FocusedItemIndexProperty, value); }
        }
    }
}
