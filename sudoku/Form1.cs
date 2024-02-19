using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool czyjestjakieszero(int[,] tab, int h, int w)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (tab[i, j] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool czysiezgadza(int[,] d, int h, int w)
        {
            return true;
        }
        private void wstawliczby(int[,] d, int h, int w, Random rand)
        {
            
            int a = 1;

            do
            {
                int kordy = rand.Next(0, h);
                int kordy2 = rand.Next(0, w);

                if (d[kordy, kordy2] == 0)
                {
                    d[kordy, kordy2] = a;
                    a++;
                    if (a > 9)
                    {
                        a = 1;
                    }
                }
            } while (czyjestjakieszero(d, h, w));

            if(!czysiezgadza(d,h, w))
            {
                d = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
                wstawliczby(d, h, w, rand);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int[,] d = new int[,] { { 0, 0, 0, 0, 0, 0,0,0,0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0  }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            int h = d.GetLength(0);
            int w = d.GetLength(1);
            int cellSize = 100 / Convert.ToInt32(button1.Text);
            Random rand = new Random();

            this.dataGridView1.ColumnCount = w;
            this.dataGridView1.RowCount = h;
            this.dataGridView1.Width = cellSize * w + 3;
            this.dataGridView1.Height = cellSize * h + 3;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = cellSize;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = cellSize;
            }
          

            for(int j = 0; j < h; j++)
                {
                for (int k = 0; k < w; k++)
                {
                    dataGridView1[k, j].Value = null;
                }
            }

    
                wstawliczby(d, h, w, rand);
                for (int j = 0; j < h; j++)
                {
                    for (int k = 0; k < w; k++)
                    {
                        string currentCellValue = dataGridView1[k, j].Value != null ? dataGridView1[k, j].Value.ToString() : "";
                        string newValue = currentCellValue + (currentCellValue != "" ? ", " : "") + d[j, k].ToString();
                        dataGridView1[k, j].Value = newValue;
                    }
                }
                d = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}