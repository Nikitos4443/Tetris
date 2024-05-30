using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Tetris
{
    public static class ProgenitorRectangle
    {
        static public int squareSize = 30;
        public static void CopyRectangleProperties(ref Rectangle target, SolidColorBrush currColor)
        {
            target.Focusable = true;
            target.Height = squareSize;
            target.Width = squareSize;
            target.StrokeThickness = 0.4;
            target.VerticalAlignment = VerticalAlignment.Top;
            target.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(target, 0);
            Grid.SetZIndex(target, 1);

            SolidColorBrush randomFill = currColor;
            Color color = randomFill.Color;
            target.Fill = randomFill;

            DropShadowEffect glowEffect = new DropShadowEffect
            {
                Color = color,
                Direction = 0,
                ShadowDepth = 0,
                BlurRadius = 15,
                Opacity = 1
            };
            target.Effect = glowEffect;
        }

        public static SolidColorBrush GetRandomLightColor()
        {
            Random random = new Random();
            byte r = (byte)random.Next(128, 256);
            byte g = (byte)random.Next(128, 256);
            byte b = (byte)random.Next(128, 256); 
            return new SolidColorBrush(Color.FromArgb(255, r, g, b));
        }


    }
}
