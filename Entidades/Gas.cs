using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Gas
    {
        private int _id;
        private string _descripcion;
        private string _formula;
        private double _pesoMolecular;
        private double _tempFuga;
        private double _densidadFuga;
        private double _calorEspecifico;
        private double _capacidadCalorifica;
        private double _ULC;

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }
        public string formula
        {
            get
            {
                return _formula;
            }
            set
            {
                _formula = value;
            }
        }
        public double pesoMolecular
        {
            get
            {
                return _pesoMolecular;
            }
            set
            {
                _pesoMolecular = value;
            }
        }
        public double tempFuga
        {
            get
            {
                return _tempFuga;
            }
            set
            {
                _tempFuga = value;
            }
        }
        public double densidadFuga
        {
            get
            {
                return _densidadFuga;
            }
            set
            {
                _densidadFuga = value;
            }
        }
        public double calorEspecifico
        {
            get
            {
                return _calorEspecifico;
            }
            set
            {
                _calorEspecifico = value;
            }
        }
        public double capacidadCalorifica
        {
            get
            {
                return _capacidadCalorifica;
            }
            set
            {
                _capacidadCalorifica = value;
            }
        }
        public double ULC
        {
            get
            {
                return _ULC;
            }
            set
            {
                _ULC = value;
            }
        }
        public double LLC { get; set; }
        public double AlturaFuga { get; set; }
    }
}
