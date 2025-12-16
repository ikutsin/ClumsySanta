using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClumsySanta.Controls
{
    public class CollectionFlowPanel : Panel
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty AnimationFunctionProperty = DependencyProperty.Register("AnimationFunction", typeof(IEasingFunction), typeof(CollectionFlowPanel), null);
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(CollectionFlowPanel), null);

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register("ItemWidth", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty ItemRelativeSizeProperty = DependencyProperty.Register("ItemRelativeSize", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty UseRelativeSizeProperty = DependencyProperty.Register("UseRelativeSize", typeof(bool), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty ItemVisibilityProperty = DependencyProperty.Register("ItemVisibility", typeof(int), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty ItemFadingProperty = DependencyProperty.Register("ItemFading", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty FocusedItemDistanceProperty = DependencyProperty.Register("FocusedItemDistance", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemDistanceProperty = DependencyProperty.Register("UnfocusedItemDistance", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty FocusedItemOffsetProperty = DependencyProperty.Register("FocusedItemOffset", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemOffsetProperty = DependencyProperty.Register("UnfocusedItemOffset", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty FocusedItemAngleProperty = DependencyProperty.Register("FocusedItemAngle", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));
        public static readonly DependencyProperty UnfocusedItemAngleProperty = DependencyProperty.Register("UnfocusedItemAngle", typeof(double), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty FocusedItemIndexProperty = DependencyProperty.Register("FocusedItemIndex", typeof(int), typeof(CollectionFlowPanel), new PropertyMetadata(PropertyChanged));

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
                if (!(child.RenderTransform is TransformGroup))
                {
                    var tg = new TransformGroup();
                    tg.Children.Add(new RotateTransform());
                    tg.Children.Add(new TranslateTransform());
                    child.RenderTransform = tg;
                }

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

                SetZIndex(child, -(int)disti);

                var tg = (TransformGroup)child.RenderTransform;
                var rtransform = (RotateTransform)tg.Children[0];
                var transform = (TranslateTransform)tg.Children[1];

                DoubleAnimation tanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(tanim, transform);

                if (Orientation == Orientation.Vertical)
                    Storyboard.SetTargetProperty(tanim, new PropertyPath(TranslateTransform.YProperty));
                else
                    Storyboard.SetTargetProperty(tanim, new PropertyPath(TranslateTransform.XProperty));
                tanim.To = disti > 1 ? (dist < 0 ? -FocusedItemOffset : FocusedItemOffset) + dist * UnfocusedItemOffset : dist * FocusedItemOffset;

                DoubleAnimation aanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(aanim, rtransform);

                if (dist != 0)
                {
                    // simulate depth with opacity and ZIndex; angle applied via RotateTransform
                    if (Orientation == Orientation.Vertical)
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(RotateTransform.AngleProperty));
                    else
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(RotateTransform.AngleProperty));
                    aanim.To = dist < 0 ? -UnfocusedItemAngle : UnfocusedItemAngle;
                }
                else
                {
                    if (Orientation == Orientation.Vertical)
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(RotateTransform.AngleProperty));
                    else
                        Storyboard.SetTargetProperty(aanim, new PropertyPath(RotateTransform.AngleProperty));
                    aanim.To = FocusedItemAngle;
                }

                DoubleAnimation fanim = new DoubleAnimation() { EasingFunction = AnimationFunction, Duration = AnimationDuration };
                Storyboard.SetTarget(fanim, child);
                Storyboard.SetTargetProperty(fanim, new PropertyPath(OpacityProperty));

                if (disti > ItemVisibility)
                    fanim.To = 0.0;
                else
                    fanim.To = Math.Max(0.0, 1.0 - disti * ItemFading);

                storyboard.Children.Add(tanim);
                storyboard.Children.Add(aanim);
                storyboard.Children.Add(fanim);
                storyboard.Begin();
            }

            return base.ArrangeOverride(finalSize);
        }

        [Category("CollectionFlow Panel")]
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public IEasingFunction AnimationFunction
        {
            get { return (IEasingFunction)GetValue(AnimationFunctionProperty); }
            set { SetValue(AnimationFunctionProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double ItemRelativeSize
        {
            get { return (double)GetValue(ItemRelativeSizeProperty); }
            set { SetValue(ItemRelativeSizeProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public bool UseRelativeSize
        {
            get { return (bool)GetValue(UseRelativeSizeProperty); }
            set { SetValue(UseRelativeSizeProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double ItemFading
        {
            get { return (double)GetValue(ItemFadingProperty); }
            set { SetValue(ItemFadingProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public int ItemVisibility
        {
            get { return (int)GetValue(ItemVisibilityProperty); }
            set { SetValue(ItemVisibilityProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public double FocusedItemDistance
        {
            get { return (double)GetValue(FocusedItemDistanceProperty); }
            set { SetValue(FocusedItemDistanceProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemDistance
        {
            get { return (double)GetValue(UnfocusedItemDistanceProperty); }
            set { SetValue(UnfocusedItemDistanceProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double FocusedItemOffset
        {
            get { return (double)GetValue(FocusedItemOffsetProperty); }
            set { SetValue(FocusedItemOffsetProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemOffset
        {
            get { return (double)GetValue(UnfocusedItemOffsetProperty); }
            set { SetValue(UnfocusedItemOffsetProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double FocusedItemAngle
        {
            get { return (double)GetValue(FocusedItemAngleProperty); }
            set { SetValue(FocusedItemAngleProperty, value); }
        }
        [Category("CollectionFlow Panel")]
        public double UnfocusedItemAngle
        {
            get { return (double)GetValue(UnfocusedItemAngleProperty); }
            set { SetValue(UnfocusedItemAngleProperty, value); }
        }

        [Category("CollectionFlow Panel")]
        public int FocusedItemIndex
        {
            get { return (int)GetValue(FocusedItemIndexProperty); }
            set { SetValue(FocusedItemIndexProperty, value); }
        }
    }
}
