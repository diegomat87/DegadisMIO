using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LineaSource
    {
        private double _ptime;
        private double _et;
        private double _r1t;
        private double _pwc;
        private double _ptemp;
        private double _pfracv;

        public double PTIME
        {
            get { return _ptime; }
            set { _ptime = value; }
        }

        public double ET
        {
            get { return _et; }
            set { _et = value; }
        }

        public double R1T
        {
            get { return _r1t; }
            set { _r1t = value; }
        }

        public double PWC
        {
            get { return _pwc; }
            set { _pwc = value; }
        }

        public double PTEMP
        {
            get { return _ptemp; }
            set { _ptemp = value; }
        }
        public double PFRACV
        {
            get { return _pfracv; }
            set { _pfracv = value; }
        }
    }
}
