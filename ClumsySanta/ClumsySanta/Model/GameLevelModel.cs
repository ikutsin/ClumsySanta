using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ClumsySanta.ViewModel;

namespace ClumsySanta.Model
{
    public class GameLevelModel : INotifyPropertyChanged
    {
        private string _levelPickerImage;
        private bool _isAvailableToPlay;
        private TimeSpan _playbackTime;
        private string _backgroundImage;
        private string _laterBackgroundImage;
        private string _presentIcon;
        private string _name;
        private Size _bgSize;
        private Point _center;
        
        private ObservableCollection<GamePresentViewModel> _presents = new();
        private ObservableCollection<GameDecorationViewModel> _decorations = new();
        private string _grayPresentIcon;
        private int _PresentsToHide;
        private string _TimerPresentIcon;

        public ObservableCollection<GamePresentViewModel> Presents
        {
            get => _presents;
            set
            {
                if (value == _presents) return;
                _presents = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameDecorationViewModel> Decorations
        {
            get => _decorations;
            set
            {
                if (value == _decorations) return;
                _decorations = value;
                OnPropertyChanged();
            }
        }

        public string LevelPickerImage
        {
            get => _levelPickerImage;
            set
            {
                if (value == _levelPickerImage) return;
                _levelPickerImage = value;
                OnPropertyChanged();
            }
        }

        public string PresentIcon
        {
            get => _presentIcon;
            set
            {
                if (value == _presentIcon) return;
                _presentIcon = value;
                TimerPresentIcon = value;
                OnPropertyChanged();
            }
        }
        public string TimerPresentIcon
        {
            get => _TimerPresentIcon;
            set
            {
                if (value == _TimerPresentIcon) return;
                _TimerPresentIcon = value;
                OnPropertyChanged();
            }
        }

        public string GrayPresentIcon
        {
            get => _grayPresentIcon;
            set
            {
                if (value == _grayPresentIcon) return;
                _grayPresentIcon = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                if (value == _backgroundImage) return;
                _backgroundImage = value;
                OnPropertyChanged();
            }
        }

        public string LaterBackgroundImage
        {
            get => _laterBackgroundImage;
            set
            {
                if (value == _laterBackgroundImage) return;
                _laterBackgroundImage = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan PlaybackTime
        {
            get => _playbackTime;
            set
            {
                if (value.Equals(_playbackTime)) return;
                _playbackTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailableToPlay
        {
            get => _isAvailableToPlay;
            set
            {
                if (value.Equals(_isAvailableToPlay)) return;
                _isAvailableToPlay = value;
                OnPropertyChanged();
            }
        }

        public Size BgImgSize
        {
            get => _bgSize;
            set
            {
                if (value.Equals(_bgSize)) return;
                _bgSize = value;
                OnPropertyChanged();
            }
        }

        public int PresentsToShow
        {
            get => _PresentsToHide;
            set
            {
                if (value.Equals(_PresentsToHide)) return;
                _PresentsToHide = value;
                OnPropertyChanged();
            }
        }

        public Point StartPoint
        {
            get => _center;
            set
            { 
                if (value.Equals(_center)) return;
                _center = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void RemoveDependence(object sender, EventArgs args)
        {
            var model = (GameDecorationViewModel)sender;
            model.DependenceRemoved -= RemoveDependence;
            foreach (var item in Decorations.Concat(Presents))
            {
                item.DependsOn.Remove(model.Name);
            }
        }
    }
}
