using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ClumsySanta.Redist.Controls.Snow;

namespace ClumsySanta.Redist.Controls
{
    //See more at: http://compiledexperience.com/windows-phone/tutorials/snowfall#sthash.tw7VUUuZ.dpuf
    public class SnowFlakes : Canvas
    {
        public const int MAXFLAKE = 100;
        private DispatcherTimer _timer = new DispatcherTimer();
        private readonly Random _random = new Random();
        Point currentTransform = new Point();

        public static readonly DependencyProperty FlakesCountProperty =
            DependencyProperty.Register("FlakesCount", typeof (int), typeof (SnowFlakes), new PropertyMetadata(25));

        public static readonly DependencyProperty FlakeImageSourceProperty =
            DependencyProperty.Register("FlakeImageSource", typeof (ImageSource), typeof (SnowFlakes), new PropertyMetadata(default(ImageSource)));

        public ImageSource FlakeImageSource
        {
            get { return (ImageSource) GetValue(FlakeImageSourceProperty); }
            set { SetValue(FlakeImageSourceProperty, value); }
        }

        public int FlakesCount
        {
            get { return (int)GetValue(FlakesCountProperty); }
            set { SetValue(FlakesCountProperty, value); }
        }

        public SnowFlakes()
        {
            _timer.Tick += OnTimerTicker;
            _timer.Interval = TimeSpan.FromSeconds(1 / 25.0d);

            Loaded += OnLoaded;
            Unloaded += SnowFlakes_Unloaded;
            this.ManipulationDelta += SnowFlakes_ManipulationDelta;
        }

        void SnowFlakes_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        void SnowFlakes_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            Point translation = new Point(
                e.CumulativeManipulation.Translation.X,
                (e.CumulativeManipulation.Translation.Y > 0 ? e.CumulativeManipulation.Translation.Y : e.CumulativeManipulation.Translation.Y*.1d));
            currentTransform = translation;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CreateInitialSnowflakes();
            _timer.Start();
        }

        private void CreateInitialSnowflakes()
        {
            if(Children.Any()) return;
            for (int i = 0; i < FlakesCount; i++)
            {
                var left = _random.NextDouble() * ActualWidth;
                var top = _random.NextDouble()*ActualHeight;
                var size = _random.Next(10, 50);

                CreateSnowflake(left, top, size);
            }
        }

        private void OnTimerTicker(object sender, EventArgs e)
        {
            var snowflakes = Children.OfType<SnowFlake>().ToList();

            foreach (var snowflake in snowflakes)
            {
                snowflake.UpdatePosition(currentTransform);

                if (snowflake.IsOutOfBounds(ActualWidth, ActualHeight))
                {
                    Children.Remove(snowflake);
                    AddNewSnowflake();
                }

                currentTransform.X = currentTransform.X * 0.999d;
                currentTransform.Y = currentTransform.Y * 0.999d;
            }
        }

        private void AddNewSnowflake()
        {
            var left = _random.NextDouble() * ActualWidth;

            var size = _random.Next(MAXFLAKE/5, MAXFLAKE);

            CreateSnowflake(left, -20, size);
        }
 

        private void CreateSnowflake(double left, double top, double size)
        {
            var snowflake = new SnowFlake(FlakeImageSource)
            {
                Width = size,
                Height = size
            };

            Canvas.SetLeft(snowflake, left);
            Canvas.SetTop(snowflake, top);

            Children.Add(snowflake);
        }
    }
}
