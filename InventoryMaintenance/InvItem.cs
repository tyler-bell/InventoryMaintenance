using System;

namespace InventoryMaintenance {
    public class InvItem {
        private int itemNo;             //Gets or sets an int that contains the item’s number.
        private string description;     //Gets or sets a string that contains the item’s description.
        private decimal price;          //Gets or sets a decimal that contains the item’s price. 

        public InvItem() {
        }

        public InvItem(int itemNo, string description, decimal price) {
            this.ItemNo = itemNo;
            this.Description = description;
            this.Price = price;
        }

        public decimal Price { get => price; set => price = value; }

        public string Description { get => description; set => description = value; }     //Gets or sets a string that contains the item’s description.

        public int ItemNo { get => itemNo; set => itemNo = value; }

        public string GetDisplayText() {                                                                                           //Returns a string that contains the item’s number, description, and price formatted like this:
            return itemNo.ToString() + "    " + description + "(" + price.ToString("C") + ")";      //3245649    Agapanthus ($7.95). (The item number and description are separated by four spaces.)
        }


    }
}
