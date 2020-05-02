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
    /// Lógica de interacción para CurvaDeDensidad.xaml
    /// </summary>
    public partial class CurvaDeDensidad : Page
    {
        Controlador cont = new Controlador();
        Entidades.ListaDEN ListaLinea = new Entidades.ListaDEN();

        #region constructores
        public CurvaDeDensidad()
        {
            InitializeComponent();
            cont.DENtriples = null;
            cont.rhoe = cont.gasrho;
            cont.rhoa = cont.pamb * (1.0 + cont.humedad) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humedad)) / cont.tamb;
            generarColumnas();
            DataListaDEN.ItemsSource = ListaLinea;
        }
        #endregion

        #region Componentes
        #region Ayudas
        private void BtnAyudaDenCurv_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The density is determined as a function of concentration by a listing of ordered triples supplied by the user.  Use the following form:
first point    -- pure air  y=0.0,Cc=0.0,RHOG=RHOA=kg/m**3,
          .
          .
          .
last point     -- pure gas  y=1.0,Cc=RHOE,RHOG=RHOE");
        }
        private void BtnAyudaFraccionMolarCont_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The contaminant mole fraction is the first element in each ordered triple. The triples must start with pure air (contaminant mole fraction = 0.) and increase to the last point with pure released contaminant.

The contaminant mole fraction must be entered as a monotonically increasing function starting with pure air (i.e.contaminant mole fraction =0.0).");
        }
        private void BtnAyudaConcentracionContaminante_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The contaminant concentration (in kg/m**3) entered here should correspond with the contaminant mole fraction just entered. For a contaminant mole fraction of zero, the contaminant concentration must also be zero. For a contaminant mole fraction of one, the contaminant concentration must also be equal to the pure contaminant density.");
        }
        private void BtnAyudaDensidadMezcla_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The mixture density (in kg/m**3) entered here should correspond with the contaminant mole fraction and contaminant concentration just entered. For a contaminant mole fraction of zero, the mixture density must be the ambient air density.
For a contaminant mole fraction of one, the mixture density must be equal to the pure contaminant density.");
        }
        #endregion

        #region Transformaciones
        private void TxtFraccionMContaminante_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        private void TxtConcentracionContaminante_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        private void TxtDensidadMezcla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion
        private void BtnAgregarFila_Click(object sender, RoutedEventArgs e)
        {
            agregar();
        }
        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            cont.nden = ListaLinea.Count;
            Entidades.LineaDensidad linea = ListaLinea[cont.nden - 1];
            if (cont.booljet == false)
            {
                if (linea.Den2 / cont.gasrho > 1.005 || cont.gasrho / linea.Den2 > 1.005 || linea.Den3 / cont.gasrho > 1.005 || cont.gasrho / linea.Den3 > 1.005)
                {
                    linea.Den2 = cont.gasrho; linea.Den3 = cont.gasrho; MessageBox.Show("Contaminant density corrected to" + Convert.ToString(cont.gasrho) + " kg/m\xB3");
                }
                cont.DENtriples = ListaLinea;

                DescripcionEmision Emision = new DescripcionEmision();
                this.NavigationService.Navigate(Emision);
            }
        }

        private void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListaLinea.RemoveAt(DataListaDEN.SelectedIndex);
            DataListaDEN.Items.Refresh();
        }
        #endregion

        #region Metodos
        private void generarColumnas()
        {
            DataGridTextColumn fraccion = new DataGridTextColumn();
            fraccion.Header = "Contaminant mole fraction";
            fraccion.Binding = new Binding("Den1");
            DataListaDEN.Columns.Add(fraccion);

            DataGridTextColumn concentracion = new DataGridTextColumn();
            concentracion.Header = "Contaminant concentration [Kg/m\xB3]";
            concentracion.Binding = new Binding("Den2");
            DataListaDEN.Columns.Add(concentracion);

            DataGridTextColumn densidad = new DataGridTextColumn();
            densidad.Header = "Mixture density [Kg/m\xB3]";
            densidad.Binding = new Binding("Den3");
            DataListaDEN.Columns.Add(densidad);

            DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
            Eliminar.Header = "Eliminar";
            DataTemplate tami = new DataTemplate();
            FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
            frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, "Eliminae");
            frBoton.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(btnEliminar_Click));
            tami.VisualTree = frBoton;
            Eliminar.CellTemplate = tami;
            DataListaDEN.Columns.Add(Eliminar);
        }
        private void agregar()
        {
            if (Validar())
            {
                Entidades.LineaDensidad Lin = new Entidades.LineaDensidad();
                Lin.Den1 = Convert.ToDouble(TxtFraccionMContaminante.Text);
                Lin.Den2 = Convert.ToDouble(TxtConcentracionContaminante.Text);
                Lin.Den3 = Convert.ToDouble(TxtDensidadMezcla.Text);
                Lin.Den4 = 0;
                Lin.Den5 = cont.tamb;
                ListaLinea.Add(Lin);
                DataListaDEN.Items.Refresh();
                TxtConcentracionContaminante.Text = "";
                TxtDensidadMezcla.Text = "";
                TxtFraccionMContaminante.Text = "";
            }
        }
        private bool Validar()
        {
            string MError = "";

            try { Convert.ToDouble(TxtFraccionMContaminante.Text); }
            catch (Exception) { MError += "El valor ingresado para la fraccion molar debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtConcentracionContaminante.Text); }
            catch (Exception) { MError += "El valor ingresado para la Concentracion debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtDensidadMezcla.Text); }
            catch (Exception) { MError += "El valor ingresado para la densidad de la mezcla debe ser un numero positivo\n"; }

            if (ListaLinea.Count == 0)
            {
                if (Convert.ToDouble(TxtDensidadMezcla.Text) / cont.rhoa > 1.005 || cont.rhoa / Convert.ToDouble(TxtDensidadMezcla.Text) > 1.005)
                {
                    TxtDensidadMezcla.Text = Convert.ToString(cont.rhoa); MessageBox.Show("Air density corrected to" + Convert.ToString(cont.rhoa) + " kg/m\xB3");
                }
            }
            else if (ListaLinea.Count >= 1)
            {
                _ = new Entidades.LineaDensidad();
                Entidades.LineaDensidad Lin1 = ListaLinea[ListaLinea.Count - 1];
                if (Lin1.Den1 > Convert.ToDouble(TxtFraccionMContaminante.Text)) { MError = "The contaminant mole fraction must be entered as a monotonically increasing function starting with pure air (i.e. contaminant mole fraction =0.0).\n"; }
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
        #endregion

    }
}
