using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Infrastructure.Utilities
{
    public static class Images
    {
        public static byte[] GetImageNewSize(HttpPostedFileBase file)
        {

            using (var img = Image.FromStream(file.InputStream))
            {
                Size imgSize = ChangeSize(img);

                using (Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
                {
                    var ms = new MemoryStream();
                    newImg.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
        }

        private static byte[] GetBytesFromImage(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        private static Size ChangeSize(Image img)
        {
            Size finalSize;
            double tempval;
            Size imageSize = img.Size;
            Size newSize = new Size(600, 600);
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize;

            return finalSize;
        }

        public static byte[] GetImage(HttpPostedFileBase file)
        {

            using (var img = Image.FromStream(file.InputStream))
            {
                using (Image newImg = new Bitmap(img))
                {
                    var ms = new MemoryStream();
                    newImg.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
        }
    }
}