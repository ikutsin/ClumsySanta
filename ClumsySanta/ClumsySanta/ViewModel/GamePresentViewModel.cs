using System.ComponentModel;

namespace ClumsySanta.ViewModel
{
    public class GamePresentViewModel : GameDecorationViewModel, INotifyPropertyChanged
    {
        public GamePresentViewModel()
        {
            Level = "PresentsCanvas";
        }
    }
}