using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LamLaiTuDau
{
    public partial class Form1 : Form
    {
        public class SinhVien
        {
            public string MaSinhVien { get; set; }
            public string TenSinhVien { get; set; }
            public string LopHoc { get; set; }
        }
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        private List<SinhVien> filteredDanhSachSinhVien = new List<SinhVien>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            textBox4.TextChanged += textBox4_TextChanged;
            
        }
        private void CapNhatDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = danhSachSinhVien;
            dataGridView1.DataSource = filteredDanhSachSinhVien;
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                SinhVien sv = new SinhVien()
                {
                    MaSinhVien = textBox1.Text,
                    TenSinhVien = textBox2.Text,
                    LopHoc = textBox3.Text
                };

                danhSachSinhVien.Add(sv);
                CapNhatDataGridView();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sinh viên.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                SinhVien sv = danhSachSinhVien[index];

                sv.MaSinhVien = textBox1.Text;
                sv.TenSinhVien = textBox2.Text;
                sv.LopHoc = textBox3.Text;

                CapNhatDataGridView();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên để sửa.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                danhSachSinhVien.RemoveAt(index);

                CapNhatDataGridView();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa.");
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                SinhVien sv = danhSachSinhVien[index];

                textBox1.Text = sv.MaSinhVien;
                textBox2.Text = sv.TenSinhVien;
                textBox3.Text = sv.LopHoc;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void ApplyFilter()
        {
            string searchText = textBox4.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                
                filteredDanhSachSinhVien = new List<SinhVien>(danhSachSinhVien);
            }
            else
            {
                // Filter students based on search text
                filteredDanhSachSinhVien = danhSachSinhVien
                    .Where(sv =>
                        sv.MaSinhVien.ToLower().Contains(searchText) ||
                        sv.TenSinhVien.ToLower().Contains(searchText) ||
                        sv.LopHoc.ToLower().Contains(searchText))
                    .ToList();
            }

            CapNhatDataGridView();
        }
    }
}
