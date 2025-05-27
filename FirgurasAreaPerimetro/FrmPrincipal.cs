using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirgurasAreaPerimetro
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }


        private void romboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRombo frmRombo = FrmRombo.ObtenerInstancia();
            frmRombo.MdiParent = this;
            if (!frmRombo.Visible)
            {
                frmRombo.Show();
            }
            else
            {
                frmRombo.BringToFront();
            }
        }

        private void romboideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRomboide frmRomboide = FrmRomboide.ObtenerInstancia();
            frmRomboide.MdiParent = this;
            if (!frmRomboide.Visible)
            {
                frmRomboide.Show();
            }
            else
            {
                frmRomboide.BringToFront();
            }
        }


        private void trapezoideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTrapezoide frmTrapezoide = FrmTrapezoide.ObtenerInstancia();
            frmTrapezoide.MdiParent = this;
            if (!frmTrapezoide.Visible)
            {
                frmTrapezoide.Show();
            }
            else
            {
                frmTrapezoide.BringToFront();
            }
        }

        private void pentagonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPentagono frmPentagono = FrmPentagono.ObtenerInstancia();
            frmPentagono.MdiParent = this;
            if (!frmPentagono.Visible)
            {
                frmPentagono.Show();
            }
            else
            {
                frmPentagono.BringToFront();
            }
        }

    }
}