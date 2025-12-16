using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GoogleAnalytics;
using Windows.UI.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClumsySanta.Implementation;
using ClumsySanta.Redist.Extensions;
using ClumsySanta.ViewModel;

namespace ClumsySanta
{
    public partial class ChooseLevelPage : PhoneApplicationPage
    {
        public ChooseLevelPage()
        {
            var phoneApplicationService = PhoneApplicationService.Current;
            if (phoneApplicationService!=null) phoneApplicationService.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            InitializeComponent();

            Loaded += ChooseLevelPage_Loaded;

            ManipulationCompleted += ChooseLevelPage_ManipulationCompleted;

            var storage = IsolatedStorageSettings.ApplicationSettings;
            if (storage.Contains("LastSelectedLevel"))
            {
                LevelsCoverFlow.SelectedItemIndex = (int) storage["LastSelectedLevel"];
            }
        }

        void ChooseLevelPage_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (ChooseLevelViewModel) DataContext;
            vm.InitGameLevels();

            Tracker myTracker = EasyTracker.GetTracker();
            myTracker.SendView("ChooseLevelPage");
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

            var storage = IsolatedStorageSettings.ApplicationSettings;
            storage["LastSelectedLevel"] = LevelsCoverFlow.SelectedItemIndex;
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
            App.RootFrame.Navigate("/GamePlayEngine2.xaml", level);

        }
    }
}