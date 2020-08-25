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
using System.Threading;
using System.Globalization;

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
            cont.idioma = new CultureInfo("es-AR");
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            idiomas();
        }

        private void idiomas()
        {
            Thread.CurrentThread.CurrentCulture = cont.idioma;
            Thread.CurrentThread.CurrentUICulture = cont.idioma;
            btnSelect.Content = Properties.Resources.kSelCarp;
            BtnDeg.Content = Properties.Resources.kMDeg;
            BtnJet.Content = Properties.Resources.kMJet;
            lblNombre.Content = Properties.Resources.kNomArch;            
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
