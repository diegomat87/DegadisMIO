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
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
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
            MessageBox.Show(@"The (default) lowest mole fraction of interest is based on the lowest level of concern given before. If you wish to follow contaminant mole fractions lower than the lowest level of concern, set the lowest mole fraction of interest accordingly. For steady-state simulations, the calculations are discontinued when the mole fraction is less than the lowest level of concern. For transient simulations, the calculations are discontinued when the mole fraction is less than one-fifth of the lowest level of concern.");
        }

        private void BtnAyudaPoD_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The model can consider releases which are 'pure' or 'diluted' as follows:

A release is considered to be pure if no air is present in the released gas or aerosol. An example of a pure release would be a boiling liquid pool. The gas leaving the boiling liquid pool contains no air which comes from the pool.  For a pure release, the pure contaminant release rate (kg contaminant/s) will be required. 

A release is considered to be diluted if air is present in the released gas or aerosol. An example of a diluted release would be the discharge from an exhaust (when air is present in the exhaust stream).  The gas entering the atmosphere contains air which cannot be attributed to the atmospheric mixing processes where the stream is released.  For a diluted release, the PURE contaminant release rate (without air in kg contaminant/s) will be required; the mass fraction of air in the released gas will also be required.");
        }

        private void BtnAyudaSoT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"In a transient release, the source characteristics vary with time; in a steady state release, the source characteristics do not vary with time.  A release is also considered transient if the source characteristics do not vary with time but the duration of the source is limited.

There are three time scales which help determine whether a release is modeled as steady state or transient: 
'trel' is the duration of the (secondary) source,
'thaz' is the averaging time associated with a given contaminant concentration for hazard assessment purposes, and 
'ttrav' is the travel time(based on the mean advection speed of the contaminant) to (1) the lowest concentration of interest OR (2) the farthest downwind receptor.

A release is considered steady state when 'trel' >> 'thaz' and 'ttrav';
'tdy' should be set to 'thaz'.

A release is considered transient when it cannot be considered steady state. 'tdy' should be set to the smaller of 'trel' or 'thaz'(to properly represent the lateral plume meander).  When 'trel'< 'thaz', the concentration time history at a point (or receptor) available from DEG4 should be averaged over 'thaz' for consequence analysis.");
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
            catch (Exception) { MError += "El valor ingresado para la menor fraccion molar de interes debe ser un numero positivo"; }

            if (MError.Length == 0)
            {
                if (Convert.ToDouble(TxtYclow.Text) <= 0 || Convert.ToDouble(TxtYclow.Text) > 1)
                {
                    MessageBox.Show(@"El valor de la fraccion molar debe estar entre 0 y 1");
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
