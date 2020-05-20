using Operativo;
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
    /// Lógica de interacción para DescripcionEmisioneJet.xaml
    /// </summary>
    public partial class DescripcionEmisionJet : Page
    {
        Controlador cont = new Controlador();
        Datos.archivos arch = new Datos.archivos();
        Operativo.PropiedadesTermodinamicas proter = new Operativo.PropiedadesTermodinamicas();

        #region Constructor
        public DescripcionEmisionJet()
        {
            InitializeComponent();
            if (cont.Check4)
            {
                LblSourceDuration.Visibility = Visibility.Visible;
                BtnAyudaSourceDuration.Visibility = Visibility.Visible;
                TxtSourceDuration.Visibility = Visibility.Visible;
            }
            else
            {
                LblSourceDuration.Visibility = Visibility.Hidden;
                BtnAyudaSourceDuration.Visibility = Visibility.Hidden;
                TxtSourceDuration.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Componentes
        #region Ayuda
        private void BtnAyudaReleaseRate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@" The evolution (release) rate is the rate contaminant (without air) is released to the atmosphere (in kg contaminant/s).");
        }

        private void BtnAyudaSourceDiameter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@" The source diameter represents the area through which the evolution rate passes.");
        }

        private void BtnAyudaSourceElevation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@" The source elevation is the release height (m).");
        }

        private void BtnAyudaSourceDuration_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@" The source duration is the duration of the primary release (s).");
        }
        #endregion

        #region Transformaciones
        private void TxtReleaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceDiameter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceElevation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }

        private void TxtSourceDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemComma)
                e.Handled = false; //transformar punto en coma
            else
                e.Handled = true;
        }
        #endregion
        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                arch.crearJet(cont.nombre, cont.ruta, cont.titles, cont.u0, cont.z0, cont.zr, cont.istab, cont.indvel, cont.rml, cont.tamb, cont.pamb, cont.humedadrel, cont.tsurf, cont.gasnam, cont.gasmw, cont.avtime, cont.temjet, cont.gasulc, cont.gasllc, cont.gaszzc, cont.indht, cont.gascpk, cont.gascpp, cont.DENtriples, cont.erate, cont.elejet, cont.diajet, cont.tend, cont.distmx);
                Siguiente();
            }
        }
        #endregion

        #region Metodos
        private void Siguiente()
        {
            double qinit;
            double gamma;
            int tamDen = cont.DENtriples.Count;
            if (tamDen > 0)
            {
                Entidades.LineaDensidad den = new Entidades.LineaDensidad();
                den.Den1 = 2;
                cont.DENtriples.Add(den);
                cont.rhoa = cont.DENtriples[0].Den3;
                cont.rhoe = cont.DENtriples[tamDen].Den3;
            }
            else
            {
                cont.rhoa = cont.pamb * (1.0 + cont.humedad) * cont.wmw / (cont.rgas * (cont.wmw / cont.wma + cont.humedad)) / cont.tamb;
                cont.rhoe = cont.pamb * cont.gasmw / cont.rgas / cont.gastem;
            }
            cont.gasrho = cont.rhoe;
            qinit = cont.erate / cont.rhoe;
            gamma = (cont.rhoe - cont.rhoa) / cont.rhoe;
            cont.yclow = cont.gasllc / 4.0;
            if (cont.tend <= 0)
            {
                cont.yclow = cont.gasllc;
            }

            /*ver si esta parte es necesario
            double wc = 1;
            double wa = 0;

            double enth = proter.cpc(cont.gascpk, cont.gascpp, cont.gastem, cont.gasmw, cont.temjet) * (cont.gastem - cont.tamb);

            y lo que sigue
            */

            //Lo necesario son todas las instancias de call... las write se pueden omitir.
            PropiedadesTermodinamicas propiedadesTermodinamicas = new PropiedadesTermodinamicas();
            cont.UA= cont.Ustar / cont.vkc * (Math.Log((cont.elejet + cont.zr) / cont.zr) - propiedadesTermodinamicas.psif(cont.elejet, cont.rml)); ;
        }

        private bool Validar()
        {
            string MError = "";

            try { cont.erate = Convert.ToDouble(TxtReleaseRate.Text); }
            catch (Exception) { MError += "El valor ingresado para el flujo de contaminante debe ser un numero positivo\n"; }

            try { cont.diajet = Convert.ToDouble(TxtSourceDiameter.Text); }
            catch (Exception) { MError += "El valor ingresado para el diametro de la fuente debe ser un numero positivo\n"; }

            try { cont.elejet = Convert.ToDouble(TxtSourceElevation.Text); }
            catch (Exception) { MError += "El valor ingresado para la elevacion de la fuente debe ser un numero positivo\n"; }

            if (cont.booljet)
            {
                try { cont.tend = Convert.ToDouble(TxtSourceDuration.Text); }
                catch (Exception) { MError += "El valor ingresado para la duracion debe ser un numero positivo\n"; }
            }


            if (MError.Length == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show(MError);
                return false;
            }
        }
        #endregion
    }
}
