using System.Windows;
using System.Windows.Controls;

namespace Convert_Translit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Text_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text_Converted.Text = Text_Input.Text.ConvertText(Text_Converted.Text);
        }
    }
}
