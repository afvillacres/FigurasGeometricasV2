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
    public partial class FrmRombo : Form
    {
        private static FrmRombo instancia;
        private Rombo ObjRombo = new Rombo();

        public FrmRombo()
        {
            InitializeComponent();
        }

        public static FrmRombo ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new FrmRombo();
            }
            return instancia;
        }

        private void FrmRombo_Load(object sender, EventArgs e)
        {
            ObjRombo.InitializeData(txtLado, txtLargo, txtAncho, txtPerimetro, txtArea);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            ObjRombo.ReadData(txtLado, txtLargo, txtAncho);
            ObjRombo.PerimeterRombo();
            ObjRombo.AreaRombo();
            ObjRombo.PrintData(txtLado, txtLargo, txtAncho, txtPerimetro, txtArea);
            picCanvas.Invalidate();
            ObjRombo.PlotShape(picCanvas);
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            ObjRombo.InitializeData(txtLado, txtLargo, txtAncho, txtPerimetro, txtArea);
            picCanvas.Invalidate();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            ObjRombo.Rotar(-5f); // 5 grados antihorario
            picCanvas.Invalidate();
            ObjRombo.PlotShape(picCanvas);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            ObjRombo.Rotar(5f); // 5 grados horario
            picCanvas.Invalidate();
            ObjRombo.PlotShape(picCanvas);
        }

        private void hsZoom_Scroll(object sender, ScrollEventArgs e)
        {
            float zoom = hsZoom.Value / 10.0f;
            ObjRombo.SetZoom(zoom);
            picCanvas.Invalidate();
            ObjRombo.PlotShape(picCanvas);
        }
    }
}