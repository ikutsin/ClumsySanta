using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClumsySanta.Annotations;
using ClumsySanta.Implementation;
using ClumsySanta.Implementation.Commands;
using ClumsySanta.Model;

namespace ClumsySanta.ViewModel
{
    public class ChooseLevelViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<GameLevelModel> _levels;
        private ICommand _doStartGame;

        public ChooseLevelViewModel()
        {
            _doStartGame = new StartGameLevelCommand();
        }

        public ObservableCollection<GameLevelModel> Levels
        {
            get { return _levels; }
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

        public ICommand DoStartGame
        {
            get { return _doStartGame; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
