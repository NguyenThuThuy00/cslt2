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
    public partial class FrmDMTour : Form
    {
        DataTable Danhmuctour;
        public FrmDMTour()
        {
            InitializeComponent();
        }
        private void FrmDMTour_Load(object sender, EventArgs e)
        {
            
            loadDataToGridView();

            Functions.FillCombo("SELECT MaCongTy FROM CongTy", cboMaCongTy, "MaCongTy");
            cboMaCongTy.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaPhamVi FROM PhamVi", cboMaPhamVi, "MaPhamVi");
            cboMaPhamVi.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaMua FROM Mua", cboMaMua, "MaMua");
            cboMaMua.SelectedIndex = -1;

        }
        private void loadDataToGridView()
        {
            string sql = "SELECT* FROM Danhmuctour";


            Danhmuctour = Functions.LoadDataToTable(sql);

            dataGridView_DMTour.DataSource = Danhmuctour;
        }
        private void btnSuabtnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (Danhmuctour.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaTour.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update Danhmuctour set TenTour= N'" + txtTenTour.Text +
                "', MaCongTy= '" + cboMaCongTy.SelectedValue + "', MaMua= '" + cboMaMua.SelectedValue + "', GhiChu= N'" + txtGhiChu.Text +
                "', MaPhamVi= '" + cboMaPhamVi.SelectedValue + "', DonGia= '" + txtDonGia.Text + "', SoNgay= '" + txtSoNgay.Text +
                 "' where MaTour= '" + txtMaTour.Text + "'";
            Functions.RunSql(sql);
            loadDataToGridView();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (Danhmuctour.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaTour.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "Delete from DanhMucTour where MaTour= '" + txtMaTour.Text + "'";
                Functions.RunSql(sql);
                loadDataToGridView();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string sql;
            if ((txtTenTour.Text == "") && (cboMaMua.Text == "") && (cboMaPhamVi.Text == ""))
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select * from Danhmuctour where 1=1";
            if (txtTenTour.Text != "")
            {
                sql = sql + "and TenTour like N '%"+ txtTenTour.Text +"%";
            }
            if (cboMaMua.Text != "")
            {
                sql = sql + "and MaMua like N'%" + cboMaMua.Text + "%'";
            }
            if (cboMaPhamVi.Text != "")
            {
                sql = sql + "and MaPhamVi like N'%" + cboMaPhamVi.Text + "%'";
            }
            Danhmuctour = Functions.LoadDataToTable(sql);
            if (Danhmuctour.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + Danhmuctour.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView_DMTour.DataSource = Danhmuctour;

        
    }

        private void btnThem_Click(object sender, EventArgs e)
        {
            resetValues();
            int count = 0;
            count = dataGridView_DMTour.Rows.Count;
            string chuoi = "";
            int chuoi2 = 0;
            chuoi = Convert.ToString(dataGridView_DMTour.Rows[count - 2].Cells[0].Value);
            chuoi2 = Convert.ToInt32((chuoi.Remove(0, 1)));
            if (chuoi2 + 1 < 10)
            {
                txtMaTour.Text = "T0" + (chuoi2 + 1).ToString();
            }
            else
                if (chuoi2 + 1 < 100)
            {
                txtMaTour.Text = "T" + (chuoi2 + 1).ToString();

            }
            txtMaTour.Enabled = false;
        }

        private void btnLưu_Click(object sender, EventArgs e)
        {
            string sql;
            

            if (cboMaPhamVi.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã phạm vi");
                cboMaPhamVi.Focus();
                return;
            }
            if (cboMaCongTy.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã công ty");
                cboMaCongTy.Focus();
                return;
            }
            if (txtGhiChu.Text == "")
            {
                MessageBox.Show("Bạn cần ghi chú");
                txtGhiChu.Focus();
                return;
            }
            if (txtTenTour.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên tour");
                txtTenTour.Focus();
                return;
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá");
                txtDonGia.Focus();
                return;
            }
            if (txtSoNgay.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số ngày");
                txtSoNgay.Focus();
                return;
            }
            if (cboMaMua.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã mùa");
                cboMaMua.Focus();
                return;

            }
            sql = "select MaTour from Danhmuctour where MaTour= '" + txtMaTour.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Tour này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTour.Focus();
                txtMaTour.Text = "";
                return;
            }
            sql = "insert into Danhmuctour values ('" + txtMaTour.Text + "', '" + cboMaCongTy.SelectedValue + "', '" + cboMaPhamVi.SelectedValue
                + "', N'" + txtTenTour.Text + "', '" + cboMaMua.SelectedValue + "', '" + txtSoNgay.Text + "', '" + txtDonGia.Text + "', N'" + txtGhiChu.Text
                + "')";
            Functions.RunSql(sql);
            loadDataToGridView();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            resetValues();
            loadDataToGridView();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_DMTour_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonGia.Text = dataGridView_DMTour.CurrentRow.Cells["DonGia"].Value.ToString();
            txtGhiChu.Text = dataGridView_DMTour.CurrentRow.Cells["GhiChu"].Value.ToString();
            cboMaCongTy.Text = dataGridView_DMTour.CurrentRow.Cells["MaCongTy"].Value.ToString();
            cboMaMua.Text = dataGridView_DMTour.CurrentRow.Cells["MaMua"].Value.ToString();
            cboMaPhamVi.Text = dataGridView_DMTour.CurrentRow.Cells["MaPhamVi"].Value.ToString();
            txtMaTour.Text = dataGridView_DMTour.CurrentRow.Cells["MaTour"].Value.ToString();
            txtSoNgay.Text = dataGridView_DMTour.CurrentRow.Cells["SoNgay"].Value.ToString();
            txtTenTour.Text = dataGridView_DMTour.CurrentRow.Cells["TenTour"].Value.ToString();
            txtMaTour.Enabled = false;
        }       
        private void resetValues()
        {
            txtDonGia.Text = "";
            txtGhiChu.Text = "";
            cboMaCongTy.Text = "";
            cboMaMua.Text = "";
            cboMaPhamVi.Text = "";
            txtMaTour.Text = "";
            txtSoNgay.Text = "";
            txtTenTour.Text = "";
        }
    }
}
