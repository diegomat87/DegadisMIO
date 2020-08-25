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
    /// Lógica de interacción para DescripcionEmisionB.xaml
    /// </summary>
    public partial class DescripcionEmisionB : Page
    {
        Controlador cont = new Controlador();
        Entidades.ListaSource ListaLinea = new Entidades.ListaSource();

        #region Constructor
        public DescripcionEmisionB()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            iniciar();
        }

        private void iniciar()
        {
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            lblTitulo.Content = Properties.Resources.kEstTrans;
            LblMasainicialFuente.Content = Properties.Resources.KMasaIni;
            LblMassFraction.Content = Properties.Resources.kFracMas;
            LblReleaseRate.Content = Properties.Resources.kLibCont;
            LblSourceRadius.Content = Properties.Resources.kRadOrig;
            LblTimeSource.Content = Properties.Resources.kTiempo;
            btnSiguiente.Content = Properties.Resources.kSiguiente;
            BtnAgregarLinea.Content = Properties.Resources.kAgregar;

            if (cont.idilut == 0)
            {
                LblMassFraction.Visibility = Visibility.Hidden;
                BtnAyudaMassFraction.Visibility = Visibility.Hidden;
                TxtMassFraction.Visibility = Visibility.Hidden;
                LblSourceTemprature.Visibility = Visibility.Hidden;
                BtnAyudaSourceTemprature.Visibility = Visibility.Hidden;
                TxtSourceTemprature.Visibility = Visibility.Hidden;
                TxtSourceTemprature.Text = Convert.ToString(cont.gastem);
                TxtMassFraction.Text = "1";
            }
            generarColumnas();
            DataListaTransientSource.ItemsSource = ListaLinea;
        }
        #endregion

        #region componentes
        #region Ayuda
        private void BtnAyudaMasaInicial_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aMasaIni);
        }

        private void BtnAyudaDescrpcionTransitoria_Click(object sender, RoutedEventArgs e)
        {
            if (cont.idilut == 1) { MessageBox.Show(Properties.Resources.aDescTrans1); }
            else { MessageBox.Show(Properties.Resources.aDescTrans2); }
        }

        private void BtnAyudaTimeSource_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aTimeSource);
        }

        private void BtnAyudaReleaseRate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aReleaseRate);
        }

        private void BtnAyudaSourceRadius_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aSourceRad);
        }

        private void BtnAyudaMassFraction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aFracMass);
        }

        private void BtnAyudaSourceTemprature_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.aTempFuente);
        }
        #endregion

        #region Transformaciones
        private void TxtTimeSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtRleaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceRadius_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtMassFraction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceTemprature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void BtnAgregarLinea_Click(object sender, RoutedEventArgs e)
        {
            agregar();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Siguiente();
        }
        private void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListaLinea.RemoveAt(DataListaTransientSource.SelectedIndex);
            DataListaTransientSource.Items.Refresh();
        }
        #endregion

        #region Metodos
        private void Siguiente()
        {
            //revisar
            cont.nsrc = ListaLinea.Count;
            Entidades.LineaSource lineaf = ListaLinea[cont.nsrc - 1];
            Entidades.LineaSource lineaf2 = ListaLinea[cont.nsrc - 2];
            if (lineaf.ET != 0.0 || lineaf2.ET != 0.0 || lineaf.R1T != 0 || lineaf2.R1T != 0)
            {
                MessageBox.Show(Properties.Resources.eET);
            }
            cont.SourceSoT = ListaLinea;

            int mesn = DateTime.Now.Month - 1;
            string[] mes = new string[12] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            cont.tinp = DateTime.Now.ToString("d'-'") + mes[mesn] + DateTime.Now.ToString("'-'yyyy HH:mm:ss.ff");
            MessageBox.Show(cont.tinp);
            Datos.archivos archivos = new Datos.archivos();
            archivos.CrearInp(cont.titles,cont.u0, cont.z0, cont.zr, cont.istab, cont.avtime, cont.indvel, cont.rml, cont.tamb, cont.pamb, cont.humedad, cont.humedadrel, cont.isofl,
                   cont.tsurf, cont.ihtfl, cont.htco, cont.iwtfl, cont.wtco, cont.gasnam, cont.gasmw, cont.gastem, cont.gasrho, cont.gascpk, cont.gascpp, cont.gasulc, cont.gasllc,
                   cont.gaszzc, cont.DENtriples, cont.yclow, cont.gmass0, cont.Check4, cont.SourceSoT, cont.tinp, cont.ruta,cont.nombre);

            FileER1 fileER1 = new FileER1();
            this.NavigationService.Navigate(fileER1);
        }

        private void generarColumnas()
        {
            if (cont.idilut == 1)
            {
                DataGridTextColumn Tiempo = new DataGridTextColumn();
                Tiempo.Header = Properties.Resources.rTiempo;
                Tiempo.Binding = new Binding("PTIME");
                DataListaTransientSource.Columns.Add(Tiempo);

                DataGridTextColumn Flujo = new DataGridTextColumn();
                Flujo.Header = Properties.Resources.rFlujo;
                Flujo.Binding = new Binding("ET");
                DataListaTransientSource.Columns.Add(Flujo);

                DataGridTextColumn Radio = new DataGridTextColumn();
                Radio.Header = Properties.Resources.rRadio;
                Radio.Binding = new Binding("R1T");
                DataListaTransientSource.Columns.Add(Radio);

                DataGridTextColumn FraccionM = new DataGridTextColumn();
                FraccionM.Header = Properties.Resources.rFracMol;
                FraccionM.Binding = new Binding("PWC");
                DataListaTransientSource.Columns.Add(FraccionM);

                DataGridTextColumn TemperaturaS = new DataGridTextColumn();
                TemperaturaS.Header = Properties.Resources.rTempFuente;
                TemperaturaS.Binding = new Binding("PTEMP");
                DataListaTransientSource.Columns.Add(TemperaturaS);

                DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
                Eliminar.Header = Properties.Resources.kEliminar;
                DataTemplate tami = new DataTemplate();
                FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
                frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, Properties.Resources.kEliminar);
                frBoton.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(btnEliminar_Click));
                tami.VisualTree = frBoton;
                Eliminar.CellTemplate = tami;
                DataListaTransientSource.Columns.Add(Eliminar);
            }
            else
            {
                DataGridTextColumn Tiempo = new DataGridTextColumn();
                Tiempo.Header = Properties.Resources.rTiempo;
                Tiempo.Binding = new Binding("PTIME");
                DataListaTransientSource.Columns.Add(Tiempo);

                DataGridTextColumn Flujo = new DataGridTextColumn();
                Flujo.Header = Properties.Resources.rFlujo;
                Flujo.Binding = new Binding("ET");
                DataListaTransientSource.Columns.Add(Flujo);

                DataGridTextColumn Radio = new DataGridTextColumn();
                Radio.Header = Properties.Resources.rRadio;
                Radio.Binding = new Binding("R1T");
                DataListaTransientSource.Columns.Add(Radio);

                DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
                Eliminar.Header = Properties.Resources.kEliminar;
                DataTemplate tami = new DataTemplate();
                FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
                frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, Properties.Resources.kEliminar);
                frBoton.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(btnEliminar_Click));
                tami.VisualTree = frBoton;
                Eliminar.CellTemplate = tami;
                DataListaTransientSource.Columns.Add(Eliminar);
            }
        }

        private void agregar()
        {
            if (Validar())
            {
                if (cont.idilut == 1)
                {
                    Entidades.LineaSource Lin = new Entidades.LineaSource();
                    Lin.PTIME = Convert.ToDouble(TxtTimeSource.Text);
                    Lin.ET = Convert.ToDouble(TxtRleaseRate.Text);
                    Lin.R1T = Convert.ToDouble(TxtSourceRadius.Text);
                    Lin.PWC = Convert.ToDouble(TxtMassFraction.Text);
                    Lin.PTEMP = Convert.ToDouble(TxtSourceTemprature.Text);
                    Lin.PFRACV = 1.0;
                    ListaLinea.Add(Lin);
                    DataListaTransientSource.Items.Refresh();
                    TxtTimeSource.Text = "";
                    TxtRleaseRate.Text = "";
                    TxtSourceRadius.Text = "";
                    TxtMassFraction.Text = "";
                    TxtSourceTemprature.Text = "";
                }
                else
                {
                    Entidades.LineaSource Lin = new Entidades.LineaSource();
                    Lin.PTIME = Convert.ToDouble(TxtTimeSource.Text);
                    Lin.ET = Convert.ToDouble(TxtRleaseRate.Text);
                    Lin.R1T = Convert.ToDouble(TxtSourceRadius.Text);
                    Lin.PWC = Convert.ToDouble(TxtMassFraction.Text);
                    Lin.PTEMP = Convert.ToDouble(TxtSourceRadius.Text);
                    Lin.PFRACV = 1.0;
                    ListaLinea.Add(Lin);
                    DataListaTransientSource.Items.Refresh();
                    TxtTimeSource.Text = "";
                    TxtRleaseRate.Text = "";
                    TxtSourceRadius.Text = "";
                    TxtMassFraction.Text = "1";
                    TxtSourceTemprature.Text = Convert.ToString(cont.gastem);
                }
            }
        }

        private bool Validar()
        {
            string MError = "";

            try { Convert.ToDouble(TxtTimeSource.Text); }
            catch (Exception) { MError += Properties.Resources.eTiempoFuente + "\n"; }

            try { Convert.ToDouble(TxtRleaseRate.Text); }
            catch (Exception) { MError += Properties.Resources.eRelease + "\n"; }

            try { Convert.ToDouble(TxtSourceRadius.Text); }
            catch (Exception) { MError += Properties.Resources.eRadFuentep + "\n"; }

            try { Convert.ToDouble(TxtMassFraction.Text); }
            catch (Exception) { MError += Properties.Resources.eFracMasica + "\n"; }

            try { Convert.ToDouble(TxtSourceTemprature.Text); }
            catch (Exception) { MError += Properties.Resources.eTempFuentp + "\n"; }

            if (ListaLinea.Count >= 1)
            {
                _ = new Entidades.LineaSource();
                Entidades.LineaSource linea = ListaLinea[ListaLinea.Count - 1];
                if (linea.PTIME > Convert.ToDouble(TxtTimeSource.Text)) { MError += Properties.Resources.eLinea2; }
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
