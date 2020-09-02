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
    public partial class BaseDeDatosPropiedades : Page
    {
        Controlador cont = new Controlador();
        Datos.Gases dGas = new Datos.Gases();

        #region Constructor
        public BaseDeDatosPropiedades()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            lblTitulo.Content = Properties.Resources.kEspCont;
            lblList.Content = Properties.Resources.kListGas;
            LblGasCpk.Content = Properties.Resources.kCapCal;
            LblGasCpp.Content = Properties.Resources.kPoteCalGas;
            LblGasDescripcion.Content = Properties.Resources.kDescrip;
            LblGasFormula.Content = Properties.Resources.kFormula;
            LblGasLlc.Content = Properties.Resources.kLLC;
            LblGasMW.Content = Properties.Resources.kPesoMol;
            LblGasRho.Content = Properties.Resources.kDensGas;
            LblGasTemp.Content = Properties.Resources.kTempCalGas;
            LblGasUlc.Content = Properties.Resources.kULC;
            LblGasZzc.Content = Properties.Resources.kElevRec;
            btnSiguiente.Content = Properties.Resources.kSiguiente;
            BtnNuevoGas.Content = Properties.Resources.kNuevoGas;

            cargarComboBox();
            if (cont.booljet)
            {
                LblGasRho.Visibility = Visibility.Hidden;
                TxtGasRho.Visibility = Visibility.Hidden;
                BtnAyudaGasRho.Visibility = Visibility.Hidden;
            }
            else
            {
                LblGasRho.Visibility = Visibility.Visible;
                TxtGasRho.Visibility = Visibility.Visible;
                BtnAyudaGasRho.Visibility = Visibility.Visible;
            }
        }

        private void cargarComboBox()
        {
            cmbGases.ItemsSource = dGas.getGases();
            cmbGases.DisplayMemberPath = "Descripcion";
        }
        #endregion

        #region Componentes
        #region ayuda
        private void BtnAyudaPropiedadesContaminante_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aPropCont);
        }

        private void BtnAyudaGasMW_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasMW);
        }

        private void BtnAyudaGasTemp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasTemp);
        }

        private void BtnAyudaGasRho_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasRho);
        }

        private void BtnAyudaGasCpk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasCPK);
        }

        private void BtnAyudaGasUlc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasULC);
        }

        private void BtnAyudaGasZzc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aGasZZc);
        }
        #endregion

        #region Transformaciones
        private void TxtGasMW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasTemp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasRho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasCpk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasCpp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma || e.Key==Key.OemMinus)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasUlc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasLlc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasZzc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void BtnNuevoGas_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                Entidades.Gas gas = new Entidades.Gas();
                gas.AlturaFuga = cont.gaszzc;
                gas.calorEspecifico = cont.gascpp;
                gas.capacidadCalorifica = cont.gascpk;
                gas.densidadFuga = cont.gasrho;
                gas.Descripcion = TxtGasDescripcion.Text;
                gas.formula = TxtGasFormula.Text;
                gas.LLC = cont.gasllc;
                gas.pesoMolecular = cont.gasmw;
                gas.tempFuga = cont.gastem;
                gas.ULC = cont.gasulc;

                dGas.setGas(gas);
            }
        }

        private void cmbGases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Entidades.Gas gas = dGas.getGas(((Entidades.Gas)cmbGases.SelectedItem).id);
            TxtGasCpk.Text = gas.calorEspecifico.ToString();
            TxtGasCpp.Text = gas.capacidadCalorifica.ToString();
            TxtGasDescripcion.Text = gas.Descripcion;
            TxtGasFormula.Text = gas.formula;
            TxtGasLlc.Text = gas.LLC.ToString();
            TxtGasMW.Text = gas.pesoMolecular.ToString();
            TxtGasRho.Text = gas.densidadFuga.ToString();
            TxtGasTemp.Text = gas.tempFuga.ToString();
            TxtGasUlc.Text = gas.ULC.ToString();
            TxtGasZzc.Text = gas.AlturaFuga.ToString();
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
                    if (cont.nden == -1)
                    {
                        bool continuacion = true;
                        while (continuacion == true)
                        {
                            MessageBoxResult result = MessageBox.Show(Properties.Resources.aNden,"", MessageBoxButton.OK);

                            if (result == MessageBoxResult.OK)
                            {
                                MessageBoxResult result2 = MessageBox.Show(Properties.Resources.aOp1, "", MessageBoxButton.YesNo);
                                if (result2 == MessageBoxResult.Yes)
                                {
                                    continuacion = false;
                                    DescripcionEmision desc2 = new DescripcionEmision();
                                    this.NavigationService.Navigate(desc2);
                                }
                                else
                                {
                                    MessageBoxResult result3 = MessageBox.Show(Properties.Resources.aOp2, "", MessageBoxButton.YesNo);
                                    if (result3 == MessageBoxResult.Yes)
                                    {
                                        continuacion = false;
                                        CurvaDeDensidad curva = new CurvaDeDensidad();
                                        this.NavigationService.Navigate(curva);
                                    }
                                    else { continuacion = true; }
                                }
                            }
                        }

                    }
                    else if (cont.nden == 0)
                    {
                        DescripcionEmision desc = new DescripcionEmision();
                        this.NavigationService.Navigate(desc);
                    }
                    else
                    {
                        bool continuacion = true;
                        while (continuacion == true)
                        {
                            MessageBoxResult result = MessageBox.Show(Properties.Resources.aNden,"", MessageBoxButton.OK);

                            if (result == MessageBoxResult.OK)
                            {
                                MessageBoxResult result2 = MessageBox.Show(Properties.Resources.aOp1, "", MessageBoxButton.YesNo);
                                if (result2 == MessageBoxResult.Yes)
                                {
                                    continuacion = false;
                                    DescripcionEmision desc2 = new DescripcionEmision();
                                    this.NavigationService.Navigate(desc2);
                                }
                                else
                                {
                                    MessageBoxResult result3 = MessageBox.Show(Properties.Resources.aOp2, "", MessageBoxButton.YesNo);
                                    if (result3 == MessageBoxResult.Yes)
                                    {
                                        continuacion = false;
                                        CurvaDeDensidad curva = new CurvaDeDensidad();
                                        this.NavigationService.Navigate(curva);
                                    }
                                    else { continuacion = true; }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (cont.isofl == 1)
                    {
                        CurvaDeDensidad curva = new CurvaDeDensidad();
                        this.NavigationService.Navigate(curva);
                    }
                    else
                    {
                        DescripcionEmision desc = new DescripcionEmision();
                        this.NavigationService.Navigate(desc);
                    }
                }
            }
        }

        private bool Validar()
        {
            string mnsError = "";

            if (TxtGasDescripcion.Text.Trim() == "")
            {
                mnsError += Properties.Resources.eDescrip + "\n";
            }

            if (TxtGasFormula.Text.Trim() == "")
            {
                mnsError += Properties.Resources.eFormula + "\n";
            }
            else
            {
                cont.gasnam = TxtGasFormula.Text.ToUpper();
            }

            try
            {
                cont.gasmw = Convert.ToDouble(TxtGasMW.Text);
                if (cont.gasmw < 0)
                {
                    mnsError += Properties.Resources.eMWp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasMW.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eMW + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eMWd + "\n";
                }
            }

            try
            {
                if (cont.booljet)
                {
                    cont.temjet = Convert.ToDouble(TxtGasTemp.Text);
                    if (cont.temjet < 0)
                    {
                        mnsError += Properties.Resources.eGasTempp + "\n";
                    }
                }
                else
                {
                    cont.gastem = Convert.ToDouble(TxtGasTemp.Text);
                    if (cont.gastem < 0)
                    {
                        mnsError += Properties.Resources.eGasTempp + "\n";
                    }
                }

            }
            catch (FormatException)
            {
                if (TxtGasTemp.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasTemp + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasTempd + "\n";
                }
            }

            try
            {
                cont.gasrho = Convert.ToDouble(TxtGasRho.Text);
                cont.gasrho = cont.gasrho * cont.pamb;//porque aca
                if (cont.gasrho < 0)
                {
                    mnsError += Properties.Resources.eGasRhop + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasRho.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasRho + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasRhod + "\n";
                }
            }

            try
            {
                cont.gascpk = Convert.ToDouble(TxtGasCpk.Text);
                if (cont.gascpk < 0)
                {
                    mnsError += Properties.Resources.eGasCPKp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasCpk.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasCPK + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasCPKd + "\n";
                }
            }

            try
            {
                cont.gascpp = Convert.ToDouble(TxtGasCpp.Text);
                if (cont.gascpp < 0)
                {
                    mnsError += Properties.Resources.eGasCPPp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasCpp.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasCPP + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasCPPd + "\n";
                }
            }

            try
            {
                cont.gasulc = Convert.ToDouble(TxtGasUlc.Text);
                if (cont.gasulc < 0.0 || cont.gasulc>1.0)
                {
                    mnsError += Properties.Resources.eGasULCp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasUlc.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasULC + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasULCd + "\n";
                }
            }

            try
            {
                cont.gasllc = Convert.ToDouble(TxtGasLlc.Text);
                if (cont.gasllc < 0.0 || cont.gasllc>1.0)
                {
                    mnsError += Properties.Resources.eGasLLCp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasLlc.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasLLC + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasLLCd + "\n";
                }
            }

            try
            {
                cont.gaszzc = Convert.ToDouble(TxtGasZzc.Text);
                if (cont.gaszzc < 0)
                {
                    mnsError += Properties.Resources.eGasZZCp + "\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasZzc.Text.Trim() == "")
                {
                    mnsError += Properties.Resources.eGasZZC + "\n";
                }
                else
                {
                    mnsError += Properties.Resources.eGasZZCd + "\n";
                }
            }

            if (mnsError == "")
            {
                if (cont.booljet)
                {
                    if (cont.gasmw == 0.0) { cont.gasmw = cont.wma; }
                    if (cont.temjet == 0.0) { cont.temjet = cont.tamb; }
                    if (cont.gasrho == 0.0) { cont.gasrho = cont.pamb * cont.gasmw / cont.rgas / cont.temjet; }
                    if (cont.gascpk == 0.0 && cont.gascpp == 0.0) { cont.gascpk = 1006.3; cont.gascpp = 0.0; }
                    if (cont.gasulc == 0.0) { cont.gasulc = 0.05; }
                    if (cont.gasllc == 0.0) { cont.gasllc = cont.gasulc / 2.0; }

                    if (Convert.ToDouble(TxtGasUlc.Text) > Convert.ToDouble(TxtGasLlc.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.eLimite);
                        if (Convert.ToDouble(TxtGasUlc.Text) == Convert.ToDouble(TxtGasLlc.Text)) { TxtGasLlc.Text = Convert.ToString(Convert.ToDouble(TxtGasUlc.Text) / 2.0); }

                        return false;
                    }
                }
                else
                {
                    if (cont.gasmw == 0.0) { cont.gasmw = cont.wma; }
                    if (cont.gastem == 0.0) { cont.gastem = cont.tamb; }
                    if (cont.gasrho == 0.0) { cont.gasrho = cont.pamb * cont.gasmw / cont.rgas / cont.gastem; }
                    if (cont.gascpk == 0.0 && cont.gascpp == 0.0) { cont.gascpk = 1006.3; cont.gascpp = 0.0; }
                    if (cont.gasulc == 0.0) { cont.gasulc = 0.05; }
                    if (cont.gasllc == 0.0) { cont.gasllc = cont.gasulc / 2.0; }

                    if (Convert.ToDouble(TxtGasUlc.Text) > Convert.ToDouble(TxtGasLlc.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.eLimite);
                        if (Convert.ToDouble(TxtGasUlc.Text) == Convert.ToDouble(TxtGasLlc.Text)) { TxtGasLlc.Text = Convert.ToString(Convert.ToDouble(TxtGasUlc.Text) / 2.0); }

                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show(mnsError);
                return false;
            }
        }
        #endregion
    }
}
