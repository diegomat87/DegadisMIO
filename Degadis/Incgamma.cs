using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Converters;

namespace Degadis
{
    public class Incgamma
    {
        Gamma gam = new Gamma();
        double aa = 0;
        double gln = 0;
        public double incGama(double alfa, double beta)
        {
            if (beta<0 || alfa<=0)
            {
                //format(' GAMINC? Arugments are out-of-bounds. ALPHA: ',     .			1PG13.5,' BETA: ',1PG13.5)
            }
            if (beta<alfa+1)
            {
                //call gser( aa, alpha, beta, gln)
                gser(alfa, beta);
            }
            else
            {
                //call gcf( aa, alpha, beta, gln)		! continued fraction
                gcf(alfa, beta);
                aa = 1.0 - aa;
            }
            //gaminc = dexp(dlog(aa) + gln)
            return Math.Exp(Math.Log(aa) + gln);
        }

        private void gser(double alfa, double beta)
        {
            gln = gammln(alfa);
            if (beta < 0)
            {
                //'GSER? BETA is out-of-bounds.'
                return;
            }
            else if (beta == 0)
            {
                aa = 0;
                return;
            }

            double ap = alfa;
            double sum = 1.0 / alfa;
            double del = sum;

            for (int i=0; i<100;i++)
            {
                ap = ap + 1;
                del = del * beta / ap;
                sum += del;
                if (Math.Abs(del)<Math.Abs(sum)*1E-9)
                {
                    aa = sum * Math.Exp(-beta + alfa * Math.Log(beta) - gln);
                    return;
                }
            }
            //'GSER? ALPHA is too large or ITMAX is too small.'
        }

        private double gammln(double alfa)
        {
            List<Double> cof = new List<double>();
            cof.Add(76.18009173);
            cof.Add(-86.50532033);
            cof.Add(24.01409822);
            cof.Add(-1.231739516);
            cof.Add(0.120858003E-2);
            cof.Add(-0.536382E-5);

            if (alfa<1)
            {
                return Math.Log(gam.gama(alfa));
            }
            double xx = alfa - 0.5;
            double tmp = xx * 5.5;
            tmp = (xx + 1.0) * Math.Log(tmp) - tmp;
            double ser = 5.5;
            for (int j=0;j<6;j++)
            {
                xx += 5.5;
                ser += cof[j] / xx;
            }

            return tmp + Math.Log(2.50662827465 * ser);
        }

        private void gcf(double alfa, double beta)
        {
            gln = gammln(alfa);

            double gold = 0;
            double a0 = 1;
            double a1 = beta;
            double b0 = 0;
            double b1 = 1;
            double fac = 0;

            for (int i=0;i<100;i++)
            {
                double an = Convert.ToDouble(i);
                double ana = an - alfa;
                a0 = a1 + a0 * ana;
                b0 = b1 + b0 * ana;
                //double anf = an;
                a1 = beta * a0 + an * a1;
                b1 = beta * b0 + an * b1;
                if (a1!=0)
                {
                    fac = 1.0 / b1;
                    double gg = a1 * fac;
                    a0 = a0 * fac;
                    a1 = a1 * fac;
                    b0 = b0 * fac;
                    b1 = b1 * fac;
                    if (Math.Abs((gg-gold)/gg) < 1E-9)
                    {
                        aa = Math.Exp(-beta + alfa * Math.Log(beta) - gln) / gg;
                        return;
                    }
                    gold = gg;
                }
            }
            //write(6,*) 'GCF? ALPHA is too large or ITMAX is too small.'
        }
    }
}
