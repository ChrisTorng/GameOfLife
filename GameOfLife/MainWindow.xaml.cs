﻿using System;
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

            this.InitializeBoard(width, height);
        }

        private void InitializeBoard(int width, int height)
        {
            this.game.CreateBoard(width, height);
            this.DrawInitialBoard();
        }

        private void DrawInitialBoard()
        {
            this.BoardCanvas.Children.Clear();

            double canvasWidth = this.BoardCanvas.RenderSize.Width;
            double canvasHeight = this.BoardCanvas.RenderSize.Height;
            int width = this.game.Board.Width;
            int height = this.game.Board.Height;
            double cellWidth = canvasWidth / width;
            double cellHeight = canvasHeight / height;

            this.DrawGrid(canvasWidth, canvasHeight, width, height, cellWidth, cellHeight);
            this.DrawCells(width, height, cellWidth, cellHeight);
        }

        private void DrawGrid(double canvasWidth, double canvasHeight, int width, int height, double cellWidth, double cellHeight)
        {
            for (int widthIndex = 1; widthIndex < width; widthIndex++)
            {
                this.DrawLine(cellWidth * widthIndex,
                    0,
                    cellWidth * widthIndex,
                    canvasHeight);
            }

            for (int heightIndex = 1; heightIndex < height; heightIndex++)
            {
                this.DrawLine(0,
                    cellHeight * heightIndex,
                    canvasWidth,
                    cellHeight * heightIndex);
            }
        }

        private void DrawLine(double x1, double y1, double x2, double y2)
        {
            var line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            this.BoardCanvas.Children.Add(line);
        }

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
                Width = width - 2 < 1 ? 1 : width - 2,
                Height = height - 2 < 1 ? 1 : height - 2,
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
            (int x, int y) = (ValueTuple<int, int>)cell.Tag;
            this.DrawCellState(cell, this.game.Board.Flip(x, y));
        }

        private void DrawCellState(Rectangle cell, bool alive)
        {
            cell.Fill = alive ? Brushes.Bisque : Brushes.YellowGreen;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
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
            for (int widthIndex = 0; widthIndex < board.Width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < board.Height; heightIndex++)
                {
                    var state = board.Columns[widthIndex][heightIndex];
                    var cell = this.cells[widthIndex, heightIndex];
                    this.DrawCellState(cell, state);
                }
            }
        }

        private void ImportComponent(Board component)
        {
            int widthOffset;
            int heightOffset;
            if (this.game.Board.Width < component.Width ||
                this.game.Board.Height < component.Height)
            {
                this.InitializeBoard(component.Width, component.Height);
            }

            widthOffset = (this.game.Board.Width - component.Width) / 2;
            heightOffset = (this.game.Board.Height - component.Height) / 2;

            this.game.ImportComponent(widthOffset, heightOffset, component);
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
