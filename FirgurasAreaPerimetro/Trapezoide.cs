using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirgurasAreaPerimetro
{
    class Trapezoide
    {
        private float ladoA, ladoB, ladoC, ladoD, altura;
        private float perimetro, area;
        private const float SF = 15;
        private float mAngulo = 0.0f;
        private float mZoom = 1.0f;

        public void ReadData(TextBox txtA, TextBox txtB, TextBox txtC, TextBox txtD, TextBox txtAltura)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtA.Text) || string.IsNullOrWhiteSpace(txtB.Text) ||
                    string.IsNullOrWhiteSpace(txtC.Text) || string.IsNullOrWhiteSpace(txtD.Text) ||
                    string.IsNullOrWhiteSpace(txtAltura.Text))
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje de Error");
                    return;
                }

                if (!float.TryParse(txtA.Text, out ladoA) || ladoA <= 0 ||
                    !float.TryParse(txtB.Text, out ladoB) || ladoB <= 0 ||
                    !float.TryParse(txtC.Text, out ladoC) || ladoC <= 0 ||
                    !float.TryParse(txtD.Text, out ladoD) || ladoD <= 0 ||
                    !float.TryParse(txtAltura.Text, out altura) || altura <= 0)
                {
                    MessageBox.Show("Por favor, ingresa valores numéricos positivos válidos.", "Error");
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

            if (ladoA <= 0 || ladoB <= 0 || ladoC <= 0 || ladoD <= 0 || altura <= 0)
                return;

            Graphics g = picCanvas.CreateGraphics();
            Pen pen = new Pen(Color.DarkBlue, 3);

            float drawA = ladoA * SF * mZoom;
            float drawC = ladoC * SF * mZoom;
            float drawAltura = altura * SF * mZoom;

            float cx = picCanvas.Width / 2f;
            float cy = picCanvas.Height / 2f;

            float topY = cy - drawAltura / 2;
            float bottomY = cy + drawAltura / 2;

            float topLeftX = cx - drawA / 2;
            float topRightX = cx + drawA / 2;
            float bottomLeftX = cx - drawC / 2;
            float bottomRightX = cx + drawC / 2;

            PointF[] puntos = new PointF[4];
            puntos[0] = new PointF(topLeftX, topY);      // Arriba izquierda
            puntos[1] = new PointF(topRightX, topY);     // Arriba derecha
            puntos[2] = new PointF(bottomRightX, bottomY); // Abajo derecha
            puntos[3] = new PointF(bottomLeftX, bottomY);  // Abajo izquierda

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

        public void PerimeterTrapezoide()
        {
            perimetro = ladoA + ladoB + ladoC + ladoD;
        }

        public void AreaTrapezoide()
        {
            area = ((ladoA + ladoC) * altura) / 2;
        }

        public void PrintData(TextBox txtPerimetro, TextBox txtArea)
        {
            txtPerimetro.Text = perimetro.ToString("0.00");
            txtArea.Text = area.ToString("0.00");
        }

        public void InitializeData(TextBox txtA, TextBox txtB, TextBox txtC, TextBox txtD, TextBox txtAltura, TextBox txtPerimetro, TextBox txtArea)
        {
            ladoA = ladoB = ladoC = ladoD = altura = perimetro = area = 0.0f;

            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";
            txtD.Text = "";
            txtAltura.Text = "";
            txtPerimetro.Text = "";
            txtArea.Text = "";

            txtA.Focus();
        }
    }
}
