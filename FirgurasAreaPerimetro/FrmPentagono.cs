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
    public partial class FrmPentagono : Form
    {
        private static FrmPentagono instancia;
        private Pentagono ObjPentagono = new Pentagono();

        public FrmPentagono()
        {
            InitializeComponent();
        }

        public static FrmPentagono ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FrmPentagono();
            }
            return instancia;
        }

        private void FrmPentagono_Load(object sender, EventArgs e)
        {
            ObjPentagono.InitializeData(txtLado, txtApotema, txtPerimetro, txtArea);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            ObjPentagono.ReadData(txtLado, txtApotema);
            ObjPentagono.PerimeterPentagono();
            ObjPentagono.AreaPentagono();
            ObjPentagono.PrintData(txtLado, txtApotema, txtPerimetro, txtArea);
            picCanvas.Invalidate();
            ObjPentagono.PlotShape(picCanvas);
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            ObjPentagono.InitializeData(txtLado, txtApotema, txtPerimetro, txtArea);
            picCanvas.Invalidate();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            ObjPentagono.Rotar(-5f); // 5 grados antihorario
            picCanvas.Invalidate();
            ObjPentagono.PlotShape(picCanvas);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            ObjPentagono.Rotar(5f); // 5 grados horario
            picCanvas.Invalidate();
            ObjPentagono.PlotShape(picCanvas);
        }

        private void hsZoom_Scroll(object sender, ScrollEventArgs e)
        {
            float zoom = hsZoom.Value / 10.0f;
            ObjPentagono.SetZoom(zoom);
            picCanvas.Invalidate();
            ObjPentagono.PlotShape(picCanvas);
        }
    }
}
