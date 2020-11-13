using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Clair.Properties;

namespace Clair
{
    public partial class SettingsWindow : Window
    {


        public SettingsWindow()
        {
            InitializeComponent();

            // Sets checkbox to match user settings.

            checkUpdatesCheckbox.IsChecked = Settings.Default.isCheckingUpdates;

            autoShuffleCheckbox.IsChecked = Settings.Default.isAutoShuffleEnabled;

            enableExtensionsCheckbox.IsChecked = Settings.Default.isUnsupportedExtensionsEnabled;

        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (settingsMenu.SelectedIndex)
            {
                case 0:
                    homeGrid.Visibility = Visibility.Visible;
                    aboutGrid.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    homeGrid.Visibility = Visibility.Hidden;
                    aboutGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

            // Opens Webpage 

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void autoShuffleCheckbox_Checked(object sender, RoutedEventArgs e)
        {


            if(Settings.Default.isAutoShuffleEnabled == false)
            {
                Settings.Default.isAutoShuffleEnabled = true;
                Settings.Default.Save();

            }
        }

        private void autoShuffleCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            if (Settings.Default.isAutoShuffleEnabled == true)
            {
                Settings.Default.isAutoShuffleEnabled = false;
                Settings.Default.Save();

            }
        }

        private void enableExtensionsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isUnsupportedExtensionsEnabled == false)
            {
                Settings.Default.isUnsupportedExtensionsEnabled = true;
                Settings.Default.Save();

            }
        }

        private void enableExtensionsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isUnsupportedExtensionsEnabled == true)
            {
                Settings.Default.isUnsupportedExtensionsEnabled = false;
                Settings.Default.Save();

            }
        }

        private void checkUpdatesCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isCheckingUpdates == false)
            {
                Settings.Default.isCheckingUpdates = true;
                Settings.Default.Save();

            }
        }

        private void checkUpdatesCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isCheckingUpdates == true)
            {
                Settings.Default.isCheckingUpdates = false;
                Settings.Default.Save();

            }
        }
        private bool isUrlValid(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }
    }
}
