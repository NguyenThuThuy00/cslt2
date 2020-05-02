using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyCongTyDuLich;

namespace QuanLyCongTyDuLich
{
    public partial class FrmNgoaiNgu : Form
    {
        DataTable NgoaiNgu;
        public FrmNgoaiNgu()
        {
            InitializeComponent();
        }

        private void FrmNgoaiNgu_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "SELECT* FROM NgoaiNgu                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ";
            NgoaiNgu = Functions.LoadDataToTable(sql);
            dataGridView_NN.DataSource = NgoaiNgu;
        }

        private void dataGridView_NN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNgoaiNgu.Text = dataGridView_NN.CurrentRow.Cells["MaNgoaiNgu"].Value.ToString();
            txtTenNgoaiNgu.Text = dataGridView_NN.CurrentRow.Cells["TenNgoaiNgu"].Value.ToString();
            txtMaNgoaiNgu.Enabled = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNgoaiNgu.Text = "";
            txtTenNgoaiNgu.Text = "";
            txtMaNgoaiNgu.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (NgoaiNgu.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNgoaiNgu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update NgoaiNgu set TenNgoaiNgu= N'" + txtTenNgoaiNgu.Text +
                 "' where MaNgoaiNgu= '" + txtMaNgoaiNgu.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE From NgoaiNgu where MaNgoaiNgu='" + txtMaNgoaiNgu.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNgoaiNgu.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã NN");
                txtMaNgoaiNgu.Focus();
                return;
            }
            if (txtTenNgoaiNgu.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên NN");
                txtTenNgoaiNgu.Focus();
                return;

            }
            sql = "select MaNgoaiNgu from NgoaiNgu where MaNgoaiNgu= '" + txtMaNgoaiNgu.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Ngoại Ngu này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNgoaiNgu.Focus();
                txtMaNgoaiNgu.Text = "";
                return;
            }
            sql = "insert into NgoaiNgu values ('" + txtMaNgoaiNgu.Text + "', N'" + txtTenNgoaiNgu.Text
                + "')";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            loadDataToGridView();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
