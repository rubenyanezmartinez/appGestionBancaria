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
            Label lb1 = new Label() { Text = "Busqueda de productos por Persona", Size = new System.Drawing.Size(1000, 1000), Font = new Font("Arial", 25, FontStyle.Regular) };
            mainPanel = new TableLayoutPanel
            {
                AutoSize=true,
                Dock = DockStyle.Fill,
            };
            Panel pnl1 = new Panel
            {
                Dock = DockStyle.Top,
            };
            pnl1.Controls.Add(lb1);
            mainPanel.Controls.Add(pnl1);
            mainPanel.Controls.Add(this.buildSelect());
            mainPanel.Controls.Add(this.buildInput());
            mainPanel.Controls.Add(this.buildBoton());

            Panel pnl = new Panel
            {
                Dock = DockStyle.Fill,

            };
            pnl.Controls.Add(lbCliente);
            pnl.Controls.Add(lbTransferencias);

            pnl.Controls.Add(TablaTransferencias);
            pnl.Controls.Add(lbCuentas);

            pnl.Controls.Add(TablaCuentas);

            mainPanel.Controls.Add(pnl);


            this.Controls.Add(mainPanel);
        }
        public void search(string nombreCliente) {
            TablaCuentas.Visible = true;
            TablaTransferencias.Visible = true;
            lbCuentas.Visible = true;
            lbTransferencias.Visible = true;
            this.Input.Text = "";
            lbCliente.Text = "Cliente: "+nombreCliente;
            lbCliente.Visible = true;
        }

        public ComboBox selectBusqueda { get; set; }
        Panel buildSelect() {
            var pnl = new Panel { Dock = DockStyle.Fill };
            selectBusqueda = new ComboBox() { Location=new Point(20,20), Font = new Font("Arial", 10, FontStyle.Regular), };
            DataTable datos = new DataTable();
            datos.Columns.Add("Valor");
            datos.Columns.Add("Texto");
            datos.Rows.Add(0, "DNI");
            datos.Rows.Add(1, "Teléfono");
            datos.Rows.Add(2, "Email");
            selectBusqueda.DataSource = datos;
            selectBusqueda.DisplayMember = "Texto";
            selectBusqueda.ValueMember = "Valor";
            pnl.Controls.Add(selectBusqueda);

            return pnl;
        }
        public TextBox Input { get; set; }
        Panel buildInput() {
            var pnl = new Panel { Dock = DockStyle.Top, };
            Input = new TextBox() { Location = new Point(20, 20), Font = new Font("Arial", 12, FontStyle.Regular),Size=new Size(200,50) };
            pnl.Controls.Add(Input);
            return pnl;
        }
        Panel buildBoton() {
            var pnl = new Panel { Dock = DockStyle.Top };
            var pnl2 = new Panel { Dock = DockStyle.Left};

            pnl2.Controls.Add(BotonBusqueda);
            pnl.Controls.Add(pnl2);

            return pnl;
        }
        public Button BotonBusqueda { get; set; } = new Button() { Text = "Buscar",  Location = new Point(20, 0), Font = new Font("Arial", 12, FontStyle.Regular) };

        public DataGridView TablaTransferencias{get;set;}=new DataGridView() { Size = new Size(645, 200), Location = new Point(20, 90), AutoScrollOffset = new Point(20, 50), AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, Visible = false };
        public DataGridView TablaCuentas { get; set; } = new DataGridView() {  Size= new Size(545,200), Location = new Point(950, 90), AutoScrollOffset =  new Point(20, 50), AllowUserToAddRows = false, AllowUserToDeleteRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,Visible=false };

        public Label lbCuentas { get; set; } = new Label()
        {
            Text = "Cuentas ",
            Size = new System.Drawing.Size(100, 50),
            Font = new Font("Arial", 12, FontStyle.Regular),
            Location = new Point(900, 40),
            Visible = false
        };
        public Label lbCliente { get; set; } = new Label()
        {
            Text = "",
            Size = new System.Drawing.Size(150, 25),
            Font = new Font("Arial", 15, FontStyle.Regular),
            Location = new Point(0, 0),
            
        };

        Label lbTransferencias { get; set; } = new Label()
        {
            Text = "Transferencias ",
            Size = new System.Drawing.Size(150, 50),
            Font = new Font("Arial", 12, FontStyle.Regular),
            Location = new Point(0, 40),
            Visible = false
        };
    }
}
