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
    public partial class FrmQuocTich : Form
    {
        DataTable QuocTich;
        public FrmQuocTich()
        {
            InitializeComponent();
        }

        private void FrmQuocTich_Load(object sender, EventArgs e)
        {
           
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "SELECT* FROM QuocTich";
            QuocTich = Functions.LoadDataToTable(sql);
            dataGridView_QuocTich.DataSource = QuocTich;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaQuocTich.Text = "";
            txtTenQuocTich.Text = "";
            txtMaQuocTich.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (QuocTich.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaQuocTich.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update QuocTich set TenQuocTich= N'" + txtTenQuocTich.Text +
                 "' where MaQuocTich= '" + txtMaQuocTich.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE From QuocTich where MaQuocTich='" + txtMaQuocTich.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaQuocTich.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã quốc tịch");
                txtMaQuocTich.Focus();
                return;
            }
            if (txtTenQuocTich.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên quốc tịch");
                txtTenQuocTich.Focus();
                return;

            }
            sql = "select MaQuocTich from QuocTich where MaQuocTich= '" + txtMaQuocTich.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Quốc tịch  này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaQuocTich.Focus();
                txtMaQuocTich.Text = "";
                return;
            }
            sql = "insert into QuocTich values ('" + txtMaQuocTich.Text + "', N'" + txtTenQuocTich.Text
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

        private void dataGridView_QuocTich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaQuocTich.Text = dataGridView_QuocTich.CurrentRow.Cells["MaQuocTich"].Value.ToString();
            txtTenQuocTich.Text = dataGridView_QuocTich.CurrentRow.Cells["TenQuocTich"].Value.ToString();
            txtMaQuocTich.Enabled = false;
        }
    }
}
