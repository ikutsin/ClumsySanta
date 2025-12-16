using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using ClumsySanta.Model;
using ClumsySanta.ViewModel;

namespace ClumsySanta
{
    public partial class PresentsCounter : UserControl
    {
        public static readonly DependencyProperty PresentIconProperty =
            DependencyProperty.Register(nameof(PresentIcon), typeof (string), typeof (PresentsCounter), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty PresentCountProperty =
            DependencyProperty.Register(nameof(PresentCount), typeof(int), typeof(PresentsCounter), new PropertyMetadata(0));

        public static readonly DependencyProperty PresentsToWaitProperty =
            DependencyProperty.Register(nameof(PresentsToWait), typeof(int), typeof(PresentsCounter), new PropertyMetadata(1));

        public int PresentsToWait
        {
            get => (int) GetValue(PresentsToWaitProperty);
            set => SetValue(PresentsToWaitProperty, value);
        }

        public int PresentCount
        {
            get => (int)GetValue(PresentCountProperty);
            set => SetValue(PresentCountProperty, value);
        }

        public string PresentIcon
        {
            get => (string) GetValue(PresentIconProperty);
            set => SetValue(PresentIconProperty, value);
        }
        public PresentsCounter()
        {
            InitializeComponent();
        }

        public void CountPresent(GameEnginePresent control, Storyboard sb)
        {
            var transformer = ((Canvas)control.Parent).TransformToVisual(LayoutRoot);
            var presentVm = (GamePresentViewModel) control.DataContext;
            var point = transformer.Transform(new Point(Canvas.GetLeft(control), Canvas.GetTop(control)));

			Debug.WriteLine($"CountPresent: {presentVm.Name} at {point}");

            var presentImage = new Image { Source = new BitmapImage(new Uri(presentVm.BaseImage, UriKind.Relative))};
            var tg = new TransformGroup();
            var scale = new ScaleTransform { CenterX = presentVm.OrignCoordinate.X, CenterY = presentVm.OrignCoordinate.Y, ScaleX = 2, ScaleY = 2 };
            tg.Children.Add(scale);
            presentImage.RenderTransform = tg;
            Canvas.SetLeft(presentImage, point.X);
            Canvas.SetTop(presentImage, point.Y);

            LayoutRoot.Children.Add(presentImage);


            //animate
            Storyboard.SetTarget(sb, presentImage);

            var top = Math.Max(100, Canvas.GetTop(presentImage) - 300);
            ((DoubleAnimationUsingKeyFrames)sb.Children[0]).KeyFrames[0].Value = top;

            sb.Completed += (_, __) => AnimateToCounter(presentImage);

            sb.Begin();
        }

        private void AnimateToCounter(Image presentImage)
        {
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(sb, presentImage);

            DoubleAnimation topAnimation = new DoubleAnimation();
            topAnimation.EasingFunction = new SineEase{ EasingMode = EasingMode.EaseIn};
            topAnimation.To = 0;
            topAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(topAnimation);


            DoubleAnimation leftAnimation = new DoubleAnimation();
            leftAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            leftAnimation.To = 0;
            leftAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
            sb.Children.Add(leftAnimation);


            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            opacityAnimation.To = .4;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            sb.Children.Add(opacityAnimation);

            DoubleAnimation scaleXAnimation = new DoubleAnimation();
            scaleXAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            scaleXAnimation.To = 1;
            scaleXAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("(RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            sb.Children.Add(scaleXAnimation);

            DoubleAnimation scaleYAnimation = new DoubleAnimation();
            scaleYAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            scaleYAnimation.To = 1;
            scaleYAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("(RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            sb.Children.Add(scaleYAnimation);

            var completeAction = new EventHandler((s, e) =>
            {
                PresentCount++;
                LayoutRoot.Children.Remove(presentImage);

                if(PresentsToWait<=PresentCount) OnAllPresentsCollected(new FinishGameRequestEventArgs());
            });
            sb.Completed += completeAction;


            sb.Begin();
        }

        public event EventHandler<FinishGameRequestEventArgs> AllPresentsCollected;

        protected virtual void OnAllPresentsCollected(FinishGameRequestEventArgs e)
        {
            EventHandler<FinishGameRequestEventArgs> handler = AllPresentsCollected;
            if (handler != null) handler(this, e);
        }
    }
}
