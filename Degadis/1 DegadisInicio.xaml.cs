using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
    /// Lógica de interacción para DegadisInicio.xaml
    /// </summary>
    public partial class DegadisInicio : Page
    {
        Controlador cont = new Controlador();
        public DegadisInicio()
        {
            InitializeComponent();
            idiomas();
        }

        private void idiomas()
        {
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            btnNuevoCaso.Content = Properties.Resources.kCrearCaso;
            lblEditar.Content = Properties.Resources.kEditCaso;
            lblBuscar.Content = Properties.Resources.KBuscCaso;
            lblSobreescribir.Content = Properties.Resources.kSobreCaso;
        }

        private void btnNuevoCaso_Click(object sender, RoutedEventArgs e)
        {
            DatosAtmosfericos datosAtmosfericos = new DatosAtmosfericos();
            this.NavigationService.Navigate(datosAtmosfericos);
            System.Windows.MessageBox.Show("Se muestran valores por defecto", "", MessageBoxButton.OK);
        }
    }
}
