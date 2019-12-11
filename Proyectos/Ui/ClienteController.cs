using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class ClienteController
    {

        public ClienteView View { get; private set; }
        public GestorClientes Gestor { get; private set; }

        public ClienteController(GestorClientes gestor)
        {
            this.Gestor = gestor;
            this.Gestor.RecuperarClientes();
            this.View = new ClienteView(this.Gestor.ContenedorClientes);
            this.IniciarBotones();
        }

        private void IniciarBotones()
        {
            this.View.botonAddCliente.Click += new System.EventHandler(accionAddCliente);
            this.View.botonDeleteCliente.Click += new System.EventHandler(accionDeleteCliente);
            this.View.botonEditCliente.Click += new System.EventHandler(accionEditCliente);
            this.View.botonCloseCliente.Click += new System.EventHandler(accionCloseSave);
            this.View.botonVerDetalles.Click += new System.EventHandler(accionVerDetalles);
            //this.View.botonVerProductos.Click += new System.EventHandler(accionVerProductos);
        }

        private void accionVerDetalles(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaClientes.SelectedRows;
            if (filasSeleccionadas != null)
            {
                try
                {
                    int indiceTabla = filasSeleccionadas[0].Index;
                    if (indiceTabla < this.Gestor.ContenedorClientes.Count)
                    {
                        Cliente clienteRecuperado = this.Gestor.ContenedorClientes[indiceTabla];

                        Application.Run(new GraficoResumenSaldosClienteController(clienteRecuperado).View);

                    }
                }
                catch (Exception)
                {
                    this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
                    this.IniciarBotones();
                }


            }

            this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
            this.IniciarBotones();
        }


        private void accionAddCliente(object sender, System.EventArgs e)
        {
            this.View.BuiltAddCliente();
            this.View.BotonAdd.Click += new System.EventHandler(accionAdd);
            this.View.BotonVolver.Click += new System.EventHandler(accionVolver);
        }

        private void accionAdd(object sender, System.EventArgs e)
        {
            string dniRecuperado = this.View.Dni.Text;
            string telefonoRecuperado = this.View.Telefono.Text;
            string emailRecuperado = this.View.Email.Text;
            string nombreRecuperado = this.View.Nombre.Text;
            string direccionPostalRecuperada = this.View.DireccionPostal.Text;

            if (dniRecuperado.Equals("") || telefonoRecuperado.Equals("") || emailRecuperado.Equals("") || nombreRecuperado.Equals("") || direccionPostalRecuperada.Equals(""))
            {
                this.View.BuiltError("Ningún campo puede estar vacío", this.Gestor.ContenedorClientes);
                this.IniciarBotones();
            }
            else
            {
                Boolean noExiste = this.Gestor.Insertar(dniRecuperado, nombreRecuperado, telefonoRecuperado, emailRecuperado, direccionPostalRecuperada);

                if (!noExiste)
                {
                    this.View.BuiltError("Los campos DNI, telefono y email deben ser únicos", this.Gestor.ContenedorClientes);
                    this.IniciarBotones();
                }
                else
                {
                    this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
                    this.IniciarBotones();
                }
            }

        }

        private void accionEdit(object sender, System.EventArgs e)
        {
            string dniRecuperado = this.View.Dni.Text;
            string telefonoRecuperado = this.View.Telefono.Text;
            string emailRecuperado = this.View.Email.Text;
            string nombreRecuperado = this.View.Nombre.Text;
            string direccionPostalRecuperada = this.View.DireccionPostal.Text;

            if (dniRecuperado.Equals("") || telefonoRecuperado.Equals("") || emailRecuperado.Equals("") || nombreRecuperado.Equals("") || direccionPostalRecuperada.Equals(""))
            {
                this.View.BuiltError("Ningún campo puede estar vacío", this.Gestor.ContenedorClientes);
                this.IniciarBotones();
            }
            else
            {
                
                this.Gestor.Editar(dniRecuperado, nombreRecuperado, telefonoRecuperado, emailRecuperado, direccionPostalRecuperada);

                this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
                this.IniciarBotones();
            }

        }

        private void accionVolver(object sender, System.EventArgs e)
        {
            this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
            this.IniciarBotones();
        }


        private void accionDeleteCliente(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaClientes.SelectedRows;
            if (filasSeleccionadas != null)
            {
                try
                {
                    int indiceTabla = filasSeleccionadas[0].Index;
                    if (indiceTabla < this.Gestor.ContenedorClientes.Count)
                    {
                        Cliente clienteSeleccionado = this.Gestor.ContenedorClientes[indiceTabla];

                        if (this.View.BuiltDeleteCliente("¿Seguro que desea eliminar el cliente con DNI " + clienteSeleccionado.Dni
                            + " ?"))
                        {
                            this.Gestor.Eliminar(clienteSeleccionado.Dni);
                        }
                    }
                }catch(Exception)
                {
                    this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
                    this.IniciarBotones();
                }

                
            }
            
            this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
            this.IniciarBotones();
        }

        private void accionEditCliente(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaClientes.SelectedRows;
            
            try
            {
                int indiceTabla = filasSeleccionadas[0].Index;
                Cliente clienteSeleccionado = this.Gestor.ContenedorClientes[indiceTabla];

                this.View.BuiltEditCliente(clienteSeleccionado);
                this.View.BotonEdit.Click += new System.EventHandler(accionEdit);
                this.View.BotonVolver.Click += new System.EventHandler(accionVolver);
            }
            catch (Exception)
            {
                this.View.ClienteViewMethod(this.Gestor.ContenedorClientes);
                this.IniciarBotones();
            }

        }

        private void accionCloseSave(object sender, System.EventArgs e)
        {
            this.Gestor.GuardarClientes();
            this.View.Close();
        }


    }
}
