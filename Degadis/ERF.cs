using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degadis
{
    class ERF
    {
        public double Erf (double xxx)
        {
            double half = 0.5;
            Gamma gamma = new Gamma();
            Incgamma incgamma = new Incgamma();

            if(xxx < 0)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.aErf); return 1E30; //return falso para que no de error double
            }
            else if(xxx == 0)
            { 
                return xxx;
            }
            else { return incgamma.Gaminc(half, xxx * xxx) / gamma.Gama(half); }
        }
    }
}
