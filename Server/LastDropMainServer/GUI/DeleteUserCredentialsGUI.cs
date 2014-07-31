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
    public partial class DeleteUserCredentialsGUI : Form
    {
        private OperationController controller;

        public DeleteUserCredentialsGUI(OperationController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        private void DeleteUserCredentialsGUI_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lastDropDBDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.lastDropDBDataSet.Users);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete this user's credentials?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.usersTableAdapter.Update(this.lastDropDBDataSet);
            }
            else if (dialogResult == DialogResult.No)
            {
                this.usersTableAdapter.Fill(this.lastDropDBDataSet.Users);
            }

            this.controller.updateDatabase();
        }
    }
}
