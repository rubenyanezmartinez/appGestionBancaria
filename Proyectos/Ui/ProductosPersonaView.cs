using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    class ProductosPersonaView : Form
    {
        private TableLayoutPanel mainPanel;
        public ProductosPersonaView() {

            this.WindowState = FormWindowState.Maximized;
            mainPanel = new TableLayoutPanel
            {

                Size = new Size(3000,3000),
                Dock = DockStyle.Fill,
            };
            Panel pnl1 = new Panel
            {

                Dock = DockStyle.Top,
            };
            pnl1.Controls.Add(lbTitulo);
            mainPanel.Controls.Add(pnl1);

            Panel pnl = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,

            };
            pnl.Controls.Add(lbTransferenciasEmitidas);

            pnl.Controls.Add(TablaTransferenciasEmitidas);
            pnl.Controls.Add(lbTransferenciasRecibidas);
            pnl.Controls.Add(TablaTransferenciasRecibidas);
            pnl.Controls.Add(lbCuentas);

            pnl.Controls.Add(TablaCuentas);
            pnl.Controls.Add(PanelMovimientos);

            Panel pnlAux = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Bottom,
            };

            this.botonCloseProductos.Location = new Point((pnlAux.Width - botonCloseProductos.Width) / 2, (pnlAux.Height - botonCloseProductos.Height) / 2);



            pnlAux.Controls.Add(botonCloseProductos);

            mainPanel.Controls.Add(pnl);
            pnl.Controls.Add(pnlAux);



            this.Controls.Add(mainPanel);
        }
        Label lbTitulo = new Label() { Text = "Productos por Persona", Size = new System.Drawing.Size(1000, 1000), Font = new Font("Arial", 25, FontStyle.Regular) };
        public Button botonCloseProductos = new Button()
        {
            Size = new Size(100, 25),
            Text = "Volver",
            Anchor = AnchorStyles.None,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Arial", 10, FontStyle.Regular),
        };

        public void search(string nombreCliente,List<Cuenta> cuentasCliente) {
            lbTitulo.Text += (": " + nombreCliente);
            this.buildPanelMovimientos(cuentasCliente);
        }
        private void buildPanelMovimientos(List<Cuenta> cuentasCliente) {
            foreach (var cuenta in cuentasCliente)
            {
                Panel panelCuenta = new Panel() { Dock = DockStyle.Top,Height=new Size(0,250).Height };
                Panel panelCuentaDepositos = new Panel() { Padding=new Padding(20,0,0,0), Width = 300, Dock =DockStyle.Left,};
                Panel panelCuentaRetiradas = new Panel() { Width = 280, Dock = DockStyle.Right };
                Panel pnlTitulo = new Panel() { Dock = DockStyle.Top, };
                Label lbTituloMovimientos = new Label()
                {
                    Text = "Movimientos de la cuenta " + cuenta.CCC,
                    Size= new Size(500,40),
                    Font = new Font("Arial", 18, FontStyle.Regular),

                };

                pnlTitulo.Controls.Add(lbTituloMovimientos);

                DataGridView depositos = new DataGridView()
                {
                    ScrollBars=ScrollBars.Vertical,
                    ColumnCount = 2,
                    AutoSize = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    Dock = DockStyle.Fill

                };
                depositos.Columns[0].Name = "Cantidad";
                depositos.Columns[1].Name = "Fecha";
                depositos.Columns[1].Width = 170;
                depositos.Columns[0].Width = 75;

                foreach (var deposito in cuenta.Depositos)
                {
                    depositos.Rows.Add("+ " + deposito.Cantidad, deposito.Fecha);
                }

                DataGridView retiradas = new DataGridView()
                { 
                    ScrollBars = ScrollBars.Vertical,
                    ColumnCount = 2,
                    AutoSize = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    Dock=DockStyle.Fill

                };

                retiradas.Columns[0].Name = "Cantidad";
                retiradas.Columns[1].Name = "Fecha";
                retiradas.Columns[1].Width = 170;
                retiradas.Columns[0].Width = 75;

                foreach (var retirada in cuenta.Retiradas)
                {
                    retiradas.Rows.Add("- " + retirada.Cantidad, retirada.Fecha);
                }
                Label separator2 = new Label()
                {
                    AutoSize = false,
                    BorderStyle = BorderStyle.None,
                    Height = 20,
                    Dock = DockStyle.Bottom,
                };

                panelCuentaDepositos.Controls.Add(depositos);
                panelCuentaRetiradas.Controls.Add(retiradas);
                panelCuentaRetiradas.Controls.Add(separator2);
                panelCuentaRetiradas.Controls.Add(separator2);


                panelCuenta.Controls.Add(panelCuentaDepositos);
                panelCuenta.Controls.Add(panelCuentaRetiradas);

                Label separator = new Label()
                {
                    AutoSize=false,
                    BorderStyle=BorderStyle.Fixed3D,
                    Height=2,
                    Dock=DockStyle.Bottom,
                };
                Panel aux = new Panel() { Height=20, Dock = DockStyle.Bottom };
                aux.Controls.Add(separator2);
                aux.Controls.Add(separator);

                panelCuenta.Controls.Add(pnlTitulo);
                panelCuenta.Controls.Add(aux);

                panelCuenta.Controls.Add(separator2);



                PanelMovimientos.Controls.Add(panelCuenta);

            }
        }

        public Panel PanelMovimientos { get; set; } = new Panel() { Location = new Point(0, 700),Size=new Size(700,600),AutoSize=true, };
    
        public DataGridView TablaTransferenciasEmitidas{get;set;}=new DataGridView() { Size = new Size(645, 200), Location = new Point(20, 90), AutoScrollOffset = new Point(20, 50), AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        public DataGridView TablaTransferenciasRecibidas { get; set; } = new DataGridView() { Size = new Size(645, 200), Location = new Point(20, 400), AutoScrollOffset = new Point(20, 50), AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

        public DataGridView TablaCuentas { get; set; } = new DataGridView() {  Size= new Size(545,200), Location = new Point(950, 90), AutoScrollOffset =  new Point(20, 50), AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

        public Label lbCuentas { get; set; } = new Label()
        {
            Text = "Cuentas ",
            Size = new System.Drawing.Size(100, 50),
            Font = new Font("Arial", 14, FontStyle.Regular),
            Location = new Point(900, 40),
           
        };
        Label lbTransferenciasEmitidas { get; set; } = new Label()
        {
            Text = "Transferencias Emitidas",
            Size = new System.Drawing.Size(250, 50),
            Font = new Font("Arial", 14, FontStyle.Regular),
            Location = new Point(0, 40),
          
        };
        Label lbTransferenciasRecibidas { get; set; } = new Label()
        {
            Text = "Transferencias Recibidas",
            Size = new System.Drawing.Size(250, 50),
            Font = new Font("Arial", 14, FontStyle.Regular),
            Location = new Point(0, 350),

        };
    }
}
