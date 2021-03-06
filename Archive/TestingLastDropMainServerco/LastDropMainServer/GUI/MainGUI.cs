﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LastDropMainServer
{
    public partial class MainGUI : Form
    {
        private int buttonStatus = 0;
        private ServiceInitiation services;
        private OperationController controller;

        private MainGUI() { }

        public MainGUI(OperationController operationController)
        {
            this.CenterToScreen();
            InitializeComponent();
            this.controller = operationController;
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            services = new ServiceInitiation();

            statusTextBox.Text += "=======================================\n";
            statusTextBox.Text += "Server Application has been initialized succesfully.\n";
            statusTextBox.Text += "=======================================\n";
        }

        private void serviceLaunchButton_Click(object sender, EventArgs e)
        {
            serviceLaunchButton.Enabled = false;
            if (buttonStatus == 0)
            {
                services.startServices();
                this.statusTextBox.Text += "Service Provider has been started.\n";
                serviceLaunchButton.Text = "Stop Service";
                buttonStatus = 1;
            }
            else
            {
                services.stopServices();
                this.statusTextBox.Text += "Service provider has been stopped.\n";
                serviceLaunchButton.Text = "Start Service";
                buttonStatus = 0;
            }
            serviceLaunchButton.Enabled = true;
        }

        private void MainGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
