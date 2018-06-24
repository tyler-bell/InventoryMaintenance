using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace InventoryMaintenance
{
    public partial class frmInvMaint : Form
	{
		public frmInvMaint()
		{
			InitializeComponent();   
		}

        private int check = 0;      //flag so XML doesn't get stuck open

        // Add a statement here that declares the list of items.
        List<InvItem> invItems = new List<InvItem>();

        private void CheckXML() {
            if (!File.Exists("InventoryItems.xml")){
                // create the XmlWriterSettings object
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("    ");

                // create the XmlWriter object
                XmlWriter xmlOut = XmlWriter.Create("InventoryItems.xml", settings);

                // write the start of the document
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Items");

                // write the end tag for the root element
                xmlOut.WriteEndElement();

                // close the xmlWriter object
                xmlOut.Close();
            }
        }

        private void frmInvMaint_Load(object sender, EventArgs e)
		{
            while (check == 0) {        //checking if XML exists only once on load
                CheckXML();             //calls method
                check = 1;              //sets flag so it won't be called again
            }

            // Add a statement here that gets the list of items.
            foreach (InvItem temp in InvItemDB.GetItems()) {      //copy list from InvItemDB into invItem
                invItems.Add(temp);
            }
            FillItemListBox();
        }

		private void FillItemListBox()
		{
			lstItems.Items.Clear();
            // Add code here that loads the list box with the items in the list.
            foreach (InvItem temp in invItems) {                //loop through invItem list
                lstItems.Items.Add(temp.GetDisplayText());      //display each string in the list
            }
        }

		private void btnAdd_Click(object sender, EventArgs e)
		{
            // Add code here that creates an instance of the New Item form
            frmNewItem f2 = new frmNewItem();
            f2.ShowDialog();
            // and then gets a new item from that form.
            if (f2.GetNewItem() != null) {
                invItems.Add(f2.GetNewItem());
                InvItemDB.SaveItems(invItems);
                FillItemListBox();
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
		{
			int i = lstItems.SelectedIndex;
			if (i != -1)
			{
                // Add code here that displays a dialog box to confirm
                // the deletion and then removes the item from the list,
                // saves the list of products, and refreshes the list box
                // if the deletion is confirmed.
                DialogResult result = MessageBox.Show("Are you sure to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    invItems.RemoveAt(i);
                    InvItemDB.SaveItems(invItems);
                    FillItemListBox();
                } else {
                    
                }
            }
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}