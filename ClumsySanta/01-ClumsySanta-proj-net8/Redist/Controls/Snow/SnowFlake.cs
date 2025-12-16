using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClumsySanta.Redist.Controls.Snow
{
    public class SnowFlake : UserControl
    {
        private readonly Random _random = new Random();
        private double accelerationX, accelerationY;
        private RotateTransform _rotateTransform = new RotateTransform();

        public SnowFlake(ImageSource flakeImageSource)
        {
            Content = new Image
            {
                Source = flakeImageSource,
                Stretch = Stretch.Fill,
                RenderTransform = _rotateTransform,
            };

            _rotateTransform.CenterX = SnowFlakes.MAXFLAKE / 2;
            _rotateTransform.CenterY = SnowFlakes.MAXFLAKE / 2;

            accelerationX = _random.NextDouble() * 5 + 2;
            accelerationY = _random.NextDouble() - .5;
        }

        public void UpdatePosition(Point currentTransform)
        {
            var top = Canvas.GetTop(this);
            var left = Canvas.GetLeft(this);

            _rotateTransform.Angle += (currentTransform.X * 0.05d) * accelerationY + accelerationY * 2;

            Canvas.SetTop(this, top + accelerationX + (currentTransform.Y * 0.1d));
            Canvas.SetLeft(this, left + (currentTransform.X * 0.1d) * accelerationY);
        }

        public bool IsOutOfBounds(double width, double height)
        {
            var left = Canvas.GetLeft(this);
            var top = Canvas.GetTop(this);

            if (left < -ActualWidth)
                return true;

            if (left > width + ActualWidth)
                return true;

            if (top > height)
                return true;

            return false;
        }
    }
}