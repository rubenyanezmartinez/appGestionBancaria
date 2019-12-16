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
        public List<Transferencia> Transferencias { get; private set; }

        public GestorTransferencias()
        {
            this.Repositorio = new TransferenciasRepositorio();
            this.Transferencias = Repositorio.Leer();
            ActualizarTransferenciasPeriodicas();
        }

        public void AddTransferencia(Transferencia transferencia)
        {
            this.Transferencias.Add(transferencia);
        }

        public void RemoveTransferencia(Transferencia transferencia)
        {
            this.Transferencias.Remove(transferencia);
        }

        public void Modificar(Transferencia transferencia)
        {
            Transferencia transferenciaOriginal = this.GetTransferencia(transferencia.Id);
            if(transferenciaOriginal != null)
            {
                RemoveTransferencia(transferenciaOriginal);
                AddTransferencia(transferencia);
            }            
        }

        public Transferencia GetTransferencia(int id)
        {
            return (this.Transferencias.Where(x => x.Id == id).ToList()).FirstOrDefault();
        }

        public void GuardarTransferencias(List<Transferencia> transferencias)
        {
            this.Repositorio.Guardar(transferencias);
        }

        public void ActualizarTransferenciasPeriodicas()
        {
            foreach (Transferencia transferencia in this.Transferencias)
            {
                if (transferencia.Tipo.Equals("periodica") && DateTime.Compare(transferencia.Fecha, DateTime.Now) > 0)
                {
                    transferencia.Fecha = transferencia.Fecha.AddMonths(1);
                }
            }
        }

        public List<Transferencia> GetTransferenciaCuentaEmisor(Cuenta c)
        {
            var toret = new List<Transferencia>();
            foreach (var transferencia in this.Transferencias)
            {
                if (transferencia.CCCOrigen.CCC.Equals(c.CCC))
                {
                    toret.Add(transferencia);
                }
            }
            return toret;
        }

        public List<Transferencia> GetTransferenciaCuentaReceptor(Cuenta c)
        {
            var toret = new List<Transferencia>();
            foreach (var transferencia in this.Transferencias)
            {
                if (transferencia.CCCDestino.CCC.Equals(c.CCC))
                {
                    toret.Add(transferencia);
                }
            }
            return toret;
        }
    }
}
