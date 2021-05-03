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
    public partial class LuongHLV : Form
    {
        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public LuongHLV()
        {
            InitializeComponent();
        }

        private void LuongHLV_Load(object sender, EventArgs e)
        {
            Hienthi();
            getteam();
            txtmaluong.Text = Masinh();
            txtmaluong.Enabled = false;
            txtmagt.Enabled = false;
        }
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from LuongHLV", con);
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
            cbbchonteam.DataSource = team.Tables["team"];
            cbbchonteam.DisplayMember = "Tenteam";
            cbbchonteam.ValueMember = "Mateam";
            con.Close();
        }

        private void cbbchonteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select HuanLuyenVien.*,Team.* from HuanLuyenVien,Team where HuanLuyenVien.Mateam=Team.Mateam and HuanLuyenVien.Mateam='" + cbbchonteam.SelectedValue + "'", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet lop = new DataSet();
            add.Fill(lop, "lop");
            cbbmagt.DataSource = lop.Tables["lop"];
            cbbmagt.DisplayMember = "TenHLV";
            cbbmagt.ValueMember = "TenHLV";
            con.Close();
        }
        private DataTable table;
        private void cbbmagt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
        public string Masinh()
        {
            string sql = @"select * from Luong";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlDataAdapter ds = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
            string ma = "";
            if (dt.Rows.Count <= 0)
            {
                ma = "LgH001";
            }
            else
            {
                int k;
                ma = "LgH";
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

        private void btnnhap_Click(object sender, EventArgs e)
        {
            int chamcong = Convert.ToInt32(txtchamcong.Text);
            int luong = chamcong * 500000;
            txtluong.Text = luong.ToString();
            int phat = Convert.ToInt32(txtphat.Text);
            int tongluong = luong - phat;
            txttongluong.Text = tongluong.ToString();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO LuongHLV VALUES (@MaluongHLV,@MaHLV,@TenHLV,@Thoigiancap,@Chamcong,@Luong,@Phat,@Tongluong)", con);
            cmd.Parameters.AddWithValue("MaluongHLV", txtmaluong.Text);
            cmd.Parameters.AddWithValue("MaHLV", txtmagt.Text);
            cmd.Parameters.AddWithValue("TenHLV", cbbmagt.SelectedValue);
            cmd.Parameters.AddWithValue("Thoigiancap", dtptra.Value);
            cmd.Parameters.AddWithValue("Chamcong", txtchamcong.Text);
            cmd.Parameters.AddWithValue("Luong", txtluong.Text);
            cmd.Parameters.AddWithValue("Phat", txtphat.Text);
            cmd.Parameters.AddWithValue("Tongluong", txttongluong.Text);
            MessageBox.Show("Nhập thành công");
            cmd.ExecuteNonQuery();
            con.Close();
            Hienthi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from LuongHLV where MaluongHLV=@MaluongHLV", con);
            cmd.Parameters.AddWithValue("MaluongHLV", txtmaluong.Text);
            MessageBox.Show("Xoá thành công");
            cmd.ExecuteNonQuery();
            con.Close();
            Hienthi();
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            LuongHLV_Load(sender, e);
            Hienthi();
            txtmaluong.Text = Masinh();
            txtchamcong.ResetText();
            txtluong.ResetText();
            txtmagt.ResetText();
      
            txtmaluong.Enabled = false;
            txtmagt.Enabled = false;
            txtphat.ResetText();
            txttongluong.ResetText();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cbbmagt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select HuanLuyenVien.* from HuanLuyenVien where TenHLV=@TenHLV", con);
            cmd.Parameters.AddWithValue("TenHLV", cbbmagt.SelectedValue);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet lop = new DataSet();
            add.Fill(lop, "l");
            table = lop.Tables["l"];
            if (table.Rows.Count == 0)
            {

            }
            else
            {
                txtmagt.Text = table.Rows[0]["MaHLV"].ToString();
            }
            con.Close();
        }

        private void quayLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameThu form2 = new GameThu();
            form2.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            cbbmagt.Text = dataGridView1.Rows[n].Cells["TenHLV"].Value.ToString();
            txtchamcong.Text = dataGridView1.Rows[n].Cells["Chamcong"].Value.ToString();
            txtluong.Text = dataGridView1.Rows[n].Cells["Luong"].Value.ToString();
            txtmaluong.Text = dataGridView1.Rows[n].Cells["MaluongHLV"].Value.ToString();
            txtphat.Text = dataGridView1.Rows[n].Cells["Phat"].Value.ToString();
            txttongluong.Text = dataGridView1.Rows[n].Cells["Tongluong"].Value.ToString();
            dtptra.Text = dataGridView1.Rows[n].Cells["Thoigiancap"].Value.ToString();
            txtmagt.Text = dataGridView1.Rows[n].Cells["MaHLV"].Value.ToString();

        }
    }
}
