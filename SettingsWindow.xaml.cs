using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clair.Properties;

namespace Clair
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {


        public SettingsWindow()
        {
            InitializeComponent();

            autoShuffleCheckbox.IsChecked = Settings.Default.isAutoShuffleEnabled;

            enableExtensionsCheckbox.IsChecked = Settings.Default.isUnsupportedExtensionsEnabled;

        }

        private void homeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(homeGrid.Visibility != Visibility.Visible)
            {
                homeGrid.Visibility = Visibility.Visible;
                aboutGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void aboutButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (aboutGrid.Visibility != Visibility.Visible)
            {
                aboutGrid.Visibility = Visibility.Visible;
                homeGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
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
    }
}
