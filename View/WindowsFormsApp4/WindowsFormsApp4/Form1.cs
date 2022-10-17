using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClassLibrary3;

namespace WindowsFormsApp4
{
    public partial class MainForm : Form
    {

        private List<MovementBase> MovementList = new List<MovementBase>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddMovementForm form = new AddMovementForm();
            form.ShowDialog();
            if (form.DialogResult != DialogResult.OK)
                return;
            AddMovement(form.Movement);
            form.Dispose();
        }

        private void AddMovement(MovementBase movement)
        {
            MovementList.Add(movement);
            dataGridView1.Rows.Add();
            int countRow = dataGridView1.RowCount;
            DataGridViewRow newRow = dataGridView1.Rows[countRow - 1];
            newRow.Cells[0].Value = countRow;
            string nameOfMovement;
            switch(movement.GetType().Name)
            {
                case "UniformMovement":
                    nameOfMovement = "Равномерное движение";
                    break;
                case "UniformlyAcceleratedMotion":
                    nameOfMovement = "Равноускоренное движение";
                    break;
                case "OscillatoryMotion":
                    nameOfMovement = "Колебательное движение";
                    break;
                default:
                    nameOfMovement = "Неизвестный вид движения";
                    break;
            }
            newRow.Cells[1].Value = nameOfMovement;
            newRow.Cells[2].Value = movement.Time;
            newRow.Cells[3].Value = movement.PositionCalculation();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
