using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using ClassLibrary3;
using System.IO;

namespace WindowsFormsApp4
{
    /// <summary>
    /// Класс главной формы
    /// </summary>
    public partial class MainForm : Form
    {
        //TODO: RSDN
        /// <summary>
        /// Лист для экземплятор класса MovementBase для отображения в DataGridView
        /// </summary>
        private BindingList<MovementBase> MovementList = new BindingList<MovementBase>();

        //TODO: RSDN
        /// <summary>
        /// Лист для экземплятор класса MovementBase для отображения результатов поиска в DataGridView
        /// </summary>
        private BindingList<MovementBase> FiltredMovementList = new BindingList<MovementBase>();

        //TODO: RSDN
        /// <summary>
        /// Переменная для хранения состояния осуществления поиска
        /// </summary>
        private bool ActiveSearch = false;

        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = MovementList;
        }

        //TODO: RSDN
        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            CreatingDeletionList();
        }

        /// <summary>
        /// Процедура формирования списка индексов строк(элементов листа),
        /// которые необходимо удалить
        /// </summary>
        private void CreatingDeletionList()
        {
            List<int> deletedRows = new List<int>();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    deletedRows.Add(row.Index);
                }
            }
            else
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    deletedRows.Add(cell.RowIndex);
                }
            }
            deletedRows.Sort((index1, index2) => index2.CompareTo(index1));
            DeleteElements(deletedRows.Distinct().ToList());
            ReIndex();
        }

        /// <summary>
        /// Процедура удаления элементов из листа
        /// </summary>
        /// <param name="deletedIndexs">Индексы удаляемых элементов</param>
        private void DeleteElements(List<int> deletedIndexs)
        {
            foreach (int index in deletedIndexs)
            {
                MovementList.RemoveAt(index);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Поиск"
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0 && !ActiveSearch)
            {
                MessageBox.Show("Поиск не возможен - отсутствет движение.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ActiveSearch)
            {
                ResetDataGrid();
            }
            else
            {
                SearchForm form = new SearchForm();
                form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;
                foreach (var movement in MovementList)
                {
                    var tmpMovement = form.ParametrsMovement;
                    if (movement.Time == tmpMovement.Time
                        && movement.StartPosition == tmpMovement.Coordinate)
                    {
                        FiltredMovementList.Add(movement);
                    }
                }
                if (FiltredMovementList.Count != 0)
                {
                    dataGridView1.DataSource = FiltredMovementList;
                    (sender as Button).Text = "Сброс";
                }
                else
                {
                    MessageBox.Show("Движение с указанными параметрами отсутствует.", "Уведомление");
                    return;
                }
                ActiveSearch = true;
            }
            ReIndex();
        }

        /// <summary>
        /// Процедура восстановления содержимого DataGridView из основного листа
        /// </summary>
        private void ResetDataGrid()
        {
            dataGridView1.DataSource = MovementList;
            FiltredMovementList.Clear();
            searchButton.Text = "Поиск";
            ActiveSearch = false;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void addButton_Click(object sender, EventArgs e)
        {
            AddMovementForm form = new AddMovementForm();
            form.ShowDialog();

            if (form.DialogResult != DialogResult.OK) return;

            MovementList.Add(form.Movement);
            form.Dispose();
            ReIndex();
        }

        /// <summary>
        /// Процедура обновления номурации строк DataGridView
        /// </summary>
        private void ReIndex()
        {
            int countRow = dataGridView1.RowCount;
            for (int i = 0; i < countRow; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить", выделающий все строки
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
            }
            CreatingDeletionList();
        }

        /// <summary>
        /// Сохранение списка движений
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                //TODO: RSDN
                var serializer = new XmlSerializer(typeof(BindingList<MovementBase>));
                using (FileStream fs = new FileStream(saveFileDialog.FileName + ".xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, dataGridView1.DataSource);
                }
            }
        }

        /// <summary>
        /// Загрузка списка движений
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                try 
                {
                    var serializer = new XmlSerializer(typeof(BindingList<MovementBase>));
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        MovementList =  (BindingList<MovementBase>) serializer.Deserialize(fs);
                    }
                }
                catch (Exception)
                {
                    //TODO: RSDN
                    MessageBox.Show("Не удалось открыть выбранный файл. Проверьте тот ли файл вы пытаетесь открыть",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            ResetDataGrid();
            ReIndex();
        }

    }
}
