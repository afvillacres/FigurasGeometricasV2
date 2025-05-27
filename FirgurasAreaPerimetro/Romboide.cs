using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirgurasAreaPerimetro
{
    class Romboide
    {
        private float mLado;
        private float mBase;
        private float mAltura;
        private float mPerimetro;
        private float mArea;
        private const float SF = 15;
        private float mAngulo = 0.0f;
        private float mZoom = 1.0f;

        public void ReadData(TextBox txtLado, TextBox txtBase, TextBox txtAltura)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLado.Text) || string.IsNullOrWhiteSpace(txtBase.Text) ||
                    string.IsNullOrWhiteSpace(txtAltura.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje de Error");
                    return;
                }

                if (!float.TryParse(txtLado.Text, out mLado) || mLado <= 0 ||
                    !float.TryParse(txtBase.Text, out mBase) || mBase <= 0 ||
                    !float.TryParse(txtAltura.Text, out mAltura) || mAltura <= 0)
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

            if (mBase <= 0 || mAltura <= 0 || mLado <= 0)
                return;

            Graphics g = picCanvas.CreateGraphics();
            Pen pen = new Pen(Color.DarkBlue, 3);

            float drawBase = mBase * SF * mZoom;
            float drawAltura = mAltura * SF * mZoom;
            float drawLado = mLado * SF * mZoom;

            float cx = picCanvas.Width / 2f;
            float cy = picCanvas.Height / 2f;

            // Desfase horizontal para los lados inclinados
            float offset = Math.Max(drawLado - drawBase, 20);

            // Calcula los 4 vértices del romboide antes de rotar
            PointF[] puntos = new PointF[4];
            puntos[0] = new PointF(cx - drawBase / 2 + offset, cy - drawAltura / 2); // Superior izquierdo
            puntos[1] = new PointF(cx + drawBase / 2 + offset, cy - drawAltura / 2); // Superior derecho
            puntos[2] = new PointF(cx + drawBase / 2, cy + drawAltura / 2);          // Inferior derecho
            puntos[3] = new PointF(cx - drawBase / 2, cy + drawAltura / 2);          // Inferior izquierdo

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

        public void PerimeterRomboide()
        {
            mPerimetro = 2 * (mLado + mBase);
        }

        public void AreaRomboide()
        {
            mArea = mBase * mAltura;
        }

        public void PrintData(TextBox txtLado, TextBox txtBase, TextBox txtAltura, TextBox txtPerimetro, TextBox txtArea)
        {
            txtPerimetro.Text = mPerimetro.ToString("0.00");
            txtArea.Text = mArea.ToString("0.00");
        }

        public void InitializeData(TextBox txtLado, TextBox txtBase, TextBox txtAltura, TextBox txtPerimetro, TextBox txtArea)
        {
            mLado = mBase = mAltura = mPerimetro = mArea = 0.0f;

            txtLado.Text = "";
            txtBase.Text = "";
            txtAltura.Text = "";
            txtPerimetro.Text = "";
            txtArea.Text = "";

            txtLado.Focus();
        }
    }
}