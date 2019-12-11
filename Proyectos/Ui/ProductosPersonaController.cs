using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    class ProductosPersonaController
    {
        public ProductosPersonaView View {get;set;}
        public ProductosPersonaController(Cliente cliente) {
            View = new ProductosPersonaView();
            this.BuscarCliente(cliente);
        }
        private void BuscarCliente(Cliente cliente)
        {     
            if (cliente != null)
            {
                GestorTransferencias gestorTransferencias = new GestorTransferencias();

                List<Cuenta> cuentasCliente = getCuentasByCliente(cliente);
                List<Transferencia> transferenciasClienteEmitidas = new List<Transferencia>();
                List<Transferencia> transferenciasClienteRecibidas = new List<Transferencia>();
                foreach (var cuenta in cuentasCliente)
                {
                    var listaTransferenciasEmitidas = gestorTransferencias.Transferencias.FindAll(x => x.CCCOrigen.CCC == cuenta.CCC);

                    if (listaTransferenciasEmitidas != null)
                    {
                        foreach (var transferencia in listaTransferenciasEmitidas) {

                            transferenciasClienteEmitidas.Add(transferencia);
                            }
                    }

                    var listaTransferenciasRecibidas = gestorTransferencias.Transferencias.FindAll(x => x.CCCDestino.CCC == cuenta.CCC);

                    if (listaTransferenciasRecibidas != null)
                    {
                        foreach (var transferencia in listaTransferenciasRecibidas)
                        {

                            transferenciasClienteRecibidas.Add(transferencia);
                        }
                    }

                }

                this.View.TablaTransferenciasEmitidas.DataSource = transferenciasClienteEmitidas;
                this.View.TablaTransferenciasRecibidas.DataSource = transferenciasClienteRecibidas;
                this.View.TablaCuentas.DataSource = cuentasCliente;
                this.View.search(cliente.Nombre,cuentasCliente);
                
            }
            else {
               
                MessageBox.Show("No se ha encontrado ningun cliente con ese ",
                    "Error en la busqueda", 
                    MessageBoxButtons.OK);
            }
        }

        private void refreshForm()
        {
            FormCollection fc = Application.OpenForms;
            foreach (var form in fc)
            {
                if (form == FormMovimientos)
                {
                    FormMovimientos.Dispose();
                    break;
                }
            }
            FormMovimientos = new Form()
            {
                Size = new Size(1000, 1000)
            };
        }

        private Form FormMovimientos{ get;set;} = new Form()
        {
            Size = new Size(1000, 1000)
        };

        private List<Cuenta> getCuentasByCliente(Cliente cliente) {
            List<Cuenta> toret = new List<Cuenta>();
            GestorCuentas gestorCuentas = new GestorCuentas();
            foreach (var cuenta in gestorCuentas.Cuentas) {
                if (cuenta.Titulares.FindAll(x => x.Dni.Equals(cliente.Dni)).Count>0) {
                    toret.Add(cuenta);
                }
            }
            return toret;
        }
    }
}
