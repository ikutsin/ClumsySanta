using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClumsySanta.Model;
using ClumsySanta.Redist;

namespace ClumsySanta
{
    public partial class LevelTimer : UserControl
    {
        public LevelTimer()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(LevelTimer), new PropertyMetadata(default(string)));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public event EventHandler<FinishGameRequestEventArgs> Timeout;

        public static readonly DependencyProperty TicksLeftProperty =
           DependencyProperty.Register("TicksLeft", typeof(int), typeof(LevelTimer), new PropertyMetadata(0, TicksLeftChanged));

        public int TicksLeft
        {
            get => (int)GetValue(TicksLeftProperty);
            set => SetValue(TicksLeftProperty, value);
        }

        private static void TicksLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var _this = (LevelTimer)d;
            _this.TicksLeftNew = _this.TicksLeft - 1;
        }

        private static readonly DependencyProperty TicksLeftNewProperty =
           DependencyProperty.Register("TicksLeftNew", typeof(int), typeof(LevelTimer), new PropertyMetadata(0));

        public int TicksLeftNew
        {
            get => (int)GetValue(TicksLeftNewProperty);
            set => SetValue(TicksLeftNewProperty, value);
        }

        public static readonly DependencyProperty IsSuspendedProperty =
            DependencyProperty.Register("IsSuspended", typeof (bool), typeof (LevelTimer), new PropertyMetadata(false));

        public bool IsSuspended
        {
            get { return (bool) GetValue(IsSuspendedProperty); }
            set
            {
                SetValue(IsSuspendedProperty, value);
                if (!value) Start();
                else Stop();
            }
        }

        private void Stop()
        {
            var sb = TimerCanvas.Resources["AnimationStoryNew"] as Storyboard;
            sb.Stop();
            sb = TimerCanvas.Resources["AnimationStoryOld"] as Storyboard;
            sb.Stop();
        }

        public void Start()
        {
            GetClockElementAnimation(TicksLeftTextNew, false).Begin();
            GetClockElementAnimation(TicksLeftTextOld, true).Begin();
        }

        public Storyboard GetClockElementAnimation(FrameworkElement element, bool isOld)
        {
            var storyName = isOld ? "AnimationStoryOld" : "AnimationStoryNew";
            var sb = TimerCanvas.Resources[storyName] as Storyboard;
            return sb;
        }

        private bool _animationStoryNewCompleted;

        private void AnimationStoryOld_Completed(object sender, EventArgs e)
        {
            //var story = (Storyboard)sender;
            //story.Stop();
            _animationStoryNewCompleted = false;
        }

        private void AnimationStoryNew_Completed(object sender, EventArgs e)
        {
            _animationStoryNewCompleted = true;
            //var story = (Storyboard) sender;
            //story.Stop();
            if (TicksLeftNew == 5)
            {
                SoundPlayerHelper.PlaySound("ticktack");
            }
            if (TicksLeftNew <= 0)
            {
                TicksLeftTextOld.Opacity = 0;
                TicksLeft--;
                NotifyTimeout();
                return;
            }

            Start();
            TicksLeft--;
        }

        private void NotifyTimeout()
        {
            if (Timeout == null)
                return;

            Timeout(this, new FinishGameRequestEventArgs());
        }

        private void TimerCanvas_Unloaded(object sender, RoutedEventArgs e)
        {
            Stop();
            if (!_animationStoryNewCompleted)
                TicksLeft--;
            
        }
    }
}
