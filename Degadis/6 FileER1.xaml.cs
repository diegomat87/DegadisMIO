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
    /// Lógica de interacción para FileER1.xaml
    /// </summary>
    public partial class FileER1 : Page
    {
        Controlador cont = new Controlador();
        Datos.archivos arch = new Datos.archivos();
        double stpin, erbnd, wtrg, wttm, wtya, wtyc, wteb, wtmb, xli, xri, eps, zlow, stpinz, erbndz, srcoer, srcss, srccut, ernobl, noblpt, crfger, epsilon, ce, delrhomin, szstp0, szerr, szsz0, ialpfl, alpco, iphifl, dellay, vua, vub, vuc, vud, vudelta;
        public FileER1()
        {
            InitializeComponent();
        }

        #region Textbox
        private void TxtStpin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtErbnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtWtrg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSWttm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtWtya_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtWtyc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtWteb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtWtmb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtXli_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtXri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtEps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtZlow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtStpinz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtErbndz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSrcoer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSrcss_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSrccut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtErnobl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void Txtcrfger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtEpsilon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtCe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtDelrhomin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSzstp0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSzerr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSzsz0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtIalpfl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtAlpco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtIphifl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtDellay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtVua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtVub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtVuc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtVud_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtVudelta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || Key.OemComma == e.Key || e.Key == Key.Decimal || e.Key == Key.OemMinus || e.Key == Key.Subtract)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion

        private void Siguintea7_Click(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                arch.crearER1(stpin, erbnd, wtrg, wttm, wtya, wtyc, wteb, wtmb, xli, xri, eps, zlow, stpinz, erbndz, srcoer, srcss, srccut, ernobl, noblpt, crfger, epsilon, ce, delrhomin, szstp0, szerr, szsz0, ialpfl, alpco, iphifl, dellay, vua, vub, vuc, vud, vudelta, cont.ruta);
                MessageBox.Show("Archivo creado");
                FileER2 fileER2 = new FileER2();
                this.NavigationService.Navigate(fileER2);
            }
        }

        private bool validar()
        {

            string MError = "";

            try { stpin = Convert.ToDouble(TxtStpin.Text); }
            catch (FormatException) { MError += "El valor ingresado para stpin debe ser un numero\n"; }

            try { erbnd = Convert.ToDouble(TxtErbnd.Text); }
            catch (FormatException) { MError += "El valor ingresado para erbnd debe ser un numero positivo\n"; }

            try { wtrg = Convert.ToDouble(TxtWtrg.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtrg debe ser un numero positivo\n"; }

            try { wttm = Convert.ToDouble(TxtWttm.Text); }
            catch (FormatException) { MError += "El valor ingresado para wttm ser un numero positivo\n"; }

            try { wtya = Convert.ToDouble(TxtWtya.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtya ser un numero positivo\n"; }

            try { wtyc = Convert.ToDouble(TxtWtyc.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtyc debe ser un numero\n"; }

            try { wteb = Convert.ToDouble(TxtWteb.Text); }
            catch (FormatException) { MError += "El valor ingresado para wteb debe ser un numero positivo\n"; }

            try { wtmb = Convert.ToDouble(TxtWtmb.Text); }
            catch (FormatException) { MError += "El valor ingresado para wtmb debe ser un numero positivo\n"; }

            try { xli = Convert.ToDouble(TxtXli.Text); }
            catch (FormatException) { MError += "El valor ingresado para xli ser un numero positivo\n"; }

            try { xri = Convert.ToDouble(TxtXri.Text); }
            catch (FormatException) { MError += "El valor ingresado para xri ser un numero positivo\n"; }

            try { eps = Convert.ToDouble(TxtEps.Text); }
            catch (FormatException) { MError += "El valor ingresado para Eps debe ser un numero\n"; }

            try { zlow = Convert.ToDouble(TxtZlow.Text); }
            catch (FormatException) { MError += "El valor ingresado para zlow debe ser un numero positivo\n"; }

            try { stpinz = Convert.ToDouble(TxtStpinz.Text); }
            catch (FormatException) { MError += "El valor ingresado para stpinz debe ser un numero positivo\n"; }

            try { erbndz = Convert.ToDouble(TxtErbndz.Text); }
            catch (FormatException) { MError += "El valor ingresado para erbndz ser un numero positivo\n"; }

            try { srcoer = Convert.ToDouble(TxtSrcoer.Text); }
            catch (FormatException) { MError += "El valor ingresado para srcoer ser un numero positivo\n"; }

            try { srcss = Convert.ToDouble(TxtSrcss.Text); }
            catch (FormatException) { MError += "El valor ingresado para srcss debe ser un numero\n"; }

            try { srccut = Convert.ToDouble(TxtSrccut.Text); }
            catch (FormatException) { MError += "El valor ingresado para srccut debe ser un numero positivo\n"; }

            try { ernobl = Convert.ToDouble(TxtErnobl.Text); }
            catch (FormatException) { MError += "El valor ingresado para ernobl debe ser un numero positivo\n"; }

            try { noblpt = Convert.ToDouble(TxtNoblpt.Text); }
            catch (FormatException) { MError += "El valor ingresado para noblpt ser un numero positivo\n"; }

            try { crfger = Convert.ToDouble(TxtCrfger.Text); }
            catch (FormatException) { MError += "El valor ingresado para crfger ser un numero positivo\n"; }

            try { epsilon = Convert.ToDouble(TxtEpsilon.Text); }
            catch (FormatException) { MError += "El valor ingresado para epsilon debe ser un numero\n"; }

            try { ce = Convert.ToDouble(TxtCe.Text); }
            catch (FormatException) { MError += "El valor ingresado para ce debe ser un numero positivo\n"; }

            try { delrhomin = Convert.ToDouble(TxtDelrhomin.Text); }
            catch (FormatException) { MError += "El valor ingresado para delrhomin debe ser un numero positivo\n"; }

            try { szstp0 = Convert.ToDouble(TxtSzstp0.Text); }
            catch (FormatException) { MError += "El valor ingresado para szstp0 ser un numero positivo\n"; }

            try { szerr = Convert.ToDouble(TxtSzerr.Text); }
            catch (FormatException) { MError += "El valor ingresado para szerr ser un numero positivo\n"; }

            try { szsz0 = Convert.ToDouble(TxtSzsz0.Text); }
            catch (FormatException) { MError += "El valor ingresado para szsz0 debe ser un numero\n"; }

            try { ialpfl = Convert.ToDouble(TxtIalpfl.Text); }
            catch (FormatException) { MError += "El valor ingresado para ialpfl debe ser un numero positivo\n"; }

            try { alpco = Convert.ToDouble(TxtAlpco.Text); }
            catch (FormatException) { MError += "El valor ingresado para alpco debe ser un numero positivo\n"; }

            try { iphifl = Convert.ToDouble(TxtIphifl.Text); }
            catch (FormatException) { MError += "El valor ingresado para iphifl ser un numero positivo\n"; }

            try { dellay = Convert.ToDouble(TxtDellay.Text); }
            catch (FormatException) { MError += "El valor ingresado para dellay ser un numero positivo\n"; }

            try { vua = Convert.ToDouble(TxtVua.Text); }
            catch (FormatException) { MError += "El valor ingresado para vua debe ser un numero\n"; }

            try { vub = Convert.ToDouble(TxtVub.Text); }
            catch (FormatException) { MError += "El valor ingresado para vub debe ser un numero positivo\n"; }

            try { vuc = Convert.ToDouble(TxtVuc.Text); }
            catch (FormatException) { MError += "El valor ingresado para vuc debe ser un numero positivo\n"; }

            try { vud = Convert.ToDouble(TxtVud.Text); }
            catch (FormatException) { MError += "El valor ingresado para vud ser un numero positivo\n"; }

            try { vudelta = Convert.ToDouble(TxtVudelta.Text); }
            catch (FormatException) { MError += "El valor ingresado para vudelta ser un numero positivo\n"; }

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

