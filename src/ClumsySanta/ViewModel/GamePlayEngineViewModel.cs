using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ClumsySanta.Commands;
using ClumsySanta.Model;
using ClumsySanta.Redist;

namespace ClumsySanta.ViewModel
{
    public class GamePlayEngineViewModel : INotifyPropertyChanged
    {
        private readonly ICommand _completeLevel;

        public GamePlayEngineViewModel()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                LevelModel = new GameLevelsRepository().GetAllLevels()
                                                       .Skip(1)
                                                       .First();
            }

            _completeLevel = new LevelCompletedCommand();
        }

        private GameLevelModel _levelModel;
        public GameLevelModel LevelModel
        {
            get { return _levelModel; }
            set
            {
                if (Equals(value, _levelModel)) return;
                _levelModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CompleteLevel
        {
            get { return _completeLevel; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}