using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degadis
{
    public class Controlador
    {
        public bool booljet
        {
            set
            {
                Properties.Settings.Default.booljet = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.booljet; }
        }
        public Entidades.ListaTitles titles
        {
            set
            {
                Properties.Settings.Default.title = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.title;
            }
        }

        public double u0
        {
            set
            {
                Properties.Settings.Default.u0 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.u0;
            }
        }

        public double z0
        {
            set
            {
                Properties.Settings.Default.z0 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.z0;
            }
        }

        public double zr
        {
            set
            {
                Properties.Settings.Default.zr = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.zr;
            }
        }

        public int istab
        {
            set
            {
                Properties.Settings.Default.istab = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.istab; }
        }

        public double avtime
        {
            set
            {
                Properties.Settings.Default.avtime = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.avtime;
            }
        }

        public int indvel
        {
            set
            {
                Properties.Settings.Default.indvel = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.indvel; }
        }

        public double rml
        {
            set
            {
                Properties.Settings.Default.rml = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.rml; }
        }

        public double tamb
        {
            set
            {
                Properties.Settings.Default.tamb = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.tamb;
            }
        }

        public double pamb
        {
            set
            {
                Properties.Settings.Default.pamb = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.pamb;
            }
        }

        public double humedad
        {
            set
            {
                Properties.Settings.Default.humid = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.humid;
            }
        }

        public double humedadrel
        {
            set
            {
                Properties.Settings.Default.relhum = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.relhum;
            }
        }

        public double deltay
        {
            set
            {
                Properties.Settings.Default.deltay = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.deltay; }
        }

        public double betay
        {
            set
            {
                Properties.Settings.Default.betay = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.betay; }
        }

        public double deltaz
        {
            set
            {
                Properties.Settings.Default.deltaz = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.deltaz; }
        }

        public double betaz
        {
            set
            {
                Properties.Settings.Default.betaz = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.betaz; }
        }

        public double gammaz
        {
            set
            {
                Properties.Settings.Default.gammaz = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.gammaz; }
        }

        public double sigxco
        {
            set
            {
                Properties.Settings.Default.sigxco = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.sigxco; }
        }

        public double sigxp
        {
            set
            {
                Properties.Settings.Default.sigxp = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.sigxp; }
        }

        public double sigxmd
        {
            set
            {
                Properties.Settings.Default.sigxmd = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.sigxmd; }
        }

        public int isofl
        {
            set
            {
                Properties.Settings.Default.isofl = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.isofl;
            }
        }

        public int ihtfl
        {
            set
            {
                Properties.Settings.Default.ihtfl = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.ihtfl;
            }
        }

        public int iwtfl
        {
            set
            {
                Properties.Settings.Default.iwtfl = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.iwtfl;
            }
        }

        public double tsurf
        {
            set
            {
                Properties.Settings.Default.tsurf = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.tsurf; }
        }

        public double htco
        {
            set
            {
                Properties.Settings.Default.htco = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.htco;
            }
        }

        public double wmw
        {
            set
            {
                Properties.Settings.Default.wmw = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.wmw;
            }
        }

        public double wma
        {
            set
            {
                Properties.Settings.Default.wma = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.wma;
            }
        }

        public double wtco
        {
            set
            {
                Properties.Settings.Default.wtco = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.wtco;
            }
        }

        public string gasnam
        {
            set
            {
                Properties.Settings.Default.gasnam = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.gasnam; }
        }

        public double gasmw
        {
            set
            {
                Properties.Settings.Default.gasmw = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gasmw;
            }
        }

        public double gastem
        {
            set
            {
                Properties.Settings.Default.gastem = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gastem;
            }
        }

        public double gasrho
        {
            set
            {
                Properties.Settings.Default.gasrho = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gasrho;
            }
        }

        public double gascpk
        {
            set
            {
                Properties.Settings.Default.gascpk = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gascpk;
            }
        }

        public double gascpp
        {
            set
            {
                Properties.Settings.Default.gascpp = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gascpp;
            }
        }

        public double gasulc
        {
            set
            {
                Properties.Settings.Default.gasulc = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gasulc;
            }
        }

        public double gasllc
        {
            set
            {
                Properties.Settings.Default.gasllc = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gasllc;
            }
        }

        public double gaszzc
        {
            set
            {
                Properties.Settings.Default.gaszzc = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gaszzc;
            }
        }

        public int nden
        {
            set
            {
                Properties.Settings.Default.nden = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.nden; }
        }

        public Entidades.ListaDEN DENtriples
        {
            set
            {
                Properties.Settings.Default.DENtriples = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.DENtriples;
            }
        }

        public double yclow
        {
            set
            {
                Properties.Settings.Default.yclow = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.yclow;
            }
        }

        public int idilut
        {
            set
            {
                Properties.Settings.Default.idilut = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.idilut;
            }
        }

        public int itran
        {
            set
            {
                Properties.Settings.Default.itran = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.itran;
            }
        }

        public bool Check4
        {
            set
            {
                Properties.Settings.Default.Check4 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.Check4;
            }
        }

        public double gmass0
        {
            set
            {
                Properties.Settings.Default.gmass0 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.gmass0;
            }
        }

        public double r1ss
        {
            set
            {
                Properties.Settings.Default.r1ss = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.r1ss;
            }
        }

        public double ess
        {
            set
            {
                Properties.Settings.Default.ess = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.ess;
            }
        }

        public double slen
        {
            set
            {
                Properties.Settings.Default.slen = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.slen;
            }
        }

        public double swid
        {
            set
            {
                Properties.Settings.Default.swid = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.swid;
            }
        }

        public double pwc1
        {
            set
            {
                Properties.Settings.Default.pwc1 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.pwc1;
            }
        }

        public double ptemp1
        {
            set
            {
                Properties.Settings.Default.ptemp1 = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.ptemp1;
            }
        }

        public double rhoa
        {
            set
            {
                Properties.Settings.Default.rhoa = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.rhoa;
            }
        }

        public double rhoe
        {
            set
            {
                Properties.Settings.Default.rhoe = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.rhoe;
            }
        }

        public double rgas
        {
            set
            {
                Properties.Settings.Default.rgas = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.rgas;
            }
        }

        public Entidades.ListaSource SourceSoT
        {
            set
            {
                Properties.Settings.Default.SourceSoT = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.SourceSoT;
            }
        }

        public double tend
        {
            set
            {
                Properties.Settings.Default.tend = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.tend; }
        }

        public int nsrc
        {
            set
            {
                Properties.Settings.Default.nsrc = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.nsrc; }
        }

        public string tinp
        {
            set
            {
                Properties.Settings.Default.tinp = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.tinp; }
        }

        public string ruta
        {
            set
            {
                Properties.Settings.Default.ruta = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.ruta;
            }
        }
        public string nombre
        {
            set
            {
                Properties.Settings.Default.nombre = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.nombre;
            }
        }

        public int indht
        {
            set
            {
                Properties.Settings.Default.indht = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.indht; }
        }

        public double temjet
        {
            set
            {
                Properties.Settings.Default.temjet = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.temjet; }
        }

        public double erate
        {
            set
            {
                Properties.Settings.Default.erate = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.erate; }
        }

        public double diajet
        {
            set
            {
                Properties.Settings.Default.diajet = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.diajet; }
        }

        public double elejet
        {
            set
            {
                Properties.Settings.Default.elejet = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.elejet; }
        }

        public double distmx
        {
            set
            {
                Properties.Settings.Default.distmx = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.distmx; }
        }

        public double UA
        {
            set
            {
                Properties.Settings.Default.UA = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.UA; }
        }

        public double vkc
        {
            set
            {
                Properties.Settings.Default.vkc = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.vkc; }
        }

        public double Ustar 
        {
            set
            {
                Properties.Settings.Default.Ustar = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.Ustar; }
        }

        public double gg
        {
            set
            {
                Properties.Settings.Default.gg = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.gg; }
        }

        public double alfa1
        {
            set
            {
                Properties.Settings.Default.alfa1 = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.alfa1; }
        }

        public double alfa2
        {
            set
            {
                Properties.Settings.Default.alfa2 = value;
                Properties.Settings.Default.Save();
            }
            get { return Properties.Settings.Default.alfa2; }
        }

        public double cpw
        {
            set
            {
                Properties.Settings.Default.cpw = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.cpw;
            }
        }

        public double dhvap
        {
            set
            {
                Properties.Settings.Default.dhvap = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.dhvap;
            }
        }
        public double dhfus
        {
            set
            {
                Properties.Settings.Default.dhfus = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.dhfus;
            }
        }

        public double cpa
        {
            set
            {
                Properties.Settings.Default.cpa = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.cpa;
            }
        }

        public int igen
        {
            set
            {
                Properties.Settings.Default.igen = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.igen;
            }
        }

        public double rhowl
        {
            set
            {
                Properties.Settings.Default.rhowl = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.rhowl;
            }
        }

        public double wa
        {
            set
            {
                Properties.Settings.Default.wa = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.wa;
            }
        }

        public double ya
        {
            set
            {
                Properties.Settings.Default.ya = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.ya;
            }
        }
        public double rho
        {
            set
            {
                Properties.Settings.Default.rho = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.rho;
            }
        }

        public double wm
        {
            set
            {
                Properties.Settings.Default.wm = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.wm;
            }
        }

        public double enthalpy
        {
            set
            {
                Properties.Settings.Default.enthalpy = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.enthalpy;
            }
        }

        public double temp
        {
            set
            {
                Properties.Settings.Default.temp = value;
                Properties.Settings.Default.Save();
            }
            get
            {
                return Properties.Settings.Default.temp;
            }
        }
    }
}
