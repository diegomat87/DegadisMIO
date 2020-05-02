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
            MessageBox.Show(@"Specification of an initial mass of contaminant over the source allows the user to model an instantaneous release of material.

 Note that this is the mass of PURE contaminant over the source and does not include any air which may be present in this initial mass.  The mass fraction of contaminant and initial radius of this initial mass of contaminant over the source are taken from the ground-level source specification at time zero.

 Specification of any contaminant present over the source at time zero...

 For any contaminant present over the source at time zero, the radius and mass fraction of this material are taken from the ground-level source specification at time zero.");
        }

        private void BtnAyudaDescrpcionTransitoria_Click(object sender, RoutedEventArgs e)
        {
            if (cont.idilut == 1) { MessageBox.Show(@"Transient Source Description...

 The primary source is described by the contaminant release rate (E[=]kg contaminant/s), radius (R1[=]m), contaminant mass fraction (PWC) and release temperature (PTEMP[=]K); these are input by ordered points as follows:

  first point:
     -- t=0, E(t=0), R1(t=0), PWC(t=0), PTEMP(t=0) (initial, nonzero values)
  second point:
     -- t=t1, E(t=t1), R1(t=t1), PWC(t=t1), PTEMP(t=t1)
               .
               .
               .
  last nonzero point:
     -- t=TEND, E(t=TEND), R1(t=TEND), PWC(t=TEND), PTEMP(t=TEND)
  next to last point:
     -- t=TEND+1., E=0., R1=0., PWC=1., PTEMP=TAMB
  last point:
     -- t=TEND+2., E=0., R1=0., PWC=1., PTEMP=TAMB

 Note: the final time (TEND) is the last time when E and R1 are non-zero."); }
            else { MessageBox.Show(@" Transient Source Description...

 The primary is described by the source mass evolution rate(E[=]kg / s) and radius(R1[=]m) for a transient release which are input by ordered triples as follows:

 first point-- t = 0, E(t = 0), R1(t = 0)(initial, nonzero values)

 second point-- t = t1, E(t = t1), R1(t = t1)
              .
              .
              .

 last nonzero point-- t = TEND, E(t = TEND), R1(t = TEND)

 next to last point-- t = TEND + 1., E = 0., R1 = 0.

 last point-- t = TEND + 2., E = 0., R1 = 0.

Note: the final time(TEND) is the last time when E and R1 are non - zero."); }

        }

        private void BtnAyudaTimeSource_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"A transient (primary) source must be specified as a function of time. The primary source starts at time=0 s.");
        }

        private void BtnAyudaReleaseRate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The evolution (release) rate is the rate contaminant (without air) is released to the atmosphere (in kg contaminant/s).");
        }

        private void BtnAyudaSourceRadius_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The primary source radius represents the area through which the evolution rate passes.  Primary sources which are not circular can normally be modeled as circular with the same area.");
        }

        private void BtnAyudaMassFraction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The source contaminant mass fraction is the mass fraction of the non-air components in the released material (at this time).  This mass fraction must be specified for a 'diluted' source. ");
        }

        private void BtnAyudaSourceTemprature_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The temperature of the released material (K) must be specified for a 'diluted' source.");
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
                MessageBox.Show("The last two entries for the source rate and the source radius must be zero. You have specified one of these values as nonzero.");
            }
            cont.SourceSoT = ListaLinea;

            cont.tinp = DateTime.Now.ToString("d'-'MMM'-'yyyy HH:mm:ss.ff");
            MessageBox.Show(cont.tinp);
            Datos.archivos archivos = new Datos.archivos();
            archivos.CrearInp(cont.u0, cont.z0, cont.zr, cont.istab, cont.avtime, cont.indvel, cont.rml, cont.tamb, cont.pamb, cont.humedad, cont.humedadrel, cont.isofl,
                   cont.tsurf, cont.ihtfl, cont.htco, cont.iwtfl, cont.wtco, cont.gasnam, cont.gasmw, cont.gastem, cont.gasrho, cont.gascpk, cont.gascpp, cont.gasulc, cont.gasllc,
                   cont.gaszzc, cont.DENtriples, cont.yclow, cont.gmass0, cont.Check4, cont.SourceSoT, cont.tinp, cont.ruta);

            FileER1 fileER1 = new FileER1();
            this.NavigationService.Navigate(fileER1);
        }

        private void generarColumnas()
        {
            if (cont.idilut == 1)
            {
                DataGridTextColumn Tiempo = new DataGridTextColumn();
                Tiempo.Header = "Tiempo [s]";
                Tiempo.Binding = new Binding("PTIME");
                DataListaTransientSource.Columns.Add(Tiempo);

                DataGridTextColumn Flujo = new DataGridTextColumn();
                Flujo.Header = "Flujo [Kg/s]";
                Flujo.Binding = new Binding("ET");
                DataListaTransientSource.Columns.Add(Flujo);

                DataGridTextColumn Radio = new DataGridTextColumn();
                Radio.Header = "Radio de la fuente [m]";
                Radio.Binding = new Binding("R1T");
                DataListaTransientSource.Columns.Add(Radio);

                DataGridTextColumn FraccionM = new DataGridTextColumn();
                FraccionM.Header = "Fraccion Molar";
                FraccionM.Binding = new Binding("PWC");
                DataListaTransientSource.Columns.Add(FraccionM);

                DataGridTextColumn TemperaturaS = new DataGridTextColumn();
                TemperaturaS.Header = "Temperatura de la fuente [K]";
                TemperaturaS.Binding = new Binding("PTEMP");
                DataListaTransientSource.Columns.Add(TemperaturaS);

                DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
                Eliminar.Header = "Eliminar";
                DataTemplate tami = new DataTemplate();
                FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
                frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, "Eliminae");
                frBoton.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(btnEliminar_Click));
                tami.VisualTree = frBoton;
                Eliminar.CellTemplate = tami;
                DataListaTransientSource.Columns.Add(Eliminar);
            }
            else
            {
                DataGridTextColumn Tiempo = new DataGridTextColumn();
                Tiempo.Header = "Tiempo [s]";
                Tiempo.Binding = new Binding("PTIME");
                DataListaTransientSource.Columns.Add(Tiempo);

                DataGridTextColumn Flujo = new DataGridTextColumn();
                Flujo.Header = "Flujo [Kg/s]";
                Flujo.Binding = new Binding("ET");
                DataListaTransientSource.Columns.Add(Flujo);

                DataGridTextColumn Radio = new DataGridTextColumn();
                Radio.Header = "Radio de la fuente [m]";
                Radio.Binding = new Binding("R1T");
                DataListaTransientSource.Columns.Add(Radio);

                DataGridTemplateColumn Eliminar = new DataGridTemplateColumn();
                Eliminar.Header = "Eliminar";
                DataTemplate tami = new DataTemplate();
                FrameworkElementFactory frBoton = new FrameworkElementFactory(typeof(Button));
                frBoton.SetValue(System.Windows.Controls.Button.ContentProperty, "Eliminae");
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
            catch (Exception) { MError += "El valor ingresado para el tiempo debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtRleaseRate.Text); }
            catch (Exception) { MError += "El valor ingresado para el flujo de contaminante debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtSourceRadius.Text); }
            catch (Exception) { MError += "El valor ingresado para el radio de la fuente debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtMassFraction.Text); }
            catch (Exception) { MError += "El valor ingresado para la fraccion masica de la fuente debe ser un numero positivo\n"; }

            try { Convert.ToDouble(TxtSourceTemprature.Text); }
            catch (Exception) { MError += "El valor ingresado para la temperatura de la fuente debe ser un numero positivo\n"; }

            if (ListaLinea.Count >= 1)
            {
                _ = new Entidades.LineaSource();
                Entidades.LineaSource linea = ListaLinea[ListaLinea.Count - 1];
                if (linea.PTIME > Convert.ToDouble(TxtTimeSource.Text)) { MError += "The source specification must be entered as a monotonicly increasing function starting at time zero."; }
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
