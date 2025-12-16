using System;
using System.Windows.Input;
using ClumsySanta.Model;
using ClumsySanta.Redist.Extensions;

namespace ClumsySanta.Implementation.Commands
{
    public class StartGameLevelCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            var level = parameter as GameLevelModel;
            return level != null && level.IsAvailableToPlay;
        }

        public void Execute(object parameter)
        {
            App.RootFrame.Navigate("/GamePlayEngine2.xaml", parameter as GameLevelModel);
        }

        public event EventHandler CanExecuteChanged;
    }
}