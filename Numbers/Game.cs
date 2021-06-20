using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Numbers
{
    /// <summary>
    /// Перечисление, задающее варианты игры
    /// </summary>
    public enum GameOptions
    {
        Version22 = 4,
        Version23 = 6,
        Version24 = 8,
        Version25 = 10,
        Version26 = 12,
        Version27 = 14,
        Version28 = 16,
        Version33 = 9,
        Version34 = 12,
        Version35 = 15,
        Version44 = 16,
        Version55 = 25
    }

    /// <summary>
    /// Класс, описывающий игру
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Структура, описывающая координату пустой клетки
        /// </summary>
        struct CoordBlock
        {
            /// <summary>
            /// Координата строки
            /// </summary>
            public int row;

            /// <summary>
            /// Координата столбца
            /// </summary>
            public int column;

            /// <summary>
            /// Конструктор структуры
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            public CoordBlock(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
        }

        /// <summary>
        /// Список кнопок с числами, которые игрок должен расставить в правильном порядке
        /// </summary>
        private List<List<Button>> numbers;

        /// <summary>
        /// Координата пустой клетки
        /// </summary>
        CoordBlock coordEmptyBlock;

        /// <summary>
        /// Число строк в матрице
        /// </summary>
        int rows;

        /// <summary>
        /// Число столбцов
        /// </summary>
        int columns;

        /// <summary>
        /// Поле, содержащее количество ходов
        /// </summary>
        private Label counter;

        /// <summary>
        /// Поле, в котором распалагаются кнопки
        /// </summary>
        private Grid gameFileld;

        /// <summary>
        /// Конструктор класса Game. Инициализирует вариант игры
        /// </summary>
        /// <param name="counter"> Поле, содержащее количество ходов </param>
        /// <param name="gameFileld"> Поле, в котором располагаются кнопки </param>
        /// <param name="rows"> Количество строк в матрице </param>
        /// <param name="columns"> Количество столбцов в матрице </param>
        public Game(Label counter, Grid gameFileld, int rows, int columns)
        {
            numbers = new List<List<Button>>((int)rows);
            this.gameFileld = gameFileld;
            this.counter = counter;
            this.rows = rows;
            this.columns = columns;

            // Задаем количество строк и столбцов в таблице
            for (int i = 0; i < rows; ++i)
                gameFileld.RowDefinitions.Add(new RowDefinition());
            for (int j = 0; j < columns; ++j)        
                gameFileld.ColumnDefinitions.Add(new ColumnDefinition());

            Button temp;
            for (int i = 0; i < rows; ++i)
            {
                numbers.Add(new List<Button>((int)columns));
                for (int j = 0; j < columns; ++j)
                {
                    if ((i == rows - 1) && (j == columns - 1))
                        continue;
                    
                    // Инициализируем кнопку
                    temp = new Button();
                    temp.Click += Button_Click;
                    temp.IsEnabled = false;
                    temp.Tag = new CoordBlock(i, j);

                    // Добавляем кнопку в коллекции
                    numbers[i].Add(temp);
                    this.gameFileld.Children.Add(temp);
                }
            }

            // Перемешиваем значения
            Mix();
        }

        /// <summary>
        /// Метод, перемешивающий кости на поле
        /// </summary>
        public void Mix()
        {
            Random rand = new Random();
            int randIndex;

            // Записываем все возможные значения
            List<int> possibleValues = new List<int>(rows * columns - 1);
            for (int i = 1; i < rows * columns; ++i)
                possibleValues.Add(i);

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                {
                    if ((i == rows - 1) && (j == columns - 1))
                        continue;

                    // Ставим элемент на его место
                    Grid.SetRow(numbers[i][j], i);
                    Grid.SetColumn(numbers[i][j], j);

                    // Выбираем случайный индекс. По этому индексу из списка possibleValues выбираем значение
                    randIndex = rand.Next(0, possibleValues.Count);
                    numbers[i][j].Content = possibleValues[randIndex];
                    possibleValues.RemoveAt(randIndex);
                }

            // Делаем доступными кнопки у пустой клетки
            numbers[rows - 1][columns - 2].IsEnabled = true;
            numbers[rows - 2][columns - 1].IsEnabled = true;

            // Устанавливаем координаты путой клетки
            coordEmptyBlock.row = rows - 1;
            coordEmptyBlock.column = columns - 1;
        }

        /// <summary>
        /// Метод, вызываемый при перемещении кости по игровому полю
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button block = sender as Button;

            // Переставляем элемент на его место
            Grid.SetRow(numbers[((CoordBlock)block.Tag).row][((CoordBlock)block.Tag).column], ((CoordBlock)block.Tag).row);
            Grid.SetColumn(numbers[((CoordBlock)block.Tag).row][((CoordBlock)block.Tag).column], ((CoordBlock)block.Tag).column);

            // Меняем координаты блоков
            CoordBlock temp = coordEmptyBlock;
            coordEmptyBlock = (CoordBlock)block.Tag;
            block.Tag = temp;

            // Увеличиваем счетчик ходов
            counter.Content = int.Parse(counter.Content.ToString()) + 1;
        }
    }
}
