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
    enum GameOptions
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
    class Game
    {
        /// <summary>
        /// Список кнопок с числами, которые игрок должен расставить в правильном порядке
        /// </summary>
        private List<Button> numbers;

        /// <summary>
        /// Вариант игры, выбранный пользователем
        /// </summary>
        private GameOptions gameOption;

        /// <summary>
        /// Поле, в котором распалагаются кнопки
        /// </summary>
        private UniformGrid gamefileld;

        /// <summary>
        /// Конструктор класса Game. Инициализирует вариант игры
        /// </summary>
        /// <param name="gameOption"> Вариант игры </param>
        /// <param name="gamefileld"> Поле, в котором располагаются кнопки </param>
        public Game(GameOptions gameOption, UniformGrid gamefileld)
        {
            this.gameOption = gameOption;
            numbers = new List<Button>((int)gameOption - 1);
            this.gamefileld = gamefileld;

            Button temp;
            for (int i = 0; i < (int)gameOption - 1; ++i)
            {
                temp = new Button();
                temp.Content = (i + 1).ToString();
                temp.Click += Button_Click;
                numbers.Add(new Button());
                this.gamefileld.Children.Add(temp);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
