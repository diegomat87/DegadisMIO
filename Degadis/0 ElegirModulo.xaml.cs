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
using System.Windows.Forms;

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para ElegirModulo.xaml
    /// </summary>
    public partial class ElegirModulo : Page
    {
        Controlador cont = new Controlador();

        public ElegirModulo()
        {
            InitializeComponent();
        }

        private void BtnJet_Click(object sender, RoutedEventArgs e)
        {
            cont.booljet = true;
            cont.nombre = txtNombre.Text;
            DegadisJetInicio degadisJetInicio = new DegadisJetInicio();
            this.NavigationService.Navigate(degadisJetInicio);            
        }

        private void BtnDeg_Click(object sender, RoutedEventArgs e)
        {
            cont.booljet = false;
            cont.nombre = txtNombre.Text;
            DegadisInicio degadisInicio = new DegadisInicio();
            this.NavigationService.Navigate(degadisInicio);
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
    }
}
