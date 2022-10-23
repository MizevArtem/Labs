using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using ClassLibrary3;

namespace WindowsFormsApp4
{
    public partial class MainForm : Form
    {
        private BindingList<MovementBase> MovementList = new BindingList<MovementBase>();

        private BindingList<MovementBase> FiltredMovementList = new BindingList<MovementBase>();

        private bool ActiveSearch = false;
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = MovementList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            CreatingDeletionList();
        }

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

        private void DeleteElements(List<int> deletedIndexs)
        {
            foreach (int index in deletedIndexs)
            {
                MovementList.RemoveAt(index);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0 && !ActiveSearch)
            {
                MessageBox.Show("Поиск не возможен - отсутствет движение.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ActiveSearch)
            {
                dataGridView1.DataSource = MovementList;
                FiltredMovementList.Clear();
                (sender as Button).Text = "Поиск";
            }
            else
            {
                SearchForm form = new SearchForm();
                form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;
                foreach (var movement in MovementList)
                {
                    if (movement.Time == form.ParametrsMovement.Time
                        && movement.StartPosition == form.ParametrsMovement.Coordinate)
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
            }
            ActiveSearch = !ActiveSearch;

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddMovementForm form = new AddMovementForm();
            form.ShowDialog();
            if (form.DialogResult != DialogResult.OK)
                return;
            //TODO: databinding | Исправлено
            MovementList.Add(form.Movement);
            form.Dispose();
            ReIndex();
        }

        private void ReIndex()
        {
            int countRow = dataGridView1.RowCount;
            for (int i = 0; i < countRow; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
            }
            CreatingDeletionList();
        }
    }
}
