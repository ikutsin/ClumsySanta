using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ClumsySanta.Redist;
using ClumsySanta.ViewModel;

namespace ClumsySanta
{
    public partial class LevelCompletedPage : Page
    {
        public LevelCompletedPage()
        {
            InitializeComponent();
            Loaded += LevelCompletedPage_Loaded;
        }

        void LevelCompletedPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties.Contains("IsRateButtonClicked")) RateButton.Visibility = Visibility.Collapsed;

            PresentsAnimation.PlayDisplay("Default", PresentAnimationCallback);
        }

        private Storyboard sb;
        private void PresentAnimationCallback()
        {
            var vm = (LevelCompletedViewModel) DataContext;
            
            if (vm.CollectedPresentCount == vm.CurrentLevel.PresentsToShow)
            {
                SoundPlayerHelper.PlaySound("end-win");
            }
            else
            {
                SoundPlayerHelper.PlaySound("end-lose");
            }

            

            if (sb != null) sb.Stop();

            sb = new Storyboard();
            //sb.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTarget(sb, this);

            var sbi1 = new Storyboard();
            Storyboard.SetTarget(sbi1, PresentsText);
            sbi1.BeginTime = TimeSpan.FromSeconds(0);
            sb.Children.Add(sbi1);

            var sbi2 = new Storyboard();
            Storyboard.SetTarget(sbi2, TimeText);
            sbi2.BeginTime = TimeSpan.FromSeconds(0.8);
            sb.Children.Add(sbi2);

            var sbi3 = new Storyboard();
            Storyboard.SetTarget(sbi3, Buttons);
            sbi3.BeginTime = TimeSpan.FromSeconds(1.2);
            sb.Children.Add(sbi3);

            DoubleAnimation opacity1Animation = new DoubleAnimation();
            opacity1Animation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            opacity1Animation.To = 1;
            opacity1Animation.Duration = new Duration(TimeSpan.FromSeconds(.4));
            Storyboard.SetTargetProperty(opacity1Animation, new PropertyPath("Opacity"));
            sbi1.Children.Add(opacity1Animation);

            DoubleAnimation opacity2Animation = new DoubleAnimation();
            opacity2Animation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            opacity2Animation.To = 1;
            opacity2Animation.Duration = new Duration(TimeSpan.FromSeconds(.4));
            Storyboard.SetTargetProperty(opacity2Animation, new PropertyPath("Opacity"));
            sbi2.Children.Add(opacity2Animation);
            
            DoubleAnimation opacity3Animation = new DoubleAnimation();
            opacity3Animation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn };
            opacity3Animation.To = 1;
            opacity3Animation.Duration = new Duration(TimeSpan.FromSeconds(.4));
            Storyboard.SetTargetProperty(opacity3Animation, new PropertyPath("Opacity"));
            sbi3.Children.Add(opacity3Animation);

            sb.Completed += sb_Completed;
            sb.Begin();

            
        }

        void sb_Completed(object sender, EventArgs e)
        {
            BackButton.Click -= GoBack_Click;
            BackButton.Click += GoBack_Click;

            RateButton.Click -= Rate_Click;
            RateButton.Click += Rate_Click;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implement desktop review action; stubbed out

            Application.Current.Properties["IsRateButtonClicked"] = true;
        }
    }
}