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
    public partial class PopUpWindow : Form
    {
        public PopUpWindow(string popScreen)
        {
            InitializeComponent();
        }

        private void PopUpWindow_Load(object sender, EventArgs e)
        {
            string screenText = TimeAndAction.popUpString;
            lblPlus.Text = "Delivery Date Advanced By: " + screenText + " Days, Enjoy your free time 😀😀😀😀 ";
        }

        private void lblPlus_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
