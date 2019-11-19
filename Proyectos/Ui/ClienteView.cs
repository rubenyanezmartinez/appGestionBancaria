using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class ClienteView : WFrms.Form
    {
        private TableLayoutPanel mainPanel;
        public DataGridView TablaClientes;

        public Button botonAddCliente { get; private set; }
        public Button botonDeleteCliente { get; private set; }

        public ClienteView(GestorClientes gestor)
        {
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            mainPanel.Controls.Add(PanelClientes(gestor));
            this.Controls.Add(mainPanel);
        }

        public void ClienteViewMethod (GestorClientes gestor)
        {
            this.mainPanel.Controls.Clear();
            mainPanel.Controls.Add(PanelClientes(gestor));
            this.Controls.Add(mainPanel);
        }

        public void BuiltAddCliente()
        {
            this.mainPanel.Controls.Clear();

            this.mainPanel.Controls.Add(this.BuildDni());
            this.mainPanel.Controls.Add(this.BuildNombre());
            this.mainPanel.Controls.Add(this.BuildTelefono());
            this.mainPanel.Controls.Add(this.BuildEmail());
            this.mainPanel.Controls.Add(this.BuildDireccionPostal());

            this.mainPanel.Controls.Add(this.builtBotonAddCliente());
            this.mainPanel.Controls.Add(this.builtBotonVolver());

        }

        public void BuiltError(string mensaje, GestorClientes gestor)
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            result = MessageBox.Show(mensaje, "ERROR", buttons);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.ClienteViewMethod(gestor);
            }

        }

        public TextBox Dni { get; private set; }
        public TextBox Nombre { get; private set; }
        public TextBox Telefono { get; private set; }
        public TextBox Email { get; private set; }
        public TextBox DireccionPostal { get; private set; }
        public WFrms.Button BotonVolver {get; private set;}
        public WFrms.Button BotonAdd { get; private set; }

        public WFrms.Panel BuildDni()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "DNI"
            };

            this.Dni = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 70
            };

            panel.Controls.Add(this.Dni);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }

        public WFrms.Panel BuildNombre()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Nombre"
            };

            this.Nombre = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170
            };

            panel.Controls.Add(this.Nombre);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public WFrms.Panel BuildTelefono()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Telefono"
            };

            this.Telefono = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 80
            };

            panel.Controls.Add(this.Telefono);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public WFrms.Panel BuildEmail()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Email"
            };

            this.Email = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170
            };

            panel.Controls.Add(this.Email);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public WFrms.Panel BuildDireccionPostal()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Direccion Postal"
            };

            this.DireccionPostal = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 270
            };

            panel.Controls.Add(this.DireccionPostal);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public WFrms.Button builtBotonAddCliente()
        {
            WFrms.Button toret = new WFrms.Button();
            toret.Text = "Añadir";
            toret.Width = 200;

            this.BotonAdd = toret;

            return toret;
        }
        public WFrms.Button builtBotonVolver()
        {
            WFrms.Button toret = new WFrms.Button();
            toret.Text = "Volver";
            toret.Width = 200;

            this.BotonVolver = toret;

            return toret;
        }


        Panel BotonesPanel()
        {
            var pnl = new Panel { Dock = DockStyle.Bottom };

            botonAddCliente = new Button();

            botonAddCliente.Location = new Point(25, 16);
            botonAddCliente.AutoSize = true;
            botonAddCliente.Text = "Añadir cliente";
            

            pnl.Controls.Add(botonAddCliente);


            botonDeleteCliente = new Button();

            botonDeleteCliente.Location = new Point(25, 50);
            botonDeleteCliente.AutoSize = true;
            botonDeleteCliente.Text = "Eliminar cliente";

            pnl.Controls.Add(botonDeleteCliente);


            /***********************************************/
            /*MODIFICAR CLIENTE*/
            /***********************************************/

            /***********************************************/
            /*GUARDAR Y CERRAR this.Close(); en vista*/
            /***********************************************/

            return pnl;

        }

        DataGridView ClientesTabla(GestorClientes gestor)
        {
            this.TablaClientes = new DataGridView()
            {
                Dock = DockStyle.Left,
                ColumnCount = 5,
                MinimumSize = new Size(2000, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16)
            };
            this.TablaClientes.Columns[0].Name = "DNI";
            this.TablaClientes.Columns[1].Name = "NOMBRE";
            this.TablaClientes.Columns[2].Name = "TELEFONO";
            this.TablaClientes.Columns[3].Name = "EMAIL";
            this.TablaClientes.Columns[4].Name = "DIRECCION POSTAL";

            foreach (var cliente in gestor.ContenedorClientes)
            {
                bool aux = false;
                foreach (DataGridViewRow row in this.TablaClientes.Rows)
                {
                    if (row.Cells[0].Value.Equals(cliente.Dni))
                    {
                        aux = true;
                    }
                }
                if (!aux)
                    this.TablaClientes.Rows.Add(cliente.Dni, cliente.Nombre, cliente.Telefono, cliente.Email, cliente.DireccionPostal);
            }

            return this.TablaClientes;
        }


        Panel PanelClientes(GestorClientes gestor)
        {
            Panel pnl = new Panel() { Dock = DockStyle.Fill };
            Panel pnl1 = new Panel() { Dock = DockStyle.Fill };
            pnl1.Controls.Add(ClientesTabla(gestor));
            Panel pnl2 = new Panel() { Dock = DockStyle.Bottom };
            Panel pnl3 = new Panel() { Dock = DockStyle.Top };

            Label lb1 = new Label() { Text = "CLIENTES", Location = new Point(25, 16) };
            pnl3.Controls.Add(lb1);

            pnl2.Controls.Add(BotonesPanel());

            pnl.Controls.Add(pnl1);
            pnl.Controls.Add(pnl2);
            pnl.Controls.Add(pnl3);

            return pnl;
        }

    }
}

