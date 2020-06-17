using System;
using System.CodeDom;
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
        double wc;
        double wa;
        double yc;
        double ya;
        double wm;
        double cc;
        double temp;
        double rho;
        double cp;
        double enthalpy;
        double humsrc = 0;


        public double HumedadAbs(double tamb, double humedadrel)
        {///determina la humedad absoluta kg/kg
            double sat; //atm y °K
            sat = cont.wmw / cont.wma * Watvp(tamb) / (cont.pamb - Watvp(tamb));

            return humedadrel / 100 * sat;

        }

        private static double Watvp(double tamb)
        {
            return Math.Exp(14.683943 - 5407.0 / tamb);
        }

        public double HumedadRel()
        {///Determina la humedad relativa
            double sat; //atm y °K

            sat = cont.wmw / cont.wma * Watvp(cont.tamb) / (cont.pamb - Watvp(cont.tamb));
            return cont.humedad * 100 / sat;

        }

        public double Cpc(double temp)
        {///CPC determines the contaminant heat capacity
            double con = 3.33;
            if (temp == cont.gastem)
            {
                return (con + cont.gascpk * cont.gascpp * Math.Pow(cont.gastem, cont.gascpp - 1.0)) / cont.gasmw;
            }
            else
            {
                return (con + cont.gascpk * (Math.Pow(temp, cont.gascpp) - Math.Pow(cont.gastem, cont.gascpp)) / (temp - cont.gastem)) / cont.gasmw;
            }
        }

        public double Psif(double z)
        {
            double psif = 0.0;
            if (cont.rml < 0.0)
            {
                double a = Math.Pow((1.0 - 15.0 * z / cont.rml), 0.25);
                psif = 2.0 * Math.Log((1 + a) / 2.0) + Math.Log((1 + Math.Pow(a, 2) / 2.0)) - 2 * Math.Atan(a) + Math.PI / 2.0;
            }
            else if (cont.rml == 0)
            {
                psif = 0.0;
            }
            else if (cont.rml > 0)
            {
                psif = -4.7 * z / cont.rml;
            }
            return psif;
        }

        private void Tprop(double ifl, double wc, double wa, double enth)
        {
            #region resumen
            //            c subroutine to return:
            //c mole fractions(y's)
            //c       molecular weight(wm)
            //c       temperature(temp[=]K)
            //c       density(rho[=]kg / m * *3)
            //c       heat capacity(cp[=]J / kg / K)
            //c
            //c   for a mixture from:
            //c       mass fractions(w's)
            //c       temperature(K)     for ifl.lt.0
            //c
            //c   for a mixture from:
            //c       mass fractions(w's)
            //c       enthalpy(J / kg)     for ifl.ge.0
            //c
            //c       adiabatic mixing of:	emitted gas @ gastem
            //c                   entrained ambient humid air @ tamb
            //c                   entrained water from surface @ tsurf
            //c               for ifl.eq.0 calculate and return
            //c
            //c adiabatic lookup CALL ADIABAT
            //c               for isofl.eq.1.or.ihtfl.eq.0.and.ifl.eq.1
            #endregion
            double ww; double yw; 
            ww = 1 - wc - wa;
            wm = 1 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
            yc = wm / cont.gasmw * wc;
            ya = wm / cont.wma * wa;
            yw = 1 - yc - ya;

            if (cont.isofl == 1)
            {
                Adiabat(1);
                return;
            }

            if (ifl == 0) 
            {
                enth = wc * Cpc(cont.gastem) * (cont.gastem - cont.tamb)
                            + (ww - wa * cont.humedad) * cont.cpw * (cont.tsurf - cont.tamb); 
            }
            
            if(ifl==1 && cont.ihtfl == 0) 
            {
                Adiabat(1);
                return;
            }

            if (ifl == -1) 
            {
                DensityCalculation(wm, ww, wa, wc, ya, yc, rho, temp,enth);
                return;
            }

            var tmin=0.0; var tmax=0.0; double[] elementos = new double[3]; 
            elementos[1] =cont.gastem;
            elementos[2] =cont.tsurf;
            elementos[3] =cont.tamb;
            tmin = elementos.Min(); 
            tmax = elementos.Max();

            var elow = Enthal(wc, wa, tmin);
            if(enth < elow) 
            { 
                temp = tmin; enth = elow; 
                DensityCalculation(wm,ww,wa,wc,ya,yc,rho,temp,enth); 
                return; 
            }

            elow = Enthal(wc, wa, tmax);
            if (enth > elow)
            {
                temp = tmax; enth = elow;
                DensityCalculation(wm, ww, wa, wc, ya, yc, rho, temp,enth);
                return;
            }

            var cwc = wc; var cwa = wa; var centh = enth;
            /// programar zbrent.for
            ///call zbrent(temp, enth0, tmin, tmax, acrit, ierr)
            ///if (ierr.ne. 0) call trap(24,0)
            DensityCalculation(wm, ww, wa, wc, ya, yc, rho, temp, enth);
            return;
        }
        private void DensityCalculation(double wm, double ww, double wa, double wc, double ya, double yc, double rho, double temp, double enth)
        {
            double ywsat = Watvp(temp) / cont.pamb;
            double wwsat = cont.wmw / wm * ywsat * (ya + yc) / (1.0 - ywsat);
            double conden = Math.Max(0.0, ww - wwsat);

            rho = 1.0 / (wa / (cont.pamb * cont.wma / cont.rgas / temp) + (ww / conden) / (cont.pamb * cont.wmw / cont.rgas / temp) + conden / cont.rhowl + wc * temp / cont.gastem / cont.gasrho);

            double tmin = temp + 10.0;
            double tmax = Enthal(wc, wa, tmin);

            double cp = (enth - tmax) / (temp - tmin);
            if (cp<cont.cpa)
            {
                cp = cont.cpa;
            }
        }

        private double Enthal (double wc,double wa ,double temp)
        {
            ///ENTHAL calculates the mixture enthalpy
            double deltaf = 10; double ww; double wm; double ya; double yc; double yw;
            ww = 1.0 - wa - wc;
            wm = 1.0 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
            ya = wa * wm / cont.wma;
            yc = wc * wm / cont.gasmw;
            yw = 1 - ya - yc;

            var dh = cont.dhvap;
            var frac = 0.0;

            if (temp < 273.15) { frac = Math.Min((273.15-temp)/deltaf,1.0); }
            dh = cont.dhvap + cont.dhfus * frac;
            var ywsat = HumedadAbs(temp,100) / cont.pamb;
            var wwsat = cont.wmw / wm * ywsat * (ya + yc) / (1 - ywsat);
            var conden = Math.Max(0.0, ww - wwsat);

            var enthal = wc * Cpc(temp) * (temp - cont.tamb) - conden * dh + ww * cont.cpw * (temp - cont.tamb) + wa * cont.cpa * (temp - cont.tamb);
            return enthal;
        }

        public void Setden(double wc, double wa, double enthalpy)
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
            ///Parametros
            double tcrit = 0.002; double zero = 1e-20;
            int iils = 200; int ils = iils - 1; int iback = 25;

            if (cont.isofl == 1) { return; }

            int k = 1;
            Entidades.LineaDensidad linea = new Entidades.LineaDensidad();
            Entidades.ListaDEN listden = new Entidades.ListaDEN();
            Entidades.ListaDEN backsp = new Entidades.ListaDEN();
            Entidades.LineaDensidad curnt = new Entidades.LineaDensidad();

            linea.Den1 = 0; 
            linea.Den2 = 0; 
            linea.Den3 = cont.pamb * (1.0 + cont.humedad) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humedad)) / cont.tamb;
            linea.Den4 = 0; 
            linea.Den5 = cont.tamb;
            listden.Add(linea);

            double zbda;
            double zw;
            double zg;
            double enmix;
            double cc;
            double err = 0;
            double slope;
            double ccint;
            double rhoint;
            double wccal;
            double w1, w2;
            double entint;
            double temint;
            int ind = 1;

            for (int i = ils; i > 0; i--)
            {
                zbda = (Convert.ToDouble(i) / Convert.ToDouble(iils)) / (1.0 + cont.humedad);
                zw = zbda * cont.humedad;
                zg = 1.0 - zbda - zw;

                enmix = zg * enthalpy;

                zbda += zg * wa;
                zg = zg * wc;
                Tprop(2, zg, zbda, enmix);
                cc = zg * rho;
  
                curnt.Den1 = yc;
                curnt.Den2 = cc;
                curnt.Den3 = rho;
                curnt.Den4 = enmix;
                curnt.Den5 = temp;

                if (i == ils)
                {
                    backsp.Add(curnt);
                    continue;
                }
                err = 0.0;
                for (int iind = 1; iind < ind; iind++)
                {
                    yc = backsp[iind].Den1;
                    cc = backsp[iind].Den2;
                    rho = backsp[iind].Den3;
                    enmix = backsp[iind].Den4;
                    temp = backsp[iind].Den5;
                    slope = (listden[k].Den2 - curnt.Den2) / (listden[k].Den1 - curnt.Den1);
                    ccint = (yc - curnt.Den1) * slope + curnt.Den2;
                    err = Math.Max(err, 2.0 * Math.Abs(cc - ccint) / (Math.Abs(cc + ccint) + zero));
                    slope = (listden[k].Den3 - curnt.Den3) / (listden[k].Den1 - curnt.Den1);
                    rhoint = (yc - curnt.Den1) * slope + curnt.Den3;
                    err = Math.Max(err, 2 * Math.Abs(rho - rhoint) / (Math.Abs(rho + rhoint) + zero));
                    wccal = cc / rhoint;
                    w1 = curnt.Den2 / curnt.Den3;
                    w2 = listden[k].Den2 / listden[k].Den3;
                    slope = (listden[k].Den4 - curnt.Den4) / (w2 - w1);
                    entint = (wccal - w1) * slope + curnt.Den4;
                    err = Math.Max(err, 2 * Math.Abs(enmix - entint) / (Math.Abs(enmix + entint) + zero));
                    slope = (listden[k].Den5 - curnt.Den5) / (w2 - w1);
                    temint = (wccal - w1) * slope + curnt.Den5;
                    err = Math.Max(err, 2 * Math.Abs(temp - temint) / (Math.Abs(temp + temint) + zero));
                }

                if (err <= tcrit)
                {
                    if (ind >= iback)
                    {
                        k++;
                        listden[k].Den1 = backsp[ind].Den1;
                        listden[k].Den2 = backsp[ind].Den2;
                        listden[k].Den3 = backsp[ind].Den3;
                        listden[k].Den4 = backsp[ind].Den4;
                        listden[k].Den5 = backsp[ind].Den5;
                        ind = 1;
                        continue;
                    }
                    ind = ind + 1;
                    backsp[ind].Den1 = curnt.Den1;
                    backsp[ind].Den2 = curnt.Den2;
                    backsp[ind].Den3 = curnt.Den3;
                    backsp[ind].Den4 = curnt.Den4;
                    backsp[ind].Den5 = curnt.Den5;
                    continue;
                }
            }

            k++;
            if (k >= cont.igen)
            {
                //call trap(28,0)
            }
            if (wc == 1.0)
            {
                listden[k].Den1 = 1;
                listden[k].Den2 = cont.gasrho;
                listden[k].Den3 = cont.gasrho;
                listden[k].Den4 = enthalpy;
                listden[k].Den5 = cont.gastem;
            }
            else
            {
                Tprop(2, wc, wa, enthalpy);
                listden[k].Den1 = yc;
                listden[k].Den2 = wc * listden[k].Den3;
                listden[k].Den3 = rho;
                listden[k].Den4 = enthalpy;
                listden[k].Den5 = temp;
            }
            listden[k + 1].Den1 = 2.0;
            cont.DENtriples = listden;
            return;
        }

        private void Adiabat(double ifl, double yc, double cc, double wc)
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
            int i;
            double ccl;
            double ycl;
            double yw;
            double wcl;
            double ww;
            double cwc;
            double gamma;
            double w2;
            double w1;
            double slope;
            bool aux = true;
            switch (ifl)
            {
                case -2:
                    ycl = yc;
                    if (ycl < 0)
                    {
                        ycl = 0;
                    }
                    gamma = enthalpy;
                    ya = (1 - (1 + cont.gasmw * humsrc / cont.wmw) * ycl) / (1 + cont.humedad * cont.wma / cont.wmw);
                    yw = 1 - ya - ycl;
                    wm = ycl * cont.gasmw + ya * cont.wma + yw * cont.wmw;
                    wc = cont.gasmw / wm * ycl;
                    wa = cont.wma / wm * ya;
                    cc = wc * cont.rhoa / (1 - gamma * wc);
                    rho = cc / wc;
                    break;
                case -1:
                    ccl = cc;
                    if (ccl < 0)
                    {
                        ccl = 0;
                    }
                    gamma = enthalpy;
                    wc = ccl / (cont.rhoa + ccl * gamma);
                    wa = (1 - (1 + humsrc) * wc) / (1 + cont.humedad);
                    ww = 1 - wa - wc;
                    wm = 1 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
                    yc = wm / cont.gasmw * wc;
                    ya = wm / cont.wma * wa;
                    rho = ccl / wc;
                    break;
                case 0:
                    ccl = cc;
                    if (cc < 0)
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
                    wa = (1 - (1 + humsrc) * wc) / (1 + cont.humedad);
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
                    if (wc < 0)
                    {
                        wcl = 0;
                        wa = 1 / (1 + cont.humedad);
                    }
                    else if (wc > 1)
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
                        if (wcl < cwc)
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
