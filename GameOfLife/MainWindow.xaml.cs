using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using GameOfLife.Library;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly Game game;
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
        private Rectangle[,] cells;
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

        public MainWindow()
        {
            this.InitializeComponent();
            this.game = new Game();

            this.timer.Tick += this.Timer_Tick;
        }

        public int Steps
        {
            get { return (int)this.GetValue(StepsProperty); }
            set { this.SetValue(StepsProperty, value); }
        }

        public static readonly DependencyProperty StepsProperty =
            DependencyProperty.Register("Steps",
                typeof(int),
                typeof(MainWindow),
                new PropertyMetadata(0));

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(this.WidthTextBox.Text, out int width))
            {
                return;
            }

            if (!int.TryParse(this.HeightTextBox.Text, out int height))
            {
                return;
            }

            this.InitializeBoard(new AreaSize(width, height));
        }

        private void InitializeBoard(AreaSize areaSize)
        {
            this.game.CreateBoard(areaSize);
            this.DrawInitialBoard();
            this.Steps = 0;
        }

        private void DrawInitialBoard()
        {
            this.BoardCanvas.Children.Clear();

            double canvasWidth = this.BoardCanvas.RenderSize.Width;
            double canvasHeight = this.BoardCanvas.RenderSize.Height;
            int width = this.game.Board.AreaSize.Width;
            int height = this.game.Board.AreaSize.Height;
            double cellWidth = canvasWidth / width;
            double cellHeight = canvasHeight / height;

            ////this.DrawGrid(canvasWidth, canvasHeight, width, height, cellWidth, cellHeight);
            this.DrawCells(width, height, cellWidth, cellHeight);
        }

        ////private void DrawGrid(double canvasWidth, double canvasHeight, int width, int height, double cellWidth, double cellHeight)
        ////{
        ////    for (int widthIndex = 1; widthIndex < width; widthIndex++)
        ////    {
        ////        this.DrawLine(cellWidth * widthIndex,
        ////            0,
        ////            cellWidth * widthIndex,
        ////            canvasHeight);
        ////    }

        ////    for (int heightIndex = 1; heightIndex < height; heightIndex++)
        ////    {
        ////        this.DrawLine(0,
        ////            cellHeight * heightIndex,
        ////            canvasWidth,
        ////            cellHeight * heightIndex);
        ////    }
        ////}

        ////private void DrawLine(double x1, double y1, double x2, double y2)
        ////{
        ////    var line = new Line()
        ////    {
        ////        X1 = x1,
        ////        Y1 = y1,
        ////        X2 = x2,
        ////        Y2 = y2,
        ////        Stroke = Brushes.Black,
        ////        StrokeThickness = 1,
        ////    };

        ////    this.BoardCanvas.Children.Add(line);
        ////}

        private void DrawCells(int width, int height, double cellWidth, double cellHeight)
        {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
            this.cells = new Rectangle[width, height];
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

            for (int widthIndex = 0; widthIndex < width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < height; heightIndex++)
                {
                    var cell = this.DrawCell(cellWidth * widthIndex,
                            cellHeight * heightIndex,
                            cellWidth,
                            cellHeight);

                    cell.Tag = (x: widthIndex, y: heightIndex);

                    this.cells[widthIndex, heightIndex] = cell;
                }
            }
        }

        private Rectangle DrawCell(double left, double top, double width, double height)
        {
            var cell = new Rectangle()
            {
                ////Width = width - 2 < 1 ? 1 : width - 2,
                ////Height = height - 2 < 1 ? 1 : height - 2,
                Width = width - 1 < 0 ? 0 : width - 1,
                Height = height - 1 < 0 ? 0 : height - 1,
                Fill = Brushes.YellowGreen,
                Stroke = Brushes.Black,
                StrokeThickness = 0,
            };

            Canvas.SetLeft(cell, left + 1);
            Canvas.SetTop(cell, top + 1);

            cell.MouseEnter += this.Cell_MouseEnter;
            cell.MouseLeave += this.Cell_MouseLeave;
            cell.MouseDown += this.Cell_MouseDown;

            this.BoardCanvas.Children.Add(cell);
            return cell;
        }

        private void Cell_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Rectangle).StrokeThickness = 1;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.FlipCell(sender as Rectangle);
            }
        }

        private void Cell_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Rectangle).StrokeThickness = 0;
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.FlipCell(sender as Rectangle);
        }

        private void FlipCell(Rectangle cell)
        {
            (int x, int y) = ((int, int))cell.Tag;
            DrawCellState(cell, this.game.Board.Flip(x, y));
        }

        private static void DrawCellState(Rectangle cell, bool alive)
        {
            cell.Fill = alive ? Brushes.Bisque : Brushes.YellowGreen;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
            var board = this.game.Reset();
            this.UpdateBoard(board);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NextStep();
        }

        private void NextStep()
        {
            var board = this.game.Step();
            this.UpdateBoard(board);
        }

        private void UpdateBoard(Board board)
        {
            for (int widthIndex = 0; widthIndex < board.AreaSize.Width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < board.AreaSize.Height; heightIndex++)
                {
                    var state = board.Columns[widthIndex][heightIndex];
                    var cell = this.cells[widthIndex, heightIndex];
                    DrawCellState(cell, state);
                }
            }

            this.Steps = this.game.Steps;
        }

        private void ImportComponent(Board component)
        {
            if (this.game.Board == null ||
                this.game.Board.AreaSize.Width < component.AreaSize.Width ||
                this.game.Board.AreaSize.Height < component.AreaSize.Height)
            {
                this.InitializeBoard(component.AreaSize);
            }

            var position = new AreaPosition((this.game.Board.AreaSize.Width - component.AreaSize.Width) / 2,
                (this.game.Board.AreaSize.Height - component.AreaSize.Height) / 2);

            var board = this.game.ImportComponent(position, component);
            this.UpdateBoard(board);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.NextStep();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.UpdateSpeed(Convert.ToInt32(this.SpeedSlider.Value));
        }

        private void UpdateSpeed(int milliseconds)
        {
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var importWindow = new ImportWindow();
            if (importWindow.ShowDialog().Value &&
                importWindow.Component != null)
            {
                this.ImportComponent(importWindow.Component);
            }
        }
    }
}
