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

namespace ClumsySanta.Controls
{
    public class MyScrollViewer : UserControl
    {
        private Storyboard _panStoryboard;
        private TransformGroup _renderTransform;
        private ScaleTransform _scale;
        private TranslateTransform _translate;

        public MyScrollViewer()
        {
            _scale = new ScaleTransform();
            _translate = new TranslateTransform();
            _renderTransform = new TransformGroup();
            _renderTransform.Children.Add(_scale);
            _renderTransform.Children.Add(_translate);
            RenderTransform = _renderTransform;
            _panStoryboard = CreateSmoothMoveStoryboard();
        }

        private Storyboard CreateSmoothMoveStoryboard()
        {
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(sb, this);

            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            xAnimation.To = 0;
            xAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)"));
            sb.Children.Add(xAnimation);

            DoubleAnimation yAnimation = new DoubleAnimation();
            yAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            yAnimation.To = 0;
            yAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
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
                newCenter.X - newCenter.X * _scale.ScaleX,
                newCenter.Y - newCenter.Y * _scale.ScaleY);

            var oldZeroOrign = new Point(
                _scale.CenterX - _scale.CenterX * _scale.ScaleX,
                _scale.CenterY - _scale.CenterY * _scale.ScaleY);

            _scale.CenterX = newCenter.X;
            _scale.CenterY = newCenter.Y;

            return new Point(oldZeroOrign.X - newZeroOrign.X, oldZeroOrign.Y - newZeroOrign.Y);
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            base.OnManipulationDelta(e);
            if (false)
            {
                var translateOffset = RebaseAndGetTranslateOffset(e.ManipulationOrigin);

                var state = _panStoryboard.GetCurrentState();
                Debug.WriteLine(state);

                ((DoubleAnimation)_panStoryboard.Children[0]).To = translateOffset.X;
                ((DoubleAnimation)_panStoryboard.Children[1]).To = translateOffset.Y;
                _panStoryboard.SkipToFill();
                _translate.X = translateOffset.X;
                _translate.Y = translateOffset.Y;

                var delta = e.DeltaManipulation.Scale;

                var newDelta = Math.Max(_scale.ScaleX * delta.X, _scale.ScaleY * delta.Y);
                if (newDelta < 0.1) return;

                Size contentSize = GetContentSize();

                var maxScale = 3;
                var minScale = Math.Max(Application.Current.MainWindow.ActualWidth /contentSize.Width,
                    Application.Current.MainWindow.ActualHeight / contentSize.Height);

                newDelta = Math.Max(minScale, Math.Min(maxScale, newDelta));
                _scale.ScaleX = newDelta;
                _scale.ScaleY = newDelta;
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
                    _scale.CenterX - _scale.CenterX * _scale.ScaleX,
                    _scale.CenterY - _scale.CenterY * _scale.ScaleY);

            var topLeft = new Point(centerOrign.X + newTranslate.X, centerOrign.Y + newTranslate.Y);

            if (topLeft.X > 0) newTranslate.X -= topLeft.X;
            if (topLeft.Y > 0) newTranslate.Y -= topLeft.Y;

            var size = GetContentSize();

            var scaledHeightLimit = size.Height * _scale.ScaleY - Application.Current.MainWindow.ActualHeight;
            var scaledWidthLimit = size.Width * _scale.ScaleX - Application.Current.MainWindow.ActualWidth;

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
            _panStoryboard.Begin(this);
            _translate.X = newTranslate.X;
            _translate.Y = newTranslate.Y;

            //Debug.WriteLine("Set:" + newTranslate+" "+_renderTransform.TranslateX+"x"+_renderTransform.TranslateY);
        }

        public Point GetCurrentTranslate()
        {
            //((DoubleAnimation)_panStoryboard.Children[0]).

            var currentTranslate = new Point(_translate.X, _translate.Y);
            Debug.WriteLine("2GetCurrentTranslate: " + currentTranslate);
            return currentTranslate;
        }

        private Size GetContentSize()
        {
            var fe = Content as FrameworkElement;
            if (fe == null) return new Size(0,0);
            return new Size(fe.RenderSize.Width, fe.RenderSize.Height);
        }
    }
}
