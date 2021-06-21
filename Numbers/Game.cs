using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Numbers
{
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
        /// Пустой блок
        /// </summary>
        CoordBlock emptyBlock;

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

            GridInit();

            Button temp;
            for (int i = 0; i < rows; ++i)
            {
                numbers.Add(new List<Button>((int)columns));
                for (int j = 0; j < columns; ++j)
                {
                    // Инициализируем кнопку
                    temp = new Button();
                    temp.Click += Button_Click;
                    temp.Tag = new CoordBlock(i, j);

                    // Ставим элемент на его место
                    SetRowAndColumnForBlock(temp);

                    // Добавляем кнопку в коллекции
                    numbers[i].Add(temp);
                    gameFileld.Children.Add(temp);
                }
            }

            // Перемешиваем значения
            Mix();
        }

        /// <summary>
        /// Метод, инициализирующий строки и столбцы игрового поля
        /// </summary>
        private void GridInit()
        {
            // Задаем количество строк и столбцов в таблице
            for (int i = 0; i < rows; ++i)
                gameFileld.RowDefinitions.Add(new RowDefinition());
            for (int j = 0; j < columns; ++j)
                gameFileld.ColumnDefinitions.Add(new ColumnDefinition());
        }
        
        /// <summary>
        /// Метод устанавливает блок на его место в Grid
        /// </summary>
        private void SetRowAndColumnForBlock(Button block)
        {
            Grid.SetRow(block, ((CoordBlock)block.Tag).row);
            Grid.SetColumn(block, ((CoordBlock)block.Tag).column);
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
                    {
                        numbers[i][j].Content = "--";
                        emptyBlock = new CoordBlock(i, j);
                    }
                    else
                    {
                        // Выбираем случайный индекс. По этому индексу из списка possibleValues выбираем значение
                        randIndex = rand.Next(0, possibleValues.Count);
                        numbers[i][j].Content = possibleValues[randIndex];
                        possibleValues.RemoveAt(randIndex);
                    }

                    numbers[i][j].IsEnabled = false;
                }

            // Делаем доступными кнопки у пустой клетки
            numbers[rows - 1][columns - 2].IsEnabled = true;
            numbers[rows - 2][columns - 1].IsEnabled = true;
        }

        /// <summary>
        /// Метод, вызываемый при перемещении кости по игровому полю
        /// </summary>
        /// <param name="sender"> Объект, вызывающий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CoordBlock blockCoord = (CoordBlock)(sender as Button).Tag;

            // Меняем теги блоков
            numbers[emptyBlock.row][emptyBlock.column].Tag = blockCoord;
            numbers[blockCoord.row][blockCoord.column].Tag = emptyBlock;

            // Переставляем ячейки
            Button temp = numbers[blockCoord.row][blockCoord.column];
            numbers[blockCoord.row][blockCoord.column] = numbers[emptyBlock.row][emptyBlock.column];
            numbers[emptyBlock.row][emptyBlock.column] = temp;

            // Отображаем перестановку на Grid
            SetRowAndColumnForBlock(numbers[blockCoord.row][blockCoord.column]);
            SetRowAndColumnForBlock(numbers[emptyBlock.row][emptyBlock.column]);

            // Меняем координату пустого блока
            emptyBlock = blockCoord;

            // Увеличиваем счетчик ходов
            counter.Content = int.Parse(counter.Content.ToString()) + 1;
        }
    }
}
