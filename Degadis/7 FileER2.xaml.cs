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
    /// Lógica de interacción para FileER2.xaml
    /// </summary>
    public partial class FileER2 : Page
    {
        Controlador cont = new Controlador();
        Datos.archivos arch = new Datos.archivos();
        double sy0er, erro, sz0er, wtaio, wtqoo, wtszo, errp, smxp, wtszp, wtsyp, wtbep, wtdh, errg, smxg, ertdnf, ertupf, wtruh, wtdhg, stpo, stpp, odlp, odllp, stpg, odlg, odllg, nobs;
        public FileER2()
        {
            InitializeComponent();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                arch.crearER2(sy0er, erro, sz0er, wtaio, wtqoo, wtszo, errp, smxp, wtszp, wtsyp, wtbep, wtdh, errg, smxg, ertdnf, ertupf, wtruh, wtdhg, stpo, stpp, odlp, odllp, stpg, odlg, odllg, nobs, cont.ruta);
                MessageBox.Show("Archivo creado");
                FileER3 fileER3 = new FileER3();
                this.NavigationService.Navigate(fileER3);
            }
        }

        private bool validar()
        {

            string MError = "";

            try { sy0er = Convert.ToDouble(TxtSy0er.Text); }
            catch (FormatException) { MError += "El valor ingresado para sy0er debe ser un numero\n"; }

            try { erro = Convert.ToDouble(TxtErro.Text); }
            catch (FormatException) { MError += "El valor ingresado para erro debe ser un numero positivo\n"; }

            try { sz0er = Convert.ToDouble(TxtSz0er.Text); }
            catch (FormatException) { MError += "El valor ingresado para sz0er debe ser un numero positivo\n"; }

            try { wtaio = Convert.ToDouble(TxtWtaio.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtaio ser un numero positivo\n"; }

            try { wtqoo = Convert.ToDouble(TxtWtqoo.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtqoo ser un numero positivo\n"; }

            try { wtszo = Convert.ToDouble(TxtWtszo.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtszo debe ser un numero\n"; }

            try { errp = Convert.ToDouble(TxtErrp.Text); }
            catch (FormatException) { MError += "El valor ingresado para errp debe ser un numero positivo\n"; }

            try { smxp = Convert.ToDouble(TxtSmxp.Text); }
            catch (FormatException) { MError += "El valor ingresado para smxp debe ser un numero positivo\n"; }

            try { wtszp = Convert.ToDouble(TxtWtszp.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtszp ser un numero positivo\n"; }

            try { wtsyp = Convert.ToDouble(TxtWtsyp.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtsyp ser un numero positivo\n"; }

            try { wtbep = Convert.ToDouble(TxtWtbep.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtbep debe ser un numero\n"; }

            try { errg = Convert.ToDouble(TxtErrg.Text); }
            catch (FormatException) { MError += "El valor ingresado para errg debe ser un numero positivo\n"; }

            try { smxg = Convert.ToDouble(TxtSmxg.Text); }
            catch (FormatException) { MError += "El valor ingresado para smxg debe ser un numero positivo\n"; }

            try { ertdnf = Convert.ToDouble(TxtErtdnf.Text); }
            catch (FormatException) { MError += "El valor ingresado para ertdnf ser un numero positivo\n"; }

            try { ertupf = Convert.ToDouble(TxtErtupf.Text); }
            catch (FormatException) { MError += "El valor ingresado para ertupf ser un numero positivo\n"; }

            try { wtruh = Convert.ToDouble(TxtWtruh.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtruh debe ser un numero\n"; }

            try { wtdhg = Convert.ToDouble(TxtWtdhg.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtdhg debe ser un numero positivo\n"; }

            try { stpo = Convert.ToDouble(TxtStpo.Text); }
            catch (FormatException) { MError += "El valor ingresado para stpo debe ser un numero positivo\n"; }

            try { stpp = Convert.ToDouble(TxtStpp.Text); }
            catch (FormatException) { MError += "El valor ingresado para stpp ser un numero positivo\n"; }

            try { odlp = Convert.ToDouble(TxtOdlp.Text); }
            catch (FormatException) { MError += "El valor ingresado para odlp debe ser un numero positivo\n"; }

            try { odllp = Convert.ToDouble(TxtOdllp.Text); }
            catch (FormatException) { MError += "El valor ingresado para Odllp debe ser un numero positivo\n"; }

            try { stpg = Convert.ToDouble(TxtStpg.Text); }
            catch (FormatException) { MError += "El valor ingresado para stpg ser un numero positivo\n"; }

            try { odlg = Convert.ToDouble(TxtOdlg.Text); }
            catch (FormatException) { MError += "El valor ingresado para odlg ser un numero positivo\n"; }

            try { odllg = Convert.ToDouble(TxtOdllg.Text); }
            catch (FormatException) { MError += "El valor ingresado para odllg debe ser un numero\n"; }

            try { nobs = Convert.ToDouble(TxtNobs.Text); }
            catch (FormatException) { MError += "El valor ingresado para nobs ser un numero positivo\n"; }

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
