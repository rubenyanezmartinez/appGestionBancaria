using App_Gestion_Bancaria.Core.Acceso_a_datos;
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
        private TransferenciasRepositorio Repositorio;
        private List<Transferencia> Transferencias;

        public GestorTransferencias()
        {
            this.Transferencias = Repositorio.Leer();
        }

        public void AddTransferencia(Transferencia transferencia)
        {
            this.Transferencias.Add(transferencia);
        }

        public void RemoveTransferencia(Transferencia transferencia)
        {
            this.Transferencias.Remove(transferencia);
        }

        public bool Modificar(Transferencia transferencia)
        {
            if (this.GetTransferencia(transferencia.Id) != null)
            {
                this.RemoveTransferencia(transferencia);
                this.AddTransferencia(transferencia);
                return true;
            }
            return false;
        }

        public Transferencia GetTransferencia(int id)
        {
            return (this.Transferencias.Where(x => x.Id == id).ToList()).FirstOrDefault();
        }
    }
}
