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
    /// Lógica de interacción para EspecificacionesFuga.xaml
    /// </summary>
    public partial class EspecificacionesFuga : Page
    {
        Controlador cont = new Controlador();

        #region Constructor
        public EspecificacionesFuga()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            if (cont.booljet)
            {
                LblCoefTransfCalor.Visibility = Visibility.Hidden;
                BtnAyudaCoefTransfCalor.Visibility = Visibility.Hidden;
                RdBtnCoefTransfCInput.Visibility = Visibility.Hidden;
                RdBtnCoefTransfCDegadis.Visibility = Visibility.Hidden;
                RdBtnCoefTransfCLlnLCorr.Visibility = Visibility.Hidden;
                TxtCoeficienteCalorInput.Visibility = Visibility.Hidden;
                LblTransfAgua.Visibility = Visibility.Hidden;
                BtnAyudaTransfAgua.Visibility = Visibility.Hidden;
                RdBtnTransfAguaNo.Visibility = Visibility.Hidden;
                RdBtnCoefTransfADegadis.Visibility = Visibility.Hidden;
                RdBtnCoefTransfAInput.Visibility = Visibility.Hidden;
                TxtCoeficienteAguaInput.Visibility = Visibility.Hidden;
            }
            else
            {
                LblCoefTransfCalor.Visibility = Visibility.Visible;
                BtnAyudaCoefTransfCalor.Visibility = Visibility.Visible;
                RdBtnCoefTransfCInput.Visibility = Visibility.Visible;
                RdBtnCoefTransfCDegadis.Visibility = Visibility.Visible;
                RdBtnCoefTransfCLlnLCorr.Visibility = Visibility.Visible;
                TxtCoeficienteCalorInput.Visibility = Visibility.Visible;
                LblTransfAgua.Visibility = Visibility.Visible;
                BtnAyudaTransfAgua.Visibility = Visibility.Visible;
                RdBtnTransfAguaNo.Visibility = Visibility.Visible;
                RdBtnCoefTransfADegadis.Visibility = Visibility.Visible;
                RdBtnCoefTransfAInput.Visibility = Visibility.Visible;
                TxtCoeficienteAguaInput.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Controles
        #region Ayuda
        private void BtnAyudaIsotermico_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"An isothermal simulation simply does not use the energy balance in DEGADIS to estimate the mixture density (from the mixture temperature). For an isothermal simulation then, the user must specify the relationship between contaminant mole fraction, contaminant concentration (kg/m**3), and mixture density (kg/m**3) via a series of ordered triples. This relationship is normally determined prior to running DEGADIS by calculating the pertinent properties for various mixtures of air and contaminant assuming the air and contaminant mix adiabatically (i.e., without any heat transfer with the surroundings.)

For preliminary hazard assessment purposes, it may be adequate to approximate the density-concentration relationship using only two triples - one for pure air and the other for the released material. (See Spicer, Havens, and Key, Extension of DEGADIS for Modeling Aerosol Releases, in International Conference on Vapor Cloud Modeling, J.Woodward, ed., AIChE, 1987.) A nonisothermal simulation uses the energy balance in DEGADIS to calculate the mixture density (from the mixture temperature).  For a nonisothermal simulation, the user must specify the contaminant heat capacity and release temperature.  The energy balance in DEGADIS accounts for heat transfer with the surroundings and for water phase changes but does not account for contaminant phase changes.");
        }

        private void BtnAyudaTransfCalor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"DEGADIS has provisions for heat transfer to the contaminant cloud from the ground surface.");
        }

        private void BtnAyudaTempSup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The ground (surface) temperature should reflect the conditions present during the release. If unknown, the surface temperature can normally be approximated by the ambient air temperature.");
        }

        private void BtnAyudaCoefTransfCalor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"DEGADIS provides for three methods of specifying the heat transfer coefficient describing heat transfer from the ground surface to the contaminant cloud: (C) the built in (DEGADIS) correlation which is based on handbook correlations for forced and natural convection from a flat plate, (L) the LLNL correlation which is based on field scale experimental observations for LNG releases in the desert, or (V) a particular (constant) heat transfer coefficient

V) The average ground-to-cloud heat transfer coefficient is assumed to apply everywhere along the bottom surface of the cloud.

L) The form of the correlation is: Q = (Vh * rho * cp) * area * (tsurf-temp)
Vh [m/s] is based on analysis by Lawrence Livermore National Laboratory of field-scale release experiments using liquefied natural gas(LNG).");
        }

        private void BtnAyudaTransfAgua_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@" Water transfer is modeled as a mass transfer process from a water substrate to the contaminant cloud.  This transfer may be important when:
(1) the contaminant cloud is significantly colder than the water surface, AND
(2) the contaminat molecular weight is less than air.

Water transfer is modeled only in the secondary source where it would be most significant.  Water is assumed to be transferred to the secondary source cloud in the area outside the primary source.

The DEGADIS correlation option uses a heat-to-mass transfer analogy; the exact nature of theanalogy depends on whether natural or forced convection conditions exist.

Particular Value option Use a constant for the water-to-cloud mass  transfer coefficient.");
        }
        #endregion

        #region Transformaciones
        private void TxtTemperaturaSuperficie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtCoeficienteCalorInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtCoeficienteAguaInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtLlnlCorrVh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma 
            else
                e.Handled = true;
        }
        #endregion
        private void ChcIsotermico_Checked(object sender, RoutedEventArgs e)
        {
            ChcTransfCalor.IsChecked = false;
            ChcTransfCalor.IsEnabled = false;
            RdBtnCoefTransfCDegadis.IsChecked = true;
            RdBtnCoefTransfADegadis.IsChecked = true;
            TxtTemperaturaSuperficie.Text = "";
            TxtTemperaturaSuperficie.IsEnabled = false;
            RdBtnCoefTransfCDegadis.IsEnabled = false;
            RdBtnCoefTransfCLlnLCorr.IsEnabled = false;
            RdBtnCoefTransfCInput.IsEnabled = false;
            TxtCoeficienteCalorInput.Text = "";
            TxtCoeficienteCalorInput.IsEnabled = false;
            RdBtnCoefTransfADegadis.IsEnabled = false;
            RdBtnCoefTransfAInput.IsEnabled = false;
            RdBtnTransfAguaNo.IsEnabled = false;
            TxtCoeficienteAguaInput.Text = "";
            TxtCoeficienteAguaInput.IsEnabled = false;
        }

        private void ChcIsotermico_Unchecked(object sender, RoutedEventArgs e)
        {
            ChcTransfCalor.IsEnabled = true;
            TxtTemperaturaSuperficie.IsEnabled = false;
            RdBtnCoefTransfCDegadis.IsEnabled = false;
            RdBtnCoefTransfCDegadis.IsEnabled = false;
            RdBtnCoefTransfCInput.IsEnabled = false;
            TxtCoeficienteCalorInput.IsEnabled = false;
            RdBtnCoefTransfADegadis.IsEnabled = false;
            RdBtnCoefTransfAInput.IsEnabled = false;
            RdBtnTransfAguaNo.IsEnabled = false;
            TxtCoeficienteAguaInput.IsEnabled = false;
        }

        private void ChcTransfCalor_Checked(object sender, RoutedEventArgs e)
        {
            TxtTemperaturaSuperficie.IsEnabled = true;
            RdBtnCoefTransfCDegadis.IsEnabled = true;
            RdBtnCoefTransfCLlnLCorr.IsEnabled = true;
            RdBtnCoefTransfCInput.IsEnabled = true;
            RdBtnCoefTransfADegadis.IsEnabled = true;
            RdBtnCoefTransfAInput.IsEnabled = true;
            RdBtnTransfAguaNo.IsEnabled = true;
        }

        private void ChcTransfCalor_Unchecked(object sender, RoutedEventArgs e)
        {
            TxtTemperaturaSuperficie.Text = "";
            TxtCoeficienteCalorInput.Text = "";
            TxtCoeficienteAguaInput.Text = "";
            RdBtnCoefTransfCDegadis.IsChecked = true;
            RdBtnCoefTransfADegadis.IsChecked = true;
            TxtTemperaturaSuperficie.IsEnabled = false;
            RdBtnCoefTransfCDegadis.IsEnabled = false;
            RdBtnCoefTransfCLlnLCorr.IsEnabled = false;
            RdBtnCoefTransfCInput.IsEnabled = false;
            TxtCoeficienteCalorInput.IsEnabled = false;
            RdBtnCoefTransfADegadis.IsEnabled = false;
            RdBtnCoefTransfAInput.IsEnabled = false;
            RdBtnTransfAguaNo.IsEnabled = false;
            TxtCoeficienteAguaInput.IsEnabled = false;
        }


        private void RdBtnCoefTransfCDegadis_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteCalorInput.Text = "";
            TxtCoeficienteCalorInput.IsEnabled = false;
        }

        private void RdBtnCoefTransfCInput_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteCalorInput.IsEnabled = true;
        }

        private void RdBtnCoefTransfCLlnLCorr_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteCalorInput.Text = "";
            TxtCoeficienteCalorInput.IsEnabled = false;
            TxtLlnlCorrVh.Text = "0,0125";
            LblLlnlCorrVh.Visibility = Visibility.Visible;
            TxtLlnlCorrVh.Visibility = Visibility.Visible;
            TxtLlnlCorrVh.IsEnabled = true;
        }

        private void RdBtnCoefTransfCLlnLCorr_Unchecked(object sender, RoutedEventArgs e)
        {
            LblLlnlCorrVh.Visibility = Visibility.Hidden;
            TxtLlnlCorrVh.Visibility = Visibility.Hidden;
            TxtLlnlCorrVh.IsEnabled = false;
        }

        private void RdBtnTransfAguaNo_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteAguaInput.Text = "";
            TxtCoeficienteAguaInput.IsEnabled = false;
        }

        private void RdBtnCoefTransfADegadis_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteAguaInput.Text = "";
            TxtCoeficienteAguaInput.IsEnabled = false;
        }

        private void RdBtnCoefTransfAInput_Checked(object sender, RoutedEventArgs e)
        {
            TxtCoeficienteAguaInput.IsEnabled = true;
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            siguiente();
        }

        #endregion

        #region Metodos
        private void siguiente()
        {
            if (Validar())
            {
                if (cont.booljet)
                {
                    if (ChcIsotermico.IsChecked == true)
                    {
                        cont.indht = 0;
                        cont.nden = -1;
                        cont.tsurf = cont.tamb;
                    }
                    else if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == false)
                    {
                        cont.indht = 0;
                        cont.tsurf = cont.tamb;
                        cont.nden = 0;
                    }
                    else if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true)
                    {
                        cont.indht = 1;
                        cont.tsurf = Convert.ToDouble(TxtTemperaturaSuperficie.Text);
                        cont.nden = 0;
                    }
                    BaseDeDatosPropiedades baseDeDatosPropiedades = new BaseDeDatosPropiedades();
                    this.NavigationService.Navigate(baseDeDatosPropiedades);
                }
                else
                {
                    if (ChcIsotermico.IsChecked == true)
                    {
                        cont.isofl = 1;
                        cont.ihtfl = 0;
                        cont.iwtfl = 0;
                        cont.tsurf = cont.tamb;
                        cont.htco = 0.0;
                        cont.wtco = 0.0;
                    }
                    else
                    {
                        cont.isofl = 0;
                        if (ChcTransfCalor.IsChecked == false)
                        {
                            cont.ihtfl = 0;
                            cont.iwtfl = 0;
                            cont.tsurf = cont.tamb;
                            cont.htco = 0.0;
                            cont.wtco = 0.0;
                        }
                        else
                        {
                            cont.tsurf = Convert.ToDouble(TxtTemperaturaSuperficie.Text);
                            if (RdBtnCoefTransfCInput.IsChecked == true)
                            {
                                cont.ihtfl = -1;
                                cont.htco = Convert.ToDouble(TxtCoeficienteCalorInput.Text);
                            }
                            else if (RdBtnCoefTransfCDegadis.IsChecked == true)
                            {
                                cont.ihtfl = 1;
                            }
                            else
                            {
                                cont.ihtfl = 2;
                                cont.htco = Convert.ToDouble(TxtLlnlCorrVh.Text);
                            }

                            if (RdBtnCoefTransfAInput.IsChecked == true)
                            {
                                cont.iwtfl = -1;
                                cont.wtco = Convert.ToDouble(TxtCoeficienteAguaInput.Text);
                            }
                            else if (RdBtnCoefTransfADegadis.IsChecked == true)
                            {
                                cont.iwtfl = 1;
                                cont.wtco = 0.0;
                            }
                            else
                            {
                                cont.iwtfl = 0;
                                cont.wtco = 0.0;
                            }
                        }
                    }

                    BaseDeDatosPropiedades baseDeDatosPropiedades = new BaseDeDatosPropiedades();
                    this.NavigationService.Navigate(baseDeDatosPropiedades);
                }
            }
        }

        private bool Validar()
        {
            string MError = "";

            if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true)
                {
                    try { cont.tsurf = Convert.ToDouble(TxtTemperaturaSuperficie.Text); }
                    catch (Exception) { MError += "El valor ingresado para la tempratura de la superficie debe ser un numero positivo"; }
                }
            
            if (!cont.booljet)
            {

                if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true && RdBtnCoefTransfCInput.IsChecked == true)
                {
                    try { cont.htco = Convert.ToDouble(TxtCoeficienteCalorInput.Text); }
                    catch (Exception) { MError += "El valor ingresado para el coeficiente de transferncia de calor debe ser un numero positivo"; }
                }

                if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true && RdBtnCoefTransfAInput.IsChecked == true)
                {
                    try { cont.wtco = Convert.ToDouble(TxtCoeficienteAguaInput.Text); }
                    catch (Exception) { MError += "El valor ingresado para el coeficiente de transferncia de calor debe ser un numero positivo"; }
                }

                if (RdBtnCoefTransfCLlnLCorr.IsChecked == true)
                {
                    try { cont.htco = Convert.ToDouble(TxtLlnlCorrVh.Text); }
                    catch (Exception) { MError += "El valor ingresado para Vh debe ser un numero positivo"; }
                }
            }

            if (MError == "")
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
