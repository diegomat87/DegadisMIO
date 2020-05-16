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
using System.Diagnostics;

namespace Degadis
{
    /// <summary>
    /// Lógica de interacción para FileER3.xaml
    /// </summary>
    public partial class FileER3 : Page
    {
        Controlador cont = new Controlador();
        Datos.archivos arch = new Datos.archivos();
        double ert1, erdt, erntim, check5, sigx_flag;

        private void BtnAyudaSigxflag_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"sigx_flag    0.       no correction for x-direction dispersion");
        }

        private void BtnAyudaCHECK5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"The value of CHECK5 determines whether the above sort parameters are used. CHECK5 is  initialized through the passed transfer files to FALSE. CHECK5 is set to TRUE if a real value of 1 is passed in this file.");
        }

        public FileER3()
        {
            InitializeComponent();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                arch.crearER3(ert1, erdt, erntim, check5, sigx_flag, cont.ruta,cont.nombre);
                MessageBox.Show("Archivo creado");
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                Process p = new Process();
                psi.WorkingDirectory = cont.ruta;
                psi.Arguments = "/k deg1 prueba"; //Cambiar a /c cuando este revisado
                psi.WindowStyle = ProcessWindowStyle.Normal;
                p.StartInfo = psi;
                p.Start();
            }
        }

        private bool validar()
        {

            string MError = "";

            try { ert1 = Convert.ToDouble(TxtErt1.Text); }
            catch (FormatException) { MError += "El valor ingresado para ERT1 debe ser un numero\n"; }

            try { erdt = Convert.ToDouble(TxtErdt.Text); }
            catch (FormatException) { MError += "El valor ingresado para erdt debe ser un numero positivo\n"; }

            try { erntim = Convert.ToDouble(TxtErntim.Text); }
            catch (FormatException) { MError += "El valor ingresado para erntim debe ser un numero positivo\n"; }

            try { check5 = Convert.ToDouble(TxtCheck5.Text); }
            catch (FormatException) { MError += "El valor ingresado para check5 ser un numero positivo\n"; }

            try { sigx_flag = Convert.ToDouble(TxtSigxflag.Text); }
            catch (FormatException) { MError += "El valor ingresado para sigx-flag ser un numero positivo\n"; }

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
    }
}
