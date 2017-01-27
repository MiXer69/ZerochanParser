using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZerochanParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance; 
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
        }

        public void SetWallpaper()
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Parser z = new Parser();
            if (nameBox.Text.Length != 0)
            {
                z.ParseAnimeNumber(nameBox.Text.Replace("!", "%21"));
            }
            if (CharBox.Text.Length != 0)
            {
                z.ParseAnimeNumber(nameBox.Text, CharBox.Text.Replace("!", "%21"));
            }
            if (nameBox.Text.Length != 0 && PageBox.Text.Length != 0)
            {
                z.ParseAnimeNumber(nameBox.Text.Replace("!", "%21"), int.Parse(PageBox.Text));
            }
            if (CharBox.Text.Length != 0 && PageBox.Text.Length != 0)
            {
                z.ParseAnimeNumber(nameBox.Text, CharBox.Text.Replace("!", "%21"), PageBox.Text);
            }
        }

    }
}
