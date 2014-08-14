using System;
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
    public partial class DatabaseCRUDForm : Form
    {
        private OperationController controller;

        public DatabaseCRUDForm(OperationController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        private void DatabaseCRUDForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lastDropDBDataSet.Plants' table. You can move, or remove it, as needed.
            this.plantsTableAdapter.Fill(this.lastDropDBDataSet.Plants);
            // TODO: This line of code loads data into the 'lastDropDBDataSet.Plants' table. You can move, or remove it, as needed.
            this.plantsTableAdapter.Fill(this.lastDropDBDataSet.Plants);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.plantsTableAdapter.Update(this.lastDropDBDataSet);
                controller.updateDatabase();

                MessageBox.Show("Update completed succesfully.");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Update cannot be performed: " + exc.Message);
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.plantsTableAdapter.FillBy(this.lastDropDBDataSet.Plants);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

    }
}
