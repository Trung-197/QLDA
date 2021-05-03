using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVCS
{
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
        }

        private void quảnLýTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLTeam form1 = new QLTeam();
            form1.Show();
            this.Hide();
        }

        private void quảnLýGameThủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameThu form2 = new GameThu();
            form2.Show();
            this.Hide();
        }

        private void quảnLýHLVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HopDongGameThu form3 = new HopDongGameThu();
            form3.Show();
            this.Hide();
        }
    }
}
