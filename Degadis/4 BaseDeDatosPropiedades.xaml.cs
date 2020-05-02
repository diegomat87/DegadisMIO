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
    public partial class BaseDeDatosPropiedades : Page
    {
        Controlador cont = new Controlador();
        Datos.Gases dGas = new Datos.Gases();

        #region Constructor
        public BaseDeDatosPropiedades()
        {
            InitializeComponent();
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
            MessageBox.Show(@"Pertinent contaminant properties are included in data files (extension GAS).  The contents of a data file can be edited with your favorite text editor.  An example data file is included in EXAMPLE.GAS. Blank entries in the data file are defaulted to the properties of air with the exception of the density which is calculated with the ideal gas law.");
        }

        private void BtnAyudaGasMW_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The contaminant molecular weight is used to convert between mass fraction, mole fraction, and concentration; it may also be used to estimate the density. The contaminant molecular weight can be the molecular weight of a mixture. (Any air present in the released material should not be included when determining the contaminant (mixture) molecular weight for the model. If any air is present in the released material which cannot be attributed to atmospheric mixing processes modeled by DEGADIS, the release is considered 'diluted'.) In the model, the contaminant is treated as a single species. If a mixture is released, the released mixture is treated as the contaminant.  If only one component of the released mixture is hazardous, the mole fraction of the hazardous component can be determined from the mole fraction of the released contaminant in the model output by multiplying the mole fraction of the released contaminant by the initial mole fraction of the hazardous component in the released contaminant.

Because the contaminant molecular weight converts between mass and mole fraction, 'pseudo' molecular weights should not be used such as are used at times to represent aerosol mixtures, for example.  In this model, aerosols should be modeled using the 'isothermal' option.  See the HELP under the isothermal question for further information about modeling aerosols.");
        }

        private void BtnAyudaGasTemp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The contaminant release temperature should represent the temperature of the contaminant as it enters the atmosphere. For a boiling liquid pool, the contaminant release temperature is the normal boiling point. For a pressurized liquid release, the releae, the contaminant release temperature is the normal boiling point.  (The release temperature should reflect the contaminant's condition after depressurizing to atmospheric pressure.) For an 'isothermal' simulation, the contaminant release temperature is not used.");
        }

        private void BtnAyudaGasRho_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The contaminant release density is the density of the contaminant as it enters the atmosphere.  If the contaminant release density is taken from the gas property data base, it was corrected for the simulation pressure. For an 'isothermal' release, the contaminant release density will be compared to the last entry in the density specification required later in this program.");
        }

        private void BtnAyudaGasCpk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"For the heat capacity specification, a temperature dependent heat capacity can be entered using the correlation coded into DEGADIS.  If a constant heat capacity is adequate, set GASCPK equal to the heat capacity (in J/kg/K) and GASCPP equal to zero; DEGINP will recalculate the values as used in the model.");
        }

        private void BtnAyudaGasUlc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The upper and lower levels of concern (as mole fractions) can represent such levels of concern as the lower flammability limit (LFL) and LFL/2 or the short term exposure limit (STEL) and STEL/2. The upper and lower levels of concern are used to calculate isopleths shown in the model output. The lower level of concern (LLC) is used to determine the default lowest mole fraction of interest (yclow).");
        }

        private void BtnAyudaGasZzc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The elevation for the contour (receptor) calculations is used along with the upper and lower levels of concern to calculate the isopleths shown in the model output.");
        }
        #endregion

        #region Transformaciones
        private void TxtGasMW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasTemp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasRho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasCpk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasCpp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasUlc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasLlc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtGasZzc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
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
                            MessageBoxResult result = MessageBox.Show(@" For ""isothermal"" cases, there are two options for entering the concentration density relationship:

 (1) NDEN = -1; The simulation treats the contaminant as if it were an ideal gas with a constant molal heat capacity equal to that of air. Water condensation is ignored.

 (2) NDEN > 0; In this case, NDEN is the number of triples which are used to specify the contaminant concentration and mixture density as functions of the contaminant mole fraction(based on adiabatic mixing between the released contaminant and ambient air).",
"", MessageBoxButton.OK);

                            if (result == MessageBoxResult.OK)
                            {
                                MessageBoxResult result2 = MessageBox.Show("continue with option 1 ? ", "", MessageBoxButton.YesNo);
                                if (result2 == MessageBoxResult.Yes)
                                {
                                    continuacion = false;
                                    DescripcionEmision desc2 = new DescripcionEmision();
                                    this.NavigationService.Navigate(desc2);
                                }
                                else
                                {
                                    MessageBoxResult result3 = MessageBox.Show("continue with option 2 ? ", "", MessageBoxButton.YesNo);
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
                            MessageBoxResult result = MessageBox.Show(@" For ""isothermal"" cases, there are two options for entering the concentration density relationship:

 (1) NDEN = -1; The simulation treats the contaminant as if it were an ideal gas with a constant molal heat capacity equal to that of air. Water condensation is ignored.

 (2) NDEN > 0; In this case, NDEN is the number of triples which are used to specify the contaminant concentration and mixture density as functions of the contaminant mole fraction(based on adiabatic mixing between the released contaminant and ambient air).",
"", MessageBoxButton.OK);

                            if (result == MessageBoxResult.OK)
                            {
                                MessageBoxResult result2 = MessageBox.Show("continue with option 1 ? ", "", MessageBoxButton.YesNo);
                                if (result2 == MessageBoxResult.Yes)
                                {
                                    continuacion = false;
                                    DescripcionEmision desc2 = new DescripcionEmision();
                                    this.NavigationService.Navigate(desc2);
                                }
                                else
                                {
                                    MessageBoxResult result3 = MessageBox.Show("continue with option 2 ? ", "", MessageBoxButton.YesNo);
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
                mnsError += "Debe indicar una descripcion\n";
            }

            if (TxtGasFormula.Text.Trim() == "")
            {
                mnsError += "Debe indicar una formula\n";
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
                    mnsError += "El valor ingresado para el peso molecular debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasMW.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar el Peso Molecular\n";
                }
                else
                {
                    mnsError += "El Peso Molecular debe ser un Nº decimal\n";
                }
            }

            try
            {
                if (cont.booljet)
                {
                    cont.temjet = Convert.ToDouble(TxtGasTemp.Text);
                    if (cont.temjet < 0)
                    {
                        mnsError += "El valor ingresado para la temperatura del gas debe ser un numero positivo\n";
                    }
                }
                else
                {
                    cont.gastem = Convert.ToDouble(TxtGasTemp.Text);
                    if (cont.gastem < 0)
                    {
                        mnsError += "El valor ingresado para la temperatura del gas debe ser un numero positivo\n";
                    }
                }

            }
            catch (FormatException)
            {
                if (TxtGasTemp.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar la temperatura de gas\n";
                }
                else
                {
                    mnsError += "La temperatura del gas debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gasrho = Convert.ToDouble(TxtGasRho.Text);
                cont.gasrho = cont.gasrho * cont.pamb;//porque aca
                if (cont.gasrho < 0)
                {
                    mnsError += "El valor ingresado para la densidad debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasRho.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar la densidad\n";
                }
                else
                {
                    mnsError += "La densidad debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gascpk = Convert.ToDouble(TxtGasCpk.Text);
                if (cont.gascpk < 0)
                {
                    mnsError += "El valor ingresado para la capacidad calorifica debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasCpk.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar la capacidad calorifica\n";
                }
                else
                {
                    mnsError += "La capacidad calorifica debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gascpp = Convert.ToDouble(TxtGasCpp.Text);
                if (cont.gascpp < 0)
                {
                    mnsError += "El valor ingresado para el exponente de la capacidad calorifica debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasCpp.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar el exponente de la capacidad calorifica\n";
                }
                else
                {
                    mnsError += "El exponente de la capacidad calorifica debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gasulc = Convert.ToDouble(TxtGasUlc.Text);
                if (cont.gasulc < 0)
                {
                    mnsError += "El valor ingresado para el limite superior debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasUlc.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar el limite superior\n";
                }
                else
                {
                    mnsError += "El limite superior debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gasllc = Convert.ToDouble(TxtGasLlc.Text);
                if (cont.gasllc < 0)
                {
                    mnsError += "El valor ingresado para el limite inferior debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasLlc.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar el limite inferior\n";
                }
                else
                {
                    mnsError += "El limite inferior debe ser un Nº decimal\n";
                }
            }

            try
            {
                cont.gaszzc = Convert.ToDouble(TxtGasZzc.Text);
                if (cont.gaszzc < 0)
                {
                    mnsError += "El valor ingresado para altura de la fuente debe ser un numero positivo\n";
                }
            }
            catch (FormatException)
            {
                if (TxtGasZzc.Text.Trim() == "")
                {
                    mnsError += "Debe ingresar la altura de la fuente\n";
                }
                else
                {
                    mnsError += "La altura de la fuente debe ser un Nº decimal\n";
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
                        MessageBox.Show(@"The lower limit of concern must be less than the upper limit of concern.");
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
                        MessageBox.Show(@"The lower limit of concern must be less than the upper limit of concern.");
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
