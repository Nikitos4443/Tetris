using System.Collections.Generic;
using System.Windows.Shapes;

namespace Tetris
{
    public class Pentamino : ProgenitorFigure
    {
        public override MyDelegate[] myDelegates()
        {
            return new MyDelegate[] { FormModZLeft, FormModZRight, FormPRight, FormPLeft, FormLowBranchLeft, FormLowBranchRight, FormStair, FormLine, FormLRight, FormT, FormLLeft, FormZLeft, FormZRight, FormX, FormAngle, FormBowl, FormBranchLeft, FormBranchRight };
        }

        public void FormT(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 90, 150, 60, 180, 30, 150, 30 }, new int[] { 90, 330, 120, 360, 150, 330, 120, 330, 120, 390 });
        }

        public void FormLRight(char check)
        {
            FormFigure(check, new int[] { 90, 60, 120, 60, 150, 60, 180, 60, 180, 30 }, new int[] { 75, 375, 105, 375, 135, 375, 165, 375, 165, 345 });
        }

        public void FormLLeft(char check)
        {
            FormFigure(check, new int[] { 90, 60, 120, 60, 150, 60, 180, 60, 90, 30 }, new int[] { 75, 375, 105, 375, 135, 375, 165, 375, 75, 345 });
        }

        public void FormZLeft(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 150, 60, 150, 90, 180, 90 }, new int[] { 90, 330, 120, 330, 120, 360, 120, 390, 150, 390 });
        }

        public void FormZRight(char check)
        {
            FormFigure(check, new int[] { 120, 90, 150, 90, 150, 60, 150, 30, 180, 30 }, new int[] { 90, 390, 120, 390, 120, 360, 120, 330, 150, 330 });
        }

        public void FormX(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 30, 150, 60, 150, 90, 180, 60 }, new int[] { 90, 390, 120, 360, 120, 390, 120, 420, 150, 390 });
            centerRect = null;
        }

        public void FormLine(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 180, 30, 210, 30, 240, 30 }, new int[] { 60, 360, 90, 360, 120, 360, 150, 360, 180, 360 });
        }

        public void FormAngle(char check)
        {
            FormFigure(check, new int[] { 120, 90, 150, 90, 180, 90, 180, 60, 180, 30 }, new int[] { 90, 420, 120, 420, 150, 420, 150, 390, 150, 360 });
        }

        public void FormBowl(char check)
        {
            FormFigure(check, new int[] { 120, 30, 120, 60, 150, 60, 180, 60, 180, 30 }, new int[] { 90, 345, 90, 375, 120, 375, 150, 375, 150, 345 });
        }

        public void FormBranchRight(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 180, 60, 180, 30, 210, 30 }, new int[] { 75, 345, 105, 345, 135, 345, 165, 345, 135, 375 });
        }

        public void FormBranchLeft(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 60, 150, 30, 180, 30, 210, 30 }, new int[] { 75, 345, 105, 345, 135, 345, 165, 345, 105, 375 });
        }

        public void FormStair(char check)
        {
            FormFigure(check, new int[] { 120, 90, 150, 90, 150, 60, 180, 60, 180, 30 }, new int[] { 90, 420, 120, 420, 120, 390, 150, 390, 150, 360 });
        }

        public void FormLowBranchRight(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 90, 150, 60, 150, 30, 180, 30 }, new int[] { 90, 390, 120, 360, 120, 390, 120, 420, 150, 360 });
        }

        public void FormLowBranchLeft(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 150, 60, 150, 90, 180, 60 }, new int[] { 90, 360, 120, 360, 120, 390, 120, 420, 150, 390 });
        }

        public void FormPLeft(char check)
        {
            FormFigure(check, new int[] { 120, 30, 120, 60, 150, 30, 150, 60, 180, 30 }, new int[] { 90, 345, 90, 375, 120, 345, 120, 375, 150, 345 });
        }

        public void FormPRight(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 60, 150, 30, 180, 60, 180, 30 }, new int[] { 90, 345, 120, 375, 120, 345, 150, 375, 150, 345 });
        }

        public void FormModZRight(char check)
        {
            FormFigure(check, new int[] { 120, 30, 150, 30, 150, 60, 180, 60, 210, 60 }, new int[] { 75, 345, 105, 345, 105, 375, 135, 375, 165, 375 });
        }

        public void FormModZLeft(char check)
        {
            FormFigure(check, new int[] { 120, 60, 150, 60, 180, 60, 180, 30, 210, 30 }, new int[] { 75, 375, 105, 375, 135, 375, 135, 345, 165, 345 });
        }
    }
}