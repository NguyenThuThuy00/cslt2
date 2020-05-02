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
    public partial class FrmXepLoai : Form
    {
        DataTable XepLoai;
        public FrmXepLoai()
        {
            InitializeComponent();
        }

        private void FrmXepLoai_Load(object sender, EventArgs e)
        {
            
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "SELECT* FROM XepLoai";
            XepLoai = Functions.LoadDataToTable(sql);
            dataGridView_XepLoai.DataSource = XepLoai;
        }

        private void dataGridView_XepLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaXepLoai.Text = dataGridView_XepLoai.CurrentRow.Cells["MaXepLoai"].Value.ToString();
            txtTenXepLoai.Text = dataGridView_XepLoai.CurrentRow.Cells["TenXepLoai"].Value.ToString();
            txtMaXepLoai.Enabled = false;
        }
 
       

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaXepLoai.Text = "";
            txtTenXepLoai.Text = "";
            txtMaXepLoai.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (XepLoai.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaXepLoai.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update XepLoai set TenXepLoai= N'" + txtTenXepLoai.Text +
                 "' where MaXepLoai= '" + txtMaXepLoai.Text+ "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE From XepLoai where MaXepLoai='" + txtMaXepLoai.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaXepLoai.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã xếp loại");
                txtMaXepLoai.Focus();
                return;
            }
            if (txtTenXepLoai.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên xếp loại");
                txtTenXepLoai.Focus();
                return;
            }

                sql = "select MaXepLoai  from XepLoai where MaXepLoai= '" + txtMaXepLoai.Text + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã Xếp Loại này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaXepLoai.Focus();
                        txtTenXepLoai.Text = "";
                        return;
                    }
                
            sql = "insert into XepLoai values ('" + txtMaXepLoai.Text + "', N'" + txtTenXepLoai.Text
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
