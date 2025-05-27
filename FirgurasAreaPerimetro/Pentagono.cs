using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirgurasAreaPerimetro
{
    class Pentagono
    {
        private float mLado;
        private float mApotema;
        private float mPerimetro;
        private float mArea;
        private const float SF = 20;
        private float mAngulo = 0.0f;
        private float mZoom = 1.0f;

        public void ReadData(TextBox txtLado, TextBox txtApotema)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLado.Text) || string.IsNullOrWhiteSpace(txtApotema.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje de Error");
                    return;
                }

                if (!float.TryParse(txtLado.Text, out mLado) || mLado <= 0 ||
                    !float.TryParse(txtApotema.Text, out mApotema) || mApotema <= 0)
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

            if (mLado <= 0)
                return;

            Graphics g = picCanvas.CreateGraphics();
            Pen pen = new Pen(Color.DarkBlue, 3);

            float radio = mLado * SF * mZoom / (2 * (float)Math.Sin(Math.PI / 5));
            float cx = picCanvas.Width / 2f;
            float cy = picCanvas.Height / 2f;

            // Convierte el ángulo a radianes
            float anguloRad = mAngulo * (float)Math.PI / 180f;

            PointF[] puntos = new PointF[5];
            for (int i = 0; i < 5; i++)
            {
                double angulo = -Math.PI / 2 + i * 2 * Math.PI / 5 + anguloRad;
                puntos[i] = new PointF(
                    cx + radio * (float)Math.Cos(angulo),
                    cy + radio * (float)Math.Sin(angulo)
                );
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
            if (zoom < 0.1f) zoom = 0.1f;
            if (zoom > 5.0f) zoom = 5.0f; 
            mZoom = zoom;
        }

        public void PerimeterPentagono()
        {
            mPerimetro = 5 * mLado;
        }

        public void AreaPentagono()
        {
            mArea = (mPerimetro * mApotema) / 2;
        }

        public void PrintData(TextBox txtLado, TextBox txtApotema, TextBox txtPerimetro, TextBox txtArea)
        {
            txtPerimetro.Text = mPerimetro.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtLado, TextBox txtApotema, TextBox txtPerimetro, TextBox txtArea)
        {
            mLado = mApotema = mPerimetro = mArea = 0.0f;

            txtLado.Text = "";
            txtApotema.Text = "";
            txtPerimetro.Text = "";
            txtArea.Text = "";

            txtLado.Focus();
        }
    }
}