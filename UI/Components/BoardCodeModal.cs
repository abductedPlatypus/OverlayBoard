using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveSplit.UI.Components;

namespace TwopIt.LiveSplit.UI.Components
{
    public partial class BoardCodeModal : Form
    {
        public BoardCodeModal()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
