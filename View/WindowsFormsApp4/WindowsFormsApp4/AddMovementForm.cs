using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ClassLibrary3;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    /// <summary>
    /// Класс формы создания и добавления движения
    /// </summary>
    public partial class AddMovementForm : Form
    {
        /// <summary>
        /// Экземляр класса MovementBase для вытягивания выбора с родительской формы
        /// </summary>
        public MovementBase Movement { get; private set; }

        /// <summary>
        /// Типы движения
        /// </summary>
        public enum MovementsType
        {
            UniformMotion,
            UniformlyAcceleratedMotion,
            OscillatoryMotion
        }

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public AddMovementForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Ок"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    throw new Exception("Не выбран тип движения");
                }

                foreach (Control ctrl in groupBox1.Controls)
                {
                    if (ctrl.GetType() == typeof(Label))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(ctrl.Text))
                    {
                        throw new Exception("Присутствуют незаполненные строки");
                    }
                    else 
                        if (!CorrectValue.CheckParameterString(ctrl.Text))
                        {
                            throw new Exception("Неверный формат введеных данных");
                        }

                }
                FillModelParametrs();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.InnerException is null 
                        ? exception.Message 
                        : exception.InnerException.Message,
                    "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Обработчик выбора значения в comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MovementsType selectTypeMovement = (MovementsType)(sender as ComboBox).SelectedIndex;

            switch (selectTypeMovement)
            {
                case MovementsType.UniformMotion:
                    Movement = new UniformMovement();       
                    break;
                case MovementsType.UniformlyAcceleratedMotion:
                    Movement = new UniformlyAcceleratedMotion();
                    break;
                case MovementsType.OscillatoryMotion:
                    Movement = new OscillatoryMotion();
                    break;
                default: 
                    throw new ArgumentOutOfRangeException("Неизвестный вид движения");
            }
            ConstrolsRemove();
            CreateFields(Movement.GetType().GetProperties());
        }

        /// <summary>
        /// Функция создания полей для введения параметров
        /// </summary>
        /// <param name="properties"> Перечень параметров, характеризующих 
        /// выбранный тип движения</param>
        private void CreateFields(PropertyInfo[] properties)
        {
            int controlY = 65;
            const int stepByY = 43;
            
            foreach (PropertyInfo field in properties)
            {
                if (field.Name != nameof(MovementBase.Time) &&
                    field.Name != nameof(MovementBase.TypeMovement))
                {
                    CreateLabels(field, controlY);
                    CreateEdits(field, controlY);
                    controlY += stepByY;
                }
            }
        }

        /// <summary>
        /// Функция создания Label для выведения наименования параметра 
        /// </summary>
        /// <param name="field"> Параметр, для которого создается Label</param>
        /// <param name="y"> Координата "y" для размещения Label</param>
        private void CreateLabels(PropertyInfo field, int y)
        {
            Label label = new Label();
            switch(field.Name)
            {
                case nameof(MovementBase.StartPosition):
                    label.Text = "Начальное положение, м";
                    break;
                case nameof(UniformMovement.Speed):
                    label.Text = "Скорсоть, м/с";
                    break;
                case nameof(UniformlyAcceleratedMotion.Acceleration):
                    label.Text = "Ускороение, м/с^2";
                    break;
                case nameof(OscillatoryMotion.Amplitude):
                    label.Text = "Амплитуда, м";
                    break;
                case nameof(OscillatoryMotion.CyclicFrequency):
                    label.Text = "Циклическая частота, рад/с";
                    break;
                default:
                    throw new IndexOutOfRangeException("Неизвестное имя свойства");
            }
            label.Name = $"{field.Name}Label";

            label.AutoSize = true;
            label.Location = new Point(6, y);
            groupBox1.Controls.Add(label);
        }

        /// <summary>
        /// Функция создания TextBox для введения значения параметра 
        /// </summary>
        /// <param name="field"> Параметр, для которого создается TextBox</param>
        /// <param name="y"> Координата "y" для размещения TextBox</param>
        private void CreateEdits(PropertyInfo field, int y)
        {
            TextBox edit = new TextBox();
            edit.Name = $"{field.Name}TextBox";
            edit.Location = new Point(170, y - 2);
            edit.Size = new Size(100, 20);
            groupBox1.Controls.Add(edit);
        }

        /// <summary>
        /// Процедура удаления элементов из groupBox1
        /// </summary>
        private void ConstrolsRemove()
        {
            for (int i = groupBox1.Controls.Count - 1; i > 1; i--)
            {
                groupBox1.Controls.Remove(groupBox1.Controls[i]);
            }
        }

        /// <summary>
        /// Процедура считывания параметров движения
        /// </summary>
        private void FillModelParametrs()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is Label)
                {
                    continue;
                }
                if (control.Text.Contains('.'))
                {
                    control.Text = control.Text.Replace(".", ",");
                }
                string parametersName = control.Name.Replace("TextBox","");
                //TODO: RSDN | +
                var propertyInfo = Movement.GetType().GetProperty(parametersName);
                propertyInfo.SetValue(Movement, Convert.ChangeType(control.Text,
                                                    propertyInfo.PropertyType));
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки для создания случайного движения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int selectTypeMovement = rand.Next(0, comboBox1.Items.Count);
            comboBox1.Text = comboBox1.Items[selectTypeMovement].ToString();
            
            List<string> parametersThatMustBeGreaterThanZero = new List<string>
            {
                nameof(MovementBase.Time),
                nameof(OscillatoryMotion.Amplitude),
                nameof(OscillatoryMotion.CyclicFrequency)
            };

            foreach (Control control in groupBox1.Controls)
            {
                if (control is Label)
                {
                    continue;
                }

                const int minRandomValue = -100;
                const int maxRadomValue = 100;

                if (parametersThatMustBeGreaterThanZero.Any((word) => control.Name.Contains(word)))
                {
                    control.Text = rand.Next(1, maxRadomValue).ToString();
                    continue;
                }
                control.Text = rand.Next(minRandomValue, maxRadomValue).ToString();
            }
        }
    }
}
