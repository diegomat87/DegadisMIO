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
    /// Lógica de interacción para DegadisJetInicio.xaml
    /// </summary>
    public partial class DegadisJetInicio : Page
    {
        Controlador cont = new Controlador();
        public DegadisJetInicio()
        {
            InitializeComponent();
            this.Title = "Degadis";
        }

        private void BtnNuevoCaso_Click(object sender, RoutedEventArgs e)
        {
            cont.nden = 0;
            cont.itran = 0;
            DatosAtmosfericos datosAtmosfericos = new DatosAtmosfericos();
            this.NavigationService.Navigate(datosAtmosfericos);
            MessageBox.Show("Se muestran valores por defecto", "", MessageBoxButton.OK);
        }
    }
}
