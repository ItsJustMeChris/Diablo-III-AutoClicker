using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Diablo3Auto_Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Clicker c = new Clicker(4);

        public MainWindow()
        {
            InitializeComponent();
            c.clickKeys();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            int i;

            if (textBox.Name == "one")
            {
                int.TryParse(textBox.Text, out i);
                c.setKeyDelay(1, i);
            }
            else if (textBox.Name == "two")
            {
                int.TryParse(textBox.Text, out i);
                c.setKeyDelay(2, i);
            }
            else if (textBox.Name == "three")
            {
                int.TryParse(textBox.Text, out i);
                c.setKeyDelay(3, i);
            }
            else if (textBox.Name == "four")
            {
                int.TryParse(textBox.Text, out i);
                c.setKeyDelay(4, i);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button.Background == Brushes.DarkRed)
            {
                button.Background = Brushes.DarkGreen;
                c.toggleKey(int.Parse(button.Content.ToString()));
            }
            else
            {
                button.Background = Brushes.DarkRed;
                c.toggleKey(int.Parse(button.Content.ToString()));
            }
        }

    }
}
