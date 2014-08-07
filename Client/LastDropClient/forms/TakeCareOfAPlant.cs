﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TrayApplicationTest
{
    [CallbackBehavior(UseSynchronizationContext=false)]
    public partial class TakeCareOfAPlant : Form, IMyServiceCallback
    {
        SynchronizationContext _SyncContext = null;
        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        private string mail;
        private string password;
        
        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            //MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(this, netTcp, new EndpointAddress(Properties.Settings.Default.serverIP));
            _SyncContext = SynchronizationContext.Current;
            serviceProvider = netTcpFactory.CreateChannel();
            serviceProvider.subscribe();
        }
        public void OnCallback()
        {
            Task.Factory.StartNew(() =>
            {
                MessageBox.Show("A user modified the plant list");
            });     
        }
        public TakeCareOfAPlant(string mail, string password)
        {
            this.mail = mail;
            this.password = password;
            ServerConnect();
            InitializeComponent();
        }

        private void TakeCareOfAPlant_Load(object sender, EventArgs e)
        {
            getAvailablePlants();
            getSubscribedPlants();
        }

        private void TakeCareOfAPlant_FormClosed(object sender, FormClosedEventArgs e)
        {
            serviceProvider.unsubscribe();
            netTcpFactory.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i)==true)
                {
                    string plantName = checkedListBox1.Items[i].ToString();                   
                    serviceProvider.unsubscribeToPlant(mail, plantName);
                }          
            }
            getSubscribedPlants();
            getAvailablePlants();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {           
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i) == true)
                {
                    string plantName = checkedListBox2.Items[i].ToString();
                    
                    serviceProvider.subscribeToPlant(mail, plantName);
                }
            }
            getAvailablePlants();
            getSubscribedPlants();
        }

        public void getSubscribedPlants() 
        {
            checkedListBox1.Items.Clear();
            string subscribed = serviceProvider.getSubscribedPlants(mail, password);             
            var serializer2 = new XmlSerializer(typeof(List<Plant>));
            List<Plant> subscribedPlantList;
            using (TextReader reader = new StringReader(subscribed))
            {
                subscribedPlantList = (List<Plant>)serializer2.Deserialize(reader);
            }
            foreach (Plant plant in subscribedPlantList)
            {
                checkedListBox1.Items.Add(plant.Name);
            }
        }

        public void getAvailablePlants() 
        {
            checkedListBox2.Items.Clear();
            string list = serviceProvider.getAvailablePlants(mail, password);
            var serializer = new XmlSerializer(typeof(List<Plant>));
            List<Plant> plantList;
            using (TextReader reader = new StringReader(list))
            {
                plantList = (List<Plant>)serializer.Deserialize(reader);
            }
            foreach (Plant plant in plantList)
            {
                checkedListBox2.Items.Add(plant.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getAvailablePlants();
            getSubscribedPlants();
        }




    }
}