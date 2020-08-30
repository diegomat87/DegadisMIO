using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LineaAdiabat
    {
        private double _ifl;
        private double _wc;
        private double _wa;
        private double _yc;
        private double _ya;
        private double _cc;
        private double _rho;
        private double _wm;
        private double _enthalpy;
        private double _temp;
        public double IFL
        {
            get { return _ifl; }
            set { _ifl = value; }
        }

        public double WC
        {
            get { return _wc; }
            set { _wc = value; }
        }

        public double WA
        {
            get { return _wa; }
            set { _wa = value; }
        }

        public double YC
        {
            get { return _yc; }
            set { _yc = value; }
        }

        public double YA
        {
            get { return _ya; }
            set { _ya = value; }
        }
        public double CC
        {
            get { return _cc; }
            set { _cc = value; }
        }

        public double RHO
        {
            get { return _rho; }
            set { _rho = value; }
        }

        public double WM
        {
            get { return _wm; }
            set { _wm = value; }
        }

        public double ENTHALPY
        {
            get { return _enthalpy; }
            set { _enthalpy = value; }
        }

        public double TEMP
        {
            get { return _temp; }
            set { _temp = value; }
        }
    }
}
