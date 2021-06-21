using System;
using System.Windows;

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
            _ = choose.ShowDialog();
            if (choose != null)
                Close();
        }

        /// <summary>
        /// Метод, задающий вариант игры
        /// </summary>
        /// <param name="rows"> Количество строк </param>
        /// <param name="columns"> Количество столбцов </param>
        private void OptionChosen(int rows, int columns)
        {
            choose.Close();
            choose = null;
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
