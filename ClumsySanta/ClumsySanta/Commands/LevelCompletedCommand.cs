using ClumsySanta.Redist.Extensions;
using ClumsySanta.ViewModel;
using System;
using System.Linq;
using System.Windows.Input;

namespace ClumsySanta.Commands
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
            var nav = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault() as System.Windows.Navigation.NavigationWindow;
            if (nav != null)
            {
                //nav.NavigationService.Navigate("LevelCompletedPage.xaml", parameter as LevelCompletedViewModel);
				NavigationExtensions.Navigate(nav.NavigationService, "LevelCompletedPage.xaml", parameter as LevelCompletedViewModel);
			}
        }

        #endregion

        public LevelCompletedViewModel LevelCompletedModel { get; set; }
    }
}
