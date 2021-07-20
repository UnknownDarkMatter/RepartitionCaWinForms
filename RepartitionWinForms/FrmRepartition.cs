using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepartitionWinForms
{
    public partial class FrmRepartition : Form
    {
        public DataGridView DataGridView;

        public FrmRepartition()
        {
            InitializeComponent();
            DataGridView = dataGridView1;
        }
    }
}
