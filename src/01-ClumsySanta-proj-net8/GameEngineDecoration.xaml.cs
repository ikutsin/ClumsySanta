using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using ClumsySanta.Implementation;
using ClumsySanta.Model;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClumsySanta.ViewModel;

namespace ClumsySanta
{
    public partial class GameEngineDecoration : UserControl, IGameEngineSceneItem
    {
        Random random = new Random();
        public GameEngineDecoration()
        {
            InitializeComponent();
            Unloaded += GameEngineDecoration_Unloaded;
        }

        void GameEngineDecoration_Unloaded(object sender, RoutedEventArgs e)
        {
            LayoutRoot.Tap -= LayoutRoot_Tap;
        }

        public void Appear(EventHandler completed)
        {
            var viewModel = (GameDecorationViewModel)DataContext;
            CompositeTransform.CenterX = viewModel.OrignCoordinate.X;
            CompositeTransform.CenterY = viewModel.OrignCoordinate.Y;

            ScaleTransform.CenterX = viewModel.Size.Width;
            ScaleTransform.CenterY = viewModel.Size.Height;

            var sb = (Storyboard) Resources["AppearSb"];
            Storyboard.SetTarget(sb, this);
            sb.Completed += AppearSb_Completed;
            sb.Completed += completed;
            sb.Begin();

            Canvas.SetTop(this, viewModel.Coordinate.Y);
        }

        private Storyboard MoveDownAnimation()
        {
            var sb = Resources["MoveDownSb"] as Storyboard;

            (sb.Children[2] as DoubleAnimationUsingKeyFrames).KeyFrames[0].Value = random.NextDouble()*80-40;

            Storyboard.SetTarget(sb, this);
            sb.Begin();

            //final values
            this.Opacity = 0;

            return sb;
        }

        private Storyboard FadeOutSafeAnimation()
        {
            var sb = Resources["FadeOutSb"] as Storyboard;
            sb.Stop();
            Storyboard.SetTarget(sb, this);
            sb.Begin();
            
            //final values
            this.Opacity = 0;
            return sb;
        }

        private Storyboard ThrowAwayAnimation()
        {
            var sb = Resources["ThrowAwaySb"] as Storyboard;
            
            //Left
            var left = Canvas.GetLeft(this);
            if (random.Next()%2 == 0) left = left + App.Current.Host.Content.ActualWidth + random.NextDouble()*400;
            else left = left - App.Current.Host.Content.ActualWidth - random.NextDouble() * 400;
            Debug.WriteLine(Canvas.GetLeft(this)-left);
            (sb.Children[0] as DoubleAnimationUsingKeyFrames).KeyFrames[0].Value = left;

            //top
            var top = Canvas.GetTop(this);
            (sb.Children[1] as DoubleAnimationUsingKeyFrames).KeyFrames[0].Value = random.NextDouble() * 200 - top;

            //rotation
            (sb.Children[2] as DoubleAnimationUsingKeyFrames).KeyFrames[0].Value = random.NextDouble() * 720 - 360;

            Storyboard.SetTarget(sb, this);
            sb.Begin();

            //final values
            this.Opacity = 0;

            return sb;
        }

        void AppearSb_Completed(object sender, EventArgs e)
        {
            var sb = (Storyboard)Resources["AppearSb"];
            sb.Completed -= AppearSb_Completed;
            sb.Stop();
            LayoutRoot.Tap += LayoutRoot_Tap;
        }

        void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var viewModel = (GameDecorationViewModel)DataContext;
            if (viewModel.DependsOn.Any())
            {
                SoundPlayerHelper.PlaySound("bad");
                return;
            }


            viewModel.Removed();

            // remove tap
            
            LayoutRoot.Tap -= LayoutRoot_Tap;
            this.IsHitTestVisible = false;

            SoundPlayerHelper.PlaySound(viewModel.SpecialSound ?? "tick1");

            //TODO: XXX: тут SpecialAnimation // ikutsin@13.12.13
            if (viewModel.SpecialAnimation != null)
            {
                if (viewModel.SpecialAnimation == "MoveDown")
                {
                    MoveDownAnimation().Completed += GameEngineDecoration_OutAnimationCompleted;
                }
                else
                {
                    FadeOutSafeAnimation().Completed += GameEngineDecoration_OutAnimationCompleted;
                }
            }
            else
            {
                ThrowAwayAnimation().Completed += GameEngineDecoration_OutAnimationCompleted;
            }

            
            //ThrowAwayAnimation();
            //FadeOutSafeAnimation();

            //nice :))
            //PendulumAnimation();
        }

        private void GameEngineDecoration_OutAnimationCompleted(object sender, EventArgs e)
        {
            var sb = (Storyboard) sender;
            sb.Completed -= GameEngineDecoration_OutAnimationCompleted;

            ((Canvas)Parent).Children.Remove(this);
        }
    }
}
