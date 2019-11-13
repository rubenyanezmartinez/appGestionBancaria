using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gestion_Bancaria.Core.Gestores
{
    public class GestorTransferencias
    {
        private List<Transferencia> transferencias;

        public GestorTransferencias()
        {
            this.transferencias = new List<Transferencia>();
        }

        public void addTransferencia(Transferencia transferencia)
        {
        }

        public void removeTransferencia(Transferencia transferencia)
        {
        }

        public Transferencia getTransferencia(int id)
        {
            return new Transferencia();
        }
    }
}
