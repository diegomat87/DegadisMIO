using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degadis
{
    class ERF
    {
        public double erf (double xxx)
        {
            double half = 0.5;

            if(xxx < 0)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.aErf); return;
            }
            else if(xxx == 0)
            { 
                return xxx;
            }
            else { xxx = gaminc(half, xxx * xxx) / gamma(half)}
        }
    }
}
