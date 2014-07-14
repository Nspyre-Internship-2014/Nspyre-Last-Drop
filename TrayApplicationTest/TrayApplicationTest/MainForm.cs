using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayApplicationTest
{
    public partial class MainForm : Form
    {

        private Icon ico;

        public MainForm()
        {
            InitializeComponent();
            notifyIcon1.Visible = false;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ico = notifyIcon1.Icon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
            this.notifyIcon1.BalloonTipText = "Application has been minized to tray.";
            this.notifyIcon1.ShowBalloonTip(1000);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.BalloonTipText = "Login has been clicked.";
            this.notifyIcon1.ShowBalloonTip(1000);
            LoginForm loginForm = new LoginForm();
            loginForm.Visible = true;
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.BalloonTipText = "Register has been clicked.";
            this.notifyIcon1.ShowBalloonTip(1000);
            RegisterForm registerForm = new RegisterForm();
            registerForm.Visible = true;
        }
    }
}
