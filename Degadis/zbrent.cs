using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Degadis
{
    public delegate double del(double x);
    class zbrent
    {
        public List<double> zb(del func, double x1, double x2, double tol)
        {
            List<double> resultados = new List<double>();
            int ierr = 0;
            double a = x1;
            double b = x2;
            double c = 0;
            double d = 0;
            double e = 0;
            double p;
            double r;
            double q;
            double s;
            double fa = func(a);
            double fb = func(b);
            double fc;
            double tol1;
            double xm;
            if (fa == 0)
            {
                resultados.Clear();
                resultados.Add(a);
                resultados.Add(ierr);
                return resultados;
            }
            if (fb == 0)
            {
                resultados.Clear();
                resultados.Add(b);
                resultados.Add(ierr);
                return resultados;
            }
            if (Signo(1.0,fa) * Signo(1.0,fb) > 0)
            {
                ierr = 2; 
                resultados.Clear();
                resultados.Add(0);
                resultados.Add(ierr);
                return resultados;
            }
            fc = fb;
            for (int i = 1; 1 <= 100; i++)
            {
                if (Signo(1.0,fb) * Signo(1.0,fc) > 0)
                {
                    c = a;
                    fc = fa;
                    d = b - a;
                    e = d;
                }
                if (Math.Abs(fc) < Math.Abs(fb))
                {
                    a = b;
                    b = c;
                    c = a;
                    fa = fb;
                    fb = fc;
                    fc = fa;
                }
                tol1 = 2 * 3e-8 * Math.Abs(b) + 0.5 * tol;
                xm = 0.5 * (c - b);
                if ((Math.Abs(xm) <= tol1) || (fb == 0))
                {
                    resultados.Clear();
                    resultados.Add(b);
                    resultados.Add(ierr);
                    return resultados;
                }
                if ((Math.Abs(e) >= tol1) && (Math.Abs(fa) > Math.Abs(fb)))
                {
                    s = fb / fa;
                    if (a == c)
                    {
                        p = 2.0 * xm * s;
                        q = 1 - s;
                    }
                    else
                    {
                        q = fa / fc;
                        r = fb / fc;
                        p = s * (2.0 * xm * q * (q - r) - (b - a) * (r - 1));
                        q = (q - 1) * (r - 1) * (s - 1);
                    }
                    if (p >= 0)
                    {
                        q = -q;
                        p = Math.Abs(p);
                        if (2.0 * p < Math.Min(3.0 * xm * q - Math.Abs(tol1 * q), Math.Abs(e * q)))
                        {
                            e = d;
                            d = p / q;
                        }
                        else
                        {
                            d = xm;
                            e = d;
                        }
                    }
                    else
                    {
                        d = xm;
                        e = d;
                    }
                    a = b;
                    fa = fb;

                    if (Math.Abs(d) >= tol1)
                    {
                        b = b + d;
                    }
                    else
                    {
                        b = b + Signo(tol1, xm);
                    }
                    fb = func(b);
                }
            }

            ierr = 1;
            resultados.Clear();
            resultados.Add(0);
            resultados.Add(ierr);
        }

        private static double Signo(double a, double b)
        {
            if (b>=0)
            {
                return Math.Abs(a);
            }
            else
            {
                return -Math.Abs(a);
            }
        }
    }
}
