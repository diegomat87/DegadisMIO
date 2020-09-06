using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Degadis
{
    public class Gamma
    {
        double x = 0;
        int ier = 0;
        double err = 0;
        double gamma = 0;
        double y = 0;
        //cambiar nombre
        public List<double> gama(double xx)
        {
            
            if (xx <= 34.5)
            {
                //6
                x = xx;
                err = 1.0e-6;
                ier = 0;
                gamma = 1.0;
                if (x <= 2.0)
                {
                    //50	IF(X-1.0)60,120,110
                    if (x<1)
                    {
                        //   60	IF(X-ERR)62,62,80
                        if (x-err<=0)
                        {
                            //62 Y=FLOAT(INT(X))-X
                            y = Math.Truncate(x) - x;
                            if (Math.Abs(y)-err<=0)
                            {
                                //130
                                ier = 1;
                            }
                            else
                            {
                                //64
                                if (1.0-y-err<=0)
                                {
                                    //130
                                    ier = 1;
                                }
                                else
                                {
                                    //70	IF(X-1.0)80,80,110
                                    while(x<=1.0)
                                    {
                                        gamma = gamma / x;
                                        x = x + 1.0;
                                    }
                                    a110();
                                }
                            }
                        }
                    }
                    else if (x<1)
                    {
                        a110();
                    }
                }
                else
                {
                    //15
                    while(x>2.0)
                    {
                        x = x - 1;
                        gamma = gamma * x;
                    }
                    a110();
                }
            }
            else
            {
                //4
                ier = 2;
                gamma = 1.0E38;
            }

            switch (ier)
            {
                case 1:
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.eGamma1);
                    break;
                case 2:
                    System.Windows.Forms.MessageBox.Show(Properties.Resources.eGamma2);
                    break;
            }
            List<double> resultados = new List<double>();
            resultados.Add(gamma);
            resultados.Add(ier);
            return resultados;
        }

        private void a110()
        {
            y = x - 1;
            double gy = 1.0 + y * (-0.5771017 + y * (0.985854 + y * (-0.8764218 + y * (0.8328212 + y * (-0.5684729 + y * (0.2548205 + y * (-0.05149930)))))));
            gamma = gamma * gy;
        }
    }
}
