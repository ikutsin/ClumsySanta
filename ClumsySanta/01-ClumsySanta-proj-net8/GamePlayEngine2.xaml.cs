using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using GoogleAnalytics;
using ClumsySanta.Implementation;
using ClumsySanta.Implementation.Commands;
using ClumsySanta.Model;
using ClumsySanta.Redist.Extensions;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClumsySanta.ViewModel;
using Microsoft.Xna.Framework;

namespace ClumsySanta
{
    public enum GamePlayEngine2State
    {
        Starting, Playing, Finishing
    }
    public partial class GamePlayEngine2 : PhoneApplicationPage
    {
        public GamePlayEngine2State GameSate { get; private set; }

        public GamePlayEngine2()
        {
            InitializeComponent();
            Loaded += GamePlayEngine2_Loaded;

            GameSate = GamePlayEngine2State.Starting;
        }

        void GamePlayEngine2_Loaded(object sender, RoutedEventArgs e)
        {
            var levels = MainCanvas.Children.OfType<Canvas>().ToList();
            var decorations = levels.SelectMany(c => c.Children.OfType<GameEngineDecoration>()).ToList();
            var presents = levels.SelectMany(c => c.Children.OfType<GameEnginePresent>()).ToList();

            var batchAppearManager = new BatchAppearManager();
            batchAppearManager.BatchAppear(decorations, () =>
            {
                SoundPlayerHelper.PlaySound("fall");
                batchAppearManager.BatchAppear(presents, () =>
                {
                    GameSate = GamePlayEngine2State.Playing;
                    LevelTimer.Start();
                });
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.LevelTimer.IsSuspended = true;
            if (GameSate == GamePlayEngine2State.Playing)
            {
                if (MessageBox.Show("You are about to stop the game. " +
                                    "Do you want to continue ?", "Return to main menu",
                    MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    this.LevelTimer.IsSuspended = false;
                    e.Cancel = true;
                }
            }

            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // skip if restored
            if (e.NavigationMode != NavigationMode.New)
                return;

            Tracker myTracker = EasyTracker.GetTracker();
            myTracker.SendView("GamePlayEngine");

            var levelModel = App.RootFrame.GetLastNavigationData() as GameLevelModel;
            if (levelModel != null)
            {
                DataContext = new GamePlayEngineViewModel
                {
                    LevelModel = levelModel
                };
            }
            else
            {
                DataContext = new GamePlayEngineViewModel
                {
                    LevelModel = new GameLevelsRepository().GetAllLevels().Skip(2).First()
                };
            }

            var viewmodel = ((GamePlayEngineViewModel)DataContext);
            if (String.IsNullOrEmpty(viewmodel.LevelModel.LaterBackgroundImage))
            {
                LaterBackgroundImage.Visibility = Visibility.Collapsed;
            }

            var levels = MainCanvas.Children.OfType<Canvas>().ToList();

            // TODO shuffle and remove not used presents
            var presents = viewmodel.LevelModel.Presents.Shuffle();
            
            var allSceneItems = presents
                // concat with decorations
                .Concat(viewmodel.LevelModel.Decorations)
                // subscribe all scene items
                .Select(m => SubscribeDependenceRemoved(m, viewmodel.LevelModel)).ToList();

            var itemsToRemove = presents.Skip(viewmodel.LevelModel.PresentsToShow).ToList();
            allSceneItems.RemoveAll(si => 
            { 
                if (!itemsToRemove.Contains(si))
                    return false;

                // execute to remove dependencies
                si.Removed();
                return true;
            });

            // stable sort
            allSceneItems.OrderBy(m => m, new GameDecorationViewModelComparer())
                .Select((m, i) => new { m, i })
                .ToList()
                // add controls
                .ForEach(mi => AddControlToCanvas(mi.i, mi.m, levels.Single(c => c.Name == mi.m.Level)));

        }

        void AddControlToCanvas(int ZIndex, GameDecorationViewModel model, Canvas level)
        {
            Control control = null;

            if (model is GamePresentViewModel)
            {
                control = new GameEnginePresent { GameEngine = this, DataContext = model };
                Canvas.SetTop(control, model.Coordinate.Y);
            }
            else
            {
                control = new GameEngineDecoration { DataContext = model };
                Canvas.SetTop(control, -model.Size.Height);//Start point for animation //model.Coordinate.Y);
            }

            Canvas.SetZIndex(control, ZIndex);
            Canvas.SetLeft(control, model.Coordinate.X - Canvas.GetLeft(level));
            level.Children.Add(control);
        }

        GameDecorationViewModel SubscribeDependenceRemoved(GameDecorationViewModel decorationViewModel, GameLevelModel levelModel)
        {
            decorationViewModel.DependenceRemoved += levelModel.RemoveDependence;
            return decorationViewModel;
        }

        void ScrollView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewmodel = ((GamePlayEngineViewModel)DataContext);

            var sceneSceollViewer = (ScrollViewer)sender;
            sceneSceollViewer.ScrollToHorizontalOffset(viewmodel.LevelModel.StartPoint.X);
            sceneSceollViewer.ScrollToVerticalOffset(viewmodel.LevelModel.StartPoint.Y);
        }

        private void OnLevelCompleted(object sender, FinishGameRequestEventArgs ea)
        {
            var viewmodel = ((GamePlayEngineViewModel)DataContext);
            GameSate = GamePlayEngine2State.Finishing;
            viewmodel.CompleteLevel
                .Execute(new LevelCompletedViewModel
                {
                    CurrentLevel = viewmodel.LevelModel,
                    CollectedPresentCount = PresentsCounter.PresentCount,
                    TimeSecondsLeft = this.LevelTimer.TicksLeft
                });
        }
    }
}