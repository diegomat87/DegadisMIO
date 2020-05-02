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
            DegadisJetInicio degadisJetInicio = new DegadisJetInicio();
            this.NavigationService.Navigate(degadisJetInicio);            
        }

        private void BtnDeg_Click(object sender, RoutedEventArgs e)
        {
            cont.booljet = false;
            DegadisInicio degadisInicio = new DegadisInicio();
            this.NavigationService.Navigate(degadisInicio);
        }
    }
}
