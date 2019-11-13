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
        public GestorCuentas()
        {
            this.Cuentas = CuentasRepositorio.LeerDeFichero();

            if(this.Cuentas == null)
            {
                this.Cuentas = new List<Cuenta>();
            }

        }
        public void Guardar()
        {
            CuentasRepositorio.Guardar(Cuentas);
        }
        public List<Cuenta> Cuentas { get; set; }

    }


}
