using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp4
{
    /// <summary>
    /// Класс формы для задания параметров поиска
    /// </summary>
    public partial class SearchForm : Form
    {
        /// <summary>
        /// Регулярка для проверки правильности введеных параметров
        /// </summary>
        private const string CorrectParametersValue = @"(^(-)?([0-9]+)(,|.)?([0-9])+$)|(^(-)?([0-9])+$)";

        /// <summary>
        /// Коллекция для вытягивания выбора с родительской формы
        /// </summary>
        public (double Coordinate, double Time) ParametrsMovement;

        /// <summary>
        /// Конструктор формы для задания параметров поиска
        /// </summary>
        public SearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Ок"
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            try 
            {
                if (string.IsNullOrEmpty(coordinateTextBox.Text) &&
                   string.IsNullOrEmpty(timeTextBox.Text))
                {
                    throw new Exception("Параметры поиска не могут быть пустыми.");
                }
                Regex regex = new Regex(CorrectParametersValue);
                if (!regex.IsMatch(coordinateTextBox.Text) ||
                     !regex.IsMatch(timeTextBox.Text))
                {
                    throw new Exception("Параметры поиска должны быть числами.");
                }
                if (!double.TryParse(coordinateTextBox.Text.Replace(".", ","), out ParametrsMovement.Coordinate))
                {
                    throw new Exception("Не удалось считать координату");
                }
                if (!double.TryParse(timeTextBox.Text.Replace(".", ","), out ParametrsMovement.Time))
                {
                    throw new Exception("Не удалось считать время");
                }
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
}
