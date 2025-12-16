using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ClumsySanta.Annotations;
using ClumsySanta.Implementation;
using ClumsySanta.Implementation.Commands;
using ClumsySanta.Model;

namespace ClumsySanta.ViewModel
{
    public class GamePlayEngineViewModel : INotifyPropertyChanged
    {
        private readonly ICommand _completeLevel;

        public GamePlayEngineViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}