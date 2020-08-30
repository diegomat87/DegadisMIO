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
            cont.rhoa = cont.pamb * (1.0 + cont.humid) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humid)) / cont.tamb;
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            lblTitulo.Content = Properties.Resources.kCurva;
            LblConcntracionContaminante.Content = Properties.Resources.kConcCon;
            LblDensidadMezcla.Content = Properties.Resources.kDensCon;
            LblFraccionMContaminante.Content = Properties.Resources.kFracMol;
            btnSiguiente.Content = Properties.Resources.kSiguiente;
            BtnAgregarFila.Content = Properties.Resources.kAgregar;

            generarColumnas();
            DataListaDEN.ItemsSource = ListaLinea;
        }
        #endregion

        #region Componentes
        #region Ayudas
        private void BtnAyudaDenCurv_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aDenCurv);
        }
        private void BtnAyudaFraccionMolarCont_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aFracMol);
        }
        private void BtnAyudaConcentracionContaminante_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aConCont);
        }
        private void BtnAyudaDensidadMezcla_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aDenMezcla);
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
                    linea.Den2 = cont.gasrho; linea.Den3 = cont.gasrho; MessageBox.Show(Properties.Resources.kContDenCor + " " + Convert.ToString(cont.gasrho) + " kg/m\xB3");
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
            fraccion.Header = Properties.Resources.rContFracMol;
            fraccion.Binding = new Binding("Den1");
            DataListaDEN.Columns.Add(fraccion);

            DataGridTextColumn concentracion = new DataGridTextColumn();
            concentracion.Header = Properties.Resources.rConCont;
            concentracion.Binding = new Binding("Den2");
            DataListaDEN.Columns.Add(concentracion);

            DataGridTextColumn densidad = new DataGridTextColumn();
            densidad.Header = Properties.Resources.rDenMezcla;
            densidad.Binding = new Binding("Den3");
            DataListaDEN.Columns.Add(densidad);

            DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
            Eliminar.Header = Properties.Resources.kEliminar;
            DataTemplate tami = new DataTemplate();
            FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
            frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, Properties.Resources.kEliminar);
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
            catch (Exception) { MError += Properties.Resources.eFracMolp + "\n"; }

            try { Convert.ToDouble(TxtConcentracionContaminante.Text); }
            catch (Exception) { MError += Properties.Resources.eConcp + "\n"; }

            try { Convert.ToDouble(TxtDensidadMezcla.Text); }
            catch (Exception) { MError += Properties.Resources.eDenMezcla + "\n"; }

            if (ListaLinea.Count == 0)
            {
                if (Convert.ToDouble(TxtDensidadMezcla.Text) / cont.rhoa > 1.005 || cont.rhoa / Convert.ToDouble(TxtDensidadMezcla.Text) > 1.005)
                {
                    TxtDensidadMezcla.Text = Convert.ToString(cont.rhoa); MessageBox.Show(Properties.Resources.eDenAireCor + " " + Convert.ToString(cont.rhoa) + " kg/m\xB3");
                }
            }
            else if (ListaLinea.Count >= 1)
            {
                Entidades.LineaDensidad Lin1 = ListaLinea[ListaLinea.Count - 1];
                if (Lin1.Den1 > Convert.ToDouble(TxtFraccionMContaminante.Text)) { MError = Properties.Resources.eFracMolCont + "\n"; }
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
