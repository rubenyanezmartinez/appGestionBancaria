using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectos.Ui
{
    class ProductosPersonaController
    {
        public ProductosPersonaView View {get;set;}

        public ProductosPersonaController() {
            View = new ProductosPersonaView();
            this.View.BotonBusqueda.Click += (sender, e) => this.BuscarCliente();
        }
        private void BuscarCliente() {
            var buscaPor = View.selectBusqueda.SelectedIndex;
            GestorClientes gestorClientes=new GestorClientes();
            gestorClientes.RecuperarClientes();
            var cliente = new Cliente(null,null,null,null,null);
            switch (buscaPor) {
                case 0: 
                     cliente= gestorClientes.ConsultarPorDni(this.View.Input.Text);
                    break;
                case 1:
                     cliente = gestorClientes.ConsultarPorTelefono(this.View.Input.Text);
                    break;
                case 2:
                     cliente = gestorClientes.ConsultarPorEmail(this.View.Input.Text);
                    break;
            }
            GestorCuentas gestorCuentas = new GestorCuentas();
            GestorTransferencias gestorTransferencias = new GestorTransferencias();

            List<Cuenta> cuentasCliente = gestorCuentas.Cuentas.FindAll(x=>x.Titulares.Contains(cliente));
            List<Transferencia> transferenciasCliente = new List<Transferencia>();
            List<List<Movimiento>> depositosCliente = new List<List<Movimiento>>();
            List<List<Movimiento>> retiradasCliente = new List<List<Movimiento>>();
            foreach (var cuenta in cuentasCliente){
                var transferencia = gestorTransferencias.Transferencias.Find(x => x.CCCOrigen == cuenta);
                if (transferencia != null) {
                    transferenciasCliente.Add(transferencia);
                }
                depositosCliente.Add(cuenta.Depositos);
                retiradasCliente.Add(cuenta.Retiradas);
             }

            this.View.TablaTransferencias.DataSource = transferenciasCliente;
            this.View.TablaCuentas.DataSource = transferenciasCliente;

        }
    }
}
