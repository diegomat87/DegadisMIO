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

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para DescripcionEmisionA.xaml
    /// </summary>
    public partial class DescripcionEmisionA : Page
    {
        Controlador cont = new Controlador();

        #region Constructor
        public DescripcionEmisionA()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            iniciar();
        }

        private void iniciar()
        {
            lblTitulo.Content = Properties.Resources.kEstEstacionario;
            LblFlujoEmison.Content = Properties.Resources.kTasaLib;
            LblFraccionMolarCont.Content = Properties.Resources.kMasaCont;
            LblRadioFuente.Content = Properties.Resources.kRadFuente;
            LblTemperaturaFuente.Content = Properties.Resources.kTempFuente;
            btnSiguiente.Content = Properties.Resources.kSiguiente;

            if (cont.idilut == 0)
            {
                BtnAyudaFraccionMolarCont.Visibility = Visibility.Hidden;
                TxtFraccionMolarCont.Visibility = Visibility.Hidden;
                TxtTemperaturaFuente.Visibility = Visibility.Hidden;
                LblFraccionMolarCont.Visibility = Visibility.Hidden;
                LblTemperaturaFuente.Visibility = Visibility.Hidden;
                TxtFraccionMolarCont.Text = Convert.ToString(1.0);
                TxtTemperaturaFuente.Text = Convert.ToString(cont.gastem);
            }
        }
        #endregion

        #region Componentes
        #region ayudas
        private void BtnAyudaFlujoEmision_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aFluEm);
        }

        private void BtnAyudaRadioFuente_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aRadFuente);
        }

        private void BtnAyudaFraccionMolarCont_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aFracMolCon);
        }
        #endregion

        #region Transformaciones
        private void TxtFlujoEmision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtRadioFuente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtTemperaturaFuente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtFraccionMolarCont_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Siguiente();
        }

        private void Siguiente()
        {
            if (Validar())
            {
                Entidades.LineaSource lineaTransient1 = new Entidades.LineaSource();
                Entidades.LineaSource lineaTransient2 = new Entidades.LineaSource();
                Entidades.LineaSource lineaTransient3 = new Entidades.LineaSource();
                Entidades.LineaSource lineaTransient4 = new Entidades.LineaSource();

                Entidades.ListaSource lineaTransients = new Entidades.ListaSource();

                lineaTransient1.PTIME = 0;
                lineaTransient1.ET = cont.ess;
                lineaTransient1.R1T = cont.r1ss;
                lineaTransient1.PWC = cont.pwc1;
                lineaTransient1.PTEMP = cont.ptemp1;
                lineaTransient1.PFRACV = 1.0;

                lineaTransient2.PTIME = cont.tend;
                lineaTransient2.ET = cont.ess;
                lineaTransient2.R1T = cont.r1ss;
                lineaTransient2.PWC = cont.pwc1;
                lineaTransient2.PTEMP = cont.ptemp1;
                lineaTransient2.PFRACV = 1.0;

                lineaTransient3.PTIME = cont.tend + 1.0;
                lineaTransient3.ET = 0.0;
                lineaTransient3.R1T = 0.0;
                lineaTransient3.PWC = cont.pwc1;
                lineaTransient3.PTEMP = cont.ptemp1;
                lineaTransient3.PFRACV = 1.0;

                lineaTransient4.PTIME = cont.tend + 2.0;
                lineaTransient4.ET = 0.0;
                lineaTransient4.R1T = 0.0;
                lineaTransient4.PWC = cont.pwc1;
                lineaTransient4.PTEMP = cont.ptemp1;
                lineaTransient4.PFRACV = 1.0;

                cont.slen = 2.0 * cont.r1ss;
                cont.swid = Math.PI * Math.Pow(cont.r1ss, 2) / cont.slen / 2.0;

                lineaTransients.Add(lineaTransient1);
                lineaTransients.Add(lineaTransient2);
                lineaTransients.Add(lineaTransient3);
                lineaTransients.Add(lineaTransient4);
                cont.SourceSoT = lineaTransients;

                int mesn = DateTime.Now.Month - 1;
                string[] mes = new string[12] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                cont.tinp = DateTime.Now.ToString("d'-'") + mes[mesn] + DateTime.Now.ToString("'-'yyyy HH:mm:ss.ff");
                MessageBox.Show(cont.tinp);

                MessageBox.Show("OK");
                Datos.archivos archivos = new Datos.archivos();
                archivos.CrearInp(cont.titles,cont.u0, cont.z0, cont.zr, cont.istab, cont.avtime, cont.indvel, cont.rml, cont.tamb, cont.pamb, cont.humedad, cont.humedadrel, cont.isofl,
                    cont.tsurf, cont.ihtfl, cont.htco, cont.iwtfl, cont.wtco, cont.gasnam, cont.gasmw, cont.gastem, cont.gasrho, cont.gascpk, cont.gascpp, cont.gasulc, cont.gasllc,
                    cont.gaszzc, cont.DENtriples, cont.yclow, cont.gmass0, cont.Check4, cont.SourceSoT, cont.tinp, cont.ruta,cont.nombre);

                FileER1 fileER1 = new FileER1();
                this.NavigationService.Navigate(fileER1);
            }
        }
        #endregion

        #region Metodos
        private bool Validar()
        {
            string MError = "";

            try { cont.ess = Convert.ToDouble(TxtFlujoEmision.Text); }
            catch (Exception) { MError += Properties.Resources.eFluContp + "\n"; }

            try { cont.r1ss = Convert.ToDouble(TxtRadioFuente.Text); }
            catch (Exception) { MError += Properties.Resources.eRadFuentep + "\n"; }

            try { cont.ptemp1 = Convert.ToDouble(TxtTemperaturaFuente.Text); }
            catch (Exception) { MError += Properties.Resources.eTempFuentp + "\n"; }

            try { cont.pwc1 = Convert.ToDouble(TxtFraccionMolarCont.Text); }
            catch (Exception) { MError += Properties.Resources.eFracMolp + "\n"; }

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
        #endregion   
    }
}
