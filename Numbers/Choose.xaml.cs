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
using System.Windows.Shapes;

namespace Numbers
{
    /// <summary>
    /// Логика взаимодействия для Choose.xaml
    /// </summary>
    public partial class Choose : Window
    {
        /// <summary>
        /// Событие, возникающее, когда выбрали определеную кнопку
        /// </summary>
        private event Action<int, int> ButtonChosen;

        /// <summary>
        /// Конструктор класса Choose
        /// </summary>
        /// <param name="func"> Делегат, вызываемый в случае выбора варианта игры </param>
        public Choose(Action<int, int> func)
        {
            InitializeComponent();
            ButtonChosen += func;
        }

        /// <summary>
        /// Метод, вызываемый при срабатывании события Click
        /// </summary>
        /// <param name="sender"> Объект, вызвавщий метод </param>
        /// <param name="e"> Дополнительная информация </param>
        public void ChooseButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            switch (button.Name)
            {
                case ("Version22"):
                    ButtonChosen.Invoke(2, 2);
                    break;
                case ("Version23"):
                    ButtonChosen.Invoke(2, 3);
                    break;
                case ("Version24"):
                    ButtonChosen.Invoke(2, 4);
                    break;
                case ("Version25"):
                    ButtonChosen.Invoke(2, 5);
                    break;
                case ("Version26"):
                    ButtonChosen.Invoke(2, 6);
                    break;
                case ("Version27"):
                    ButtonChosen.Invoke(2, 7);
                    break;
                case ("Version28"):
                    ButtonChosen.Invoke(2, 8);
                    break;
                case ("Version33"):
                    ButtonChosen.Invoke(3, 3);
                    break;
                case ("Version34"):
                    ButtonChosen.Invoke(3, 4);
                    break;
                case ("Version35"):
                    ButtonChosen.Invoke(3, 5);
                    break;
                case ("Version44"):
                    ButtonChosen.Invoke(4, 4);
                    break;
                case ("Version55"):
                    ButtonChosen.Invoke(5, 5);
                    break;
            }
        }
    }
}
