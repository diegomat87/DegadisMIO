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
    /// Lógica de interacción para DescripcionEmision.xaml
    /// </summary>
    public partial class DescripcionEmision : Page
    {
        Controlador cont = new Controlador();

        #region Constructor
        public DescripcionEmision()
        {
            InitializeComponent();
            iniciar();
        }

        private void iniciar()
        {
            lblTitulo.Content = Properties.Resources.kDescVer;
            LblYclow.Content = Properties.Resources.kFracMolBaja;
            LblEmisionPoD.Content = Properties.Resources.kPoD;
            LblSteadyorTransient.Content = Properties.Resources.kEoT;
            RdBtnDiluted.Content = Properties.Resources.kDiluido;
            RdBtnPure.Content = Properties.Resources.kPuro;
            RdBtnSteady.Content = Properties.Resources.kEstacionaria;
            RdBtnTransient.Content = Properties.Resources.kTransitoria;
            btnSiguiente.Content = Properties.Resources.kSiguiente;

            TxtYclow.Text = cont.gasllc.ToString();
            if (cont.booljet == true)
            {
                LblYclow.Visibility = Visibility.Hidden;
                TxtYclow.Visibility = Visibility.Hidden;
                BtnAyudaYclow.Visibility = Visibility.Hidden;
                LblEmisionPoD.Visibility = Visibility.Hidden;
                BtnAyudaPoD.Visibility = Visibility.Hidden;
                RdBtnPure.Visibility = Visibility.Hidden;
                RdBtnDiluted.Visibility = Visibility.Hidden;
            }
            else
            {
                LblYclow.Visibility = Visibility.Visible;
                TxtYclow.Visibility = Visibility.Visible;
                BtnAyudaYclow.Visibility = Visibility.Visible;
                LblEmisionPoD.Visibility = Visibility.Visible;
                BtnAyudaPoD.Visibility = Visibility.Visible;
                RdBtnPure.Visibility = Visibility.Visible;
                RdBtnDiluted.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Componenetes        
        #region Transfornaciones
        private void TxtYclow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void BtnAyudaYclow_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aYclow);
        }

        private void BtnAyudaPoD_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aPoD);
        }

        private void BtnAyudaSoT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aSoT);
        }
        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            siguiente();
        }
        #endregion

        #region Metodos
        private void siguiente()
        {
            if (cont.booljet == false)
            {
                if (Validar())
                {
                    if (RdBtnPure.IsChecked == true && RdBtnSteady.IsChecked == true)
                    {
                        cont.idilut = 0;
                        cont.itran = 3;
                        cont.gmass0 = 0;
                        cont.Check4 = true;
                        DescripcionEmisionA descripcionEmisionA = new DescripcionEmisionA();
                        this.NavigationService.Navigate(descripcionEmisionA);
                    }
                    else if (RdBtnPure.IsChecked == true && RdBtnSteady.IsChecked == false)
                    {
                        cont.idilut = 0;
                        cont.itran = 4;
                        cont.Check4 = false;
                        DescripcionEmisionB descripcionEmisionB1 = new DescripcionEmisionB();
                        this.NavigationService.Navigate(descripcionEmisionB1);
                    }
                    else if (RdBtnPure.IsChecked == false && RdBtnSteady.IsChecked == true)
                    {
                        cont.idilut = 1;
                        cont.itran = 3;
                        cont.gmass0 = 0;
                        cont.Check4 = true;
                        DescripcionEmisionA descripcionEmisionA = new DescripcionEmisionA();
                        this.NavigationService.Navigate(descripcionEmisionA);
                    }
                    else
                    {
                        cont.idilut = 1;
                        cont.itran = 4;
                        cont.Check4 = false;
                        DescripcionEmisionB descripcionEmisionB2 = new DescripcionEmisionB();
                        this.NavigationService.Navigate(descripcionEmisionB2);
                    }
                }
            }
            else
            {
                if (RdBtnSteady.IsChecked == true)
                {
                    cont.itran = 0;
                    cont.Check4 = false;
                }
                else
                {
                    cont.itran = 1;
                    cont.Check4 = true;
                }
                DescripcionEmisionJet descripcionEmisionJet = new DescripcionEmisionJet();
                this.NavigationService.Navigate(descripcionEmisionJet);
            }
        }

        private bool Validar()
        {
            string MError = "";
            try { cont.yclow = Convert.ToDouble(TxtYclow.Text); }
            catch (Exception) { MError += Properties.Resources.eYclowp; }

            if (MError.Length == 0)
            {
                if (Convert.ToDouble(TxtYclow.Text) <= 0 || Convert.ToDouble(TxtYclow.Text) > 1)
                {
                    MessageBox.Show(Properties.Resources.eYclow01);
                    return false;
                }
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
