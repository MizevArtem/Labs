﻿using System;
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
                if (!CorrectValue.CheckParameterString(coordinateTextBox.Text) ||
                     !CorrectValue.CheckParameterString(timeTextBox.Text))
                {
                    throw new Exception("Параметры поиска должны быть числами.");
                }//TODO: RSDN | +
                if (!double.TryParse(coordinateTextBox.Text.Replace(".", ","), 
                                            out ParametrsMovement.Coordinate))
                {
                    throw new Exception("Не удалось считать координату");
                }
                if (!double.TryParse(timeTextBox.Text.Replace(".", ","), 
                                            out ParametrsMovement.Time))
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
