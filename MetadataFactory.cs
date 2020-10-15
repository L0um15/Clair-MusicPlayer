using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Clair
{
    class MetadataFactory
    {

        // Get Metadata from File

        public static string getArtist(String path, String notfoundmessage)
        {
            TagLib.File file = TagLib.File.Create(path);
            return (file.Tag.Artists[0] != null) ? file.Tag.Artists[0] : notfoundmessage;
        }

        public static string getTitle(String path, String notfoundmessage)
        {
            TagLib.File file = TagLib.File.Create(path);
            return (file.Tag.Title != null) ? file.Tag.Title : notfoundmessage;
        }

        public static string getLyrics(String path, String notfoundmessage)
        {
            TagLib.File file = TagLib.File.Create(path);
            return (file.Tag.Lyrics != null) ? file.Tag.Lyrics : notfoundmessage;
        }

        public static ImageSource getAlbumPicture(String path)
        {
            TagLib.File file = TagLib.File.Create(path);

            var albumPicture = file.Tag.Pictures.FirstOrDefault();

            if(albumPicture != null)
            {
                MemoryStream memory = new MemoryStream(albumPicture.Data.Data);
                memory.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();
                return bitmap;
            }
            else
            {
                return new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
            }

        }

        public static bool hasAlbumPicture(string path)
        {
            TagLib.File file = TagLib.File.Create(path);
            var albumPicture = file.Tag.Pictures.FirstOrDefault();
            return (albumPicture != null) ? true : false;
        }

    }
}
