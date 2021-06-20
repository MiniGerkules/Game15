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
        /// Список кнопок с числами, которые игрок должен расставить в правильном порядке
        /// </summary>
        private List<List<Button>> numbers;

        int rows;

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

                    // Добавляем кнопку в коллекции
                    numbers[i].Add(temp);
                    this.gameFileld.Children.Add(temp);
                }
            }

            // Делаем доступными кнопки у отверстия
            numbers[rows - 1][columns - 2].IsEnabled = true;
            numbers[rows - 2][columns - 1].IsEnabled = true;

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
        }

        /// <summary>
        /// Метод, вызываемый при перемещении кости по игровому полю
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Переставляем кости
            

            // Увеличиваем счетчик ходов
            counter.Content = int.Parse(counter.Content.ToString()) + 1;
        }
    }
}
