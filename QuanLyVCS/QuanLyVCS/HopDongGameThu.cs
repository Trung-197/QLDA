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

namespace QuanLyVCS
{
    public partial class HopDongGameThu : Form
    {
        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public HopDongGameThu()
        {
            InitializeComponent();
        }

        private void HopDongGameThu_Load(object sender, EventArgs e)
        {
            Hienthi();
            txtmahd.Text = Masinh();
            getteam();
            getgt();
            txtmahd.Enabled = false;
        }
        private void getteam()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Team", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet team = new DataSet();
            add.Fill(team, "team");
            cbbteam.DataSource = team.Tables["team"];
            cbbteam.DisplayMember = "Tenteam";
            cbbteam.ValueMember = "Mateam";
            con.Close();
        }
        private void getgt()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from GameThu", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet team = new DataSet();
            add.Fill(team, "team");
            cbbgt.DataSource = team.Tables["team"];
            cbbgt.DisplayMember = "Hoten";
            cbbgt.ValueMember = "MaGT";
            con.Close();
        }
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from HopDong", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public string Masinh()
        {
            string sql = @"select * from HopDong";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlDataAdapter ds = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "HD001";
            }
            else
            {
                int k;
                ma = "HD";
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

        private void cbbteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select GameThu.*,Team.* from GameThu,Team where GameThu.Mateam=Team.Mateam and GameThu.Mateam='" + cbbteam.SelectedValue + "'", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet lop = new DataSet();
            add.Fill(lop, "lop");
            cbbgt.DataSource = lop.Tables["lop"];
            cbbgt.DisplayMember = "Hoten";
            cbbgt.ValueMember = "MaGT";
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("INSERT INTO HopDong VALUES (@MaHD,@Mateam,@MaGT,@Ngayky,@Ngayhethan)", con);
                cmd1.Parameters.AddWithValue("MaHD", txtmahd.Text);
                cmd1.Parameters.AddWithValue("Mateam", cbbteam.SelectedValue);

                cmd1.Parameters.AddWithValue("MaGT", cbbgt.SelectedValue);
                cmd1.Parameters.AddWithValue("Ngayky", dtpngayky.Value);
                cmd1.Parameters.AddWithValue("Ngayhethan", dtpngayhet.Value);
                cmd1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Thêm thành công");
                Hienthi();
            }
            catch
            {
                MessageBox.Show("Vui lòng làm mới hợp đồng để tiếp tục thêm");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("delete from HopDong where MaHD=@MaHD", con);
            cmd1.Parameters.AddWithValue("MaHD", txtmahd.Text);
 
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Xoá thành công");
            Hienthi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HeThong form1 = new HeThong();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HopDongGameThu_Load(sender, e);
            Hienthi();
            txtmahd.Text = Masinh();
            cbbgt.ResetText();
            cbbteam.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM HopDong WHERE MaHD=@MaHD", con);
            if (txttim.Text != "")
            {
                cmd.Parameters.AddWithValue("MaHD", txttim.Text);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã hợp đồng cần tìm");
                return;
            }
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1, "tim");
            dataGridView1.DataSource = ds1.Tables["tim"];
            MessageBox.Show("Tìm kiếm thành công");
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            txtmahd.Text = dataGridView1.Rows[n].Cells["MaHD"].Value.ToString();
            cbbgt.Text = dataGridView1.Rows[n].Cells["MaGT"].Value.ToString();
            cbbteam.Text = dataGridView1.Rows[n].Cells["Mateam"].Value.ToString();
            dtpngayky.Text = dataGridView1.Rows[n].Cells["Ngayky"].Value.ToString();
            dtpngayhet.Text = dataGridView1.Rows[n].Cells["Ngayhethan"].Value.ToString();
        }
    }
}
