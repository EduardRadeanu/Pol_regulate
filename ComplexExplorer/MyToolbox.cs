using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ComplexExplorer
{

    public class PoligoaneRegulate : ComplexForm
    {
        void traseaza(Complex[] a, Color col)
        {
           for (int k = 1; k < a.Length; k++)
            {
                setLine(a[k - 1], a[k], col);
           }
        }
        Complex[] npGonQA(Complex q, Complex a0, int n, int p = 1)
        { //returneaza varfurile n-p-gonului cu centrul q si primul varf a0
            Complex[] a = new Complex[n + 1];
            double theta = p * 2.0 * Math.PI / n;
            for (int k = 0; k <= n; k++)
            {
               a[k] = q + Complex.setRoTheta(1, k * theta) * (a0 - q);
            }
            return a;
        }
        public override void makeImage()
        {
           setXminXmaxYminYmax(-10, 10, -10, 10);
            ScreenColor = Color.White;
            initScreen();
            Complex q = 0;
            Complex a = 8;
           int n = 7;
            //ex1
            //traseaza(npGonQA(q, a, n), Color.Black);
            //traseaza(npGonQA(q, a, n, 3), Color.Red);
            //traseaza(npGonQA(0,2, n,3), Color.Orange);
            //traseaza(npGonQA(0, 0.5, n, 3), Color.Green);
            //traseaza(npGonQA(0, 0.1, n, 3), Color.Blue);

            //ex2
            traseaza(npGonQA(q, a, n), Color.Black);
            traseaza(npGonQA(q, a, n, 5), Color.Red);
            traseaza(npGonQA(0, -5.5, n, 5), Color.Orange);
            traseaza(npGonQA(0, 3.8, n, 5), Color.Green);
            traseaza(npGonQA(0, -2.6, n, 5), Color.Blue);
            traseaza(npGonQA(0, 1.8, n, 5), Color.Red);
            traseaza(npGonQA(0, -1.2, n, 5), Color.Orange);
            traseaza(npGonQA(0, 0.8, n, 5), Color.Green);
            traseaza(npGonQA(0, -0.5, n, 5), Color.Red);
            traseaza(npGonQA(0, 0.3, n, 5), Color.Blue);
            traseaza(npGonQA(0, -0.17, n, 5), Color.Red);

            //ex3
            //traseaza(npGonQA(q, a, 8), Color.Black);
            //traseaza(npGonQA(q, a, 8, 3), Color.Red);
            //traseaza(npGonQA(0, 3.3, 8, 3), Color.Purple);
            //traseaza(npGonQA(0, 1.4, 8, 3), Color.Green);
            //traseaza(npGonQA(0, 0.6, 8, 3), Color.Aqua);
            //traseaza(npGonQA(0, 0.2, 8, 3), Color.Red);

           

           resetScreen();
        }
    }
   


    public static class MyMath
    {
        public static Complex ExpSerie(Complex z)
        {
            Complex s = 0, p = 1;
            for (int n = 1; n < 100; n++)
            {
                s += p;
                p *= z / n;
            }
            return s;
        }
        public static Complex ExpReIm(Complex z)
        {
            return Complex.setRoTheta(Math.Exp(z.Re), z.Im);
        }
    }
    public class Analitic : ComplexForm
    {   //comparam vizual functiile 
        //MyMath.ExpReIm() si MyMath.ExpSerie()

        void traseaza(Complex[] a, Color col)
        {
            for (int k = 1; k < a.Length; k++)
            {
                setLine(a[k - 1], a[k], col);
            }
        }
        void arataMod(int ii, int jj, Complex z)
        {
            int k = (int)(10 * z.Ro);
            setPixel(ii, jj, getColor(k));
        }
        void arataArg(int ii, int jj, Complex z)
        {
            int k = (int)(512 * (1 + z.Theta / Math.PI));
            setPixel(ii, jj, getColor(k));
        }
        Complex[] npGonQA(Complex q, Complex a0, int n, int p = 1)
        {
            Complex[] a = new Complex[n + 1];
            double theta = p * 2.0 * Math.PI / n;
            for (int k = 0; k <= n; k++)
            {
                a[k] = q + Complex.setRoTheta(1, k * theta) * (a0 - q);
            }
            return a;
        }
        public override void makeImage()
        {
            setXminXmaxYminYmax(-10, 10, -10, 10);
            ScreenColor = Color.White;
            initScreen();
            Complex q = 0;
            Complex a = 8;
            int n = 7;
            traseaza(npGonQA(q, a, n), Color.Black);
            traseaza(npGonQA(q, a, n, 3), Color.Red);

            resetScreen();
        }
    }


    public class DecagonPentagonat : ComplexForm
    {
        public bool esteInInteriorJordan(Complex[] p, Complex z)
        {   //este necesar ca p[N-1]==p[0]
            //poligonul P are N-1 varfuri
            //int N = p.Length;
            double s = 0;
            for (int k = 1; k < p.Length; k++)
            {
                s += ((p[k] - z) / (p[k - 1] - z)).Theta;
            }
            return Math.Abs(s) > 0.5;
        }
        public void umpleInteriorJordan(Complex[] p, Color col)
        {   //int N = p.Length;
            //este necesar ca p[N-1]==p[0]
            foreach (Pixel px in bmpPixels)
            {
                if (esteInInteriorJordan(p, getZ(px))) setPixel(px, col);
            }
        }

        void traseaza(Complex q, Complex[] a, Color col)
        {
            umpleInteriorJordan(a, col);
            for (int k = 1; k < a.Length; k++)
            {
                setLine(q, a[k], PenColor);
                setLine(a[k - 1], a[k], PenColor);
            }
        }

        Complex[] npGonQA(Complex q, Complex a0, int n, int p = 1)
        {
            Complex[] a = new Complex[n + 1];
            double theta = p * 2.0 * Math.PI / n;
            for (int k = 0; k <= n; k++)
            {
                a[k] = q + Complex.setRoTheta(1, k * theta) * (a0 - q);
            }
            return a;
        }

        public override void makeImage()
        {
            double raza = 6;
            setXminXmaxYminYmax(-10, 10, -10, 10);
            ScreenColor = Color.White;
            PenColor = Color.Black;
            initScreen();
            Complex q = 0;
            Complex a0 = q + Complex.setRoTheta(raza, -Math.PI / 10);
            Complex[] dec = npGonQA(q, a0, 10);

            //trasam 5 pentagoane centrate in varfurile decagonului
            for (int k = 0; k < 5; k++)
            {
                traseaza(dec[2 * k], npGonQA(dec[2 * k], dec[2 * k + 1], 5), Color.Red);
            }

            //trasam pentagonul mic din centru
            Complex aa0 = a0 + Complex.setRoTheta(1, 2 * Math.PI / 5) * (dec[1] - a0);
            traseaza(q, npGonQA(q, aa0, 5), Color.Red);
            resetScreen();
        }
    }




}
