using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace ClumsySanta.ViewModel
{
    public class GameDecorationViewModel : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, int> _levelPriorities;

        static GameDecorationViewModel()
        {
            _levelPriorities = new Dictionary<string, int>
            {
                {"FirstDecorationsCanvas", 1},
                {"FirstLaterDecorationsCanvas", 2},
                {"PresentsCanvas", 3},
                {"LaterPresentsCanvas", 4},
                {"DecorationsCanvas", 5},
                {"LaterDecorationsCanvas", 6}
            };
        }

        public GameDecorationViewModel()
        {
            //Level = "FirstDecorationsCanvas";
            Level = "DecorationsCanvas";
            OrignCoordinate = new Point();
        }

        private Point _coordinate;
        private Point _orignCoordinate;
        private string _level;
        private string _baseImage;
        private Size _size;
        private ObservableCollection<string> _dependsOn = new ObservableCollection<string>();
        private string _specialAnimation;
        private string _specialSound;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DependenceRemoved;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Point Coordinate
        {
            get => _coordinate;
            set
            {
                if (value.Equals(_coordinate)) return;
                _coordinate = value;
                OnPropertyChanged();
            }
        }

        public Size Size
        {
            get => _size;
            set
            {
                if (value.Equals(_size)) return;
                _size = value;
                OrignCoordinate = new Point(_size.Width/2, _size.Height/2);
                OnPropertyChanged();
            }
        }

        public string BaseImage
        {
            get => _baseImage;
            set
            {
                if (value == _baseImage) return;
                _baseImage = value;
                OnPropertyChanged();
            }
        }

        public string SpecialSound
        {
            get => _specialSound;
            set
            {
                if (value == _specialSound) return;
                _specialSound = value;
                OnPropertyChanged();
            }
        }

        public string Level
        {
            get => _level;
            set
            {
                if (value == _level) return;
                _level = value;
                OnPropertyChanged();
            }
        }

        public Point OrignCoordinate
        {
            get => _orignCoordinate;
            set
            {
                if (value.Equals(_orignCoordinate)) return;
                _orignCoordinate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> DependsOn
        {
            get => _dependsOn;
            set
            {
                if (value == _dependsOn) return;
                _dependsOn = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }

        public string SpecialAnimation
        {
            get => _specialAnimation;
            set
            {
                if (value == _specialAnimation) return;
                _specialAnimation = value;
                OnPropertyChanged();
            }
        }

        internal void Removed()
        {
            if (this.DependenceRemoved != null)
                this.DependenceRemoved(this, EventArgs.Empty);
        }

        internal int LevelPriority => _levelPriorities[Level];
    }
}