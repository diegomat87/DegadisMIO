using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Degadis
{
    public class OperativoDegPropTermod
    {
        Controlador cont = new Controlador();

        private void setden()
        {
            #region Descripcion
            //subroutine to load /GEN2/ as needed
            // 
            //adiabatic mixing of: WC
            // WA
            //WW @ specified enthalpy
            //
            //with ambient humid air @ tamb
            //
            //den(1,i)	mole fraction(yc)
            //den(2,i)	concentration(cc[=] kg c / m * *3)
            //den(3,i)	mixture density(rho[=] kg mix/ m * *3)
            //den(4,i)	mixture enthalpy(enthalpy[=] J/ kg)
            //den(5,i)	mixture temperature(temp[=] K)
            #endregion
            int k = 1; //contador
            Entidades.LineaDensidad linea = new Entidades.LineaDensidad();
            linea.Den1 = 0; linea.Den2 = 0; linea.Den3 = cont.rhoa; ; linea.Den4 = 0; linea.Den5 = cont.tamb;
        }
        private void adiabat(double ifl, double wc, double wa, double yc, double ya, double cc, double rho, double wm, double enthalpy, double temp)
        {
            #region comment
            //subroutine to return:
            //mass fractions(w's)
            //       mole fractions(y's)
            //       concentration(cc[=]kg / m * *3)
            //       density(rho[=]kg / m * *3)
            //       molecular weight(wm)
            //       enthalpy([=]J / kg)
            //       temperature(temp[=]K)
            //
            //  for a mixture from DEN lookup of adiabatic mixing calculation
            //  den(1, i)    mole fraction(yc)
            //  den(2, i)    concentration(cc[=] kg c / m * *3)
            //  den(3, i)    mixture density(rho[=] kg mix / m * *3)
            //  den(4, i)    mixture enthalpy(enthalpy[=] J / kg)
            //  den(5, i)    mixture temperature(temp[=] K)
            //
            //  ifl indicates given information:
            // - 2) mole fraction(Yc) and assumption of constant gamma in enthalpy
            // - 1)concentration(cc) and assumption of constant gamma in enthalpy
            //   0) concentration(cc)
            //   1) mass fraction c(wc)
            //   2) mole fraction(Yc)
            #endregion
            ///cheuquear humsrc que es cont.humedadrel aca///
            int i = 0;
            double ccl = 0;
            double ycl = 0;
            double yw = 0;
            double wcl = 0;
            double ww = 0;
            double cwc = 0;
            bool aux = true;
            double slope = 0;
            double w1 = 0;
            double w2 = 0;
            double gamma = 0;
            switch (ifl)
            {
                case -2:
                    ycl = yc;
                    if (ycl<0)
                    {
                        ycl = 0;
                    }
                    gamma = enthalpy;
                    ya = (1 - (1 + cont.gasmw * cont.humedadrel / cont.wmw) * ycl) / (1 + cont.humedad * cont.wma / cont.wmw);
                    yw = 1 - ya - ycl;
                    wm = ycl * cont.gasmw + ya * cont.wma + yw * cont.wmw;
                    wc = cont.gasmw / wm * ycl;
                    wa = cont.wma / wm * ya;
                    cc = wc * cont.rhoa / (1 - gamma * wc);
                    rho = cc / wc;
                    break;
                case -1:
                    ccl = cc;
                    if(ccl<0)
                    {
                        ccl = 0;
                    }
                    gamma = enthalpy;
                    wc = ccl / (cont.rhoa + ccl * gamma);
                    wa = (1 - (1 + cont.humedadrel) * wc) / (1 + cont.humedad);
                    ww = 1 - wa - wc;
                    wm = 1 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
                    yc = wm / cont.gasmw * wc;
                    ya = wm / cont.wma * wa;
                    rho = ccl / wc;
                    break;
                case 0:
                    ccl = cc;
                    if(cc<0)
                    {
                        ccl = 0;
                    }
                    i = 2;
                    aux = true;
                    do
                    {
                        if (cont.DENtriples[i].Den1 > 1)
                        {
                            i--;
                            if (cc > cont.DENtriples[i].Den2)
                            {
                                ccl = cont.DENtriples[i].Den2;
                            }
                            aux = false;
                            break;
                        }
                        if (cc <= cont.DENtriples[i].Den2)
                        {
                            aux = false;
                            break;
                        }
                        i++;
                    } while (aux);
                    slope = (cont.DENtriples[i].Den3 - cont.DENtriples[i - 1].Den3) / (cont.DENtriples[i].Den2 - cont.DENtriples[i - 1].Den2);
                    rho = (ccl - cont.DENtriples[i - 1].Den2) * slope + cont.DENtriples[i - 1].Den3;
                    wcl = ccl / rho;
                    wc = wcl;
                    wa = (1 - (1 + cont.humedadrel) * wc) / (1 + cont.humedad);
                    ww = 1 - wa - wc;
                    wm = 1 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
                    yc = wm / cont.gasmw * wc;
                    ya = wm / cont.wma * wa;
                    w1 = cont.DENtriples[i - 1].Den2 / cont.DENtriples[i - 1].Den3;
                    w2 = cont.DENtriples[i].Den2 / cont.DENtriples[i].Den3;
                    slope = (cont.DENtriples[i].Den4 - cont.DENtriples[i - 1].Den4) / (w2 - w1);
                    enthalpy = (wcl - w1) * slope + cont.DENtriples[i - 1].Den4;
                    slope = (cont.DENtriples[i].Den5 - cont.DENtriples[i - 1].Den5) / (w2 - w1);
                    temp = (wcl - w1) * slope + cont.DENtriples[i - 1].Den5;
                    break;
                case 1:
                    wcl = wc;
                    if(wc<0)
                    {
                        wcl = 0;
                        wa = 1 / (1 + cont.humedad);
                    }
                    else if(wc>1)
                    {
                        wcl = 1;
                        wa = 0;
                    }
                    ww = 1 - wa - wcl;
                    wm = 1 / (wcl / cont.gasmw + wa / cont.wma + ww / cont.wmw);
                    yc = wm / cont.gasmw * wcl;
                    ya = wm / cont.wma * wa;
                    i = 2;
                    aux = true;
                    do
                    {
                        if (cont.DENtriples[i].Den1 > 1)
                        {
                            i--;
                            aux = false;
                            break;
                        }
                        if (yc < cont.DENtriples[i].Den1)
                        {
                            aux = false;
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    } while (aux);
                    slope = (cont.DENtriples[i].Den3 - cont.DENtriples[i - 1].Den3) / (cont.DENtriples[i].Den2 - cont.DENtriples[i - 1].Den2);
                    rho = (cont.DENtriples[i - 1].Den3 - cont.DENtriples[i - 1].Den2 * slope) / (1 - slope * wcl);
                    cc = rho * wcl;
                    i = 2;
                    aux = true;
                    do
                    {
                        if (cont.DENtriples[i].Den1 > 1)
                        {
                            i--;
                            aux = false;
                            break;
                        }
                        cwc = cont.DENtriples[i].Den2 / cont.DENtriples[i].Den3;
                        if (wcl<cwc)
                        {
                            aux = false;
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    } while (aux);
                    w1 = cont.DENtriples[i - 1].Den2 / cont.DENtriples[i - 1].Den3;
                    w2 = cont.DENtriples[i].Den2 / cont.DENtriples[i].Den3;
                    slope = (cont.DENtriples[i].Den4 - cont.DENtriples[i - 1].Den4) / (w2 - w1);
                    enthalpy = (wcl - w1) * slope + cont.DENtriples[i - 1].Den4;
                    slope = (cont.DENtriples[i].Den5 - cont.DENtriples[i - 1].Den5) / (w2 - w1);
                    temp = (wcl - w1) * slope + cont.DENtriples[i - 1].Den5;
                    break;
                case 2:
                    i = 0;
                    ycl = yc;
                    if (yc < 0)
                    {
                        ycl = 0;
                        wa = 1 / (1 + cont.humedad);
                        ww = 1 - wa;
                        wm = 1 / (cont.wma / wa + cont.wmw / ww);
                        ya = wm / cont.wma * wa;
                    }
                    else if (yc > 1)
                    {
                        ycl = 1;
                        ya = 0;
                    }
                    i = 2;
                    aux = true;
                    do
                    {
                        if (cont.DENtriples[i].Den1 > 1)
                        {
                            i--;
                            aux = false;
                            break;
                        }
                        if (ycl < cont.DENtriples[i].Den1)
                        {
                            aux = false;
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    } while (aux);
                    wm = ycl * cont.gasmw + (1 - ycl) * cont.wma * cont.wmw * (1 + cont.humedad) / (cont.wmw + cont.wma * cont.humedad);
                    wc = ycl * cont.gasmw / wm;
                    wa = (1 - wc) / (1 + cont.humedad);
                    ww = 1 - wc - wa;
                    slope = (cont.DENtriples[i].Den3 - cont.DENtriples[i - 1].Den3) / (cont.DENtriples[i].Den2 - cont.DENtriples[i - 1].Den2);
                    cc = wc * (cont.DENtriples[i - 1].Den3 - slope * cont.DENtriples[i - 1].Den2) / (1 - wc * slope);
                    rho = cc / wc;
                    w1 = cont.DENtriples[i - 1].Den2 / cont.DENtriples[i - 1].Den3;
                    w2 = cont.DENtriples[i].Den2 / cont.DENtriples[i].Den3;
                    slope = (cont.DENtriples[i].Den4 - cont.DENtriples[i - 1].Den4) / (w2 - w1);
                    enthalpy = (wc - w1) * slope + cont.DENtriples[i - 1].Den4;
                    slope = (cont.DENtriples[i].Den5 - cont.DENtriples[i - 1].Den5) / (w2 - w1);
                    enthalpy = (wc - w1) * slope + cont.DENtriples[i - 1].Den5;
                    break;
            }
        }
    }
}
