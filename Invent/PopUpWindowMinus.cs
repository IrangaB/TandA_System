using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invent
{
    public partial class PopUpWindowMinus : Form
    {
        public PopUpWindowMinus(string minusPopUp)
        {
            InitializeComponent();
        }

        private void PopUpWindowMinus_Load(object sender, EventArgs e)
        {
            string strPopUp = TimeAndAction.popUpString;
            lblPlus.Text = "Delivery Date Push Backed By: " + strPopUp + " Days, Bukle up, need to work on dates. 🙁🙁🙁🙁 ";

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
