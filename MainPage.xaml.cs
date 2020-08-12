using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;
using System;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectVise
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFile Archive { get; set; }
        StorageFolder Folder { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            openPicker.CommitButtonText = "Открыть";
            openPicker.FileTypeFilter.Add(".rar");
            Archive = await openPicker.PickSingleFileAsync();

            if (Archive != null)
            {
                archivePath.Text = Archive.Path;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openPicker = new FolderPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add("*");

            Folder = await openPicker.PickSingleFolderAsync();
            if (Folder != null)
            {
                folderPath.Text = Folder.Path;
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var archiveStream = await Archive.OpenStreamForReadAsync())
            using (var reader = ReaderFactory.Open(archiveStream))
            {
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        var fileToExtract = await Folder.CreateFileAsync(reader.Entry.Key);
                        var fileStream = await fileToExtract.OpenStreamForWriteAsync();
                        reader.WriteEntryTo(fileStream);
                    }
                }
            }
        }
    }
}
