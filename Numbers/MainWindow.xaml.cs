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

namespace Numbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект игры
        /// </summary>
        private Game game;

        /// <summary>
        /// Окно, в котором пользователь выбирает вариант игры
        /// </summary>
        Choose choose;

        public MainWindow()
        {
            InitializeComponent();

            choose = new Choose();
            choose.Owner = this;
            choose.ShowDialog();

            game = new Game(, playingField);
        }
    }
}
