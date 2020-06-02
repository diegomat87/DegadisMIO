using Operativo;
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

        private void tprop(double ifl, double wc, double wa, double enth, double yc, double ya, double wm, double temp, double rho, double cp)
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
            double ww; double yw; PropiedadesTermodinamicas propiedades = new PropiedadesTermodinamicas();
            ww = 1 - wc - wa;
            wm = 1 / (wc / cont.gasmw + wa / cont.wma + ww / cont.wmw);
            yc = wm / cont.gasmw * wc;
            ya = wm / cont.wma * wa;
            yw = 1 - yc - ya;

            if (cont.isofl == 1)
            {
                adiabat(1, wc, wa, yc, ya, cc, rho, wm, enth, temp);
            }

            if (ifl == 0) { enth = wc * propiedades.cpc(cont.gascpk, cont.gascpp, cont.gastem, cont.gasmw, cont.gastem) * (cont.gastem - cont.tamb)
                            + (ww - wa * cont.humedad) * cont.cpw * (cont.tsurf - cont.tamb); }
            
            if(ifl==1 && cont.ihtfl == 0) { adiabat(1, wc, wa, yc, ya, cc, rho, wm, enth, temp);}

            if (ifl == -1) { //goto
            }

            var tmin=0; var tmax=0; var elementos =[cont.gastem, cont.tsurf, cont.tamb];
            tmin = Math.Min(elementos) ;
            tmax = Math.Max(elementos);

            var elow = enthal(wc, wa, tmin);
            if(enth < elow) { temp = tmin; enth = elow; }//goto
        }

        private double enthal (double wc,double wa ,double temp)
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
            PropiedadesTermodinamicas prop = new PropiedadesTermodinamicas();
            var ywsat = prop.HumedadAbs(temp,cont.wmw,cont.wma,cont.pamb,100) / cont.pamb;
            var wwsat = cont.wmw / wm * ywsat * (ya + yc) / (1 - ywsat);
            var conden = Math.Max(0.0, ww - wwsat);

            var enthal = wc * prop.cpc(cont.gascpk, cont.gascpp, cont.gastem, cont.gasmw, temp) * (temp - cont.tamb) - conden * dh + ww * cont.cpw * (temp - cont.tamb) + wa * cont.cpa * (temp - cont.tamb);
            return enthal;
        }
        private void setden(double wc, double wa, double enthalpy, double rho, double yc)
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
            linea.Den1 = 0; 
            linea.Den2 = 0; 
            linea.Den3 = cont.rhoa;
            linea.Den4 = 0; 
            linea.Den5 = cont.tamb;

            /*esto lo escribo por lo que entiendo de
             parameter (iils=200, ils=iils-1, iback=25)
             aca declaro todas las variables que se usan, seguramente algunas van a controlador
            */
            Entidades.ListaDEN backsp = new Entidades.ListaDEN();
            double iils = 200;
            int ils = 199;
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
            double igen = 0; //no se que es
            double zero = 1E-20;//si lo entendi bien busca un valor practicamente 0, no se porque no 0 bsoluto
            int ind = 1;
            // aca vi en internet que el do es un for pero no encuentro el end do asi que no se cuando termina
            for (int i = ils; i > 0; i--)
            {
                zbda = (i / iils) / (1 + cont.humedad);
                zw = zbda * cont.humedad;
                zg = 1 - zbda - zw;
                enmix = zg * enthalpy;
                zbda += zg * wa;
                zg = zg * wc;
                //tprop(2,zg,zbda,enmix,yc,ya,wm,temp,rho,cp)
                cc = zg * rho;

                //esto no se si es una linea de densidad o que pero tiene la misma estructura
                Entidades.LineaDensidad curnt = new Entidades.LineaDensidad();
                curnt.Den1 = yc;
                curnt.Den2 = cc;
                curnt.Den3 = rho;
                curnt.Den4 = enmix;
                curnt.Den5 = cont.tamb; //puse temperatura ambiente porser la unica temperatura que tengo en cont,pero no se

                if (i == ils)
                {
                    //esto asigna la linea anterior a una lista
                    backsp.Add(curnt);
                    k = Auxiliar(wc, enthalpy, k);
                }
                err = 0;
                for (int iind = 0; iind < ind; iind++)
                {
                    yc = backsp[iind].Den1;
                    cc = backsp[iind].Den2;
                    rho = backsp[iind].Den3;
                    enmix = backsp[iind].Den4;
                    cont.tamb = backsp[iind].Den5;
                    slope = (cont.DENtriples[k].Den2 - curnt.Den2) / (cont.DENtriples[k].Den1 - curnt.Den1);
                    ccint = (yc - curnt.Den1) * slope + curnt.Den2;
                    err = Math.Max(err, 2 * Math.Abs(cc - ccint) / (Math.Abs(cc + ccint) + zero));
                    slope = (cont.DENtriples[k].Den3 - curnt.Den3) / (cont.DENtriples[k].Den1 - curnt.Den1);
                    rhoint = (yc - curnt.Den1) * slope + curnt.Den3;
                    err = Math.Max(err, 2 * Math.Abs(rho - rhoint) / (Math.Abs(rho + rhoint) + zero));
                    wccal = cc / rhoint;
                    w1 = curnt.Den2 / curnt.Den3;
                    w2 = cont.DENtriples[k].Den2 / cont.DENtriples[k].Den3;
                    slope = (cont.DENtriples[k].Den4 - curnt.Den4) / (w2 - w1);
                    entint = (wccal - w1) * slope + curnt.Den4;
                    err = Math.Max(err, 2 * Math.Abs(enmix - entint) / (Math.Abs(enmix + entint) + zero));
                    slope = (cont.DENtriples[k].Den5 - curnt.Den5) / (w2 - w1);
                    double temint = (wccal - w1) * slope + curnt.Den5;
                    err = Math.Max(err, 2 * Math.Abs(cont.tamb - temint) / (Math.Abs(cont.tamb + temint) + zero));
                }
                if (err <= 0.002)
                {
                    if (ind < 25)
                    {
                        ind++;
                        backsp.Add(curnt);
                        //extraje para llamar desde 2 lugares no se que nombre ponerle
                        k = Auxiliar(wc, enthalpy, k);
                    }
                    else
                    {
                        k++;
                        if (k >=igen)
                        {
                            //call trap(28,0)
                            cont.DENtriples[k] = backsp[ind];
                            ind = 1;
                        }
                    }
                }
                k = Auxiliar(wc, enthalpy, k);
            }
        }

        private int Auxiliar(double wc, double enthalpy, int k)
        {
            k++;
            if (k >= igen)
            {
                //call trap(28,0)
            }
            if (wc == 1)
            {
                cont.DENtriples[k].Den1 = 1;
                cont.DENtriples[k].Den2 = cont.gasrho;
                cont.DENtriples[k].Den3 = cont.gasrho;
                cont.DENtriples[k].Den4 = enthalpy;
                cont.DENtriples[k].Den5 = cont.gastem;
            }
            else
            {
                //call tprop(2,wc,wa,enthalpy,den(1,k),ya,wm,den(5,k),den(3,k),cp)
                cont.DENtriples[k].Den2 = wc * cont.DENtriples[k].Den3;
                cont.DENtriples[k].Den4 = enthalpy;
            }
            cont.DENtriples[k + 1].Den1 = 2;
            return k;
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
