using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;
using Entidades;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para DescripcionEmisioneJet.xaml
    /// </summary>
    public partial class DescripcionEmisionJet : Page
    {
        Controlador cont = new Controlador();
        Datos.archivos arch = new Datos.archivos();
        OperativoDegPropTermod proter = new OperativoDegPropTermod();

        #region Constructor
        public DescripcionEmisionJet()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            lblTitulo.Content = Properties.Resources.kDescVer;
            LblReleaseRate.Content = Properties.Resources.kTasaLib;
            LblSourceDiameter.Content = Properties.Resources.kDiamFuente;
            LblSourceDuration.Content = Properties.Resources.kDuracionFuente;
            LblSourceElevation.Content = Properties.Resources.kElevFuente;
            btnSiguiente.Content = Properties.Resources.kSiguiente;

            if (cont.Check4)
            {
                LblSourceDuration.Visibility = Visibility.Visible;
                BtnAyudaSourceDuration.Visibility = Visibility.Visible;
                TxtSourceDuration.Visibility = Visibility.Visible;
            }
            else
            {
                LblSourceDuration.Visibility = Visibility.Hidden;
                BtnAyudaSourceDuration.Visibility = Visibility.Hidden;
                TxtSourceDuration.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Componentes
        #region Ayuda
        private void BtnAyudaReleaseRate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aReleaseRate);
        }

        private void BtnAyudaSourceDiameter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aDiamFuente);
        }

        private void BtnAyudaSourceElevation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aElevFuente);
        }

        private void BtnAyudaSourceDuration_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aSourceDurp);
        }
        #endregion

        #region Transformaciones
        private void TxtReleaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceDiameter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceElevation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                arch.crearJet(cont.nombre, cont.ruta, cont.titles, cont.u0, cont.z0, cont.zr, cont.istab, cont.indvel, cont.rml, cont.tamb, cont.pamb, cont.relhum, cont.tsurf, cont.gasnam, cont.gasmw, cont.avtime, cont.temjet, cont.gasulc, cont.gasllc, cont.gaszzc, cont.indht, cont.gascpk, cont.gascpp, cont.DENtriples, cont.erate, cont.elejet, cont.diajet, cont.tend, cont.distmx);
                Siguiente();
            }
        }
        #endregion

        #region Metodos
        private void Siguiente()
        {
            JetPlu();
        }

        private bool Validar()
        {
            string MError = "";

            try { cont.erate = Convert.ToDouble(TxtReleaseRate.Text); }
            catch (Exception) { MError += Properties.Resources.eFluContp + "\n"; }

            try { cont.diajet = Convert.ToDouble(TxtSourceDiameter.Text); }
            catch (Exception) { MError += Properties.Resources.eDiamFuentep + "\n"; }

            try { cont.elejet = Convert.ToDouble(TxtSourceElevation.Text); }
            catch (Exception) { MError += Properties.Resources.eElevFuentep + "\n"; }

            if (cont.booljet)
            {
                try { cont.tend = Convert.ToDouble(TxtSourceDuration.Text); }
                catch (Exception) { MError += Properties.Resources.eDurFuentep + "\n"; }
            }


            if (MError.Length == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(MError);
                return false;
            }
        }

        private void JetPlu()
        {

            double qinit;
            double gamma;
            double cc;
            double uj;
            double zj;
            double uc;
            double dist;
            double theta;
            double qqq;
            double rk1, rk2, rk3, rk4, rk5, rk6;

            if (cont.tsurf < 250) { cont.tsurf = cont.tamb; }
            cont.Ustar = cont.u0 * cont.vkc / (Math.Log((cont.z0 + cont.zr) / cont.zr) - proter.Psif(cont.z0));
            if (cont.gascpp == 0) { cont.gascpp = 1; cont.gascpk = cont.gascpk * cont.gasmw - 3.34; }
            if (cont.nden == -1)
            {
                cont.isofl = 1;
                cont.rhoe = cont.pamb * cont.gasmw / cont.rgas / cont.temjet;
                cont.rhoa = cont.pamb * (1.0 + cont.humid) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humid)) / cont.tamb;
                Entidades.LineaDensidad linea1 = new Entidades.LineaDensidad();
                Entidades.LineaDensidad linea2 = new Entidades.LineaDensidad();
                linea1.Den1 = 0; linea1.Den2 = 0; linea1.Den3 = cont.rhoa; linea1.Den4 = 0; linea1.Den5 = cont.tamb;
                linea2.Den1 = 1; linea2.Den2 = cont.rhoe; linea2.Den3 = cont.rhoe; linea2.Den4 = 0; linea2.Den5 = cont.tamb;
                cont.DENtriples = null;
                cont.DENtriples.Add(linea1);
                cont.DENtriples.Add(linea2);
            }
            else if (cont.nden == 0) { cont.isofl = 0; cont.DENtriples = null; }
            else { cont.isofl = 1; }
            if (cont.elejet < 2 * cont.zr) { cont.elejet = 2 * cont.zr; MessageBox.Show(Properties.Resources.kJetPLU + cont.elejet); }
            cont.alfa1 = 0.028; cont.alfa2 = 0.37;
            int tamDen = cont.DENtriples.Count;
            if (tamDen > 0)
            {
                Entidades.LineaDensidad den = new Entidades.LineaDensidad();
                den.Den1 = 2;
                cont.DENtriples.Add(den);
                cont.rhoa = cont.DENtriples[0].Den3;
                cont.rhoe = cont.DENtriples[tamDen].Den3;
            }
            else
            {
                cont.rhoa = cont.pamb * (1.0 + cont.humid) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humid)) / cont.tamb;
                cont.rhoe = cont.pamb * cont.gasmw / cont.rgas / cont.gastem;
            }
            cont.gasrho = cont.rhoe;
            qinit = cont.erate / cont.rhoe;
            gamma = (cont.rhoe - cont.rhoa) / cont.rhoe;
            cont.yclow = cont.gasllc / 4.0;
            if (cont.tend <= 0)
            {
                cont.yclow = cont.gasllc;
            }
            double wc = 1.0; double wa = 0.0; double enth;
            enth = proter.Cpc(cont.gastem) * (cont.gastem - cont.tamb);
            if (cont.isofl == 0)
            {
                proter.Setden(wc, wa, enth);
            }
            Entidades.LineaAdiabat linad = new Entidades.LineaAdiabat();
            linad = proter.Adiabat(2, 0, cont.gasllc, 0);
            double cllc = linad.CC;
            linad = proter.Adiabat(2, 0, cont.gasllc, 0);
            double culc = linad.CC;

            //PRINT VALUES OF PARAMETERS INITIAL CONDITIONS

            cont.UA = cont.Ustar / cont.vkc * (Math.Log((cont.elejet + cont.zr) / cont.zr) - proter.Psif(cont.elejet));

            double[] dyr = new double[6];
            double[] yr = new double[6];
            for (int i = 1; i < 7; i++)
            {
                dyr[i] = 1.0;
                yr[i] = 0.0;
            }

            yr[1] = cont.rhoe;
            SetJet(cont.UA,cont.diajet,cont.elejet);
            cont.rho = cont.gasrho;
            cont.temp = cont.gastem;

            void SetJet(double ua, double diajet, double elejet)
            {
                cc = yr[1];
                uj = qinit / Math.PI / Math.Pow(diajet, 2) * 4.0;

                double sod = 7.7 * (1.0 - Math.Exp(-0.48 * Math.Sqrt(cont.rhoe * uj / cont.rhoa / cont.UA)));

                double rj = (cont.rhoe / cont.rhoa) * Math.Pow(uj / ua, 2);
                double delrho = cont.rhoe - cont.rhoa;
                double froude = 1.688;
                if (delrho > 0) { froude = cont.rhoa * Math.Pow(ua, 2) / cont.gg / delrho / diajet; }
                froude = Math.Min(froude, 1.688);

                double aj; double bj;
                if (rj < 0.036)
                {
                    aj = 18.519 * rj;
                    bj = 0.4;
                }
                else if (rj > 0.036 && rj <= 10)
                {
                    aj = Math.Exp(0.2476 + 0.3016 * Math.Log(froude) + 0.24386 * Math.Log(rj));
                    bj = 0.4;
                }
                else if (rj > 10 && rj <= 50)
                {
                    aj = Math.Exp(0.405465 + 0.131386 * Math.Log(rj) + 0.054931 * Math.Pow(Math.Log(rj), 2));
                    bj = Math.Exp(-0.744691 - 0.074525 * Math.Log(rj));
                }
                else if (rj > 50 && rj <= 600)
                {
                    aj = Math.Exp(-2.55104 + 1.49202 * Math.Log(rj) - 0.097623 * Math.Pow(Math.Log(rj), 2));
                    bj = Math.Exp(-0.446718 - 0.150694 * Math.Log(rj));
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.JSetJet);
                    aj = Math.Exp(1.44099 + 0.243045 * Math.Log(rj));
                    bj = Math.Exp(-0.446718 - 0.150694 * Math.Log(rj));
                }

                double bji = 1 / bj;

                double zod = sod;
                double xod; double fff; double fffp; double zodn; double check;
                for (int iii = 0; iii < 102; iii++)
                {
                    if (iii > 100) { System.Windows.Forms.MessageBox.Show(Properties.Resources.LSetJet); return; }
                    xod = Math.Pow(zod / aj, bji);
                    fff = Math.Pow(xod, 2) + Math.Pow(zod, 2) - Math.Pow(sod, 2);
                    fffp = Math.Pow(xod, 2) * 2 / bj / zod + 2 * zod;
                    zodn = zod - fff / fffp;
                    check = Math.Abs((zodn - zod) / zod);
                    if (check > 0.00001) { zod = zodn; }
                }

                xod = xod;
                dist = xod * diajet;
                double slope;
                if (xod > 0.0)
                {
                    slope = bj * zod / xod;
                    theta = Math.Atan(slope);
                }
                else if (xod == 0.0)
                {
                    theta = Math.PI / 2;
                }
                else { System.Windows.Forms.MessageBox.Show(Properties.Resources.XSetJet); return; }
                zj = zod * diajet + elejet;
                uc = uj - ua * Math.Cos(theta);

                cont.erate = qinit * cont.rhoe;

                double ppp = Math.Sqrt(Math.PI) / 2;



        }



        }

        
        #endregion
    }
}
