using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClumsySanta.Model;

namespace ClumsySanta.Redist.Controls
{
    public class MyScrollViewer : UserControl
    {
        private Storyboard _panStoryboard;
        private CompositeTransform _renderTransform;

        public MyScrollViewer()
        {
            _renderTransform = new CompositeTransform();
            RenderTransform = _renderTransform;
            _panStoryboard = CreateSmoothMoveStoryboard();
        }

        private Storyboard CreateSmoothMoveStoryboard()
        {
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(sb, _renderTransform);

            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            xAnimation.To = 0;
            xAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("TranslateX"));
            sb.Children.Add(xAnimation);

            DoubleAnimation yAnimation = new DoubleAnimation();
            yAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            yAnimation.To = 0;
            yAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath("TranslateY"));
            sb.Children.Add(yAnimation); 
            
return sb;
        }



        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            base.OnManipulationStarted(e);
            e.Handled = true;
        }

        //rebase
        private Point RebaseAndGetTranslateOffset(Point manipulationOrigin)
        {
            var newCenter = manipulationOrigin;
            var newZeroOrign = new Point(
                newCenter.X - newCenter.X * _renderTransform.ScaleX,
                newCenter.Y - newCenter.Y * _renderTransform.ScaleY);

            var oldZeroOrign = new Point(
                _renderTransform.CenterX - _renderTransform.CenterX * _renderTransform.ScaleX,
                _renderTransform.CenterY - _renderTransform.CenterY * _renderTransform.ScaleY);

            _renderTransform.CenterX = newCenter.X;
            _renderTransform.CenterY = newCenter.Y;

            return new Point(oldZeroOrign.X - newZeroOrign.X, oldZeroOrign.Y - newZeroOrign.Y);
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            if (e.PinchManipulation != null)
            {
                var translateOffset = RebaseAndGetTranslateOffset(e.ManipulationOrigin);

                var state = _panStoryboard.GetCurrentState();
                Debug.WriteLine(state);

                ((DoubleAnimation)_panStoryboard.Children[0]).To = translateOffset.X;
                ((DoubleAnimation)_panStoryboard.Children[1]).To = translateOffset.Y;
                _panStoryboard.SkipToFill();
                _renderTransform.TranslateX = translateOffset.X;
                _renderTransform.TranslateY = translateOffset.Y;

                var delta = e.DeltaManipulation.Scale;

                var newDelta = Math.Max(_renderTransform.ScaleX * delta.X, _renderTransform.ScaleY * delta.Y);
                if (newDelta < 0.1) return;

                Size contentSize = GetContentSize();

                var maxScale = 3;
                var minScale = Math.Max(App.Current.Host.Content.ActualWidth /contentSize.Width,
                    App.Current.Host.Content.ActualHeight / contentSize.Height);

                newDelta = Math.Max(minScale, Math.Min(maxScale, newDelta));
                _renderTransform.ScaleX = newDelta;
                _renderTransform.ScaleY = newDelta;
            }
            else
            {
                var delta = e.DeltaManipulation.Translation;

                var translate = GetCurrentTranslate();

                var newTranslate = new Point(
                    translate.X + delta.X,
                    translate.Y + delta.Y);

                newTranslate = Limit(newTranslate);

                NavigateToPoint(newTranslate);
            }
        }
        

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            var delta = e.TotalManipulation.Translation;
            double power = 3;

            var translate = GetCurrentTranslate();

            var newTranslate = new Point(
                translate.X + delta.X*power,
                translate.Y + delta.Y*power);

            newTranslate = Limit(newTranslate);
            NavigateToPoint(newTranslate);
        }
        public Point Limit(Point oldPoint)
        {
            Point newTranslate = new Point(oldPoint.X, oldPoint.Y);
            var centerOrign = new Point(
                    _renderTransform.CenterX - _renderTransform.CenterX * _renderTransform.ScaleX,
                    _renderTransform.CenterY - _renderTransform.CenterY * _renderTransform.ScaleY);

            var topLeft = new Point(centerOrign.X + newTranslate.X, centerOrign.Y + newTranslate.Y);

            if (topLeft.X > 0) newTranslate.X -= topLeft.X;
            if (topLeft.Y > 0) newTranslate.Y -= topLeft.Y;

            var size = GetContentSize();

            var scaledHeightLimit = size.Height * _renderTransform.ScaleY - App.Current.Host.Content.ActualHeight;
            var scaledWidthLimit = size.Width * _renderTransform.ScaleX - App.Current.Host.Content.ActualWidth;

            if (topLeft.Y < -scaledHeightLimit)
            {
                double overflow = topLeft.Y + scaledHeightLimit;
                newTranslate.Y -= overflow;
            }

            if (topLeft.X < -scaledWidthLimit)
            {
                double overflow = topLeft.X + scaledWidthLimit;
                newTranslate.X -= overflow;
            }
            return newTranslate;
        }

        public void NavigateToPoint(Point newTranslate)
        {
            ((DoubleAnimation)_panStoryboard.Children[0]).To = newTranslate.X;
            ((DoubleAnimation)_panStoryboard.Children[1]).To = newTranslate.Y;
            _panStoryboard.Begin();
            _renderTransform.TranslateX = newTranslate.X;
            _renderTransform.TranslateY = newTranslate.Y;

            //Debug.WriteLine("Set:" + newTranslate+" "+_renderTransform.TranslateX+"x"+_renderTransform.TranslateY);
        }

        public Point GetCurrentTranslate()
        {
            //((DoubleAnimation)_panStoryboard.Children[0]).

            var currentTranslate = new Point(_renderTransform.TranslateX, _renderTransform.TranslateY);
            Debug.WriteLine("2GetCurrentTranslate: " + currentTranslate);
            return currentTranslate;
        }

        private Size GetContentSize()
        {
            return new Size(
                 Content.RenderSize.Width,
                  Content.RenderSize.Height);
        }
    }
}
