using System;
using System.IO;
using System.Windows;
using GameOfLife.Library;
using Microsoft.Win32;

namespace GameOfLife
{
    public partial class ImportWindow : Window
    {
        public Board Component { get; private set; }

        public ImportWindow()
        {
            this.InitializeComponent();
            this.Owner = Application.Current.MainWindow;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var patternsPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Patterns");
            var fileDialog = new OpenFileDialog()
            {
                Filter = "Plaintext (*.cells)|*.cells",
                InitialDirectory = patternsPath,
            };

            if (fileDialog.ShowDialog().Value)
            {
                this.ContentTextBox.Text = File.ReadAllText(fileDialog.FileName);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Component = new PlaintextBoardReader(new FileReader())
                .GetBoardByContent(this.ContentTextBox.Text);

            this.DialogResult = true;
            this.Close();
        }
    }
}
