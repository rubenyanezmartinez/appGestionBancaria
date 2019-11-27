using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    class ProductosPersonaView : Form
    {
        private TableLayoutPanel mainPanel;
        public ProductosPersonaView() {
            mainPanel = new TableLayoutPanel
            {
                AutoSize=true,
                Dock = DockStyle.Fill,
            };
            mainPanel.Controls.Add(this.buildSelect());
            mainPanel.Controls.Add(this.buildInput());
            mainPanel.Controls.Add(this.buildBoton());

            Panel pnl = new Panel
            {
                Dock = DockStyle.Top,
            };

            Panel panelTransferencias = new Panel
            {
                Dock = DockStyle.Left,
            };
            panelTransferencias.Controls.Add(TablaTransferencias);
            Panel panelCuentas = new Panel
            {
                Dock = DockStyle.Right,
            };
            panelCuentas.Controls.Add(TablaCuentas);

            mainPanel.Controls.Add(this.buildBoton());
            mainPanel.Controls.Add(this.buildBoton());
            pnl.Controls.Add(panelCuentas);
            pnl.Controls.Add(panelTransferencias);
            mainPanel.Controls.Add(pnl);

            this.Controls.Add(mainPanel);
        }
        public ComboBox selectBusqueda { get; set; }
        Panel buildSelect() {
            var pnl = new Panel { Dock = DockStyle.Fill };
            selectBusqueda = new ComboBox();
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
            var pnl = new Panel { Dock = DockStyle.Top };
            Input = new TextBox();
            pnl.Controls.Add(Input);
            return pnl;
        }
        Panel buildBoton() {
            var pnl = new Panel { Dock = DockStyle.Top, Size = new Size(50, 30) };
            var pnl2 = new Panel { Dock = DockStyle.Left, Size = new Size(50, 30) };

            pnl2.Controls.Add(BotonBusqueda);
            pnl.Controls.Add(pnl2);

            return pnl;
        }
        public Button BotonBusqueda { get; set; } = new Button() { Text = "Buscar", Dock = DockStyle.Fill };

        public DataGridView TablaTransferencias{get;set;}=new DataGridView() { Dock = DockStyle.Right };
        public DataGridView TablaCuentas { get; set; } = new DataGridView() { Dock = DockStyle.Left };


    }
}
