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
    public partial class QLTeam : Form
    {

        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public QLTeam()
        {
            InitializeComponent();
        }

        private void QLTeam_Load(object sender, EventArgs e)
        {
            Hienthi();
            txtMateam.Text = Masinh();
            txtMateam.Enabled = false;
        }
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Team", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public string Masinh()
        {
            string sql = @"select * from Team";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlDataAdapter ds = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "Team01";
            }
            else
            {
                int k;
                ma = "Team";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(5));
                k = k + 1;
                if (k < 10)
                {
                    ma = ma + "0";
                }
                else if (k < 100)
                {
                    ma = ma + "";
                }
                ma = ma + k.ToString();
            }
            return ma;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Team VALUES (@Mateam,@Tenteam,@Logo,@Nhataitro,@Ngaythamgia)", con);
                cmd.Parameters.AddWithValue("Mateam", txtMateam.Text);
                if (txtTenteam.Text != "")
                {
                    cmd.Parameters.AddWithValue("Tenteam", txtTenteam.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên team");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Logo", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh");
                    return;
                }
                if (txtNhataitro.Text != "")
                {
                    cmd.Parameters.AddWithValue("Nhataitro", txtNhataitro.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên nha tài trợ");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaythamgia", dtpNgaythamgia.Value);
                MessageBox.Show("Thêm thành công");
                cmd.ExecuteNonQuery();
                con.Close();
                txthinh.Text = txtTenteam.Text = "";
                Hienthi();
            }
            catch
            {
                MessageBox.Show("Lỗi");
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("update Team set Tenteam=@Tenteam,Logo=@Logo,Nhataitro=@Nhataitro,Ngaythamgia=@Ngaythamgia where Mateam=@Mateam", con);
                cmd.Parameters.AddWithValue("Mateam", txtMateam.Text);
                if (txtTenteam.Text != "")
                {
                    cmd.Parameters.AddWithValue("Tenteam", txtTenteam.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên team");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Logo", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh");
                    return;
                }
                if (txtNhataitro.Text != "")
                {
                    cmd.Parameters.AddWithValue("Nhataitro", txtNhataitro.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập tên nha tài trợ");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaythamgia", dtpNgaythamgia.Value);
                MessageBox.Show("Sửa thành công");
                cmd.ExecuteNonQuery();
                con.Close();
                txthinh.Text = txtTenteam.Text = "";
                Hienthi();
            }
            catch
            {
                MessageBox.Show("Bạn chưa cập nhật ảnh");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Team where Mateam=@Mateam", con);
            cmd.Parameters.AddWithValue("Mateam", txtMateam.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Xoá thành công");
            con.Close();
            Hienthi();
        }

        private void btnTimGV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Team WHERE  Tenteam='" + txtTimKiem.Text + "'", con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1, "ti");
            dataGridView1.DataSource = ds1.Tables["ti"];
            MessageBox.Show("Tìm kiếm thành công");
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLTeam_Load(sender, e);
            Hienthi();
            txtMateam.Text = Masinh();
            txtTenteam.ResetText();
            txtNhataitro.ResetText();
            txtTimKiem.ResetText();
            picsGV.Image = null;
            txthinh.ResetText();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            txthinh.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            byte[] b = (byte[])dataGridView1.Rows[r].Cells[2].Value;
            picsGV.Image = ByteArrayToImage(b);
            int numrow;
            numrow = e.RowIndex;
            txtMateam.Text = dataGridView1.Rows[numrow].Cells[0].Value.ToString();
            txtTenteam.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
            txtNhataitro.Text = dataGridView1.Rows[numrow].Cells[3].Value.ToString();
            dtpNgaythamgia.Text = dataGridView1.Rows[numrow].Cells[4].Value.ToString();
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

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeThong form2 = new HeThong();
            form2.Show();
            this.Hide();
        }
    }
}
