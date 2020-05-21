using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operativo
{
    public class PropiedadesTermodinamicas
    {
        public double HumedadAbs(double tamb, double wmw, double wma, double pamb, double humedadrel)
        {
            double watvp; double sat; //atm y °K

            watvp = Math.Exp(14.683943 - 5407.0 / tamb);
            sat = wmw / wma * watvp / (pamb - watvp);

            return humedadrel / 100 * sat; //Evento no controlado: humedad relativa mayor al 100%

        }

        public double HumedadRel(double tamb, double wmw, double wma, double pamb, double humedad)
        {
            double watvp; double sat; //atm y °K

            watvp = Math.Exp(14.683943 - 5407.0 / tamb);
            sat = wmw / wma * watvp / (pamb - watvp);
            return humedad * 100 / sat;

        }

        public double cpc(double gascpk,double gascpp, double gastem, double gasmw, double temp)
        {
            double con = 3.33;
            if (temp==gastem)
            {
                return (con + gascpk * gascpp * Math.Pow(gastem, gascpp-1.0))/gasmw;
            }
            else
            {
                return (con + gascpk * (Math.Pow(temp, gascpp) - Math.Pow(gastem, gascpp)) / (temp - gastem)) / gasmw;
            }
        }

        public double psif(double z, double rml)
        {
            double psif = 0;
            if (rml<0.0)
            {
                double a = Math.Pow((1.0 - 15.0 * z / rml), 0.25);
                psif= 2 * Math.Log((1 + a) / 2) + Math.Log((1 + Math.Pow(a, 2) / 2)) - 2 * Math.Atan(a) + Math.PI / 2;
            }
            else if (rml==0)
            {
                psif = 0;
            }
            else if (rml>0)
            {
                psif = -4.7 * z / rml;
            }
            return psif;
        }

        //public double adiabat(int ifl,double wc,double wa, double yc,double ya, double cc, double rho, double wm, double enthalpy, double temp, double humedad,double gasmw,double wma,double wmw,Entidades.ListaDEN ld,double humsrc)
        //{
        //    double wcl;
        //    double ww;
        //    double ccl;
        //    double slope;
        //    int i=0;
        //    switch (ifl)
        //    {
        //        case -2:
        //            break;
        //        case -1:
        //            break;
        //        case 0:
        //            ccl = cc;
        //            if (cc < 0)
        //            {
        //                ccl = 0;
        //            }
        //            else
        //            {
        //                i = 2;
        //                do
        //                {
        //                    if (ld[i].Den1 > 1)
        //                    {
        //                        i--;
        //                        if (cc > ld[i].Den2)
        //                        {
        //                            ccl = ld[i].Den2;
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    if (cc >= ld[i].Den2)
        //                    {
        //                        i++;
        //                    }
        //                } while (cc >= ld[i].Den2);
        //            }
        //            slope = (ld[i].Den3 - ld[i - 1].Den3) / (ld[i].Den2 - ld[i - 1].Den2);
        //            rho = (ccl - ld[i - 1].Den2) * slope + ld[i - 1].Den3;
        //            wcl = ccl / rho;
        //            wc = wcl;
        //            wa = (1 - (1 + humsrc) * wc) / (1 + humedad);
        //            ww = 1 - wa - wc;
        //            wm = 1 / (wc / gasmw + wa / wma + ww / wmw);
        //            yc = wm / gasmw * wc;
        //            ya = wm / wma * wa;
        //            break;
        //        case 1:
        //            wcl = wc;
        //            if(wc<0)
        //            {
        //                wcl = 0;
        //                wa = 1 / (1 + humedad);
        //            }
        //            else if (wc>1)
        //            {
        //                wcl = 1;
        //                wa = 0;
        //            }
        //            ww = 1 - wa - wcl;
        //            wm = 1 / (wcl / gasmw + wa / wma + ww / wmw);
        //            yc = wm / gasmw * wcl;
        //            ya = wm / wma * wa;
        //            i= 2;
        //            break;
        //        case 2:
        //            break;
        //        default:
        //            //call trap(26,0)
        //            break;
        //    }
        //}
    }
}
