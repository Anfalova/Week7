﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer.Timer();
            Bitmap bitmap = new Bitmap("C:\\Users\\Надежда\\Desktop\\123.jpg");
            Color clr = Color.Green;
            using (timer.Start())
            {
                for (int i = 0; i < bitmap.Height; i++)
                    for (int j = 0; j < bitmap.Width; j++)
                        bitmap.SetPixel(j, i, clr);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            timer.Reset();
            using (timer.Start())
            {
                using (var bitmapEditor = new BitmapEditor.BitmapEditor(bitmap))
                {
                    for (int i = 0; i < bitmap.Height; i++)
                        for (int j = 0; j < bitmap.Width; j++)
                            bitmapEditor.SetPixel(j, i, 255, 255, 255);
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
