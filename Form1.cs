using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudiaMCK2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            UpdateTable(tableLayoutPanel1, 3, 3, true);
            UpdateTable(tableLayoutPanel2, 3, 3, true);
            UpdateTable(tableLayoutPanel3, 3, 3, true);
        }

        private void UpdateTable(TableLayoutPanel tableLayoutPanel, int columns, int rows, bool init = false)
        {
            //int prev_columns = tableLayoutPanel.ColumnCount;
            //int prev_rows = tableLayoutPanel.RowCount;
            tableLayoutPanel.SuspendLayout();
            tableLayoutPanel.ColumnCount = columns;
            tableLayoutPanel.RowCount = rows;
            //int to_add = columns * rows - prev_columns * prev_rows;
            int to_add = columns * rows;
            if (init)
            {
                tableLayoutPanel.ColumnStyles.Clear();
                tableLayoutPanel.RowStyles.Clear();
            }
            // styling columns for equal sell width
            for (int i = 0; i < columns - tableLayoutPanel.ColumnStyles.Count; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }
            // styling rows for equal cell height
            for (int i = 0; i < rows - tableLayoutPanel.RowStyles.Count; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            // re-adding cells
            tableLayoutPanel.Controls.Clear();
            for (int i = 0; i < to_add; i++)
            {
                NumericUpDown item = new NumericUpDown();
                //item.Controls[0].Hide();
                //item.Minimum = -100;
                //item.Controls.RemoveAt(0);
                item.Minimum = -100;
                item.Size = new Size(40, 40);
                tableLayoutPanel.Controls.Add(item);
                item.SuspendLayout();
                item.Controls[0].Width = item.Width - 4;
                item.ResumeLayout();
            }
            tableLayoutPanel.ResumeLayout();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Controls.Add(new NumericUpDown());
            UpdateTable(tableLayoutPanel1, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateTable(tableLayoutPanel2, Convert.ToInt32(numericUpDown4.Value), Convert.ToInt32(numericUpDown3.Value));
        }

        private void multiply(TableLayoutPanel matrix1, TableLayoutPanel matrix2, TableLayoutPanel matrix3)
        {
            for (int w = 0; w < matrix1.RowCount; w++)
            {
                for (int h = 0; h < matrix2.ColumnCount; h++)
                {
                    int sum = 0;
                    // m3 [w, h]
                    for (int i = 0; i < matrix1.ColumnCount; i++)
                    {
                        // m1 [ i, h ] * m2 [w, i] 
                        // m [x, y] = m[x + y * columns]
                        NumericUpDown item1 = (NumericUpDown)matrix1.Controls[i + h * matrix1.ColumnCount];
                        NumericUpDown item2 = (NumericUpDown)matrix2.Controls[w + i * matrix2.ColumnCount];
                        sum += Convert.ToInt32(item1.Value * item2.Value);
                    }
                    NumericUpDown item3 = (NumericUpDown)matrix3.Controls[w + h * matrix3.ColumnCount];
                    item3.Value = sum;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.ColumnCount == tableLayoutPanel2.RowCount)
            {
                int width = tableLayoutPanel1.RowCount;
                int height = tableLayoutPanel2.ColumnCount;
                UpdateTable(tableLayoutPanel3, height, width);
                multiply(tableLayoutPanel1, tableLayoutPanel2, tableLayoutPanel3);
            }
            else
            {
                MessageBox.Show("Number of columns in the first matrix must be equal to the number of rows in the second matrix");
            }
        }
    }
}
