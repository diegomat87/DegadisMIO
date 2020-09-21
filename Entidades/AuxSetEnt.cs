using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AuxSetEnt
    {
        private double _hmrte;
        private double _harte;
        private double _hwrte;

        public AuxSetEnt()
        {
            hmrte = 0;
            harte = 0;
            hwrte = 0;
        }

        public double hmrte
        {
            get
            { return _hmrte; }
            set
            { _hmrte = value; }
        }
        public double harte
        {
            get
            { return _harte; }
            set
            { _harte = value; }
        }
        public double hwrte
        {
            get
            { return _hwrte; }
            set
            { _hwrte = value; }
        }
    }
}
