using App_Gestion_Bancaria.Core.Acceso_a_datos;
using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Gestores
{
    public class GestorCuentas
    {
        public List<Cuenta> Cuentas { get; set; }
        public GestorCuentas()
        {
            this.Cuentas = CuentasRepositorio.Leer();

            if(this.Cuentas == null)
            {
                this.Cuentas = new List<Cuenta>();
            }

        }
        public void Guardar()
        {
            CuentasRepositorio.Guardar(Cuentas);
        }

        public String GetNextCCC()
        {
            int i = 0;

            while (this.GetCuentaByCCC(i.ToString()) != null && i < this.Cuentas.Count)
            {
                i++;
            }

            return i.ToString();
        }


        public void Actualizar(Cuenta cuenta)
        {
            Cuentas.Remove(this.GetCuentaByCCC(cuenta.CCC));
            Cuentas.Add(cuenta);
        }

        public Cuenta GetCuentaByCCC(String ccc)
        {
            return Cuentas.Where(x => x.CCC == ccc).FirstOrDefault();
        }

        public void RemoveTitularByDni(String CCC, String dni)
        {
            Cuenta cuenta = this.GetCuentaByCCC(CCC);

            cuenta.Titulares.Remove(cuenta.Titulares.Where(x => x.Dni == dni).FirstOrDefault());
        }
    }


}
