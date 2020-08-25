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
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para DegadisJetInicio.xaml
    /// </summary>
    public partial class DegadisJetInicio : Page
    {
        Controlador cont = new Controlador();
        public DegadisJetInicio()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            idiomas();
        }

        private void idiomas()
        {
            btnNuevoCaso.Content = Properties.Resources.kCrearCaso;
            lblEditar.Content = Properties.Resources.kEditCaso;
            lblBuscar.Content = Properties.Resources.KBuscCaso;
            lblSobreescribir.Content = Properties.Resources.kSobreCaso;
        }

        private void BtnNuevoCaso_Click(object sender, RoutedEventArgs e)
        {
            cont.nden = 0;
            cont.itran = 0;
            DatosAtmosfericos datosAtmosfericos = new DatosAtmosfericos();
            this.NavigationService.Navigate(datosAtmosfericos);
            MessageBox.Show(Properties.Resources.kValDefecto, "", MessageBoxButton.OK);
        }
    }
}
