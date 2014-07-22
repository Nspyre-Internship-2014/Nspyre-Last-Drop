using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TrayApplicationTest
{
    public partial class TakeCareOfAPlant : Form
    {

        DuplexChannelFactory<IServicesWCF> netTcpFactory;
        IServicesWCF serviceProvider;
        private string mail;
        private string password;
        public void ServerConnect()
        {
            NetTcpBinding netTcp = new NetTcpBinding();
            MyServiceCallback serviceCallback = new MyServiceCallback();

            netTcp.Security.Mode = SecurityMode.None;
            netTcpFactory = new DuplexChannelFactory<IServicesWCF>(serviceCallback, netTcp, new EndpointAddress(
                  "net.tcp://10.33.92.62:8021/Service1/LastDropService"));

            serviceProvider = netTcpFactory.CreateChannel();
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
            Console.WriteLine(mail);
            Console.WriteLine(password);

            string list = serviceProvider.getAvailablePlants(mail,password);

            Console.WriteLine(list);
            var serializer = new XmlSerializer(typeof(List<Plant>));
            List<Plant> plantList;
            using (TextReader reader = new StringReader(list))
            {
                plantList = (List<Plant>)serializer.Deserialize(reader);
            }
            foreach (Plant plant in plantList)
            {
                Console.WriteLine(plant.ToString());
                checkedListBox1.Items.Add(plant.Name);
               
            }
        }



        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TakeCareOfAPlant_FormClosed(object sender, FormClosedEventArgs e)
        {
            netTcpFactory.Close();
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }




    }
}