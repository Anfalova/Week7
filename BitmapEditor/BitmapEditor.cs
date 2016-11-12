using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BitmapEditor
{
    public class BitmapEditor : IDisposable
    {

        private Bitmap source;
        private BitmapData bitmapData;
        private bool isDisposed = false;
        private byte[] bytes;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public BitmapEditor(Bitmap bitmap)
        {
            this.source = bitmap;
            Width = source.Width;
            Height = source.Height;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);
            bytes = new byte[Width * Height * 3];
            Marshal.Copy(bitmapData.Scan0, bytes, 0, bytes.Length);
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (isDisposed)
                throw new InvalidOperationException("BitmapEditor isn't available");
            int i = ((y * Width) + x) * 3;
            bytes[i] = b;
            bytes[i + 1] = g;
            bytes[i + 2] = r;
        }

        ~BitmapEditor()
        {
            Dispose();
        }

        public void UnlockBits()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
                source.UnlockBits(bitmapData);
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
