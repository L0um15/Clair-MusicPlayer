using System;
using System.Diagnostics;
using System.Net;
using System.Windows;

namespace Clair
{

    class VersionHandler
    {
        public static String Version { get { return "pre-0.7.0"; } }

        public static void checkForUpdates()
        {
            try
            {
                string ver = new WebClient().DownloadString("https://raw.githubusercontent.com/L0um15/Clair-MusicPlayer/master/version.txt");
                if (ver != Version)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("New version is available to download.\n" +
                        "Should i open github page?", "Information", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("https://github.com/L0um15/Clair-MusicPlayer"));
                    }

                }
            }
            catch (WebException e)
            {
                String title = "Unable to check for updates";
                switch (e.Status)
                {
                    case WebExceptionStatus.Timeout:
                        MessageBox.Show("Connection Timeout.", title);
                        break;
                    case WebExceptionStatus.ProtocolError:
                        MessageBox.Show("ProtocolError", title);
                        break;
                    case WebExceptionStatus.RequestCanceled:
                        MessageBox.Show("Request Canceled", title);
                        break;
                    case WebExceptionStatus.UnknownError:
                        MessageBox.Show("Unknown Error", title);
                        break;
                    case WebExceptionStatus.ConnectFailure:
                        MessageBox.Show("Connection Failure", title);
                        break;
                    case WebExceptionStatus.ServerProtocolViolation:
                        MessageBox.Show("Server Protocol Violation", title);
                        break;
                    case WebExceptionStatus.ReceiveFailure:
                        MessageBox.Show("Receive Failure", title);
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        MessageBox.Show("Could not resolve hostname", title);
                        break;
                }
            }
        }

    }
}
