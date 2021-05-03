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
using System.Globalization;

namespace QuanLyVCS
{
    public partial class GameThu : Form
    {
        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public GameThu()
        {
            InitializeComponent();
        }

        private void GameThu_Load(object sender, EventArgs e)
        {
            Hienthi();
            txtMaGT.Text = Masinh();
            getteam();
            txtMaGT.Enabled = false;
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
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from GameThu", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public string Masinh()
        {
            string sql = @"select * from GameThu";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlDataAdapter ds = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "GT001";
            }
            else
            {
                int k;
                ma = "GT";
                k = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(3));
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
                SqlCommand cmd = new SqlCommand("INSERT INTO GameThu VALUES (@MaGT,@Hoten,@Anhhoso,@Ngaysinh,@Gioitinh,@Quequan,@Dantoc,@SDT,@Email,@Mateam,@CMND,@Ngaythamgia,@Chuyenmon,@Trangthai)", con);
                cmd.Parameters.AddWithValue("MaGT", txtMaGT.Text);
                if (txtHoten.Text != "")
                {
                    cmd.Parameters.AddWithValue("Hoten", txtHoten.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập họ tên");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Anhhoso", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh");
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
                if (txtdantoc.Text != "")
                {
                    cmd.Parameters.AddWithValue("Dantoc", txtdantoc.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập dân tộc");
                    return;
                }
                if (txtsdt.Text != "")
                {
                    cmd.Parameters.AddWithValue("SDT", txtsdt.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập số điện thoại");
                    return;
                }
                if (txtemail.Text != "")
                {
                    cmd.Parameters.AddWithValue("Email", txtemail.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập email");
                    return;
                }
                cmd.Parameters.AddWithValue("Mateam", cbboxteam.SelectedValue);
                if (txtcmnd.Text != "")
                {
                    cmd.Parameters.AddWithValue("CMND", txtcmnd.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập CMND");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaythamgia", dtpngaythamgia.Value);
                if (txtchuyenmon.Text != "")
                {
                    cmd.Parameters.AddWithValue("Chuyenmon", txtchuyenmon.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập chuyên môn");
                    return;
                }

                cmd.Parameters.AddWithValue("Trangthai", txttrangthai.Text);
                MessageBox.Show("Thêm thành công");
                cmd.ExecuteNonQuery();
                con.Close();
                txthinh.Text = txtHoten.Text = "";
                Hienthi();
            }
            catch
            {
                MessageBox.Show("Bạn chưa thêm ảnh");
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dataGridView1.CurrentCell.RowIndex;
            txthinh.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            byte[] b = (byte[])dataGridView1.Rows[r].Cells[2].Value;
            picsGV.Image = ByteArrayToImage(b);
            int numrow;
            numrow = e.RowIndex;
            txtMaGT.Text = dataGridView1.Rows[numrow].Cells[0].Value.ToString();
            txtHoten.Text = dataGridView1.Rows[numrow].Cells[1].Value.ToString();
            txtquequan.Text = dataGridView1.Rows[numrow].Cells[5].Value.ToString();
            txtdantoc.Text = dataGridView1.Rows[numrow].Cells[6].Value.ToString();
            txtsdt.Text = dataGridView1.Rows[numrow].Cells[7].Value.ToString();
            txtemail.Text = dataGridView1.Rows[numrow].Cells[8].Value.ToString();
            txtcmnd.Text = dataGridView1.Rows[numrow].Cells[10].Value.ToString();
            txtchuyenmon.Text = dataGridView1.Rows[numrow].Cells[12].Value.ToString();
            txttrangthai.Text = dataGridView1.Rows[numrow].Cells[13].Value.ToString();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select GameThu.*,Team.Tenteam from GameThu,Team where GameThu.Mateam=Team.Mateam and MaGT='" + txtMaGT.Text + "'", con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cbboxteam.DisplayMember = "Tenteam";
            cbboxteam.ValueMember = "Mateam";
            cbboxteam.DataSource = dt;
            con.Close();
        }
        Image ByteArrayToImage(byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }

        private void cbboxteam_Click(object sender, EventArgs e)
        {
            getteam();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameThu_Load(sender, e);
            Hienthi();
            txtMaGT.Text = Masinh();
            
            txthinh.ResetText();
            txtchuyenmon.ResetText();
            txtcmnd.ResetText();
            txtsdt.ResetText();
            picsGV.Image = null;
            txtdantoc.ResetText();
            txtemail.ResetText();
            txtHoten.ResetText();
            txtquequan.ResetText();
            txttrangthai.ResetText();
            
        }

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HeThong form1 = new HeThong();
            form1.Show();
            this.Hide();
        }

        private void quảnLýGameThủToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameThu form2 = new GameThu();
            form2.Show();
            this.Hide();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("update GameThu set Hoten=@Hoten,Anhhoso=@Anhhoso,Ngaysinh=@Ngaysinh,Gioitinh=@Gioitinh,Quequan=@Quequan,Dantoc=@Dantoc,SDT=@SDT,Email=@Email,Mateam=@Mateam,CMND=@CMND,Ngaythamgia=@Ngaythamgia,Chuyenmon=@Chuyenmon,Trangthai=@Trangthai where MaGT=@MaGT", con);
                cmd.Parameters.AddWithValue("MaGT", txtMaGT.Text);
                if (txtHoten.Text != "")
                {
                    cmd.Parameters.AddWithValue("Hoten", txtHoten.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập họ tên");
                    return;
                }
                if (picsGV.Image != null)
                {
                    cmd.Parameters.AddWithValue("Anhhoso", convertImageToBytes());
                }
                else
                {
                    MessageBox.Show("Bạn chưa thêm ảnh");
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
                if (txtdantoc.Text != "")
                {
                    cmd.Parameters.AddWithValue("Dantoc", txtdantoc.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập dân tộc");
                    return;
                }
                if (txtsdt.Text != "")
                {
                    cmd.Parameters.AddWithValue("SDT", txtsdt.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập số điện thoại");
                    return;
                }
                if (txtemail.Text != "")
                {
                    cmd.Parameters.AddWithValue("Email", txtemail.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập email");
                    return;
                }
                cmd.Parameters.AddWithValue("Mateam", cbboxteam.SelectedValue);
                if (txtcmnd.Text != "")
                {
                    cmd.Parameters.AddWithValue("CMND", txtcmnd.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập CMND");
                    return;
                }
                cmd.Parameters.AddWithValue("Ngaythamgia", dtpngaythamgia.Value);
                if (txtchuyenmon.Text != "")
                {
                    cmd.Parameters.AddWithValue("Chuyenmon", txtchuyenmon.Text);
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập chuyên môn");
                    return;
                }

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
            SqlCommand cmd = new SqlCommand("delete from GameThu where MaGT=@MaGT", con);
            cmd.Parameters.AddWithValue("MaGT", txtMaGT.Text);
            MessageBox.Show("Xoá thành công");
            cmd.ExecuteNonQuery();
            con.Close();
            Hienthi();
        }

        private void btnTimGV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT GameThu.* FROM GameThu WHERE  Mateam='" + txtTimKiem.Text + "' or MaGT='" + txtTimKiem.Text + "' ", con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1, "ti");
            dataGridView1.DataSource = ds1.Tables["ti"];
            MessageBox.Show("Tìm kiếm thành công");
            con.Close();
        }

        private void quảnLýHLVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HuanLuyenVien form2 = new HuanLuyenVien();
            form2.Show();
            this.Hide();
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
                cbboxGioitinh.Text = dataGridView1.Rows[VT].Cells[4].Value.ToString();
                cbboxteam.Text = dataGridView1.Rows[VT].Cells[9].Value.ToString();
                dtpngaythamgia.Text = dataGridView1.Rows[VT].Cells[11].Value.ToString();
                txttrangthai.Text = dataGridView1.Rows[VT].Cells[13].Value.ToString();


            }
            catch (Exception e) { }
        }

        private void quảnLýGameThủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void quảnLýLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void lươngGameThủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LuongGameThu form5 = new LuongGameThu();
            form5.Show();
            this.Hide();
        }

        private void txttrangthai_Leave(object sender, EventArgs e)
        {
            if (txttrangthai.Text==null)
            {
                txttrangthai.Text = "Ghi rõ nếu game thủ đang bị phạt";
                txttrangthai.ForeColor = Color.Silver;
            }
        }

        private void txttrangthai_Enter(object sender, EventArgs e)
        {
            if(txttrangthai.Text== "Ghi rõ nếu game thủ đang bị phạt")
            {
                txttrangthai.Text = "";
                txttrangthai.ForeColor = Color.Black;
            }
        }

        private void lươngHuấnLuyệnViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LuongHLV form4 = new LuongHLV();
            form4.Show();
            this.Hide();
        }
    }
}
