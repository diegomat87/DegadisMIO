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
    /// Lógica de interacción para DatosAtmosfericos.xaml
    /// </summary>
    public partial class DatosAtmosfericos : Page
    {
        Controlador Cont = new Controlador();
        Entidades.ListaTitles title1to4 = new Entidades.ListaTitles();
        OperativoDegPropTermod opTermodinamicas = new OperativoDegPropTermod();
        double rml=0;

        #region Constructores
        public DatosAtmosfericos()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = Cont.idioma;
            Thread.CurrentThread.CurrentUICulture = Cont.idioma;
            Inicializar();
        }

        private void Inicializar()
        {
            lblAtmospheric.Content = Properties.Resources.kAtmPar;
            lblestabilidad.Content = Properties.Resources.kEstabilidad;
            lbllongitudMoninObukhov.Content = Properties.Resources.kMonin;
            lblPresionAmbiente.Content = Properties.Resources.kPresAmb;
            lblTemperaturaAmbiente.Content = Properties.Resources.kTempAire;
            lblz0.Content = Properties.Resources.kZ0;
            lblzr.Content = Properties.Resources.kRugosidad;
            LblNombreArchivoCaso.Content = Properties.Resources.kTitulo;
            Lbltiempopromedio.Content = Properties.Resources.kDELTAY;
            Lblu0.Content = Properties.Resources.kU0;
            btnSiguiente.Content = Properties.Resources.kSiguiente;
            RdBtnHumedadA.Content = Properties.Resources.kHumAbs;
            RdBtnHumedadR.Content = Properties.Resources.kHumRel;
            RdBtnLongMODefault.Content = Properties.Resources.kDefault;
            RdBtnLongMOSet.Content = Properties.Resources.kSet;

            txtu0.Text = Cont.u0.ToString();
            txtz0.Text = Cont.z0.ToString();
            txtzr.Text = Cont.zr.ToString();
            txttiempopromedio.Text = Cont.avtime.ToString();
            txtTemperaturaAmbiente.Text = Cont.tamb.ToString();
            txtPresionAmbiente.Text = Cont.pamb.ToString();
            txtlongitudMoninObukhov.Text = Cont.rml.ToString();
            txtHumedad.Text = Cont.humedadrel.ToString();
            opTermodinamicas.HumedadAbs(Cont.tamb, Cont.humedadrel);
            calcularRml();
            cargarComboBox();
        }

        private void cargarComboBox()
        {
            cmbEstabilidad.Items.Add("A");
            cmbEstabilidad.Items.Add("B");
            cmbEstabilidad.Items.Add("C");
            cmbEstabilidad.Items.Add("D");
            cmbEstabilidad.Items.Add("E");
            cmbEstabilidad.Items.Add("F");
            cmbEstabilidad.Items.Add("G");
            cmbEstabilidad.SelectedIndex = Cont.istab;
        }
        #endregion

        #region Controles
        #region Ayudas
        private void BtnAyudau0_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyu0);
        }

        private void BtnAyudaz0_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyz0);
        }

        private void BtnAyudaZr_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyzr);
        }

        private void BtnAyudaEstabilidad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyEst);
        }

        private void BtnAyudaTiempoPromedio_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAytProm);
        }

        private void BtnAyudaLongitudMO_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyLongMO);
        }

        private void BtnAyudaTemperaturaAmb_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyTempAm);
        }

        private void BtnAyudaPresionAtm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyPresAm);
        }

        private void BtnAyudaHumedad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.kAyHum);
        }
        #endregion

        #region Transformaciones
        private void Txtu0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtz0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtzr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txttiempopromedio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtlongitudMoninObukhov_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtTemperaturaAmbiente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtPresionAmbiente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void txtHumedadAbsoluta_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbEstabilidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cont.istab = cmbEstabilidad.SelectedIndex;
            calcularRml();
        }

        private void txtzr_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Cont.zr = Convert.ToDouble(txtzr.Text);
                calcularRml();
            }
            catch (FormatException) { MessageBox.Show(Properties.Resources.eZr); }
        }

        private void RdBtnLongMODefault_Checked(object sender, RoutedEventArgs e)
        {
            txtlongitudMoninObukhov.Text = rml.ToString();
            txtlongitudMoninObukhov.IsEnabled = false;
            Cont.indvel = 1;
        }

        private void RdBtnLongMOSet_Checked(object sender, RoutedEventArgs e)
        {
            txtlongitudMoninObukhov.IsEnabled = true;
            Cont.indvel = 2;
        }

        private void RdBtnHumedadR_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtHumedad.Text = Cont.humedadrel.ToString();
            }
            catch (NullReferenceException) { }
        }

        private void RdBtnHumedadA_Checked(object sender, RoutedEventArgs e)
        {
            txtHumedad.Text = Cont.humedad.ToString();
        }
        #endregion

        #region Metodos
        private void Siguiente()
        {
            if (Validar())
            {
                Cont.istab = cmbEstabilidad.SelectedIndex;
                SetParametros();
                if (RdBtnHumedadA.IsChecked == true)
                {
                    Cont.humedadrel = opTermodinamicas.HumedadRel();
                    if (Cont.humedadrel > 100)
                    {
                        Cont.humedadrel = 100;
                        Cont.humedad = opTermodinamicas.HumedadAbs(Cont.tamb, Cont.humedadrel);
                        MessageBox.Show(Properties.Resources.eHumAbs+" "+ Cont.humedad);
                    }
                }
                else
                {
                    Cont.humedad = opTermodinamicas.HumedadAbs(Cont.tamb, Cont.humedadrel);
                }
                Cont.rhoa = Cont.pamb * (1.0 + Cont.humedad) * Cont.wmw / (Cont.rgas * (Cont.wmw / Cont.wma + Cont.humedad)) / Cont.tamb;

                EspecificacionesFuga especificacionesFuga = new EspecificacionesFuga();
                this.NavigationService.Navigate(especificacionesFuga);
            }
        }

        private void SetParametros()
        {
            switch (Cont.istab)
            {
                case 0:
                    Cont.deltay = 0.423 * Math.Pow((Math.Max(Cont.avtime, 18.4) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 107.66;
                    Cont.betaz = -1.7172;
                    Cont.gammaz = 0.2770;

                    Cont.sigxco = 0.02;
                    Cont.sigxp = 1.22;
                    Cont.sigxmd = 130.0;
                    break;
                case 1:
                    Cont.deltay = 0.313 * Math.Pow((Math.Max(Cont.avtime, 18.4) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 0.1355;
                    Cont.betaz = 0.8752;
                    Cont.gammaz = 0.0136;

                    Cont.sigxco = 0.02;
                    Cont.sigxp = 1.22;
                    Cont.sigxmd = 130.0;
                    break;
                case 2:
                    Cont.deltay = 0.210 * Math.Pow((Math.Max(Cont.avtime, 18.4) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 0.09623;
                    Cont.betaz = 0.9477;
                    Cont.gammaz = 0.0020;

                    Cont.sigxco = 0.02;
                    Cont.sigxp = 1.22;
                    Cont.sigxmd = 130.0;
                    break;
                case 3:
                    Cont.deltay = 0.136 * Math.Pow((Math.Max(Cont.avtime, 18.3) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 0.04134;
                    Cont.betaz = 1.1737;
                    Cont.gammaz = -0.0316;

                    Cont.sigxco = 0.04;
                    Cont.sigxp = 1.14;
                    Cont.sigxmd = 100.0;
                    break;
                case 4:
                    Cont.deltay = 0.102 * Math.Pow((Math.Max(Cont.avtime, 11.4) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 0.02275;
                    Cont.betaz = 1.3010;
                    Cont.gammaz = -0.0450;

                    Cont.sigxco = 0.17;
                    Cont.sigxp = 0.97;
                    Cont.sigxmd = 50.0;
                    break;
                case 5:
                    Cont.deltay = 0.0674 * Math.Pow((Math.Max(Cont.avtime, 4.6) / 600.0), 0.2);
                    Cont.betay = 0.9;

                    Cont.deltaz = 0.01122;
                    Cont.betaz = 1.4024;
                    Cont.gammaz = -0.0540;

                    Cont.sigxco = 0.17;
                    Cont.sigxp = 0.97;
                    Cont.sigxmd = 50.0;
                    break;
            }
        }

        private void calcularRml()
        {
            switch (Cont.istab)
            {
                case 0:
                    rml = -11.43 * Math.Pow(Cont.zr, 0.103);
                    break;
                case 1:
                    rml = -25.98 * Math.Pow(Cont.zr, 0.171);
                    break;
                case 2:
                    rml = -123.4 * Math.Pow(Cont.zr, 0.304);
                    break;
                case 3:
                    rml = 0.0; //used for infinity
                    break;
                case 4:
                    rml = -123.4 * Math.Pow(Cont.zr, 0.304);
                    break;
                case 5:
                    rml = 25.98 * Math.Pow(Cont.zr, 0.171);
                    break;
            }
            if (RdBtnLongMODefault.IsChecked==true)
            {
                txtlongitudMoninObukhov.Text = rml.ToString();
            }
        }

        private bool Validar()
        {
            string MError = "";

            if (txttiempopromedio.Text.Length <= 320)
            {
                switch (TxtTituloCaso.Text.Length - 1 / 80)
                {
                    case 0:
                        title1to4.Add(TxtTituloCaso.Text);
                        title1to4.Add("");
                        title1to4.Add("");
                        title1to4.Add("");
                        break;
                    case 1:
                        title1to4.Add(TxtTituloCaso.Text.Substring(0, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(80));
                        title1to4.Add("");
                        title1to4.Add("");
                        break;
                    case 2:
                        title1to4.Add(TxtTituloCaso.Text.Substring(0, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(80, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(160));
                        title1to4.Add("");
                        break;
                    case 3:
                        title1to4.Add(TxtTituloCaso.Text.Substring(0, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(80, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(160, 80));
                        title1to4.Add(TxtTituloCaso.Text.Substring(240));
                        break;
                }
                Cont.titles = title1to4;
            }
            else { MError += Properties.Resources.eTitulo+"\n"; }

            try { Cont.u0 = Convert.ToDouble(txtu0.Text); }
            catch (FormatException) { MError += Properties.Resources.eu0+"\n"; }

            try { Cont.z0 = Convert.ToDouble(txtz0.Text); }
            catch (FormatException) { MError += Properties.Resources.ez0+"\n"; }

            try { Cont.zr = Convert.ToDouble(txtzr.Text); }
            catch (FormatException) { MError += Properties.Resources.eZr+"\n"; }

            try { Cont.avtime = Convert.ToDouble(txttiempopromedio.Text); }
            catch (FormatException) { MError += Properties.Resources.eTiemProm+"\n"; }

            if (RdBtnLongMOSet.IsChecked == true)
            {
                try { Cont.rml = Convert.ToDouble(txtlongitudMoninObukhov.Text); }
                catch (FormatException) { MError += Properties.Resources.eLongMO+"\n"; }
            }
            else
            {
                Cont.rml = rml;
            }

            try { Cont.tamb = Convert.ToDouble(txtTemperaturaAmbiente.Text); }
            catch (FormatException) { MError += Properties.Resources.eTempAmb+"\n"; }

            try { Cont.pamb = Convert.ToDouble(txtPresionAmbiente.Text); }
            catch (FormatException) { MError += Properties.Resources.ePresAmb+"\n"; }

            try 
            { 
                Convert.ToDouble(txtHumedad.Text);
                if (RdBtnHumedadR.IsChecked == true)
                {
                    if (Convert.ToDouble(txtHumedad.Text) > 100) { MError += Properties.Resources.eHumRel+"\n"; }
                    else { Cont.humedadrel = Convert.ToDouble(txtHumedad.Text); }
                }
                else
                {
                    Cont.humedad= Convert.ToDouble(txtHumedad.Text);
                }
                
            }
            catch (FormatException) { MError += Properties.Resources.eHum+"\n"; }

            if (MError == "")
            {
                if (Convert.ToDouble(txtPresionAmbiente.Text) < 0.7 || Convert.ToDouble(txtPresionAmbiente.Text) > 1.1)
                {
                    MessageBoxResult result = MessageBox.Show(Properties.Resources.ePresIrreal, Properties.Resources.advertencia, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return true;
                }
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
