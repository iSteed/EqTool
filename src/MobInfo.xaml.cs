﻿using EQTool.Models;
using EQTool.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace EQTool
{
    /// <summary>
    /// Interaction logic for MobInfo.xaml
    /// </summary>
    public partial class MobInfo : Window
    {
        private readonly LogParser logParser;
        private readonly ViewModels.MobInfoViewModel mobInfoViewModel;
        private readonly EQToolSettings settings;
        private readonly EQToolSettingsLoad toolSettingsLoad;
        private readonly WikiApi wikiApi;

        public MobInfo(WikiApi wikiApi, LogParser logParser, EQToolSettings settings, EQToolSettingsLoad toolSettingsLoad)
        {
            this.wikiApi = wikiApi;
            this.settings = settings;
            this.toolSettingsLoad = toolSettingsLoad;
            this.logParser = logParser;
            this.logParser.ConEvent += LogParser_ConEvent;
            DataContext = mobInfoViewModel = new ViewModels.MobInfoViewModel();
            InitializeComponent();
            WindowExtensions.AdjustWindow(settings.MobWindowState, this);
            SaveState();
            SizeChanged += DPSMeter_SizeChanged;
            StateChanged += SpellWindow_StateChanged;
            LocationChanged += DPSMeter_LocationChanged;
        }

        private void LogParser_ConEvent(object sender, LogParser.ConEventArgs e)
        {
            try
            {
                mobInfoViewModel.Results = wikiApi.GetData(e.Name);
            }
            catch (Exception ex)
            {
                mobInfoViewModel.ErrorResults = ex.Message;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            logParser.ConEvent -= LogParser_ConEvent;
            SizeChanged -= DPSMeter_SizeChanged;
            StateChanged -= SpellWindow_StateChanged;
            LocationChanged -= DPSMeter_LocationChanged;
            base.OnClosing(e);
        }

        private void SpellWindow_StateChanged(object sender, EventArgs e)
        {
            SaveState();
        }

        private void DPSMeter_LocationChanged(object sender, EventArgs e)
        {
            SaveState();
        }

        private void DPSMeter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SaveState();
        }
        private void SaveState()
        {
            WindowExtensions.SaveWindowState(settings.MobWindowState, this);
            toolSettingsLoad.Save(settings);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            _ = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigatebutton(object sender, RoutedEventArgs args)
        {
            _ = Process.Start(new ProcessStartInfo(mobInfoViewModel.Url));
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == System.Windows.WindowState.Maximized ? System.Windows.WindowState.Normal : System.Windows.WindowState.Maximized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            settings.MobWindowState.Closed = true;
            SaveState();
            Close();
        }

        private void opendps(object sender, RoutedEventArgs e)
        {
            (App.Current as App).OpenDPSWindow();
        }

        private void opensettings(object sender, RoutedEventArgs e)
        {
            (App.Current as App).OpenSettingsWindow();
        }

        private void openmap(object sender, RoutedEventArgs e)
        {
            (App.Current as App).OpenMapWindow();
        }

        private void openspells(object sender, RoutedEventArgs e)
        {
            (App.Current as App).OpenSpellsWindow();
        }
    }
}
