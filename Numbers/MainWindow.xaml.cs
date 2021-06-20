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
        /// Вариант игры, выбранный пользователем
        /// </summary>
        private GameOptions gameOptions;

        /// <summary>
        /// Окно, в котором пользователь выбирает вариант игры
        /// </summary>
        private Choose choose;

        /// <summary>
        /// Делегат, вызываемый, когда пользователь выбирает вариант игры
        /// </summary>
        private Action<int, int> action;

        /// <summary>
        /// Конструктор класса MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            action = new Action<int, int>(OptionChosen);

            choose = new Choose(action);
            choose.ShowDialog();
        }

        /// <summary>
        /// Метод, задающий вариант игры
        /// </summary>
        /// <param name="rows"> Количество строк </param>
        /// <param name="columns"> Количество столбцов </param>
        private void OptionChosen(int rows, int columns)
        {
            choose.Close();
            game = new Game(counter, playingField, rows, columns);
        }

        /// <summary>
        /// Метод, вызываемый при нажатии на кнопку перемешивания
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void mixButtonClick(object sender, RoutedEventArgs e) => game.Mix();
    }
}
