using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using ClumsySanta.Implementation;
using ClumsySanta.Implementation.Commands;
using ClumsySanta.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ClumsySanta
{
    public partial class GameEnginePresent : UserControl, IGameEngineSceneItem
    {
        private readonly Random _random = new Random();
        public GameEnginePresent()
        {
            InitializeComponent();
            Loaded += GameEnginePresent_Loaded;

            Opacity = 0;
        }

        public GamePlayEngine2 GameEngine { get; set; }

        void GameEnginePresent_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (GamePresentViewModel)DataContext;
            //CompositeTransform.CenterX = viewModel.OrignCoordinate.X;
            //CompositeTransform.CenterY = viewModel.OrignCoordinate.Y;
        }

        public void Appear(EventHandler completed)
        {
            var sb = (Storyboard)Resources["AppearSb"];
            Storyboard.SetTarget(sb, this);
            sb.Completed += AppearSb_Completed;
            sb.Completed += completed;
            sb.Begin();

            //final values
            Opacity = 1;
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

            if (viewModel.SpecialSound != null)
            {
                SoundPlayerHelper.PlaySound(viewModel.SpecialSound);
            }
            else
            {
                var @int = _random.Next(1, 4);
                SoundPlayerHelper.PlaySound("cl" + @int);
            }
            //random

            // remove tap
            LayoutRoot.Tap -= LayoutRoot_Tap;
            this.IsHitTestVisible = false;

            var sb = (Storyboard)Resources["DisapearSb"];

            if (GameEngine != null)
            {
                GameEngine.PresentsCounter.CountPresent(this, sb);
            }
            ((Canvas)Parent).Children.Remove(this);
        }
    }
}
