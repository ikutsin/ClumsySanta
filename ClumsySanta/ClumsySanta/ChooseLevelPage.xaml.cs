using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ClumsySanta.Redist;
using ClumsySanta.Redist.Extensions;
using ClumsySanta.ViewModel;

namespace ClumsySanta
{
    public partial class ChooseLevelPage : Page
    {
        public ChooseLevelPage()
        {
            InitializeComponent();

            Loaded += ChooseLevelPage_Loaded;

            ManipulationCompleted += ChooseLevelPage_ManipulationCompleted;

            if (Application.Current.Properties.Contains("LastSelectedLevel"))
            {
                LevelsCoverFlow.SelectedItemIndex = (int) Application.Current.Properties["LastSelectedLevel"];
            }
        }

        void ChooseLevelPage_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (ChooseLevelViewModel) DataContext;
            vm.InitGameLevels();
        }

        void ChooseLevelPage_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (Math.Abs(e.TotalManipulation.Translation.X) > 100)
            {
                if (e.TotalManipulation.Translation.X < 0)
                {
                    CoverflowRight(null, null);
                }
                else
                {
                    CoverflowLeft(null, null);
                }
            }
        }

        private void CoverflowLeft(object sender, MouseButtonEventArgs e)
        {
            if (LevelsCoverFlow.SelectedItemIndex > 0)
            {
                LevelsCoverFlow.SelectedItemIndex--;
                PersistCurrentLevel();
            }
            
        }

        private void PersistCurrentLevel()
        {
            SoundPlayerHelper.PlaySound("snowstep");

            Application.Current.Properties["LastSelectedLevel"] = LevelsCoverFlow.SelectedItemIndex;
        }

        private void CoverflowRight(object sender, MouseButtonEventArgs e)
        {
            if (LevelsCoverFlow.SelectedItemIndex < LevelsCoverFlow.Items.Count - 1)
            {
                LevelsCoverFlow.SelectedItemIndex++;
                PersistCurrentLevel();
            }

            
        }
        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            var repo = new GameLevelsRepository();
            var level = repo.GetLevelsAt(LevelsCoverFlow.SelectedItemIndex);
            // Navigate to WPF page version of GamePlayEngine2 with payload using extension to persist navigation data
            if (this.NavigationService != null)
            {
                NavigationExtensions.Navigate(this.NavigationService, "GamePlayEngine2.xaml", level);
            }
        }
    }
}