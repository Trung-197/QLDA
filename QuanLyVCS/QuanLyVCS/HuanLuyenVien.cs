using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QuanLyVCS
{
    public partial class HuanLuyenVien : Form
    {
        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public HuanLuyenVien()
        {
            InitializeComponent();
        }

        private void HuanLuyenVien_Load(object sender, EventArgs e)
        {
            Hienthi();
            txtMahlv.Text = Masinh();
            getteam();
            txtMahlv.Enabled = false;
        }
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from HuanLuyenVien", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void getteam()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Team", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet team = new DataSet();
            add.Fill(team, "team");
            cbboxteam.DataSource = team.Tables["team"];
            cbboxteam.DisplayMember = "Tenteam";
            cbboxteam.ValueMember = "Mateam";
            con.Close();
        }
        public string Masinh()
        {
            string sql = @"select * from HuanLuyenVien";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlDataAdapter ds = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "HLV001";
            }
            else
            {
                int k;
                ma = "HLV";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(4));
                k = k + 1;
                if (k < 10)
                {
                    ma = ma + "00";
                }
                else if (k < 100)
                {
                    ma = ma + "0";
                }
                ma = ma + k.ToString();
            }
            return ma;
        }

        private void btnchonhinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*";
            dlg.Title = "Select Student Picture";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picsGV.ImageLocation = dlg.FileName;
                txthinh.Text = dlg.FileName;
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO HuanLuyenVien VALUES (@MaHLV,@TenHLV,@Mateam,@Ngaysinh,@Anhhoso,@Gioitinh,@Quequan,@CMND,@Email,@SDT,@Chucvu,@Ngaygianhap,@Trangthai)", con);
                cmd.Parameters.AddWithValue("MaHLV", txtMahlv.Text);
                if (txtHoten.Text != "")
                {
                    cmd.Parameters.AddWithValue("TenHLV", txtHoten.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên hlv");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Anhhoso", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh hồ sơ");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaysinh", dtpngaysinh.Value);
                cmd.Parameters.AddWithValue("Gioitinh", cbboxGioitinh.SelectedItem);
                if (txtquequan.Text != "")
                {
                    cmd.Parameters.AddWithValue("Quequan", txtquequan.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập quê quán");
                    return;
                }
                if (txtsdt.Text != "")
                {
                    cmd.Parameters.AddWithValue("SDT", txtsdt.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập sdt");
                    return;
                }
                if (txtemail.Text != "")
                {

                    cmd.Parameters.AddWithValue("Email", txtemail.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập Email");
                    return;
                }
                cmd.Parameters.AddWithValue("Mateam", cbboxteam.SelectedValue);
                if (txtcmnd.Text != "")
                {
                    cmd.Parameters.AddWithValue("CMND", txtcmnd.Text);
                }
                else
                {
                    MessageBox.Show("bạn chưa nhập Email");
                    return;
                }
                if (txtchuyenmon.Text != "")
                {
                    cmd.Parameters.AddWithValue("Chucvu", txtchuyenmon.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập chức vụ");
                }
                cmd.Parameters.AddWithValue("Ngaygianhap", dtpngaythamgia.Value);
                cmd.Parameters.AddWithValue("Trangthai", txttrangthai.Text);
                MessageBox.Show("Thêm thành công");
                cmd.ExecuteNonQuery();
                con.Close();
                txthinh.Text = txtHoten.Text = "";
                Hienthi();
            }
            catch
            {
                MessageBox.Show("bạn chưa cập nhật ảnh");
            }
        }
        private byte[] convertImageToBytes()
        {
            FileStream fs;
            fs = new FileStream(txthinh.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*";
            dlg.Title = "Select Student Picture";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            int r = dataGridView1.CurrentCell.RowIndex;
            txthinh.Text = dataGridView1.Rows[r].Cells[4].Value.ToString();
            picsGV.ImageLocation = dlg.FileName;
            txthinh.Text= dlg.FileName;
            byte[] b = (byte[])dataGridView1.Rows[r].Cells[4].Value;
            picsGV.Image = ByteArrayToImage(b);
            int numrow;
            numrow = e.RowIndex;
            txtMahlv.Text = dataGridView1.Rows[numrow].Cells[0].Value.ToString();
            txtHoten.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
            txtquequan.Text = dataGridView1.Rows[numrow].Cells[6].Value.ToString();
            txtsdt.Text = dataGridView1.Rows[numrow].Cells[9].Value.ToString();
            txtemail.Text = dataGridView1.Rows[numrow].Cells[8].Value.ToString();
            txtcmnd.Text = dataGridView1.Rows[numrow].Cells[7].Value.ToString();
            txtchuyenmon.Text = dataGridView1.Rows[numrow].Cells[10].Value.ToString();
            txttrangthai.Text = dataGridView1.Rows[numrow].Cells[12].Value.ToString();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select HuanLuyenVien.*,Team.Tenteam from HuanLuyenVien,Team where HuanLuyenVien.Mateam=Team.Mateam and MaHLV='" + txtMahlv.Text + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cbboxteam.DisplayMember = "Tenteam";
            cbboxteam.ValueMember = "Mateam";
            cbboxteam.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int VT = dataGridView1.CurrentCell.RowIndex;
            load(VT);
        }
        private void load(int VT)
        {
            try
            {

                dtpngaysinh.Value = Convert.ToDateTime(dataGridView1.Rows[VT].Cells[3].Value.ToString());
                cbboxGioitinh.Text = dataGridView1.Rows[VT].Cells[5].Value.ToString();
                cbboxteam.Text = dataGridView1.Rows[VT].Cells[2].Value.ToString();
                dtpngaythamgia.Text = dataGridView1.Rows[VT].Cells[11].Value.ToString();

            }
            catch (Exception e) { }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("update HuanLuyenVien set TenHLV=@TenHLV,Mateam=@Mateam,Ngaysinh=@Ngaysinh,Anhhoso@Anhhoso,Gioitinh=@Gioitinh,Quequan=@Quequan,CMND=@CMND,Email=@Email,SDT=@SDT,Chucvu=@Chucvu,Ngaygianhap=@Ngaygianhap,Trangthai=@Trangthai where MaHLV=@MaHLV", con);
                cmd.Parameters.AddWithValue("MaHLV", txtMahlv.Text);
                if (txtHoten.Text != "")
                {
                    cmd.Parameters.AddWithValue("TenHLV", txtHoten.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên hlv");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Anhhoso", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh hồ sơ");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaysinh", dtpngaysinh.Value);
                cmd.Parameters.AddWithValue("Gioitinh", cbboxGioitinh.SelectedItem);
                if (txtquequan.Text != "")
                {
                    cmd.Parameters.AddWithValue("Quequan", txtquequan.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập quê quán");
                    return;
                }
                if (txtsdt.Text != "")
                {
                    cmd.Parameters.AddWithValue("SDT", txtsdt.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập sdt");
                    return;
                }
                if (txtemail.Text != "")
                {

                    cmd.Parameters.AddWithValue("Email", txtemail.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập Email");
                    return;
                }
                cmd.Parameters.AddWithValue("Mateam", cbboxteam.SelectedValue);
                if (txtcmnd.Text != "")
                {
                    cmd.Parameters.AddWithValue("CMND", txtcmnd.Text);
                }
                else
                {
                    MessageBox.Show("bạn chưa nhập Email");
                    return;
                }
                if (txtchuyenmon.Text != "")
                {
                    cmd.Parameters.AddWithValue("Chucvu", txtchuyenmon.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập chức vụ");
                }
                cmd.Parameters.AddWithValue("Ngaygianhap", dtpngaythamgia.Value);
                cmd.Parameters.AddWithValue("Trangthai", txttrangthai.Text);
                MessageBox.Show("Sửa thành công");
                cmd.ExecuteNonQuery();
                con.Close();
                txthinh.Text = txtHoten.Text = "";
                Hienthi();
            }
            catch
            {
                MessageBox.Show("Bạn chưa cập nhật lại ảnh");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from HuanLuyenVien where MaHLV=@MaHLV", con);
            cmd.Parameters.AddWithValue("MaHLV", txtMahlv.Text);
            MessageBox.Show("Xoá thành công");
            cmd.ExecuteNonQuery();
            con.Close();
            Hienthi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HuanLuyenVien_Load(sender, e);
            Hienthi();
            txtMahlv.Text = Masinh();
            txthinh.ResetText();
            txtchuyenmon.ResetText();
            txtcmnd.ResetText();
            txtsdt.ResetText();
            picsGV.Image = null;
            txtemail.ResetText();
            txtHoten.ResetText();
            txtquequan.ResetText();
            txttrangthai.ResetText();
        }

        private void btnTimGV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM HuanLuyenVien WHERE  MaHLV='" + txtTimKiem.Text + "' ", con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1, "ti");
            dataGridView1.DataSource = ds1.Tables["ti"];
            MessageBox.Show("Tìm kiếm thành công");
            con.Close();
        }

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameThu form2 = new GameThu();
            form2.Show();
            this.Hide();
        }
    }
}
