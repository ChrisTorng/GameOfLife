using System.Windows;
using GameOfLife.Library;

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
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Component = new PlaintextBoardReader().GetBoardByContent(this.ContentTextBox.Text);

            this.DialogResult = true;
            this.Close();
        }
    }
}
