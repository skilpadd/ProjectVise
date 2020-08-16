using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ProjectVise
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavigateToPage(item as NavigationViewItem);
            }
        }

        private void NavigateToPage(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "OpenArchive":
                    ContentFrame.Navigate(typeof(OpenArchivePage), null, new EntranceNavigationTransitionInfo());
                    break;

                case "CreateArchive":
                    ContentFrame.Navigate(typeof(CreateArchivePage), null, new EntranceNavigationTransitionInfo());
                    break;

                case "ConvertArchive":
                    ContentFrame.Navigate(typeof(ConvertArchivePage), null, new EntranceNavigationTransitionInfo());
                    break;
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItem item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "OpenArchive")
                {
                    NavView.SelectedItem = item;
                    NavigateToPage(item);
                    break;
                }
            }
        }
    }
}
