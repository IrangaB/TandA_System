﻿using System;
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
    public partial class styleDateRange: Form
    {
        public styleDateRange()
        {
            InitializeComponent();
        }
        bool menuExpand = false;

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void styleDateRange_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false)
            {
                menuContainer.Height += 10;
                if(menuContainer.Height >= 204)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if(menuContainer.Height <=44)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }
    }
}
