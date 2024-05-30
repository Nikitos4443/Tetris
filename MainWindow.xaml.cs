using Learning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Effects;

namespace Tetris
{
    public partial class MainWindow : Window
    {
        double timeToFall = Settings.FallingTime;
        int scoreToVel = 10000;
        public static char checkMode;
        private DispatcherTimer timer = new DispatcherTimer();
        public bool isContinue;

        ProgenitorFigure fig;
        ProgenitorFigure log = new ProgenitorFigure();
        int score = 0;

        public MainWindow(char checking)
        {
            InitializeComponent();
            Saving.ClearSavings();
            checkMode = checking;

            if(checkMode == 't')
            {
                fig = new Tetramino();
                Title = "Tetramino";
            }
            else
            {
                fig = new Pentamino();
                Title = "Pentamino";
            }
            Audio.PlayMusic();
            SetupGame();
        }

        public MainWindow(char checking, List<Rectangle> placed, int score, double timeFall)
        {
            InitializeComponent();
            checkMode = checking;
            this.score = score;
            _score.Content = $"Score: {score}";
            timeToFall = timeFall;

            if (checkMode == 't')
            {
                fig = new Tetramino();
                Title = "Tetramino";
            }
            else
            {
                fig = new Pentamino();
                Title = "Pentamino";
            }

            fig.placedRectangles = placed;
            GenStartFigures();
            Audio.PlayMusic();
            SetupGame();
        }

        private void MoveHandle(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && fig.centerRect != null)
            {
                fig.FigureTurn();
                Audio.PlaySoundStep('s');
            }

            if (e.Key == Key.Space)
            {
                Audio.PlaySoundStep('f');
                QuickDown();
            }

            if (e.Key == Key.Right && !isCollision()[1])
            {
                log.Move(fig.Figure, 'r');
                Audio.PlaySoundStep('s');
            }

            if (e.Key == Key.Left && !isCollision()[0])
            {
                log.Move(fig.Figure, 'l');
                Audio.PlaySoundStep('s');
            }

            if (e.Key == Key.Down)
            {
                if (IsDown())
                {
                    NewF();
                }
                else if (!checkIsDownFuture())
                {
                    MoveDown();
                }
                else
                {
                    MoveDown();
                    NewF();
                }
                Audio.PlaySoundStep('s');
            }

            if(e.Key == Key.Escape)
            {
                isContinue = false;

                timer.Stop();
                Audio.backGroundMediaPlayer.Pause();

                if(fig.placedRectangles.Count != 0)
                {
                    FileSystem.WritingSaveInFile(fig.placedRectangles, score, timeToFall, "Saves/Savings.txt");
                }

                Pause pause = new Pause(this);
                pause.ShowDialog();

                if(isContinue)
                {
                    timer.Start();
                    Audio.backGroundMediaPlayer.Play();
                }
            }
        }

        void MoveDown()
        {
            for (int i = 0; i < fig.Figure.Count; i++)
            {
                Thickness margin = fig.Figure[i].Margin;
                margin.Top += ProgenitorRectangle.squareSize;
                fig.Figure[i].Margin = margin;
            }
        }

        void QuickDown()
        {
            double[] pairs = new double[fig.Figure.Count];
            double currentMinMarg = 0;
            short ind = 0;

            foreach (var rect in fig.Figure)
            {
                currentMinMarg = 630 - rect.Margin.Top;

                foreach (var r in fig.placedRectangles)
                {
                    if (r.Margin.Left == rect.Margin.Left && r.Margin.Top > rect.Margin.Top)
                    {
                        if (currentMinMarg > r.Margin.Top - rect.Margin.Top)
                        {
                            currentMinMarg = r.Margin.Top - rect.Margin.Top;
                        }
                    }
                }
                pairs[ind] = currentMinMarg;
                ind++;
            }

            double distance = pairs.Min();

            foreach (var r in fig.Figure)
            {
                Thickness margin = r.Margin;
                margin.Top += distance - ProgenitorRectangle.squareSize;
                r.Margin = margin;
            }

            NewF();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsDown())
            {
                NewF();
            }
            else
            {
                MoveDown();
            }
        }

        private void SetupGame()
        {
            timer.Interval = TimeSpan.FromMilliseconds(timeToFall);
            timer.Tick += Timer_Tick;
            StartNewFigure();
        }

        void NewF()
        {
            timer.Stop();

            Color aqua = Color.FromRgb(0, 200, 200);
            SolidColorBrush color = new SolidColorBrush(aqua);

            foreach (var rect in fig.Figure)
            {
                rect.Effect = null;
                rect.Fill = color;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 0.1;
            }

            fig.placedRectangles.AddRange(fig.Figure);
            double[] topValues = fig.Figure.Select(rectangle => rectangle.Margin.Top).Distinct().ToArray();

            BreakLine(topValues);
            StartNewFigure();
        }

        private void StartNewFigure()
        {
            if (score - scoreToVel >= 0)
            {
                scoreToVel += 10000;

                if (timeToFall <= 400)
                {
                    timeToFall -= timeToFall / 3;
                    timer.Interval = TimeSpan.FromMilliseconds(timeToFall);
                }
                else
                {
                    timeToFall -= 70;
                    timer.Interval = TimeSpan.FromMilliseconds(timeToFall);
                }
            }

            ClearNextFigureView();

            fig.GetRandomFigure(fig.myDelegates());
            SetSecondColumn();
            GenNextFigureView();

            if (!IsLoseGame())
            {
                for (int i = 0; i < fig.Figure.Count; i++)
                {
                    gr.Children.Add(fig.Figure[i]);
                }
                timer.Start();
            }
            else
            {
                FinishGame();
            }
        }

        private bool IsDown()
        {
            foreach (Rectangle r in fig.Figure)
            {
                if (r.Margin.Top > 570)
                {
                    return true;
                }

                foreach (var placedRect in fig.placedRectangles)
                {
                    if (r.Margin.Left == placedRect.Margin.Left && r.Margin.Top + ProgenitorRectangle.squareSize == placedRect.Margin.Top)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private bool checkIsDownFuture()
        {
            foreach (Rectangle r in fig.Figure)
            {
                if (r.Margin.Top + ProgenitorRectangle.squareSize > 570)
                {
                    return true;
                }
            }
            return false;
        }

        private bool[] isCollision()
        {
            bool[] collisions = { false, false };

            foreach (Rectangle r in fig.Figure)
            {
                if (r.Margin.Left < 60)
                {
                    collisions[0] = true;
                }

                if (r.Margin.Left > 270)
                {
                    collisions[1] = true;
                }

                foreach (var placedRect in fig.placedRectangles)
                {
                    if (r.Margin.Top == placedRect.Margin.Top)
                    {
                        if (r.Margin.Left - ProgenitorRectangle.squareSize == placedRect.Margin.Left)
                            collisions[0] = true;
                        if (r.Margin.Left + ProgenitorRectangle.squareSize == placedRect.Margin.Left)
                            collisions[1] = true;
                    }
                }
            }
            return collisions;
        }

        public void GenStartFigures()
        {
            foreach (var r in fig.placedRectangles)
            {
                gr.Children.Add(r);
            }
        }

        public void GenNextFigureView()
        {
            foreach (var r in fig.NextFigure)
            {
                gr.Children.Add(r);
            }
        }

        public void ClearNextFigureView()
        {
            foreach (var r in fig.NextFigure)
            {
                gr.Children.Remove(r);
            }
        }

        public void SetSecondColumn()
        {
            foreach (var r in fig.NextFigure)
            {
                Grid.SetColumn(r, 2);
            }
        }

        public void BreakLine(double[] tops)
        {
            byte sum = 0;
            List<double> topToDelete = new List<double>();
            foreach (double top in tops)
            {
                Rectangle[] potentialSqr = fig.placedRectangles.Where(x => x.Margin.Top == top).ToArray();

                if (potentialSqr.Length == 10)
                {
                    sum++;
                    topToDelete.Add(top);
                }
            }
            topToDelete.Sort();

            foreach (var top in topToDelete)
            {
                DeleteAndDownRectangles(top);
            }
            Scoring(sum);
        }

        private void DeleteAndDownRectangles(double marginTop)
        {
            foreach (var r in fig.placedRectangles.Where(x => x.Margin.Top == marginTop).ToArray())
            {
                gr.Children.Remove(r);
                fig.placedRectangles.Remove(r);
            }

            foreach (var r in fig.placedRectangles.Where(x => x.Margin.Top < marginTop))
            {
                Thickness margin = r.Margin;
                margin.Top += ProgenitorRectangle.squareSize;
                r.Margin = margin;
            }
        }

        void Scoring(int sum)
        {
            switch (sum)
            {
                case 1:
                    score += 100;
                    break;
                case 2:
                    score += 300;
                    break;
                case 3:
                    score += 700;
                    break;
                case 4:
                    score += 1500;
                    break;
                case 5:
                    score += 3000;
                    break;
                default:
                    break;
            }

            _score.Content = $"Score: {score}";
        }

        private bool IsLoseGame()
        {
            foreach (var r in fig.placedRectangles)
            {
                foreach (var pr in fig.Figure)
                {
                    if (r.Margin == pr.Margin || r.Margin.Top < 30)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void FinishGame()
        {
            string filePath = "Saves/Savings.txt";
            StreamWriter writer = new StreamWriter(filePath);
            writer.Close();

            FileSystem.WritingRecordInFile(score, "Saves/Records.txt");

            Audio.backGroundMediaPlayer.Stop();

            MessageBoxResult m = MessageBox.Show("Not bad, you can try again", "End", MessageBoxButton.OK, MessageBoxImage.Information);
            if (m == MessageBoxResult.OK)
            {
                MainMenu main = new MainMenu();
                main.Show();
                Close();
            }
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            foreach (var rect in fig.placedRectangles)
            {
                gr.Children.Remove(rect);
            }
            fig.placedRectangles.Clear();

            foreach (var rect in fig.Figure)
            {
                gr.Children.Remove(rect);
            }
            fig.Figure.Clear();

            score = 0;
            _score.Content = $"Score: {score}";

            timeToFall = Settings.FallingTime;
            timer.Interval = TimeSpan.FromMilliseconds(timeToFall);

            fig.Index = 0;
            fig.LastThreeFigures = new int[] { 0, 0, 0 };
            fig.NextRandomIndex = -1;

            Audio.backGroundMediaPlayer.Stop();

            ClearNextFigureView();

            StartNewFigure();

            timer.Start();

            Audio.PlayMusic();
        }
    }
}

