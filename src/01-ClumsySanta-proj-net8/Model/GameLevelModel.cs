using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Foundation;
using ClumsySanta.Annotations;
using ClumsySanta.Implementation.Commands;
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
        
        private ObservableCollection<GamePresentViewModel> _presents = new ObservableCollection<GamePresentViewModel>();
        private ObservableCollection<GameDecorationViewModel> _decorations = new ObservableCollection<GameDecorationViewModel>();
        private string _grayPresentIcon;
        private int _PresentsToHide;
        private string _TimerPresentIcon;

        public ObservableCollection<GamePresentViewModel> Presents
        {
            get { return _presents; }
            set
            {
                if (value == _presents) return;
                _presents = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameDecorationViewModel> Decorations
        {
            get { return _decorations; }
            set
            {
                if (value == _decorations) return;
                _decorations = value;
                OnPropertyChanged();
            }
        }

        public string LevelPickerImage
        {
            get { return _levelPickerImage; }
            set
            {
                if (value == _levelPickerImage) return;
                _levelPickerImage = value;
                OnPropertyChanged();
            }
        }

        public string PresentIcon
        {
            get { return _presentIcon; }
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
            get { return _TimerPresentIcon; }
            set
            {
                if (value == _TimerPresentIcon) return;
                _TimerPresentIcon = value;
                OnPropertyChanged();
            }
        }

        public string GrayPresentIcon
        {
            get { return _grayPresentIcon; }
            set
            {
                if (value == _grayPresentIcon) return;
                _grayPresentIcon = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                if (value == _backgroundImage) return;
                _backgroundImage = value;
                OnPropertyChanged();
            }
        }

        public string LaterBackgroundImage
        {
            get { return _laterBackgroundImage; }
            set
            {
                if (value == _laterBackgroundImage) return;
                _laterBackgroundImage = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan PlaybackTime
        {
            get { return _playbackTime; }
            set
            {
                if (value.Equals(_playbackTime)) return;
                _playbackTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailableToPlay
        {
            get { return _isAvailableToPlay; }
            set
            {
                if (value.Equals(_isAvailableToPlay)) return;
                _isAvailableToPlay = value;
                OnPropertyChanged();
            }
        }

        public Size BgImgSize
        {
            get { return _bgSize; }
            set
            {
                if (value.Equals(_bgSize)) return;
                _bgSize = value;
                OnPropertyChanged();
            }
        }

        public int PresentsToShow
        {
            get { return _PresentsToHide; }
            set
            {
                if (value.Equals(_PresentsToHide)) return;
                _PresentsToHide = value;
                OnPropertyChanged();
            }
        }

        public Point StartPoint
        {
            get { return _center; }
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
