using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tetris
{
    public class Tetramino: ProgenitorFigure
    {
        public override MyDelegate[] myDelegates()
        {
            return new MyDelegate[] { FormT, FormZLeft, FormZRight, FormLLeft, FormLRight, FormSquare, FormLine };
        }

        public void FormT(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 60, 180, 60, 150, 30 }, new int[] { 90, 375, 120, 375, 150, 375, 120, 345 });
        }

        public void FormLRight(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 60, 180, 60, 180, 30 }, new int[] { 90, 375, 120, 375, 150, 375, 150, 345 });
        }

        public void FormLLeft(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 60, 180, 60, 120, 30 }, new int[] { 90, 375, 120, 375, 150, 375, 90, 345 });
        }

        public void FormZLeft(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 150, 60, 180, 60 }, new int[] { 90, 345, 120, 345, 120, 375, 150, 375 });
        }

        public void FormZRight(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 60, 150, 30, 180, 30 }, new int[] { 90, 375, 120, 375, 120, 345, 150, 345 });
        }

        public void FormSquare(char check)
        {
            FormFigure(check, new int[] { 150, 30, 180, 30, 180, 60, 150, 60 }, new int[] { 105, 345, 135, 345, 135, 375, 105, 375 });
            centerRect = null;
        }

        public void FormLine(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 180, 30, 210, 30 }, new int[] { 75, 360, 105, 360, 135, 360, 165, 360 });
        }
    }
}