using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirgurasAreaPerimetro
{
    class Rombo
    {
        private float mLado;
        private float mLargo; // Diagonal mayor
        private float mAncho; // Diagonal menor
        private float mPerimetro;
        private float mArea;
        private const float SF = 10;
        private float mAngulo = 0.0f;
        private float mZoom = 1.0f;

        public void ReadData(TextBox txtLado, TextBox txtLargo, TextBox txtAncho)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLado.Text) || string.IsNullOrWhiteSpace(txtLargo.Text) ||
                    string.IsNullOrWhiteSpace(txtAncho.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje de Error");
                    return;
                }

                if (!float.TryParse(txtLado.Text, out mLado) || mLado <= 0 ||
                    !float.TryParse(txtLargo.Text, out mLargo) || mLargo <= 0 ||
                    !float.TryParse(txtAncho.Text, out mAncho) || mAncho <= 0)
                {
                    MessageBox.Show("Por favor, ingresa valores numéricos positivos válidos.", "Error de entrada");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un error.", "Error");
            }
        }

        public void PlotShape(PictureBox picCanvas)
        {
            picCanvas.Refresh();

            if (mLargo <= 0 || mAncho <= 0)
                return;

            Graphics g = picCanvas.CreateGraphics();
            Pen pen = new Pen(Color.DarkBlue, 3);

            float drawLargo = mLargo * SF * mZoom;
            float drawAncho = mAncho * SF * mZoom;

            float cx = picCanvas.Width / 2f;
            float cy = picCanvas.Height / 2f;

            // Calcula los 4 vértices del rombo antes de rotar
            PointF[] puntos = new PointF[4];
            puntos[0] = new PointF(cx, cy - drawLargo / 2); // Arriba
            puntos[1] = new PointF(cx + drawAncho / 2, cy); // Derecha
            puntos[2] = new PointF(cx, cy + drawLargo / 2); // Abajo
            puntos[3] = new PointF(cx - drawAncho / 2, cy); // Izquierda

            // Aplica la rotación a cada punto
            float anguloRad = mAngulo * (float)Math.PI / 180f;
            for (int i = 0; i < puntos.Length; i++)
            {
                float dx = puntos[i].X - cx;
                float dy = puntos[i].Y - cy;
                float xRot = cx + dx * (float)Math.Cos(anguloRad) - dy * (float)Math.Sin(anguloRad);
                float yRot = cy + dx * (float)Math.Sin(anguloRad) + dy * (float)Math.Cos(anguloRad);
                puntos[i] = new PointF(xRot, yRot);
            }

            g.DrawPolygon(pen, puntos);
        }

        public void Rotar(float grados)
        {
            mAngulo += grados;
            if (mAngulo >= 360f) mAngulo -= 360f;
            if (mAngulo < 0f) mAngulo += 360f;
        }

        public void SetZoom(float zoom)
        {
            if (zoom < 0.1f) zoom = 0.1f; // Evita valores demasiado pequeños
            if (zoom > 5.0f) zoom = 5.0f; // Evita valores demasiado grandes
            mZoom = zoom;
        }

        public void PerimeterRombo()
        {
            mPerimetro = 4 * mLado;
        }

        public void AreaRombo()
        {
            mArea = (mLargo * mAncho) / 2;
        }

        public void PrintData(TextBox txtLado, TextBox txtLargo, TextBox txtAncho, TextBox txtPerimetro, TextBox txtArea)
        {
            txtPerimetro.Text = mPerimetro.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtLado, TextBox txtLargo, TextBox txtAncho, TextBox txtPerimetro, TextBox txtArea)
        {
            mLado = mLargo = mAncho = mPerimetro = mArea = 0.0f;

            txtLado.Text = "";
            txtLargo.Text = "";
            txtAncho.Text = "";
            txtPerimetro.Text = "";
            txtArea.Text = "";

            txtLado.Focus();
        }


    }
}