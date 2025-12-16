using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClumsySanta.Model;
using ClumsySanta.Redist.Extensions;
using ClumsySanta.ViewModel;

namespace ClumsySanta.Implementation.Commands
{
    public class LevelCompletedCommand : ICommand
    {
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return LevelCompletedModel != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            App.RootFrame.Navigate("/LevelCompletedPage.xaml", parameter as LevelCompletedViewModel);
        }

        #endregion

        public LevelCompletedViewModel LevelCompletedModel { get; set; }
    }
}
