using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TrayApplicationTest
{
    public partial class RegisterForm : KryptonForm
    {
        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        bool result;

        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(Properties.Settings.Default.serverIP));

            serviceProvider = netTcpFactory.CreateChannel();
        }     
       
        public RegisterForm()
        {
            ServerConnect();
            InitializeComponent();
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            netTcpFactory.Close();
            MainForm mainForm = new MainForm();
            mainForm.Visible = true;
        }

        public Boolean validateMail(string mail)
        {
            Regex r = new Regex("^[a-zA-Z0-9@_.]*$");
            if (r.IsMatch(mail))
                return true;
            return false;
        }

        public Boolean validatePassword(string password)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(password))
                return true;
            return false;
        }

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            return (!string.IsNullOrEmpty(emailAddress)) && ValidEmailRegex.IsMatch(emailAddress);
        }

        public void ResetForm()
        {
            textBox2.Text = "";
            textBox1.Text = "";
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (EmailIsValid(textBox1.Text))
            {
                if (validateMail(textBox1.Text) && validatePassword(textBox2.Text))
                {

                    string p = SHA1Util.SHA1HashStringForUTF8String(textBox2.Text);
                    try
                    {

                        result = serviceProvider.registerUser(textBox1.Text, p);
                    }
                    catch (Exception ex)
                    {
                        KryptonMessageBox.Show("Server unavailable. Try again later.");
                        this.Dispose();
                        System.Windows.Forms.Application.Exit();
                        Application.Exit();
                        System.Environment.Exit(1);
                    }
                    if (result == true)
                    {
                        this.Hide();
                        LoginForm loginForm = new LoginForm();
                        loginForm.Visible = true;
                    }
                    if (result == false)
                    {
                        KryptonMessageBox.Show("The registration failed ! Try again.");
                        ResetForm();
                    }
                }
                else
                {
                    KryptonMessageBox.Show("Please use only letters and numbers for password !" + "\n" + " Please input only letters, number, underscore and point for email !");
                    ResetForm();
                }
            }
            else
            {
                KryptonMessageBox.Show("Please input a valid email address !");
                ResetForm();
            }
        }
    }
}
