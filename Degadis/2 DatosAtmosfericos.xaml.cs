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

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para DatosAtmosfericos.xaml
    /// </summary>
    public partial class DatosAtmosfericos : Page
    {
        Controlador Cont = new Controlador();
        Entidades.ListaTitles title1to4 = new Entidades.ListaTitles();
        Operativo.PropiedadesTermodinamicas opTermodinamicas = new Operativo.PropiedadesTermodinamicas();
        double rml=0;

        #region Constructores
        public DatosAtmosfericos()
        {
            InitializeComponent();
            
            txtu0.Text = Cont.u0.ToString();
            txtz0.Text = Cont.z0.ToString();
            txtzr.Text = Cont.zr.ToString();
            txttiempopromedio.Text = Cont.avtime.ToString();
            txtTemperaturaAmbiente.Text = Cont.tamb.ToString();
            txtPresionAmbiente.Text = Cont.pamb.ToString();
            txtlongitudMoninObukhov.Text = Cont.rml.ToString();
            txtHumedad.Text = Cont.humedadrel.ToString();
            opTermodinamicas.HumedadAbs(Cont.tamb, Cont.wmw, Cont.wma, Cont.pamb, Cont.humedadrel);
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
            MessageBox.Show(@"The windspeed(u0) must be specified at a given elevation(z0); z0 is chosen to be representative of the depth of the contaminant layer and is typically taken to be 10 m for ground-level releases. The windspeed is considered constant during the release.

For windspeeds less than about 2 m/s (at 10 m), some assumptions used in the jet-plume model and the downwind dispersion phase of DEGADIS may no longer be valid.

For low windspeed cases when the initial source momentum is not significant, this type of release can be modeled using DEGADIS by inputing a zero windspeed in the model. Concentrations will then be calculated for the secondary source cloud only. The distance associated with the calculated concentrations should be adjusted to reflect the movement of the secondary source cloud by any ambient wind present.");
        }

        private void BtnAyudaz0_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter the elevation where the windspeed is given");
        }

        private void BtnAyudaZr_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The surface roughness (along with the atmospheric stability and Monin-Obukhov length) is used to determine the friction velocity and to characterize the windspeed profile. The assumed logarithmic windspeed profile assumes that the surface roughness elements are homogeneous. The assumed profile may not be reasonable for elevations which are less than the height of the surface elements. (Typically, the surface roughness can be approximated as one-tenth of the height of the surface elements for aerodynamic purposes.) When considering dispersion of denser-than-air contaminants, the surface roughness used in DEGADIS should reflect surface element heights which are less than the depth of the dispersing layer. For typical field scale releases, the surface roughness used in DEGADIS should be less than 0.1 m.");
        }

        private void BtnAyudaEstabilidad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"Enter the Pasquill stability class: A,B,C,D,E or F.
The Pasquill-Gifford stability class is used to estimate: 
1) windspeed profile
2) Monin-Obukhov length
3) friction velocity
4) x-direction dispersion parameters");
        }

        private void BtnAyudaTiempoPromedio_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The averaging time used to estimate DELTAY (tdy) represents the time scale associated with lateral plume meander in the model. Other time scales are also pertinent: trel, thaz, and ttrav. ");
        }

        private void BtnAyudaLongitudMO_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The Monin-Obukhov length is a length scale which quantifies the vertical stability in the atmosphere; it is a parameter used in the windspeed profile. Unstable atmospheric stabilities (A, B, and C)  correspond to negative Monin-Obukhov lengths, while stable atmospheric stabilities (E and F) correspond to positive lengths. Neutral atmospheric stability (D) corresponds to an infinite Monin-Obukhov length scale (which is represented in DEGADIS with a value of zero input to the Monin-Obukhov length). If the Monin-Obukhov length scale is unknown, DEGADIS estimates it from the atmospheric stability and the surface roughness.");
        }

        private void BtnAyudaTemperaturaAmb_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The ambient air temperature should reflect the conditions present during the release.");
        }

        private void BtnAyudaPresionAtm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The ambient air pressure should reflect the conditions present during the release.");
        }

        private void BtnAyudaHumedad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The ambient humidity can be entered as: Relative or Absolute.");
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
            catch (FormatException) { MessageBox.Show("El valor ingresado para zr debe ser un numero positivo"); }
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
                    Cont.humedadrel = opTermodinamicas.HumedadRel(Cont.tamb, Cont.wmw, Cont.wma, Cont.pamb, Cont.humedad);
                    if (Cont.humedadrel > 100)
                    {
                        Cont.humedadrel = 100;
                        Cont.humedad = opTermodinamicas.HumedadAbs(Cont.tamb, Cont.wmw, Cont.wma, Cont.pamb, Cont.humedadrel);
                        MessageBox.Show("This absolute humidity lead to a relative humidity greater than 100. This is not possible so relative humidity has been adjusted to 100 giving an absolute humidity of"+ Cont.humedad);
                    }
                }
                else
                {
                    Cont.humedad = opTermodinamicas.HumedadAbs(Cont.tamb, Cont.wmw, Cont.wma, Cont.pamb, Cont.humedadrel);
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
            else { MError += "El titutlo puede contener hasta 320 caracteres\n"; }

            try { Cont.u0 = Convert.ToDouble(txtu0.Text); }
            catch (FormatException) { MError += "El valor ingresado para u0 debe ser un numero positivo\n"; }

            try { Cont.z0 = Convert.ToDouble(txtz0.Text); }
            catch (FormatException) { MError += "El valor ingresado para z0 debe ser un numero positivo\n"; }

            try { Cont.zr = Convert.ToDouble(txtzr.Text); }
            catch (FormatException) { MError += "El valor ingresado para zr debe ser un numero positivo\n"; }

            try { Cont.avtime = Convert.ToDouble(txttiempopromedio.Text); }
            catch (FormatException) { MError += "El valor ingresado para el tiempo promedio debe ser un numero positivo\n"; }

            if (RdBtnLongMOSet.IsChecked == true)
            {
                try { Cont.rml = Convert.ToDouble(txtlongitudMoninObukhov.Text); }
                catch (FormatException) { MError += "El valor ingresado para la longitud MO debe ser un numero\n"; }
            }
            else
            {
                Cont.rml = rml;
            }

            try { Cont.tamb = Convert.ToDouble(txtTemperaturaAmbiente.Text); }
            catch (FormatException) { MError += "El valor ingresado para la temperatura ambiente debe ser un numero positivo\n"; }

            try { Cont.pamb = Convert.ToDouble(txtPresionAmbiente.Text); }
            catch (FormatException) { MError += "El valor ingresado para la presion ambiente debe ser un numero positivo\n"; }

            try 
            { 
                Convert.ToDouble(txtHumedad.Text);
                if (RdBtnHumedadR.IsChecked == true)
                {
                    if (Convert.ToDouble(txtHumedad.Text) > 100) { MError += "El valor ingresado para la humedad relativa debe ser un numero positivo entre 0 y 100\n"; }
                    else { Cont.humedadrel = Convert.ToDouble(txtHumedad.Text); }
                }
                else
                {
                    Cont.humedad= Convert.ToDouble(txtHumedad.Text);
                }
                
            }
            catch (FormatException) { MError += "El valor ingresado para la humedad debe ser un numero positivo\n"; }

            if (MError == "")
            {
                if (Convert.ToDouble(txtPresionAmbiente.Text) < 0.7 || Convert.ToDouble(txtPresionAmbiente.Text) > 1.1)
                {
                    MessageBoxResult result = MessageBox.Show("El valor de la presion ambiente ingresado aparenta ser irreal. Desea Continuar?", "Advertencia", MessageBoxButton.YesNo);
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
