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
    public partial class FrmRomboide : Form
    {
        private static FrmRomboide instancia;
        private Romboide ObjRomboide = new Romboide();

        public FrmRomboide()
        {
            InitializeComponent();
        }

        public static FrmRomboide ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FrmRomboide();
            }
            return instancia;
        }

        private void FrmRomboide_Load(object sender, EventArgs e)
        {
            ObjRomboide.InitializeData(txtLado, txtBase, txtAltura, txtPerimetro, txtArea);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            ObjRomboide.ReadData(txtLado, txtBase, txtAltura);
            ObjRomboide.PerimeterRomboide();
            ObjRomboide.AreaRomboide();
            ObjRomboide.PrintData(txtLado, txtBase, txtAltura, txtPerimetro, txtArea);
            picCanvas.Invalidate();
            ObjRomboide.PlotShape(picCanvas);
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            ObjRomboide.InitializeData(txtLado, txtBase, txtAltura, txtPerimetro, txtArea);
            picCanvas.Invalidate();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            ObjRomboide.Rotar(-5f); // 5 grados antihorario
            picCanvas.Invalidate();
            ObjRomboide.PlotShape(picCanvas);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            ObjRomboide.Rotar(5f); // 5 grados horario
            picCanvas.Invalidate();
            ObjRomboide.PlotShape(picCanvas);
        }

        private void hsZoom_Scroll(object sender, ScrollEventArgs e)
        {
            float zoom = hsZoom.Value / 10.0f;
            ObjRomboide.SetZoom(zoom);
            picCanvas.Invalidate();
            ObjRomboide.PlotShape(picCanvas);
        }
    }
}