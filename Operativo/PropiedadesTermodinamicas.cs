using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degadis
{
    public class PropiedadesTermodinamicas
    {
        public double HumedadAbs(double tamb, double wmw, double wma, double pamb, double humedadrel)
        {
            double watvp; double sat; //atm y °K

            watvp = Math.Exp(14.683943 - 5407.0 / tamb);
            sat = wmw / wma * watvp / (pamb - watvp);

            return humedadrel / 100 * sat; //Evento no controlado: humedad relativa mayor al 100%

        }

        public double HumedadRel(double tamb, double wmw, double wma, double pamb, double humedad)
        {
            double watvp; double sat; //atm y °K

            watvp = Math.Exp(14.683943 - 5407.0 / tamb);
            sat = wmw / wma * watvp / (pamb - watvp);
            return humedad * 100 / sat;

        }
    }
}
