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
    /// Lógica de interacción para EspecificacionesFuga.xaml
    /// </summary>
    public partial class EspecificacionesFuga : Page
    {
        Controlador cont = new Controlador();

        #region Constructor
        public EspecificacionesFuga()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            Iniciar();
        }

        private void Iniciar()
        {
            
            lblTitulo.Content = Properties.Resources.kTitulo;
            LblCoefTransfCalor.Content = Properties.Resources.kCoefTransCal;
            LbLIsotermico.Content = Properties.Resources.kIsoterma;
            LblLlnlCorrVh.Content = Properties.Resources.kLLNL;
            LblTempSup.Content = Properties.Resources.kTempSuelo;
            LblTransfAgua.Content = Properties.Resources.kTransfereAgua;
            LblTransfCalor.Content = Properties.Resources.kTransfCalor;
            RdBtnCoefTransfADegadis.Content = Properties.Resources.kCorrDegadis;
            RdBtnCoefTransfAInput.Content = Properties.Resources.kValPar;
            RdBtnCoefTransfCDegadis.Content = Properties.Resources.kCorrDegadis;
            RdBtnCoefTransfCInput.Content = Properties.Resources.kValPar;
            RdBtnCoefTransfCLlnLCorr.Content = Properties.Resources.kCorrLLNL;
            RdBtnTransfAguaNo.Content = Properties.Resources.kNo;
            btnSiguiente.Content = Properties.Resources.kSiguiente;
            ChcIsotermico.Content = Properties.Resources.kSi;
            ChcTransfCalor.Content = Properties.Resources.kSi;

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
            MessageBox.Show(Properties.Resources.aIsotermico);
        }

        private void BtnAyudaTransfCalor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aTransCal);
        }

        private void BtnAyudaTempSup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aTempSup);
        }

        private void BtnAyudaCoefTransfCalor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aCoefTranCal);
        }

        private void BtnAyudaTransfAgua_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aTranAgua);
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
                    catch (Exception) { MError += Properties.Resources.eTempSup; }
                }
            
            if (!cont.booljet)
            {

                if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true && RdBtnCoefTransfCInput.IsChecked == true)
                {
                    try { cont.htco = Convert.ToDouble(TxtCoeficienteCalorInput.Text); }
                    catch (Exception) { MError += Properties.Resources.eTransCal; }
                }

                if (ChcIsotermico.IsChecked == false && ChcTransfCalor.IsChecked == true && RdBtnCoefTransfAInput.IsChecked == true)
                {
                    try { cont.wtco = Convert.ToDouble(TxtCoeficienteAguaInput.Text); }
                    catch (Exception) { MError += Properties.Resources.eTransCal; }
                }

                if (RdBtnCoefTransfCLlnLCorr.IsChecked == true)
                {
                    try { cont.htco = Convert.ToDouble(TxtLlnlCorrVh.Text); }
                    catch (Exception) { MError += Properties.Resources.eTransLLNL; }
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
