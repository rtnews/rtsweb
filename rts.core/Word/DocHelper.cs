using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Drawing;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace rts.core
{
    public sealed class Doc2Png
    {
        public int Pages
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }
    }

    public sealed class DocHelper
    {
        public static Doc2Png Run2Png(string nPath, string nDir)
        {
            Document document = new Document(nPath);

            var result = new Doc2Png();
            result.Pages = document.PageCount;

            var text = document.Range.Text.Trim();
            int index = text.IndexOf('\r');
            result.Title = text.Substring(0, index);
            text = text.Substring(index).Trim();
            index = text.IndexOf('\r');
            if (index > 0)
            {
                if (index > 100) index = 100;
                result.Text = text.Substring(0, index);
            }
            else
            {
                if (text.Length > 100)
                {
                    text = text.Substring(0, 100);
                }
                result.Text = text;
            }

            var shaps = document.GetChildNodes(NodeType.Shape, true);
            foreach (Shape i in shaps)
            {
                if (i.HasImage)
                {
                    using (var stream = new MemoryStream())
                    {
                        i.ImageData.Save(stream);
                        using (Image image = Bitmap.FromStream(stream))
                        {
                            using (Bitmap bitmap = new Bitmap(image, 529, 281))
                            {
                                var t = nDir + "page_i.png";
                                bitmap.Save(t, ImageFormat.Png);
                            }
                        }
                    }
                    break;
                }
            }

            var saveOptions = new ImageSaveOptions(SaveFormat.Png);
            saveOptions.Resolution = 128;
            saveOptions.PrettyFormat = true;
            saveOptions.UseAntiAliasing = true;

            for (int i = 0; i < document.PageCount; i++)
            {
                saveOptions.PageIndex = i;
                var t = string.Format(@"page_{0}.png", i);
                t = nDir + t;
                using (var stream = new MemoryStream())
                {
                    document.Save(stream, saveOptions);
                    using (Image image = Bitmap.FromStream(stream))
                    {
                        using (Bitmap bitmap = new Bitmap(image, 529, 748))
                        {
                            bitmap.Save(t, ImageFormat.Png);
                        }
                    }
                }
            }
            return result;
        }
    }
}
