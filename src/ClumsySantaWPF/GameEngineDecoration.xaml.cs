using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClumsySanta.Redist;

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
            LayoutRoot.MouseLeftButtonDown -= LayoutRoot_MouseLeftButtonDown;
        }

        public void Appear(EventHandler completed)
        {
            var viewModel = (GameDecorationViewModel)DataContext;
            var scale = this.FindName("ScaleTransform") as ScaleTransform;
            var rotation = this.FindName("Rotation") as RotateTransform;
            var translation = this.FindName("Translation") as TranslateTransform;
            if (scale != null)
            {
                scale.CenterX = viewModel.Size.Width;
                scale.CenterY = viewModel.Size.Height;
            }

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
            if (random.Next()%2 == 0) left = left + Application.Current.MainWindow.ActualWidth + random.NextDouble()*400;
            else left = left - Application.Current.MainWindow.ActualWidth - random.NextDouble() * 400;
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
            LayoutRoot.MouseLeftButtonDown += LayoutRoot_MouseLeftButtonDown;
        }

        void LayoutRoot_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = (GameDecorationViewModel)DataContext;
            if (viewModel.DependsOn.Any())
            {
                SoundPlayerHelper.PlaySound("bad");
                return;
            }


            viewModel.Removed();

            // remove tap
            
            LayoutRoot.MouseLeftButtonDown -= LayoutRoot_MouseLeftButtonDown;
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
