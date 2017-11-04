using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rts.core
{
    public sealed class ImgHelper
    {
        public static Image RunZoom(Image nImage, int nWidth, int nHeight)
        {
            try
            {
                ImageFormat format = nImage.RawFormat;
                Bitmap bitmap = new Bitmap(nWidth, nHeight);

                int width, height;
                if (nImage.Width > nWidth && nImage.Height <= nHeight)
                {
                    width = nWidth;
                    height = (width * nImage.Height) / nImage.Width;
                }
                else if (nImage.Width <= nWidth && nImage.Height > nHeight)
                {
                    height = nHeight;
                    width = (height * nImage.Width) / nImage.Height;
                }
                else if (nImage.Width <= nWidth && nImage.Height <= nHeight)
                {
                    width = nImage.Width;
                    height = nImage.Height;
                }
                else
                {
                    width = nWidth;
                    height = (width * nImage.Height) / nImage.Width;
                    if (height > nHeight)
                    {
                        height = nHeight;
                        width = (height * nImage.Width) / nImage.Height;
                    }
                }
                
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(nImage, (nWidth - width) / 2, (nHeight - height) / 2, width, height);
                nImage.Dispose();

                return bitmap;
            }
            catch (Exception e)
            {
                LogEngine.Instance().LogError(e.Message);
            }
            return null;
        }
    }
}
