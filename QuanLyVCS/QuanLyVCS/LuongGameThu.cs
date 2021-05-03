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
    public partial class LuongGameThu : Form
    {
        String conn = @"Data Source=ADMIN-2N12AHLMA\SQLEXPRESS;Initial Catalog=QuanLyGT;Integrated Security=True";
        public LuongGameThu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameThu form1 = new GameThu();
            form1.Show();
            this.Hienthi();
        }
        void Hienthi()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Luong", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void LuongGameThu_Load(object sender, EventArgs e)
        {
            Hienthi();
            getteam2();
            getteam();
            txtmagt.Enabled = false;
            txtmaluong.Text = Masinh();
            txtmaluong.Enabled = false;
            txttongluong.Enabled = false;
            txtluong.Enabled = false;
            txtthuong.Enabled = false;
        }

        private void cbbchonteam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select GameThu.*,Team.* from GameThu,Team where GameThu.Mateam=Team.Mateam and GameThu.Mateam='" + cbbchonteam.SelectedValue + "'", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet lop = new DataSet();
            add.Fill(lop, "lop");
            cbbmagt.DataSource = lop.Tables["lop"];
            cbbmagt.DisplayMember = "Hoten";
            cbbmagt.ValueMember = "Hoten";
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
        private void getteam2()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Team", con);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet team = new DataSet();
            add.Fill(team, "team");
            comboBox1.DataSource = team.Tables["team"];
            comboBox1.DisplayMember = "Tenteam";
            comboBox1.ValueMember = "Mateam";
            con.Close();
        }
        private DataTable table;
        private void cbbmagt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("select GameThu.* from GameThu where Hoten=@Hoten", con);
            cmd.Parameters.AddWithValue("Hoten", cbbmagt.SelectedValue);
            SqlDataAdapter add = new SqlDataAdapter(cmd);
            DataSet lop = new DataSet();
            add.Fill(lop, "l");
            table = lop.Tables["l"];
            if(table.Rows.Count==0)
            {
                
            }
            else
            {
                txtmagt.Text = table.Rows[0]["MaGT"].ToString();
            }
            con.Close();
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
                ma = "Lg001";
            }
            else
            {
                int k;
                ma = "Lg";
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


        private void btnnhap_Click(object sender, EventArgs e)
        {
            int chamcong = Convert.ToInt32(txtchamcong.Text);
            int luong = chamcong * 500000;
            txtluong.Text = luong.ToString();
            int somvp = Convert.ToInt32(txtsomvp.Text);
            txtsomvp.Text = somvp.ToString();
            int thuong = somvp * 500000;   
            txtthuong.Text = thuong.ToString();
            int phat = Convert.ToInt32(txtphat.Text);
            int tongluong = luong + thuong - phat;
            txttongluong.Text = tongluong.ToString();
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Luong VALUES (@Maluong,@MaGT,@Hoten,@Thoigiancap,@Chamcong,@Luong,@Thuong,@Phat,@Tongluong)", con);
            cmd.Parameters.AddWithValue("Maluong", txtmaluong.Text);
            if (txtmagt.Text != "")
            {
                cmd.Parameters.AddWithValue("MaGT", txtmagt.Text);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã game thủ");
                return;
            }
            
            cmd.Parameters.AddWithValue("Hoten",cbbmagt.SelectedValue);
            cmd.Parameters.AddWithValue("Thoigiancap", dtptra.Value);
            if (txtchamcong.Text != "")
            {
                cmd.Parameters.AddWithValue("Chamcong", txtchamcong.Text);
            }
            else
            {
                MessageBox.Show("bạn chưa nhập chấm công");
                return;
            }
       
                cmd.Parameters.AddWithValue("Luong", txtluong.Text);
    
                cmd.Parameters.AddWithValue("Thuong", txtthuong.Text);
       
            if (txtphat.Text != "")
            {
                cmd.Parameters.AddWithValue("Phat", txtphat.Text);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập phạt");
                return;
            }
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
            SqlCommand cmd = new SqlCommand("delete from Luong where Maluong=@Maluong", con);
            cmd.Parameters.AddWithValue("Maluong", txtmaluong.Text);
            MessageBox.Show("Xoá thành công");
            cmd.ExecuteNonQuery();
            con.Close();
            Hienthi();
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

                dtptra.Value = Convert.ToDateTime(dataGridView1.Rows[VT].Cells[3].Value.ToString());
                cbbmagt.Text = dataGridView1.Rows[VT].Cells[1].Value.ToString();
                txtmagt.Text = dataGridView1.Rows[VT].Cells[2].Value.ToString();
                txtmaluong.Text = dataGridView1.Rows[VT].Cells[0].Value.ToString();
                txtchamcong.Text = dataGridView1.Rows[VT].Cells[4].Value.ToString();
                txtluong.Text = dataGridView1.Rows[VT].Cells[5].Value.ToString();
                txtthuong.Text = dataGridView1.Rows[VT].Cells[6].Value.ToString();
                txtphat.Text = dataGridView1.Rows[VT].Cells[7].Value.ToString();
                txttongluong.Text = dataGridView1.Rows[VT].Cells[8].Value.ToString();

            }
            catch (Exception e) { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Luong.* from Luong,GameThu where GameThu.MaGT=Luong.MaGT and GameThu.Mateam='" + comboBox1.SelectedValue + "' ", con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1, "ti");
            dataGridView1.DataSource = ds1.Tables["ti"];
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hienthi();
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            LuongGameThu_Load(sender, e);
            Hienthi();
            txtmaluong.Text = Masinh();
            txtchamcong.ResetText();
            txtluong.ResetText();
            txtmagt.ResetText();
         
            txtphat.ResetText();
            txtsomvp.ResetText();
            txtthuong.ResetText();
            txttongluong.ResetText();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n;
            n = e.RowIndex;
            cbbmagt.Text = dataGridView1.Rows[n].Cells["Hoten"].Value.ToString();
            
        }
    }
}
