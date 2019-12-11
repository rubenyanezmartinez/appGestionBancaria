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

        public ProductosPersonaController() {
            View = new ProductosPersonaView();
            this.View.BotonBusqueda.Click += (sender, e) => this.BuscarCliente();
        }
        private void BuscarCliente()
        {
            var buscaPor = View.selectBusqueda.SelectedIndex;
            string buscadoPor = "";
            GestorClientes gestorClientes = new GestorClientes();
            gestorClientes.RecuperarClientes();
            var cliente = new Cliente();

            switch (buscaPor)
            {
                case 0:
                    cliente = gestorClientes.ConsultarPorDni(this.View.Input.Text);
                    buscadoPor = "DNI";
                    break;
                case 1:
                    cliente = gestorClientes.ConsultarPorTelefono(this.View.Input.Text);
                    buscadoPor = "Telefono";
                    break;
                case 2:
                    cliente = gestorClientes.ConsultarPorEmail(this.View.Input.Text);
                    buscadoPor = "Email";
                    break;
            }

           
            if (cliente != null)
            {
                GestorTransferencias gestorTransferencias = new GestorTransferencias();

                List<Cuenta> cuentasCliente = getCuentasByCliente(cliente);
                List<Transferencia> transferenciasCliente = new List<Transferencia>();

                TableLayoutPanel panel = new TableLayoutPanel() { Dock = DockStyle.Fill, AutoScroll = true,Size=new Size(1000,1000) 
                };

                foreach (var cuenta in cuentasCliente)
                {
                    var listaTransferencias = gestorTransferencias.Transferencias.FindAll(x => x.CCCOrigen == cuenta);

                    if (listaTransferencias != null)
                    {
                        foreach (var transferencia in listaTransferencias) {
                        
                                transferenciasCliente.Add(transferencia);
                            }
                    }
                    Panel panelCuentaDepositos = new Panel() { Size = new Size(400, 250), Dock = DockStyle.Left };
                    Label lbTitulo = new Label()
                    {
                        Text = "Movimientos de la cuenta " + cuenta.CCC,
                        Size = new System.Drawing.Size(500, 100),
                        Font = new Font("Arial", 18, FontStyle.Regular)
                    };

                    Label lbDepositos = new Label()
                    {
                        Text = "Depositos ",
                        Size = new System.Drawing.Size(100, 50),
                        Font = new Font("Arial", 12, FontStyle.Regular),
                        Location = new Point(20, 20)
                    };

                    panelCuentaDepositos.Controls.Add(lbDepositos);

                    DataGridView depositos = new DataGridView()
                    {
                        Size = new Size(250, 100),
                        Location = new Point(150, 20),
                        ColumnCount = 2,
                        AutoSize = true,
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false
                    };
                    depositos.Columns[0].Name = "Cantidad";
                    depositos.Columns[1].Name = "Fecha";
                    depositos.Columns[1].Width = new Size(200, 0).Width;

                    foreach (var deposito in cuenta.Depositos)
                    {
                        depositos.Rows.Add("+ " + deposito.Cantidad, deposito.Fecha);
                    }

                    Label lbRetiradas = new Label()
                    {
                        Text = "Retiradas ",
                        Size = new System.Drawing.Size(100, 50),
                        Font = new Font("Arial", 12, FontStyle.Regular),
                        Location = new Point(20, 20),
                    };

                    Panel panelCuenta1 = new Panel() { Size = new Size(400, 250), Dock = DockStyle.Left };

                    panelCuenta1.Controls.Add(lbRetiradas);

                    DataGridView retiradas = new DataGridView()
                    {
                        Size = new Size(250, 100),
                        Location = new Point(150, 20),
                        ColumnCount = 2,
                        AutoSize = true,
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false,
                    };

                    retiradas.Columns[0].Name = "Cantidad";
                    retiradas.Columns[1].Name = "Fecha";
                    retiradas.Columns[1].Width = new Size(200, 0).Width;

                    foreach (var retirada in cuenta.Retiradas)
                    {
                        retiradas.Rows.Add("- " + retirada.Cantidad, retirada.Fecha);
                    }

                    panel.Controls.Add(lbTitulo);
                    panelCuentaDepositos.Controls.Add(depositos);
                    panel.Controls.Add(panelCuentaDepositos);

                    panelCuenta1.Controls.Add(retiradas);
                    panel.Controls.Add(panelCuenta1);
                }

                this.View.TablaTransferencias.DataSource = transferenciasCliente;
                this.View.TablaCuentas.DataSource = cuentasCliente;
                this.View.search(cliente.Nombre);

                refreshForm();
                FormMovimientos.Text = "Movimientos del cliente: " + cliente.Nombre;
                
                FormMovimientos.Controls.Add(panel);
                FormMovimientos.Show();
            }
            else {
                this.View.Input.Text = "";
                MessageBox.Show("No se ha encontrado ningun cliente con ese "+buscadoPor,
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
