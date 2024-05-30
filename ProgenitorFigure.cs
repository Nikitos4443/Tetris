using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Documents;

namespace Tetris
{
    public class ProgenitorFigure
    {
        public delegate void MyDelegate(char check);
        public MyDelegate[] FigureFormingDelegates;
        public List<Rectangle> figure = new List<Rectangle>();
        public List<Rectangle> nextFigure = new List<Rectangle>();
        public List<Rectangle> placedRectangles = new List<Rectangle>();
        public Rectangle centerRect;


        int squareSize = ProgenitorRectangle.squareSize;
        Thickness prob = new Thickness();

        int[] lastThreeFigures = new int[3];
        SolidColorBrush nextFigureColor = ProgenitorRectangle.GetRandomLightColor();
        int index = 0;
        int nextRandomIndex = -1;

        public List<Rectangle> Figure
        {
            get { return figure; }
        }

        public List<Rectangle> NextFigure
        {
            get { return nextFigure; }
        }

        public virtual MyDelegate[] myDelegates()
        {
            return null;
        }

        public int[] LastThreeFigures
        {
            get
            {
                return lastThreeFigures;
            }
            set
            {
                lastThreeFigures = value;
            }
        }

        public int NextRandomIndex
        {
            set
            {
                nextRandomIndex = value;
            }
        }

        public int Index
        {
            set
            {
                index = value;
            }
        }

        private void SetNextFigureColor()
        {
            SolidColorBrush randomFill = ProgenitorRectangle.GetRandomLightColor();
            nextFigureColor = randomFill;
            foreach (Rectangle rectangle in NextFigure)
            {
                rectangle.Fill = randomFill;
            }
        }

        public void GetRandomFigure(MyDelegate[] del)
        {
            FigureFormingDelegates = del;

            int randomIndex = GetRandom(del);

            FigureFormingDelegates[randomIndex]('c');
            SetNextFigureColor();
        }

        public int GetRandom(MyDelegate[] del)
        {
            FigureFormingDelegates = del;

            Random rand = new Random();
            int randomIndex;
            if (nextRandomIndex == -1)
            {
                randomIndex = rand.Next(0, del.Length);
                lastThreeFigures[index] = randomIndex;
                index++;
            }
            else
            {
                randomIndex = nextRandomIndex;
            }

            nextRandomIndex = rand.Next(0, del.Length);
            lastThreeFigures[index] = nextRandomIndex;

            while (lastThreeFigures[0] == lastThreeFigures[1] && lastThreeFigures[1] == lastThreeFigures[2])
            {
                nextRandomIndex = rand.Next(0, del.Length);
                lastThreeFigures[index] = nextRandomIndex;
            }
            FigureFormingDelegates[nextRandomIndex]('n');

            index = index == 2 ? 0 : index + 1;
            return randomIndex;
        }

        protected void FormFigure(char check, int[] marginsCurr, int[] marginsNext)
        {
            if (check == 'c')
            {
                figure.Clear();
                for (int i = 0; i < marginsCurr.Length - 1; i += 2)
                {
                    Rectangle rectangle = new Rectangle();
                    ProgenitorRectangle.CopyRectangleProperties(ref rectangle, nextFigureColor);
                    rectangle.Margin = new Thickness(marginsCurr[i], marginsCurr[i + 1], 0, 0);
                    figure.Add(rectangle);
                }
                centerRect = marginsCurr.Length == 8 ? figure[1] : figure[2];
            }
            else
            {
                nextFigure.Clear();
                for (int i = 0; i < marginsNext.Length - 1; i += 2)
                {
                    Rectangle rectangle = new Rectangle();
                    ProgenitorRectangle.CopyRectangleProperties(ref rectangle, nextFigureColor);
                    rectangle.Margin = new Thickness(marginsNext[i], marginsNext[i + 1], 0, 0);
                    nextFigure.Add(rectangle);
                }
            }
        }

        public List<Rectangle> FigureTurn()
        {
            List<Thickness> turnedArr = new List<Thickness>();

            for (int i = 0; i < figure.Count; i++)
            {
                turnedArr.Add(figure[i].Margin);
            }

            turnedArr = MatrixTurn(turnedArr);

            while (IsCollision(turnedArr)[0])
            {
                if (CanMove('l', turnedArr))
                {
                    Move(turnedArr, 'r');
                }
                else
                {
                    return figure;
                }
            }

            while (IsCollision(turnedArr)[1])
            {
                if (CanMove('r', turnedArr))
                {
                    Move(turnedArr, 'l');
                }
                else
                {
                    return figure;
                }
            }

            if (isCollisionUpOrDown(turnedArr))
            {
                return figure;
            }



            for (int i = 0; i < figure.Count; i++)
            {
                figure[i].Margin = turnedArr[i];
            }


            return figure;
        }

        public void Move(List<Rectangle> r, char check)
        {
            for (int i = 0; i < r.Count; i++)
            {
                Thickness margin = r[i].Margin;
                margin.Left = check == 'l' ? margin.Left - 30 : margin.Left + 30;
                r[i].Margin = margin;
            }
        }

        public void Move(List<Thickness> th, char check)
        {
            for (int i = 0; i < th.Count; i++)
            {
                Thickness margin = th[i];
                margin.Left = check == 'l' ? margin.Left - 30 : margin.Left + 30;
                th[i] = margin;
            }
        }

        private List<Thickness> MatrixTurn(List<Thickness> arr)
        {
            List<Thickness> resultOfTurning = new List<Thickness>();

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] == centerRect.Margin)
                {
                    resultOfTurning.Add(centerRect.Margin);
                    continue;
                }

                Point startPoint = new Point();
                Point endPoint = new Point();
                Thickness marg = arr[i];

                startPoint.X = arr[i].Left - centerRect.Margin.Left;
                startPoint.Y = centerRect.Margin.Top - arr[i].Top;
                endPoint.X = startPoint.Y;
                endPoint.Y = -startPoint.X;

                marg.Left += endPoint.X - startPoint.X;
                marg.Top -= endPoint.Y - startPoint.Y;

                resultOfTurning.Add(marg);
            }

            return resultOfTurning;
        }

        public bool[] IsCollision(List<Thickness> collArr)
        {
            bool[] collisions = { false, false };

            foreach (Thickness r in collArr)
            {
                if (r.Left < 30)
                {
                    collisions[0] = true;
                    prob = r;
                    break;
                }

                if (r.Left > 300)
                {
                    collisions[1] = true;
                    prob = r;
                    break;
                }
            }

            foreach (Thickness r in collArr)
            {
                foreach (var placedRect in placedRectangles)
                {
                    if (r == placedRect.Margin && centerRect.Margin.Left > placedRect.Margin.Left)
                    {
                        collisions[0] = true;
                        prob = r;
                        break;
                    }
                    if (r == placedRect.Margin && centerRect.Margin.Left < placedRect.Margin.Left)
                    {
                        collisions[1] = true;
                        prob = r;
                        break;
                    }
                }
            }

            return collisions;
        }

        public bool isCollisionUpOrDown(List<Thickness> collArr)
        {
            foreach (Thickness r in collArr)
            {
                if (r.Top > 600 || r.Top < 30)
                {
                    return true;
                }

                foreach (Rectangle r2 in placedRectangles)
                {
                    if (r == r2.Margin)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanMove(char check, List<Thickness> collArr)
        {
            Thickness[] copy = new Thickness[collArr.Count];
            bool isProblem = true;
            collArr.CopyTo(copy);

            if (check == 'l')
            {
                while (isProblem)
                {
                    isProblem = false;
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i].Left += squareSize;
                        if (copy[i] == prob)
                        {
                            isProblem = true;
                        }
                    }
                }
            }
            else
            {
                while (isProblem)
                {
                    isProblem = false;
                    for (int i = 0; i < copy.Length; i++)
                    {
                        copy[i].Left -= squareSize;
                        if (copy[i] == prob)
                        {
                            isProblem = true;
                        }
                    }
                }
            }

            for (int i = 0; i < copy.Length; i++)
            {
                if (copy[i].Left >= 30 && copy[i].Left <= 300)
                {
                    foreach (Rectangle r in placedRectangles)
                    {
                        if (copy[i] == r.Margin)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
