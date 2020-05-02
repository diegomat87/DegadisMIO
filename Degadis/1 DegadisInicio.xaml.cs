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
        }

        private void btnNuevoCaso_Click(object sender, RoutedEventArgs e)
        {
            DatosAtmosfericos datosAtmosfericos = new DatosAtmosfericos();
            this.NavigationService.Navigate(datosAtmosfericos);
            System.Windows.MessageBox.Show("Se muestran valores por defecto", "", MessageBoxButton.OK);
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            using (var fd = new FolderBrowserDialog())
            {
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                {
                    string ruta = "D:\\CAIMI\\Modelos\\DEGADIS\\Degexe";
                    ruta = fd.SelectedPath;
                    cont.ruta = ruta;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FileER3 fileER3 = new FileER3();
            //this.NavigationService.Navigate(fileER3);
        }
    }
}
