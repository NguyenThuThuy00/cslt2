using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich
{
    public partial class FrmDangKyTour : Form
    {
        DataTable tblDangKyTour;
        public FrmDangKyTour()
        {
            InitializeComponent();
        }

        private void FrmDangKyTour_Load(object sender, EventArgs e)
        {           
            LoadDataToGridView();
            Functions.FillCombo("Select MaTour,DonGia from DanhMucTour", cboMaTour, "MaTour");
            cboMaTour.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaLichTour,SoNguoiDangKy, TongTien FROM LichTour where NgayKhoiHanh>GetDate()", cboMaLichTour, "MaLichTour");
            cboMaLichTour.SelectedIndex = -1;
        }  
        private void cboMaTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaTour.Text != "")
            {
                Functions.FillCombo("Select MaLichTour from LichTour where MaTour= '" + cboMaTour.SelectedValue + "'", cboMaLichTour, "MaLichTour");
                cboMaLichTour.SelectedIndex = -1;
            }
        }
        private void LoadDataToGridView()
        {
            string sql = "select * from DangKiTour ";
            DataTable myTable = Functions.LoadDataToTable(sql); 
            tblDangKyTour = Functions.LoadDataToTable(sql);
            dataGridView_DangKyTour.DataSource = myTable;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM LichTour WHERE MaLichTour = '" + cboMaLichTour.Text + "'";
            tblDangKyTour = Functions.LoadDataToTable(sql);
            dataGridView_DangKyTour.DataSource = tblDangKyTour;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            resetValues();
            LoadDataToGridView();
        }
        private void resetValues()
        {
            txtMaDangKy.Text = "";
            txtMaKhach.Text = "";
            mskNgayDangKy.Text = "";
            txtSoLuongDangKy.Text = "";       
            cboMaTour.Text = "";
            cboMaLichTour.Text = "";
            txtDonGia.Text = "";
            txtSoNguoiDangKy.Text = "";
            txtTongTien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            resetValues();
           
            int count = 0;
            count = dataGridView_DangKyTour.Rows.Count;
            string chuoi = "";
            int chuoi2 = 0;
            chuoi = Convert.ToString(dataGridView_DangKyTour.Rows[count - 2].Cells[0].Value);
            chuoi2 = Convert.ToInt32((chuoi.Remove(0, 2)));
            if (chuoi2 + 1 < 10)
            {
                txtMaDangKy.Text = "DK0" + (chuoi2 + 1).ToString();
            }
            else
                if (chuoi2 + 1 < 100)
            {
                txtMaDangKy.Text = "DK" + (chuoi2 + 1).ToString();
            }
            txtMaDangKy.Enabled = false;
        }

        private void dataGridView_DangKyTour_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaDangKy.Text = dataGridView_DangKyTour.CurrentRow.Cells["MaDangKy"].Value.ToString();
            txtMaKhach.Text = dataGridView_DangKyTour.CurrentRow.Cells["MaKhach"].Value.ToString();
            mskNgayDangKy.Text = dataGridView_DangKyTour.CurrentRow.Cells["NgayDangKy"].Value.ToString();
            txtThanhTien.Text = dataGridView_DangKyTour.CurrentRow.Cells["ThanhTien"].Value.ToString();
            txtSoLuongDangKy.Text = dataGridView_DangKyTour.CurrentRow.Cells["SoLuongDangKy"].Value.ToString();
            cboMaLichTour.Text = dataGridView_DangKyTour.CurrentRow.Cells["MaLichTour"].Value.ToString();
            txtMaDangKy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblDangKyTour.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDangKy.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "Delete from DangKiTour where MaDangKy= '" + txtMaDangKy.Text + "'";
                Functions.RunSql(sql);
                LoadDataToGridView();
            }

            double b = Convert.ToDouble(txtSoNguoiDangKy.Text) - Convert.ToDouble(txtSoLuongDangKy.Text);
            double c = Convert.ToDouble(txtTongTien.Text) - Convert.ToDouble(txtThanhTien.Text);
            sql = "update LichTour set SoNguoiDangKy= " + b + ", TongTien= " + c + "where MaLichTour= '" + cboMaLichTour.SelectedValue + "'";
            Functions.RunSql(sql);
            LoadDataToGridView();
            FrmLichTour fLT = new FrmLichTour();
            fLT.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblDangKyTour.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDangKy.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update DangKiTour set MaKhach= '" + txtMaKhach.Text + "', MaLichTour= '" + cboMaLichTour.SelectedValue + "', NgayDangKy= '" + Functions.ConvertDateTime(mskNgayDangKy.Text) + "', " +
                "SoLuongDangKy= " + txtSoLuongDangKy.Text + "where MaDangKy= '" + txtMaDangKy.Text + "'";
            Functions.RunSql(sql);
            LoadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn cần nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhach.Focus();
                return;
            }
            if (cboMaLichTour.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn cần nhập mã lịch tour");
                cboMaLichTour.Focus();
                return;
            }
            if (mskNgayDangKy.Text == "  /  /    ")
            {
                MessageBox.Show("Bạn cần nhập ngày đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayDangKy.Focus();
                return;
            }
            if (!Functions.IsDate(mskNgayDangKy.Text))
            {
                MessageBox.Show("Bạn cần nhập lại ngày đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayDangKy.Text = "";
                mskNgayDangKy.Focus();
            }
            if (txtSoLuongDangKy.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số lượng đăng ký");
                txtSoLuongDangKy.Focus();
                return;
            }

            sql = "select MaDangKy from DangKiTour where MaDangKy= '" + txtMaDangKy.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Đăng Ký này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDangKy.Focus();
                txtMaDangKy.Text = "";
                return;
            }
            double a = Convert.ToDouble(txtDonGia.Text) * Convert.ToDouble(txtSoLuongDangKy.Text);
            sql = "insert into DangKiTour values ('" + txtMaDangKy.Text.Trim() + "','" + txtMaKhach.Text.Trim() + 
                "','" + cboMaLichTour.SelectedValue.ToString() + "','" + Functions.ConvertDateTime(mskNgayDangKy.Text) + 
                "'," + txtSoLuongDangKy.Text.Trim() + ", " + a + ")";
            Functions.RunSql(sql);
            LoadDataToGridView();

            double b = Convert.ToDouble(txtSoNguoiDangKy.Text) + Convert.ToDouble(txtSoLuongDangKy.Text);
            double c = Convert.ToDouble(txtTongTien.Text) + a;
            sql = "update LichTour set SoNguoiDangKy= " + b + ", TongTien= " + c + "where MaLichTour= '" + cboMaLichTour.SelectedValue + "'";
            Functions.RunSql(sql);
            LoadDataToGridView();
            FrmLichTour fLT = new FrmLichTour();
            fLT.Show();
        }

        private void cboMaTour_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaTour.Text == "")
            {
                txtDonGia.Text = "";
            }
            str = "select DonGia from DanhMucTour where MaTour= N'" + cboMaTour.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void cboMaLichTour_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaLichTour.Text == "")
            {
                txtTongTien.Text = "";
            }
            str = "select TongTien from LichTour where MaLichTour= N'" + cboMaLichTour.SelectedValue + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            if (cboMaLichTour.Text == "")
            {
                txtSoNguoiDangKy.Text = "";
            }
            str = "select SoNguoiDangKy from LichTour where MaLichTour= '" + cboMaLichTour.SelectedValue + "'";
            txtSoNguoiDangKy.Text = Functions.GetFieldValues(str);
        }

        
    }
}
