using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace ClumsySanta.Controls
{
    public partial class ProgressImagesDisplay : UserControl
    {
        public static readonly DependencyProperty TotalAmountProperty =
            DependencyProperty.Register(nameof(TotalAmount), typeof (int), typeof (ProgressImagesDisplay), new PropertyMetadata(default(int)));

        public int TotalAmount
        {
            get => (int) GetValue(TotalAmountProperty);
            set => SetValue(TotalAmountProperty, value);
        }

        public static readonly DependencyProperty ProgressAmountProperty =
            DependencyProperty.Register(nameof(ProgressAmount), typeof (int), typeof (ProgressImagesDisplay), new PropertyMetadata(default(int)));

        public int ProgressAmount
        {
            get => (int) GetValue(ProgressAmountProperty);
            set => SetValue(ProgressAmountProperty, value);
        }

        public static readonly DependencyProperty BackImageSourceProperty =
            DependencyProperty.Register(nameof(BackImageSource), typeof (string), typeof (ProgressImagesDisplay), new PropertyMetadata(default(string)));

        public string BackImageSource
        {
            get => (string) GetValue(BackImageSourceProperty);
            set => SetValue(BackImageSourceProperty, value);
        }

        public static readonly DependencyProperty FrontImageSourceProperty =
            DependencyProperty.Register(nameof(FrontImageSource), typeof (string), typeof (ProgressImagesDisplay), new PropertyMetadata(default(string)));

        public string FrontImageSource
        {
            get => (string) GetValue(FrontImageSourceProperty);
            set => SetValue(FrontImageSourceProperty, value);
        }

        public ProgressImagesDisplay()
        {
            InitializeComponent();

            //Loaded += ProgressImagesDisplay_Loaded;
            LayoutUpdated += ProgressImagesDisplay_Loaded;

        }

        //void ProgressImagesDisplay_Loaded(object sender, RoutedEventArgs e)
        void ProgressImagesDisplay_Loaded(object sender, EventArgs e)
        {
	        LayoutUpdated -= ProgressImagesDisplay_Loaded;

			// No orientation handling in WPF
			if (Grays.Children.Count > 0) 
            {
                throw new InvalidOperationException("Again ProgressImagesDisplay_Loaded");
            }
            //Grays
            for (int i = 0; i < TotalAmount; i++)
            {
                var image = new Image {Source = new BitmapImage(new Uri(@"..\"+BackImageSource, UriKind.Relative))};
                Grays.Children.Add(image);
            }

            //colors
            for (int i = 0; i < TotalAmount; i++)
            {
                var image = new Image { 
                    Opacity = 0,
                    Source = new BitmapImage
                    (new Uri(@"..\" + 
                        (i<ProgressAmount?FrontImageSource:BackImageSource), UriKind.Relative)) };
                Colors.Children.Add(image);
            }
            //PlayDisplay("Default", null);
        }

        // Orientation change unsupported in WPF; method removed


        private string laststoryName;
        private Action lastcallback;
        private Storyboard sb;
        public void PlayDisplay(string storyName, Action callback)
        {
            laststoryName = storyName;
            lastcallback = callback;
            if(storyName==null) return;
            if (sb != null) sb.Stop();

            //var full = new Rect(0, 0, ((Panel) ViewBox.Child).ActualWidth, ((Panel) ViewBox.Child).ActualHeight);
            
            sb = new Storyboard();
            //sb.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTarget(sb, this);

            for (int counter = 0; counter < ProgressAmount; counter++)
            {
                var image = (Image)Colors.Children[counter];

                var sbi = new Storyboard();
                sbi.BeginTime = TimeSpan.FromSeconds(.5 * counter);
                Storyboard.SetTarget(sbi, image);

                DoubleAnimation opacityAnimation = new DoubleAnimation();
                opacityAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
                opacityAnimation.From = 0;
                opacityAnimation.To = 1;
                opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(.4));
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                sbi.Children.Add(opacityAnimation);

                sb.Children.Add(sbi);
            }

            sb.Completed += (_, __) =>
                                {
                                    laststoryName = null;
                                    if (callback != null) callback();
                                };
            sb.Begin();
        }
    }
}
