﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EQTool.Models
{
    public class WindowState
    {
        public Rect? WindowRect { get; set; }
        public System.Windows.WindowState State { get; set; }
        public bool Closed { get; set; }
        public bool AlwaysOnTop { get; set; }
        private double _Opacity = 1.0;
        public double? Opacity
        {
            get
            {
                return _Opacity;
            }
            set
            {
                _Opacity = value ?? 1.0;
            }
        }
    }

    public class EQToolSettings : INotifyPropertyChanged
    {
        public string DefaultEqDirectory { get; set; }
        public string EqLogDirectory { get; set; }

        private int _FontSize = 12;
        public int? FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value ?? 12;
                _FontSize = _FontSize < 12 ? 12 : _FontSize;
            }
        }

        private WindowState _OverlayWindowState;
        public WindowState OverlayWindowState
        {
            get
            {
                if (_OverlayWindowState == null)
                {
                    _OverlayWindowState = new WindowState();
                }
                return _OverlayWindowState;
            }
            set => _OverlayWindowState = value ?? new WindowState();
        }

        private WindowState _SpellWindowState;
        public WindowState SpellWindowState
        {
            get
            {
                if (_SpellWindowState == null)
                {
                    _SpellWindowState = new WindowState();
                }
                return _SpellWindowState;
            }
            set => _SpellWindowState = value ?? new WindowState();
        }

        private WindowState _DpsWindowState;
        public WindowState DpsWindowState
        {
            get
            {
                if (_DpsWindowState == null)
                {
                    _DpsWindowState = new WindowState();
                }
                return _DpsWindowState;
            }
            set => _DpsWindowState = value ?? new WindowState();
        }

        private WindowState _MapWindowState;
        public WindowState MapWindowState
        {
            get
            {
                if (_MapWindowState == null)
                {
                    _MapWindowState = new WindowState();
                }
                return _MapWindowState;
            }
            set => _MapWindowState = value ?? new WindowState();
        }

        private WindowState _MobWindowState;
        public WindowState MobWindowState
        {
            get
            {
                if (_MobWindowState == null)
                {
                    _MobWindowState = new WindowState();
                }
                return _MobWindowState;
            }
            set => _MobWindowState = value ?? new WindowState();
        }

        public List<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();

        public bool BestGuessSpells { get; set; }

        private bool _EnrageOverlay;
        public bool EnrageOverlay
        {
            get => _EnrageOverlay;
            set
            {
                _EnrageOverlay = value;
                OnPropertyChanged();
            }
        }

        public bool YouOnlySpells { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
