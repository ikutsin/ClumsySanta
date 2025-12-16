using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClumsySanta.Model;
using ClumsySanta.Redist;

namespace ClumsySanta.ViewModel
{
    public class LevelCompletedViewModel : INotifyPropertyChanged
    {
        private int _collectedPresentCount;

        public LevelCompletedViewModel()
        {
            if (true || System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                CurrentLevel = new GameLevelsRepository().GetAllLevels()
                                                       .Skip(2)
                                                       .First();
                TimeSecondsLeft = 4;
                CollectedPresentCount = 5;
            }
        }

        public int CollectedPresentCount
        {
            get { return _collectedPresentCount; }
            set
            {
                if (value == _collectedPresentCount) return;
                _collectedPresentCount = value;
                OnPropertyChanged();
            }
        }
        
        private double _timeSecondsLeft;
        private GameLevelModel _currentLevel;

        public double TimeSecondsLeft
        {
            get { return _timeSecondsLeft; }
            set
            {
                _timeSecondsLeft = value;
                OnPropertyChanged();
            }
        }

        public double TimeSecondsUsed
        {
            get { return CurrentLevel.PlaybackTime.TotalSeconds - TimeSecondsLeft; }
        }

        public GameLevelModel CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                if (value == _currentLevel) return;
                _currentLevel = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
