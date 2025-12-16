using System.Windows;
using System.Windows.Controls;

namespace ClumsySanta
{
    public partial class GamePlayEngine3 : Page
    {
        public GamePlayEngine3()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // skip if restored
            //if (e.NavigationMode != NavigationMode.New)
            //    return;

            //var viewmodel = ((GamePlayEngineViewModel) DataContext);

            //double prevTop =0, prevLeft=500;

            //var decorationRemovedCommand = new DecorationRemovedCommand { GameLevelModel = viewmodel.LevelModel };

            ////put presents to scene
            //foreach (var model in viewmodel.LevelModel.Presents)
            //{
            //    model.DecorationRemovedCommand = decorationRemovedCommand;
            //    //if(gamePresentModel.Level)
            //    var control = new GameEnginePresent
            //                      {
            //                          DataContext = model
            //                      };
            //    Canvas.SetTop(control, prevTop + model.Size.Width / 2);
            //    Canvas.SetLeft(control, prevLeft+model.Size.Height / 2);
            //    PresentsCanvas.Children.Add(control);

            //    prevLeft += model.Size.Width/2;
            //    prevTop += model.Size.Height/8;
            //}

            ////put decorations to scene
            //prevTop = 0;
            //prevLeft = 0;
            //foreach (var model in viewmodel.LevelModel.Decorations)
            //{
            //    model.DecorationRemovedCommand = decorationRemovedCommand;
            //    var control = new GameEngineDecoration
            //                      {
            //                          DataContext = model,
            //                      };

            //    Canvas.SetTop(control, prevTop + model.Size.Height / 2);
            //    Canvas.SetLeft(control, prevLeft + model.Size.Width / 2);

            //    if (model.Level == "FirstDecorationsCanvas")
            //        FirstDecorationsCanvas.Children.Add(control);
            //    else
            //        DecorationsCanvas.Children.Add(control);

            //    prevLeft += model.Size.Width/2;
            //    prevTop += model.Size.Height/8;
            //}
        }
    }
}