using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace Datos
{
    public class archivos

    {
        #region definiciones
        NumberFormatInfo culture = new CultureInfo("en-US", false).NumberFormat;
        string rut = "";
        string titulo1 = "";
        string titulo2 = "";
        string titulo3 = "";
        string titulo4 = "";
        string u0 = "";//velocidad del viento (m/s) a z0 m
        string z0 = "";//altura z0 (m)
        string zr = "";//rugosidad de la superficie (m)
        string istab = "";//estabilidad atmosferica
        string oodist = "";//distancia en que la pluma toca el suelo para escapes a chorro (m)
        string avtime = "";//tiempo promedio (seg) utilizado para estimar DELTAY
        string indvel = "";//determina el método de cálculo
        string rml = ""; // MO lenght
        string tamb = "";//temperatura ambiente (K)
        string pamb = "";//presion ambiente  (Atm o Nw/m2)
        string humid = "";//humedad ambien absoluta
        string relhum = "";//humedad ambiente relativa (%)
        string isofl = "";//bandera que indica una simulación isotérmica está siendo corrida (si ISOFL es 1)
        string tsurf = "";//temperatura de la superficie (K). Si TSURF < 250 K, la misma es ajustada por TAMB.
        string ihtfl = "";//bandera que indica la transferencia de calor
        string htco = "";//coeficiente de transferencia de calor
        string iwtfl = "";//bandera que indica que la transferencia de H2O de la tierra a la nube se estima para la fuente cubierta
        string wtco = "";//coeficiente de transferencia de H2O, si se desea tener un coeficiente constante
        string gasnam = "";//nombre del contaminante (3 letras)
        string gasmw = "";//peso molecular del contaminante (Kg/Kmol). si se deja en blanco se toma el peso molecular del aire
        string gastem = "";//temperatura de emisión (ºK) del gas contaminante después de cualquier despresurización. Cuando está el espacio en blanco, GASTEM es la temperatura ambiente.
        string gasrho = "";//densidad del contaminante a la condición de GASTEM (Kg/m3). Cuando está el espacio en blanco, GASRHO, es calculado utilizando la Ley de los Gases Ideales.
        string cpk = "";//CPK es q1 en la ecuación utilizada para describir la capacidad calorífica del contaminante J/(Kg ºK)
        string cpp = "";//CPP es p1 en la ecuación utilizada para describir la capacidad calorífica del contaminante. 0 si se desea constante
        string gasulc = "";//nivel más alto de concentración del contaminante para estimar el perfil de concentraciones
        string gasllc = "";//nivel más bajo, respectivamente, de concentración del contaminante para estimar el perfil de concentraciones
        string gaszzc = "";//altura
        string nden = "";//si ISOFL no es cero, las líneas NP contienen la densidad relativa contaminante-aire en forma tabulada
        string yclow = "";//fracción molar del contaminante a la cual los cálculos se detienen (para una simulación transitoria los cálculos se llevan a cabo para YCLOW/5)
        string gmass0 = "";//masa inicial del gas contaminante sobre la fuente para derramamientos transitorios
        string nsrc = "";//número de tiempos requeridos para describir la fuente
        string check4 = "";//verdadera (T) para simulaciones en estado estacionario, de lo contrario es falso.
        string tinp = "";//marca o sello de tiempo de cuando fue creada la entrada
        #endregion

        /// <summary>
        /// Crea Archivo *.inp
        /// </summary>
        public void CrearInp(double velViento, double altViento, double rugosidad, int estabilidad, double tiempoprom, int inddvel, double molength, double tempAmbiente, double presAmbiente,
            double humAbsoluta, double humRelativa, int isofll, double tempSuperficie, int ihtfll, double htcoo, int iwtfll, double wtcoo, string nombre, double pesoMole, double temperatura,
            double densidad, double capCalorifica, double potenciaCapCal, double limitesuperior, double limiteinferior, double alturacontorno, List<Entidades.LineaDensidad> DEN, double limiteinferiorinteres,
            double masainicial, bool check04, List<Entidades.LineaSource> dNT, string tiempoinp, string ruta)
        {
            rut = ruta;
            culture.NumberGroupSeparator = "";

            u0 = velViento.ToString("F15", culture).Substring(0, 16);
            z0 = altViento.ToString("F15", culture).Substring(0, 16);
            zr = rugosidad.ToString("0.000000000000000E+000", culture);
            istab = estabilidad.ToString(culture);
            oodist = 0.0.ToString("0.000000000000000E+000", culture);
            avtime = tiempoprom.ToString("N15", culture).Substring(0, 16);
            indvel = inddvel.ToString(culture);
            rml = molength.ToString("N15", culture).Substring(0, 16);
            tamb = tempAmbiente.ToString("N7", culture).Substring(0, 8);
            pamb = presAmbiente.ToString("N7", culture).Substring(0, 8);
            humid = humAbsoluta.ToString("0.0000000E+00", culture);
            relhum = humRelativa.ToString("N7", culture).Substring(0, 8);
            isofl = isofll.ToString(culture);
            tsurf = tempSuperficie.ToString("N15", culture).Substring(0, 16);
            ihtfl = ihtfll.ToString(culture);
            htco = htcoo.ToString("0.000000000000000E+000", culture);
            iwtfl = iwtfll.ToString(culture);
            wtco = wtcoo.ToString("0.000000000000000E+000", culture);
            gasnam = nombre;
            gasmw = pesoMole.ToString("N15", culture).Substring(0, 16);
            gastem = temperatura.ToString("N15", culture).Substring(0, 16);
            gasrho = densidad.ToString("N15", culture).Substring(0, 16);
            cpk = capCalorifica.ToString("N15", culture).Substring(0, 16);
            cpp = potenciaCapCal.ToString("N15", culture).Substring(0, 16);
            gasulc = limitesuperior.ToString("0.000000000000000E+000", culture);
            gasllc = limiteinferior.ToString("0.000000000000000E+000", culture);
            gaszzc = alturacontorno.ToString("0.000000000000000E+000", culture);
            if (isofll == 1) { nden = DEN.Count.ToString("F15", culture).Substring(0, 8); }
            yclow = limiteinferiorinteres.ToString("0.000000000000000E+000", culture);
            gmass0 = masainicial.ToString("0.000000000000000E+000", culture);
            nsrc = dNT.Count.ToString(culture);
            if (check04) { check4 = "T"; }
            else { check4 = "F"; }
            tinp = tiempoinp;

            generar(DEN, dNT);
        }

        public void crearJet(string ruta,List<string> titulo, double velViento, double altViento, double rugosidad, int estabilidad, int inddvel, double molength, double tempAmbiente, double presAmbiente,
            double humRelativa, double tempSuperficie, string nombre, double pesoMole, double tiempoprom, double tiempojet, double limitesuperior, double limiteinferior, double alturacontorno,
            int indht, double capCalorifica, double potenciaCapCal, List<Entidades.LineaDensidad> DEN, double erate, double elejet, double diajet, double tend, double distMax)
        {
            StreamWriter archivo = new StreamWriter(ruta + "\\jet.inp");
            try
            {
                archivo.WriteLine(titulo[0]);
            }
            catch (Exception) {
                archivo.WriteLine("");
            }
            try
            {
                archivo.WriteLine(titulo[1]);
            }
            catch (Exception)
            {
                archivo.WriteLine("");
            }
            try
            {
                archivo.WriteLine(titulo[2]);
            }
            catch (Exception)
            {
                archivo.WriteLine("");
            }
            try
            {
                archivo.WriteLine(titulo[3]);
            }
            catch (Exception)
            {
                archivo.WriteLine("");
            }
            archivo.WriteLine("   " + velViento.ToString("F15", culture).Substring(0, 16) + "        " + altViento.ToString("F15", culture).Substring(0, 16));
            archivo.WriteLine("   " + rugosidad.ToString("0.000000000000000E+000", culture));
            archivo.WriteLine("   " + inddvel.ToString(culture) + "        " + estabilidad.ToString(culture)+ "        "+ molength.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + tempAmbiente.ToString("N7", culture).Substring(0, 8) + "       " + presAmbiente.ToString("N7", culture).Substring(0, 8) + "   " + humRelativa.ToString("N7", culture).Substring(0, 8));
            archivo.WriteLine("   " + tempSuperficie.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("");
            archivo.WriteLine(nombre);
            archivo.WriteLine("   " + pesoMole.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + tiempoprom.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + tiempojet.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + limitesuperior.ToString("0.000000000000000E+000", culture) + "  " + limiteinferior.ToString("0.000000000000000E+000", culture) + "  " + alturacontorno.ToString("0.000000000000000E+000", culture));
            archivo.WriteLine("   " + indht.ToString() + "       " + capCalorifica.ToString("N15", culture).Substring(0, 16) + "       " + potenciaCapCal.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + DEN.Count.ToString("F15", culture).Substring(0, 8));
                for (int i = 0; i < DEN.Count; i++)
                {
                    string den1 = DEN[i].Den1.ToString("N7", culture).Substring(0, 8);
                    string den2 = DEN[i].Den2.ToString("N7", culture).Substring(0, 8);
                    string den3 = DEN[i].Den3.ToString("N7", culture).Substring(0, 8);
                    string den4 = DEN[i].Den4.ToString("N7", culture).Substring(0, 8);
                    string den5 = DEN[i].Den5.ToString("N7", culture).Substring(0, 8);
                    archivo.WriteLine("   " + den1 + "       " + den2 + "       " + den3 + "       " + den4 + "       " + den5);
                }
            archivo.WriteLine("   "+ erate.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + elejet.ToString("N15", culture).Substring(0, 16) + "       " + diajet.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + tend.ToString("N15", culture).Substring(0, 16));
            archivo.WriteLine("   " + distMax.ToString("N15", culture).Substring(0, 16));
            archivo.Close();
        }

        private void generar(List<Entidades.LineaDensidad> DEN, List<Entidades.LineaSource> dNT)
        {
            int i = 0;

            StreamWriter archivo = new StreamWriter(rut + "\\prueba.inp");
            archivo.WriteLine(titulo1);
            archivo.WriteLine(titulo2);
            archivo.WriteLine(titulo3);
            archivo.WriteLine(titulo4);
            archivo.WriteLine("   " + u0 + "        " + z0 + "       " + zr);
            archivo.WriteLine("           " + istab);
            archivo.WriteLine("  " + oodist + "  " + avtime);
            archivo.WriteLine("           " + indvel + "  " + rml);
            archivo.WriteLine("   " + tamb + "       " + pamb + "      " + humid + "   " + relhum);
            archivo.WriteLine("           " + isofl + "   " + tsurf);
            archivo.WriteLine("           " + ihtfl + "  " + htco);
            archivo.WriteLine("           " + iwtfl + "  " + wtco);
            archivo.WriteLine(gasnam);
            archivo.WriteLine("   " + gasmw + "        " + gastem + "        " + gasrho);
            archivo.WriteLine("   " + cpk + "       " + cpp);
            archivo.WriteLine("  " + gasulc + "  " + gasllc + "  " + gaszzc);
            if (isofl != "0")
            {
                archivo.WriteLine(nden);
                for (i = 0; i < DEN.Count; i++)
                {
                    string den1 = DEN[i].Den1.ToString("N7", culture).Substring(0, 8);
                    string den2 = DEN[i].Den2.ToString("N7", culture).Substring(0, 8);
                    string den3 = DEN[i].Den3.ToString("N7", culture).Substring(0, 8);
                    string den4 = DEN[i].Den4.ToString("N7", culture).Substring(0, 8);
                    string den5 = DEN[i].Den5.ToString("N7", culture).Substring(0, 8);
                    archivo.WriteLine("   " + den1 + "       " + den2 + "       " + den3 + "       " + den4 + "       " + den5);
                }
            }
            archivo.WriteLine("  " + yclow);
            archivo.WriteLine("  " + gmass0);
            archivo.WriteLine("           " + nsrc);
            for (i = 0; i < dNT.Count; i++)
            {
                string ptime = dNT[i].PTIME.ToString("N7", culture).Substring(0, 8);
                string et = dNT[i].ET.ToString("N7", culture).Substring(0, 8);
                string rt = dNT[i].R1T.ToString("N7", culture).Substring(0, 8);
                string pwc = dNT[i].PWC.ToString("N7", culture).Substring(0, 8);
                string ptemp = dNT[i].PTEMP.ToString("N7", culture).Substring(0, 8);
                string pfracv = dNT[i].PFRACV.ToString("N7", culture).Substring(0, 8);

                archivo.WriteLine("   " + ptime + "       " + et + "       " + rt + "       " + pwc + "       " + ptemp + "       " + pfracv);
            }
            archivo.WriteLine(" " + check4);
            archivo.WriteLine(tinp);
            archivo.Close();
        }

        public void crearER2(double syoer, double erro, double szoer, double wtaio, double wtqoo, double wtszo, double errp, double smxp, double wtszp, double wtsyp, double wtbep, double wtdh, double errg, double smxg, double ertdnf, double ertupf, double wtruh, double wtdhg, double stpo, double stpp, double odlp, double odllp, double stpg, double odlg, double odllg, double nobs, string ruta)
        {
            StreamWriter archivo = new StreamWriter(ruta + "\\prueba.ER2");
            archivo.WriteLine(@"! This is an example for an ""ER2"" run parameter file.
! The same rules apply as for the ""ER1"" files.
!
!23456789012345678901234567890
!--------1---------2---------3
!
! These values are in common area / ERROR /
!
SY0ER     " + syoer.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST - INITIAL SY
ERRO      " + erro.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - ERROR BOUND
SZ0ER     " + szoer.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - INITIAL SZ
WTAIO     " + wtaio.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - WEIGHT FOR AI
WTQOO     " + wtqoo.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - WEIGHT FOR Q
WTSZO     " + wtszo.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - WEIGHT FOR SZ
ERRP      " + errp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - ERROR BOUND
SMXP      " + smxp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - MAXIMUM STEP
WTSZP     " + wtszp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - WEIGHT FOR SZ
WTSYP     " + wtsyp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - WEIGHT FOR SY
WTBEP     " + wtbep.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - WEIGHT FOR BEFF
WTDH      " + wtdh.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - WEIGHT FOR DH
ERRG      " + errg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - ERROR BOUND
SMXG      " + smxg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - MAXIMUM STEP SIZE
ERTDNF    " + ertdnf.ToString("N10", culture).Substring(0, 9) + @"  TDNF - CONVERGENCE CRITERION
ERTUPF    " + ertupf.ToString("N10", culture).Substring(0, 9) + @"  TUPF - CONVERGENCE CRITERION
WTRUH     " + wtruh.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - WEIGHT FOR RUH
WTDHG     " + wtdhg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - WEIGHT FOR DH
!
! These values are in common area / STP /
!
STPO      " + stpo.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(OBS) - INITIAL STEP
STPP      " + stpp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - INITIAL STEP
ODLP      " + odlp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - RELATIVE OUTPUT DELTA
ODLLP     " + odllp.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(PSS) - MAXIMUM DISTANCE BETWEEN OUTPUTS(m)
STPG      " + stpg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - INITIAL STEP
ODLG      " + odlg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - RELATIVE OUTPUT DELTA
ODLLG     " + odllg.ToString("N10", culture).Substring(0, 9) + @"  SSSUP - RKGST(SSG) - MAXIMUM DISTANCE BETWEEN OUTPUTS(m)
!
! The last variable NOBS is in / CNOBS /
!
!Note: it is read in as a real value even though it is integer type
!     in the program.
!
NOBS      " + nobs.ToString("N10", culture).Substring(0, 9) + @"  
!
!
!
! End - of - File");
            archivo.Close();
        }

        public void crearER3(double ert1, double erdt, double erntim, double check5, double sigx_flag, string ruta)
        {
            StreamWriter archivo = new StreamWriter(ruta + "\\prueba.ER3");
            archivo.WriteLine(@"! This is an example for an ""ER3"" run parameter file.
! The same rules apply as for the ""ER1"" files.
!
!23456789012345678901234567890
!--------1---------2---------3
!
! These values are in common area /ERROR/
!
ERT1      " + ert1.ToString("N10", culture).Substring(0, 9) + @"  FIRST SORT TIME
ERDT      " + erdt.ToString("N10", culture).Substring(0, 9) + @"  SORT TIME DELTA
ERNTIM    " + erntim.ToString("N10", culture).Substring(0, 9) + @"  NUMBER OF TIMES FOR THE SORT
!
!    Note: ERNTIM is entered as a real variable even though 
!           it is an integer type variable in the program.
!
!  The value of CHECK5 determines whether the above sort parameters
!     are used.  CHECK5 is initialized through the passed transfer
!     files to .FALSE.  CHECK5 is set to .TRUE. if a real value of 1.
!     is passed in this file.
!
CHECK5    " + check5.ToString("N10", culture).Substring(0, 9) + @"  USE THE DEFAULT TIME PARAMETERS
!CHECK5      1.       USE THE TIME PARAMETERS GIVEN ABOVE
!
!
sigx_flag " + sigx_flag.ToString("N10", culture).Substring(0, 9) + @"  correction for x-direction dispersion is to be made
!sigx_flag    0.       no correction for x-direction dispersion
!
!
!
! End-of-File
");
            archivo.Close();
        }

        public void crearER1(double stpin, double erbnd, double wtrg, double wttm, double wtya, double wtyc, double wteb, double wtmb, double xli, double xri, double eps, double zlow, double stpinz, double erbndz, double srcoer, double srcss, double srccut, double ernobl, double noblpt, double crfger, double epsilon, double ce, double delrhomin, double szstp0, double szerr, double szsz0, double ialpfl, double alpco, double iphifl, double dellay, double vua, double vub, double vuc, double vud, double vudelta, string ruta)
        {
            StreamWriter archivo = new StreamWriter(ruta + "\\prueba.ER1");
            archivo.WriteLine(@"!This is an example of how to set up and use the run parameter
! input files.  Comment lines start with an exclamation mark(!)
! in the first column.  The only restrictions for data input are
! as follows:
!		1) The data must be entered in the same order
!			all of the time.
!		2) Only the number can be between columns 10 and 20.
!		3) Always include the decimal point in the number
!
! Column layout:
!23456789012345678901234567890
!--------1---------2---------3
!        I         I
STPIN     " + stpin.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - INITIAL STEP SIZE
ERBND     " + erbnd.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - ERROR BOUND
WTRG      " + wtrg.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR RG
WTTM      " + wttm.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR Total Mass
WTYA      " + wtya.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR ya
WTYC      " + wtyc.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR yc
WTEB      " + wteb.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR Energy Balance
WTmB      " + wtmb.ToString("N10", culture).Substring(0, 9) + @"  MAIN - RKGST - WEIGHT FOR Momentum Balance
XLI       " + xli.ToString("N10", culture).Substring(0, 9) + @"  ALPH - LOWER LIMIT OF SEARCH FOR ALPHA
XRI       " + xri.ToString("N10", culture).Substring(0, 9) + @"  ALPH - UPPER LIMIT OF SEARCH FOR ALPHA
EPS       " + eps.ToString("N10", culture).Substring(0, 9) + @"  ALPH - convergence criteria USED BY ""ZBRENT""
ZLOW      " + zlow.ToString("N10", culture).Substring(0, 9) + @"  ALPHI - maximum BOTTOM HEIGHT FOR FIT OF ALPHA
!
STPINZ    " + stpinz.ToString("N10", culture).Substring(0, 9) + @"  ALPHI - INITIAL RKGST STEP <0.
!
ERBNDZ    " + erbndz.ToString("N10", culture).Substring(0, 9) + @"  ALPHI - ERROR BOUND FOR RKGST
!
!
!  Note that comment lines can be mixed with the numbers.
!
SRCOER    " + srcoer.ToString("N10", culture).Substring(0, 9) + @"  SRC1O - OUTPUT Error criterion
SRCSS     " + srcss.ToString("N10", culture).Substring(0, 9) + @"  SRC1O - min time for Steady; STPMX
SRCcut    " + srccut.ToString("N10", culture).Substring(0, 9) + @"  SRC1O - min height for blanket
ERNOBL    " + ernobl.ToString("N10", culture).Substring(0, 9) + @"  NOBL - CONVERGENCE ratio
NOBLPT    " + noblpt.ToString("N10", culture).Substring(0, 9) + @"  NOBL - NUMBER OF POINTS
!                       USED ON THE LAST PORTION OF THE SOURCE
!
crfger    " + crfger.ToString("N10", culture).Substring(0, 9) + @"  error criterion in building GEN3 vectors
!
epsilon   " + epsilon.ToString("N10", culture).Substring(0, 9) + @"  epsilon USED IN AIR ENTRAINMENT SPECIFICATION
!
! /SPRD_CON/
!
ce        " + ce.ToString("N10", culture).Substring(0, 9) + @"   constant in gravity slumping equation
delrhomin " + delrhomin.ToString("N10", culture).Substring(0, 9) + @"   stop cloud spread if delrho<delrhomin
!
!
! /SZFC/
!
szstp0    " + szstp0.ToString("N10", culture).Substring(0, 9) + @"   SZF - Initial step size
szerr     " + szerr.ToString("N10", culture).Substring(0, 9) + @"   SZF - Error criterion
szsz0     " + szsz0.ToString("N10", culture).Substring(0, 9) + @"   SZF - Initial Value of dellay*Ueff*Heff
!
!
!	/ALPHcom/
!
ialpfl    " + ialpfl.ToString("N10", culture).Substring(0, 9) + @"  ALPHI - calculation flag; 0) alpha=alpco; 1)1/(1+z); 2)1
alpco     " + alpco.ToString("N10", culture).Substring(0, 9) + @"  ALPHI - Value for alpha if IALPFL = 0
!
!
!	/PHIcom/
!
iphifl    " + iphifl.ToString("N10", culture).Substring(0, 9) + @"  PHIF - calc flag
dellay    " + dellay.ToString("N10", culture).Substring(0, 9) + @"  Ratio of Hl/Heff
!
!
!
!	/VUcom/
!
vua       " + vua.ToString("N10", culture).Substring(0, 9) + @"  Constant Av in source model
vub       " + vub.ToString("N10", culture).Substring(0, 9) + @"  Constant Bv in source model
vuc       " + vuc.ToString("N10", culture).Substring(0, 9) + @"  Constant Ev in source model
vud       " + vud.ToString("N10", culture).Substring(0, 9) + @"  Constant Dv in source model
vudelta   " + vudelta.ToString("N10", culture).Substring(0, 9) + @"  Constant DELTAv in source model
!
! End-of-File
");
            archivo.Close();
        }
    }
}

