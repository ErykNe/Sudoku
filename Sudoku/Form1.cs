using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace Sudoku
{
    public partial class Form1 : Form
    {
        public int[,] d = new int[9, 9];
        public bool[,] isStartingCell = new bool[9, 9];
        public bool isPlayerAWinner = false;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void WstawLiczby(int[,] d, Random rand)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> shift = new List<int>(numbers);
            ShuffleList(shift, rand);
            for (int j = 0; j < 9; j++)
            {
                d[0, j] = shift[j];
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    d[i, j] = d[0, (j + i * 3) % 9];
                }
            }
        }
        private void ShuffleList<T>(List<T> list, Random rand)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            button2.Enabled = true;
            isPlayerAWinner = false;
            label1.Text = "";
            d = new int[9,9];
            isStartingCell = new bool[9, 9];
            int h = d.GetLength(0);
            int w = d.GetLength(1);
            int cellSize = 83;
            Random rand = new Random();
            dataGridView1.ColumnCount = w;
            dataGridView1.RowCount = h;
            dataGridView1.Width = cellSize * w + 3;
            dataGridView1.Height = cellSize * h + 3;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 25);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = cellSize;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = cellSize;
            }


            for (int j = 0; j < h; j++)
            {
                for (int k = 0; k < w; k++)
                {
                    dataGridView1[k, j].Value = null;
                    dataGridView1[k, j].Style.BackColor = Color.White;   
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((i + 1) % 3 == 0 && i < dataGridView1.Rows.Count - 1)
                {
                    dataGridView1.Rows[i].DividerHeight = 3;
                }
            }

            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                if ((j + 1) % 3 == 0 && j < dataGridView1.Columns.Count - 1)
                {
                    dataGridView1.Columns[j].DividerWidth = 3;
                }
            }


            WstawLiczby(d, rand);

            Random random = new Random();
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    for (int n = 0; n < 5; n++)
                    {
                        int randomRow = random.Next(i, i + 3);
                        int randomColumn = random.Next(j, j + 3);
                        dataGridView1.Rows[randomRow].Cells[randomColumn].Value = d[randomRow, randomColumn];
                        dataGridView1.Rows[randomRow].Cells[randomColumn].Style.BackColor = Color.LemonChiffon;
                        isStartingCell[randomColumn, randomRow] = true;
                    }
                }
            }
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }
        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public DataGridViewCell currentCell;
        private void dataGridView1_CellFocus(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = dataGridView1.CurrentCell;
            currentCell.Style.BackColor = Color.LemonChiffon;
            if (currentCell.Value == null)
            {
                currentCell.Style.BackColor = Color.White;
            }
        }
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (currentCell.Value == null)
            {
                currentCell.Style.BackColor = Color.White;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < d.GetLength(0); i++) 
            {
                for (int j = 0; j < d.GetLength(1); j++)
                {
                    if (isStartingCell[j, i] == true)
                    {
                        continue;
                    }
                    if (d[i,j] == Convert.ToInt32(dataGridView1[j, i].Value))
                    {
                        isPlayerAWinner = true;
                        dataGridView1[j, i].Style.BackColor = Color.Green;
                    } else
                    {
                        isPlayerAWinner = false;
                        dataGridView1[j, i].Value = d[i, j];
                        dataGridView1[j, i].Style.BackColor = Color.Red;
                    }
                }
            }
            dataGridView1.Enabled = false;
            dataGridView1.CurrentCell = null;
            if (isPlayerAWinner)
            {
                label1.Text = "You Won!";
            }
            button2.Enabled = false;
        }

        private void dataGridView1_CellFocus(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
