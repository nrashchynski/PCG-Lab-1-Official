using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PCG_Lab_1_Official
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.Color = colorDisplayPanel.BackColor;
            cd.FullOpen = true;
            cd.AnyColor = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                Color color = cd.Color;
                RTextBox.Text = $"{color.R}";
                GTextBox.Text = $"{color.G}";
                BTextBox.Text = $"{color.B}";

                // updateColorModels(color);
                ConvertRgbToLabAndCmyk();
                colorDisplayPanel.BackColor = color;
            }
        }

        //private void updateColorModels(Color color)
        //{
        //    // RGB to CMYK
        //    var cmyk = RgbToCmyk(color.R, color.G, color.B);
        //    mTextBox.Text = $"{cmyk.C:F2}";
        //    cTextBox.Text = $"{cmyk.M:F2}";
        //    yTextBox.Text = $"{cmyk.Y:F2}";
        //    kTextBox.Text = $"{cmyk.K:F2}";

        //    //RGB to LAB
        //    var lab = RgbToLab(color.R, color.G, color.B);
        //    lTextBox.Text = $"{lab.l:F2}";
        //    aTextBox.Text = $"{lab.a:F2}";
        //    b_TextBox.Text = $"{lab.b:F2}";         
        //}

        private void ConvertRgbToLabAndCmyk()
        {
            int r = int.Parse(RTextBox.Text);
            int g = int.Parse(GTextBox.Text);
            int b = int.Parse(BTextBox.Text);

            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                MessageBox.Show("Значения RGB должны быть в диапазоне от 0 до 255");
                return;
            }

            var (L, A, B) = RgbToLab(r, g, b);

            lTextBox.Text = L.ToString("F2");
            aTextBox.Text = A.ToString("F2");
            b_TextBox.Text = B.ToString("F2");

            var (C, M, Y, K) = RgbToCmyk(r, g, b);

            cTextBox.Text = C.ToString("F2");
            mTextBox.Text = M.ToString("F2");
            yTextBox.Text = Y.ToString("F2");
            kTextBox.Text = K.ToString("F2");

            colorDisplayPanel.BackColor = Color.FromArgb(r, g, b);
            UpdateRgbTrackBars(r, g, b);
            UpdateLabTrackBars((int)L, (int)A + 128, (int)B + 128);
            UpdateCmykTrackBars((int)C, (int)M, (int)Y, (int)K);
        }

        private void ConvertLabToRgbAndCmyk()
        {
            int L = int.Parse(lTextBox.Text);
            int A = int.Parse(aTextBox.Text);
            int B = int.Parse(b_TextBox.Text);

            if (L < 0 || L > 100 || A < -128 || A > 127 || B < -128 || B > 127)
            {
                MessageBox.Show("Ваши значения LAB не попадают в заданный диапазон");
                return;
            }

            var (r, g, b) = LabToRgb(L, A, B);

            RTextBox.Text = r.ToString("F2");
            GTextBox.Text = g.ToString("F2");
            BTextBox.Text = b.ToString("F2");

            var (C, M, Y, K) = LabToCmyk(L, A, B);

            cTextBox.Text = C.ToString("F2");
            mTextBox.Text = M.ToString("F2");
            yTextBox.Text = Y.ToString("F2");
            kTextBox.Text = K.ToString("F2");

            colorDisplayPanel.BackColor = Color.FromArgb(r, g, b);
            UpdateRgbTrackBars(r, g, b);
            UpdateLabTrackBars((int)L, (int)A + 128, (int)B + 128);
            UpdateCmykTrackBars((int)C, (int)M, (int)Y, (int)K);
        }

        private void ConvertCmykToRgbAndLab()
        {
            int c = int.Parse(cTextBox.Text);
            int m = int.Parse(mTextBox.Text);
            int y = int.Parse(yTextBox.Text);
            int k = int.Parse(kTextBox.Text);

            if (c < 0 || c > 100 || m < 0 || m > 100 || y < 0 || y > 100 || k < 0 || k > 100)
            {
                MessageBox.Show("Значения CMYK должны попадать в диапазон от 0 до 100");
                return;
            }

            var (r, g, b) = CmykToRgb(c, m, y, k);

            RTextBox.Text = r.ToString("F2");
            GTextBox.Text = g.ToString("F2");
            BTextBox.Text = b.ToString("F2");

            var (L,A,B) = CmykToLab(c, m, y, k);

            lTextBox.Text = L.ToString("F2");
            aTextBox.Text = A.ToString("F2");
            b_TextBox.Text = B.ToString("F2");

            colorDisplayPanel.BackColor = Color.FromArgb(r, g, b);
            UpdateRgbTrackBars(r, g, b);
            UpdateLabTrackBars((int)L, (int)A + 128, (int)B + 128);
            UpdateCmykTrackBars((int)c, (int)m, (int)y, (int)k);
        }

        private void UpdateRgbTrackBars(int r, int g, int b)
        {
            RtrackBar.Value = r;
            GtrackBar.Value = g;
            BtrackBar.Value = b;
        }
        private void UpdateLabTrackBars(int L, int A, int B)
        {
            ltrackBar.Value = L;
            atrackBar.Value = A;
            b_trackBar.Value = B;
        }
        private void UpdateCmykTrackBars(int c, int m, int y, int k)
        {
            ctrackBar.Value = c;
            mtrackBar.Value = m;
            ytrackBar.Value = y;
            ktrackBar.Value = k;
        }




        // RGB → CMYK
        private (double C, double M, double Y, double K) RgbToCmyk(int R, int G, int B)
        {
            double rd = R / 255.0;
            double gd = G / 255.0;
            double bd = B / 255.0;

            double k = 1 - Math.Max(rd, Math.Max(gd, bd));
            double c = (1 - rd - k) / (1 - k);
            double m = (1 - gd - k) / (1 - k);
            double y = (1 - bd - k) / (1 - k);

            if (double.IsNaN(c)) c = 0;
            if (double.IsNaN(m)) m = 0;
            if (double.IsNaN(y)) y = 0;

            return (c * 100, m * 100, y * 100, k * 100);
        }

        // CMYK → RGB
        private (int R, int G, int B) CmykToRgb(double c, double m, double y, double k)
        {
            int R = (int)(255 * (1 - c / 100) * (1 - k / 100));
            int G = (int)(255 * (1 - m / 100) * (1 - k / 100));
            int B = (int)(255 * (1 - y / 100) * (1 - k / 100));

            return (R, G, B);
        }

        // RGB → XYZ
        private (double X, double Y, double Z) RgbToXyz(int R, int G, int B)
        {
            double rNorm = R / 255.0, gNorm = G / 255.0, bNorm = B / 255.0;

            rNorm = (rNorm > 0.04045) ? Math.Pow((rNorm + 0.055) / 1.055, 2.4) : (rNorm / 12.92);
            gNorm = (gNorm > 0.04045) ? Math.Pow((gNorm + 0.055) / 1.055, 2.4) : (gNorm / 12.92);
            bNorm = (bNorm > 0.04045) ? Math.Pow((bNorm + 0.055) / 1.055, 2.4) : (bNorm / 12.92);

            rNorm *= 100; gNorm *= 100; bNorm *= 100;

            double x = rNorm * 0.4124 + gNorm * 0.3576 + bNorm * 0.1805;
            double y = rNorm * 0.2126 + gNorm * 0.7152 + bNorm * 0.0722;
            double z = rNorm * 0.0193 + gNorm * 0.1192 + bNorm * 0.9505;

            return (x, y, z);
        }

        // XYZ → RGB
        private (int R, int G, int B) XyzToRgb(double x, double y, double z)
        {
            x /= 100; y /= 100; z /= 100;

            double r = x * 3.2406 + y * -1.5372 + z * -0.4986;
            double g = x * -0.9689 + y * 1.8758 + z * 0.0415;
            double b = x * 0.0557 + y * -0.2040 + z * 1.0570;

            r = (r > 0.0031308) ? 1.055 * Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
            g = (g > 0.0031308) ? 1.055 * Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
            b = (b > 0.0031308) ? 1.055 * Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

            return ((int)Math.Round(r * 255), (int)Math.Round(g * 255), (int)Math.Round(b * 255));
        }

        // XYZ → LAB
        private (double l, double a, double b) XyzToLab(double x, double y, double z)
        {
            double refX = 95.047, refY = 100.000, refZ = 108.883;

            x /= refX;
            y /= refY;
            z /= refZ;

            x = (x > 0.008856) ? Math.Pow(x, 1.0 / 3) : (7.787 * x) + (16.0 / 116);
            y = (y > 0.008856) ? Math.Pow(y, 1.0 / 3) : (7.787 * y) + (16.0 / 116);
            z = (z > 0.008856) ? Math.Pow(z, 1.0 / 3) : (7.787 * z) + (16.0 / 116);

            double l = (116 * y) - 16;
            double a = 500 * (x - y);
            double b = 200 * (y - z);

            return (l, a, b);
        }

        // LAB → XYZ
        private (double X, double Y, double Z) LabToXyz(double l, double a, double b)
        {
            double refX = 95.047, refY = 100.000, refZ = 108.883;

            double y = (l + 16) / 116.0;
            double x = a / 500.0 + y;
            double z = y - b / 200.0;

            y = (Math.Pow(y, 3) > 0.008856) ? Math.Pow(y, 3) : (y - 16.0 / 116.0) / 7.787;
            x = (Math.Pow(x, 3) > 0.008856) ? Math.Pow(x, 3) : (x - 16.0 / 116.0) / 7.787;
            z = (Math.Pow(z, 3) > 0.008856) ? Math.Pow(z, 3) : (z - 16.0 / 116.0) / 7.787;

            return (x * refX, y * refY, z * refZ);
        }

        private (double l, double a, double b) RgbToLab(int R, int G, int B)
        {
            var (X, Y, Z) = RgbToXyz(R, G, B);
            var (l, a, b) = XyzToLab(X, Y, Z);
            return (l, a, b);
        }
        private (int R, int G, int B) LabToRgb(double l, double a, double b)
        {
            var (X, Y, Z) = LabToXyz(l, a, b);
            var (R, G, B) = XyzToRgb(X, Y, Z);
            return (R, G, B);
        }

        // LAB → CMYK
        private (double C, double M, double Y, double K) LabToCmyk(double l, double a, double b)
        {
            var (R, G, B) = LabToRgb(l, a, b);
            var (C, M, Y, K) = RgbToCmyk(R, G, B);
            return (C, M, Y, K);
        }

        // CMYK → LAB
        private (double l, double a, double b) CmykToLab(double c, double m, double y, double k)
        {
            var (R, G, B) = CmykToRgb(c, m, y, k);
            var (l, a, b) = RgbToLab(R, G, B);
            return (l, a, b);
        }

        private void rgbConvertButton_Click(object sender, EventArgs e)
        {
            ConvertRgbToLabAndCmyk();
            
        }

        private void labConvertButton_Click(object sender, EventArgs e)
        {
            ConvertLabToRgbAndCmyk();
        }

        private void cmykConvertButton_Click(object sender, EventArgs e)
        {
            ConvertCmykToRgbAndLab();
        }

        private void RtrackBar_Scroll(object sender, EventArgs e)
        {
            int rValue = RtrackBar.Value;
            RTextBox.Text = rValue.ToString();
        }

        private void GtrackBar_Scroll(object sender, EventArgs e)
        {
            int gValue = GtrackBar.Value;
            GTextBox.Text = gValue.ToString();
        }

        private void BtrackBar_Scroll(object sender, EventArgs e)
        {
            int bValue = BtrackBar.Value;
            BTextBox.Text = bValue.ToString();
        }

        private void ltrackBar_Scroll(object sender, EventArgs e)
        {
            int lValue = ltrackBar.Value;
            lTextBox.Text = lValue.ToString();
        }

        private void atrackBar_Scroll(object sender, EventArgs e)
        {
            int aValue = atrackBar.Value - 128;
            aTextBox.Text = aValue.ToString();
        }

        private void b_trackBar_Scroll(object sender, EventArgs e)
        {
            int b_Value = b_trackBar.Value - 128;
            b_TextBox.Text = b_Value.ToString();
        }

        private void ctrackBar_Scroll(object sender, EventArgs e)
        {
            int cValue = ctrackBar.Value;
            cTextBox.Text = cValue.ToString();
        }

        private void mtrackBar_Scroll(object sender, EventArgs e)
        {
            int mValue = mtrackBar.Value;
            mTextBox.Text = mValue.ToString();
        }

        private void ytrackBar_Scroll(object sender, EventArgs e)
        {
            int yValue = ytrackBar.Value;
            yTextBox.Text = yValue.ToString();
        }

        private void ktrackBar_Scroll(object sender, EventArgs e)
        {
            int kValue = ktrackBar.Value;
            kTextBox.Text = kValue.ToString();
        }
    }

}
