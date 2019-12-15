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

        public Button botonVerDetalles { get; private set; }
        public Button botonVerProductos { get; private set; }
        public Button botonAddCliente { get; private set; }
        public Button botonDeleteCliente { get; private set; }
        public Button botonEditCliente { get; private set; }
        public Button botonCloseCliente { get; private set; }
        public Button botonBuscar { get; private set; }
        public TextBox Buscar { get; private set; }
        public TextBox Dni { get; private set; }
        public TextBox Nombre { get; private set; }
        public TextBox Telefono { get; private set; }
        public TextBox Email { get; private set; }
        public TextBox DireccionPostal { get; private set; }
        public Button BotonVolver { get; private set; }
        public Button BotonAdd { get; private set; }
        public Button BotonEdit { get; private set; }

        public ClienteView(List<Cliente> contenedor)
        {
            this.WindowState = FormWindowState.Maximized;
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            mainPanel.Controls.Add(PanelClientes(contenedor));
            this.Controls.Add(mainPanel);
        }

        public void ClienteViewMethod (List<Cliente> contenedor)
        {
            this.mainPanel.Controls.Clear();
            mainPanel.Controls.Add(PanelClientes(contenedor));
            this.Controls.Add(mainPanel);
        }

        public void BuiltAddCliente()
        {
            this.mainPanel.Controls.Clear();

            Panel pnl = new Panel() { Dock = DockStyle.Fill };
            Panel pnl1 = new Panel() { Dock = DockStyle.Fill};
            pnl1.Controls.Add(this.BuildDireccionPostal());
            pnl1.Controls.Add(this.BuildEmail());
            pnl1.Controls.Add(this.BuildTelefono());
            pnl1.Controls.Add(this.BuildNombre());
            pnl1.Controls.Add(this.BuildDni());
            
            Panel pnl2 = new Panel() { Dock = DockStyle.Bottom };
            Panel pnl3 = new Panel() { Dock = DockStyle.Top };

            Label lb1 = new Label() { Text = "GESTIÓN DE CLIENTES", Size = new System.Drawing.Size(1000, 1000), Font = new Font("Arial", 35, FontStyle.Regular) };
            pnl3.Controls.Add(lb1);

            pnl2.Controls.Add(builtBotonAddCliente());
            pnl2.Controls.Add(builtBotonVolver());

            pnl.Controls.Add(pnl1);
            pnl.Controls.Add(pnl2);
            pnl.Controls.Add(pnl3);

            this.mainPanel.Controls.Add(pnl);
        }

        public void BuiltError(string mensaje, List<Cliente> contenedor)
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            result = MessageBox.Show(mensaje, "ERROR", buttons);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.ClienteViewMethod(contenedor);
            }

        }

        public Boolean BuiltDeleteCliente(string mensaje)
        {

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(mensaje, "ERROR", buttons);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Panel BuildDni()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "DNI",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Dni = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 70,
                MaxLength = 9,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Dni);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }

        public Panel BuildNombre()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Nombre",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Nombre = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170,
                MaxLength = 160,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Nombre);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildTelefono()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Telefono",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Telefono = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 90,
                MaxLength = 12,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Telefono);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildEmail()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Email",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Email = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170,
                MaxLength = 160,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Email);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildDireccionPostal()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Direccion Postal",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.DireccionPostal = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 270,
                MaxLength = 260,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.DireccionPostal);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Button builtBotonAddCliente()
        {
            WFrms.Button toret = new WFrms.Button();
            toret.Text = "Añadir";
            toret.Width = 200;
            toret.FlatStyle = FlatStyle.Flat;
            toret.Location = new Point(25, 16);
            toret.Height = 30;
            toret.Font = new Font("Arial", 12, FontStyle.Regular);

            this.BotonAdd = toret;

            return toret;
        }
        public Button builtBotonEditCliente()
        {
            WFrms.Button toret = new WFrms.Button();
            toret.Text = "Modificar";
            toret.Width = 200;
            toret.FlatStyle = FlatStyle.Flat;
            toret.Location = new Point(25, 16);
            toret.Height = 30;
            toret.Font = new Font("Arial", 12, FontStyle.Regular);

            this.BotonEdit = toret;

            return toret;
        }
        public Button builtBotonVolver()
        {
            WFrms.Button toret = new WFrms.Button();
            toret.Text = "Volver";
            toret.Width = 200;
            toret.FlatStyle = FlatStyle.Flat;
            toret.Location = new Point(240, 16);
            toret.Height = 30;
            toret.Font = new Font("Arial", 12, FontStyle.Regular);

            this.BotonVolver = toret;

            return toret;
        }


        public void BuiltEditCliente(Cliente c)
        {
            this.mainPanel.Controls.Clear();

            Panel pnl = new Panel() { Dock = DockStyle.Fill };
            Panel pnl1 = new Panel() { Dock = DockStyle.Fill };
            
            pnl1.Controls.Add(this.BuildDireccionPostal(c.DireccionPostal));
            pnl1.Controls.Add(this.BuildEmail(c.Email));
            pnl1.Controls.Add(this.BuildTelefono(c.Telefono));
            pnl1.Controls.Add(this.BuildNombre(c.Nombre));
            pnl1.Controls.Add(this.BuildDni(c.Dni));

            Label lb2 = new Label() { Text = "* Deben ser unicos" };

            Panel pnl2 = new Panel() { Dock = DockStyle.Bottom };
            Panel pnl3 = new Panel() { Dock = DockStyle.Top };

            Label lb1 = new Label() { Text = "GESTIÓN DE CLIENTES", Size = new System.Drawing.Size(1000, 1000), Font = new Font("Arial", 35, FontStyle.Regular) };
            pnl3.Controls.Add(lb1);

            pnl2.Controls.Add(builtBotonEditCliente());
            pnl2.Controls.Add(builtBotonVolver());
            pnl2.Controls.Add(lb2);

            pnl.Controls.Add(pnl1);
            pnl.Controls.Add(pnl2);
            pnl.Controls.Add(pnl3);

            this.mainPanel.Controls.Add(pnl);

        }

        public Panel BuildDni(string dni)
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top,
                Visible = false
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "DNI",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Dni = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 70,
                Text = dni,
                MaxLength = 9,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Dni);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }

        public Panel BuildBucar()
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Right
            };

            this.Buscar = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Bottom,
                Width = 9,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular),
                MaxLength = 9
            };

            this.botonBuscar = new Button();

            botonBuscar.Location = new Point(100, 74);
            botonBuscar.Width = 100;
            botonBuscar.Text = "Buscar";
            botonBuscar.FlatStyle = FlatStyle.Flat;
            botonBuscar.Height = 27;
            botonBuscar.Font = new Font("Arial", 12, FontStyle.Regular);

            panel.Controls.Add(this.botonBuscar);
            panel.Controls.Add(this.Buscar);

            return panel;
        }

        public Panel BuildNombre(string nombre)
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Nombre",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Nombre = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170,
                Text = nombre,
                MaxLength = 160,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Nombre);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildTelefono(string telefono)
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Telefono *",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Telefono = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 90,
                Text = telefono,
                MaxLength = 12,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Telefono);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildEmail(string email)
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Email *",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.Email = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 170,
                Text = email,
                MaxLength = 160,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.Email);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }
        public Panel BuildDireccionPostal(string direccionPostal)
        {
            var panel = new WFrms.Panel
            {
                Dock = WFrms.DockStyle.Top
            };

            var etiquetaFila = new WFrms.Label
            {
                Dock = WFrms.DockStyle.Left,
                Text = "Direccion Postal",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.DireccionPostal = new WFrms.TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 270,
                Text = direccionPostal,
                MaxLength = 260,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            panel.Controls.Add(this.DireccionPostal);
            panel.Controls.Add(etiquetaFila);

            return panel;
        }


        Panel BotonesPanel()
        {
            var pnl = new Panel { Dock = DockStyle.Bottom };

            botonAddCliente = new Button();

            botonAddCliente.Location = new Point(25, 16);
            botonAddCliente.Width = 200;
            botonAddCliente.Height = 30;
            botonAddCliente.Font = new Font("Arial", 12, FontStyle.Regular);
            botonAddCliente.Text = "Añadir cliente";
            botonAddCliente.FlatStyle = FlatStyle.Flat;


            pnl.Controls.Add(botonAddCliente);

            botonVerDetalles = new Button();

            botonVerDetalles.Location = new Point(25, 49);
            botonVerDetalles.Width = 200;
            botonVerDetalles.Height = 30;
            botonVerDetalles.Font = new Font("Arial", 12, FontStyle.Regular);
            botonVerDetalles.Text = "Ver detalles";
            botonVerDetalles.FlatStyle = FlatStyle.Flat;


            pnl.Controls.Add(botonVerDetalles);


            botonVerProductos = new Button();

            botonVerProductos.Location = new Point(240, 49);
            botonVerProductos.Width = 200;
            botonVerProductos.Height = 30;
            botonVerProductos.Font = new Font("Arial", 12, FontStyle.Regular);
            botonVerProductos.Text = "Ver productos";
            botonVerProductos.FlatStyle = FlatStyle.Flat;


            pnl.Controls.Add(botonVerProductos);


            botonDeleteCliente = new Button();

            botonDeleteCliente.Location = new Point(240, 16);
            botonDeleteCliente.Width = 200;
            botonDeleteCliente.Height = 30;
            botonDeleteCliente.Font = new Font("Arial", 12, FontStyle.Regular);
            botonDeleteCliente.Text = "Eliminar cliente";
            botonDeleteCliente.FlatStyle = FlatStyle.Flat;

            pnl.Controls.Add(botonDeleteCliente);
            

            botonEditCliente = new Button();

            botonEditCliente.Location = new Point(455, 16);
            botonEditCliente.Width = 200;
            botonEditCliente.Height = 30;
            botonEditCliente.Font = new Font("Arial", 12, FontStyle.Regular);
            botonEditCliente.Text = "Modificar cliente";
            botonEditCliente.FlatStyle = FlatStyle.Flat;

            pnl.Controls.Add(botonEditCliente);

            botonCloseCliente = new Button();

            botonCloseCliente.Location = new Point(455, 49);
            botonCloseCliente.Width = 200;
            botonCloseCliente.Height = 30;
            botonCloseCliente.Font = new Font("Arial", 12, FontStyle.Regular);
            botonCloseCliente.Text = "Volver";
            botonCloseCliente.FlatStyle = FlatStyle.Flat;

            pnl.Controls.Add(botonCloseCliente);

            return pnl;

        }

        DataGridView ClientesTabla(List<Cliente> contenedor)
        {
            this.TablaClientes = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
                MultiSelect = false
            };
            this.TablaClientes.Columns[0].Name = "DNI";
            this.TablaClientes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
            this.TablaClientes.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TablaClientes.Columns[0].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            this.TablaClientes.Columns[1].Name = "NOMBRE";
            this.TablaClientes.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.TablaClientes.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TablaClientes.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            this.TablaClientes.Columns[2].Name = "TELEFONO";
            this.TablaClientes.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.TablaClientes.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TablaClientes.Columns[2].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            this.TablaClientes.Columns[3].Name = "EMAIL";
            this.TablaClientes.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.TablaClientes.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TablaClientes.Columns[3].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            this.TablaClientes.Columns[4].Name = "DIRECCION POSTAL";
            this.TablaClientes.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.TablaClientes.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.TablaClientes.Columns[4].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            foreach (var cliente in contenedor)
            {
                bool aux = false;
                foreach (DataGridViewRow row in this.TablaClientes.Rows)
                {
                    if (row.Cells[0].Value.Equals(cliente.Dni))
                    {
                        aux = true;
                    }

                    row.Height = 30;
                }
                if (!aux)
                    this.TablaClientes.Rows.Add(cliente.Dni, cliente.Nombre, cliente.Telefono, cliente.Email, cliente.DireccionPostal);
            }

            return this.TablaClientes;
        }

        Panel PanelClientes(List<Cliente> contenedor)
        {

            Panel pnl = new Panel() { Dock = DockStyle.Fill };
            Panel pnl1 = new Panel() { Dock = DockStyle.Fill };
            
            pnl1.Controls.Add(ClientesTabla(contenedor));
            Panel pnl2 = new Panel() { Dock = DockStyle.Bottom};
            Panel pnl3 = new Panel() { Dock = DockStyle.Top };

            Label lb1 = new Label() { Text = "GESTIÓN DE CLIENTES", Size = new System.Drawing.Size(1000, 1000), Font = new Font("Arial", 35, FontStyle.Regular) };
            pnl3.Controls.Add(lb1);
            pnl3.Controls.Add(this.BuildBucar());

            pnl2.Controls.Add(BotonesPanel());

            pnl.Controls.Add(pnl1);
            pnl.Controls.Add(pnl2);
            pnl.Controls.Add(pnl3);

            return pnl;
        }

    }
}

