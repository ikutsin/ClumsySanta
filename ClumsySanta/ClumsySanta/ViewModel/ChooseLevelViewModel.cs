using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClumsySanta.Model;
using ClumsySanta.Redist;

namespace ClumsySanta.ViewModel
{
    public class ChooseLevelViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GameLevelModel> _levels;

        public ObservableCollection<GameLevelModel> Levels
        {
	        get
	        {
		        return _levels;
	        }
            set
            {
                if (Equals(value, _levels)) return;
                _levels = value;
                OnPropertyChanged();
            }
        }

        public void InitGameLevels()
        {
            Levels = new ObservableCollection<GameLevelModel>(new GameLevelsRepository().GetAllLevels());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
