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
    using WFrms = System.Windows.Forms;
    public class TransferenciaView : WFrms.Form
    {
        public  TableLayoutPanel mainPanel;

        public DataGridView TablaTransferencias;

        public TransferenciaView(List<Transferencia> transferencias)
        {
            mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            mainPanel.Controls.Add(PanelTransferencias(transferencias));
            this.Controls.Add(mainPanel);
        }

        public TextBox Tipo { get; private set; }
        public TextBox CCCOrigen { get; private set; }
        public TextBox CCCDestino { get; private set; }
        public TextBox Importe { get; private set; }
        public Button BotonHome { get; private set; }
        public Button BotonAddTransferencia { get; private set; }
        public Button BotonDeleteTransferencia { get; private set; }

        Panel PanelTransferencias(List<Transferencia> transferencias)
        {
            Panel panelPrincipal = new Panel() { Dock = DockStyle.Fill };
            Panel panel1 = new Panel() { Dock = DockStyle.Fill };
            BuildTablaTransferencias(transferencias);
            panel1.Controls.Add(this.TablaTransferencias);
            Panel panel2 = new Panel() { Dock = DockStyle.Bottom };
            Panel panel3 = new Panel() { Dock = DockStyle.Top };

            Label etiqueta1 = new Label() {
                Text = "TRANSFERENCIAS",
                Location = new Point(25, 16),
            };
            panel3.Controls.Add(etiqueta1);

            panel2.Controls.Add(BotonesPanel());

            panelPrincipal.Controls.Add(panel1);
            panelPrincipal.Controls.Add(panel2);
            panelPrincipal.Controls.Add(panel3);

            return panelPrincipal;
        }

        Panel BotonesPanel()
        {
            var panel = new Panel { Dock = DockStyle.Bottom };

            BotonAddTransferencia = new Button()
            {
                Location = new Point(25, 16),
                AutoSize = true,
                Text = "Añadir transferencia",
            };

            panel.Controls.Add(BotonAddTransferencia);

            BotonDeleteTransferencia = new Button()
            {
                Location = new Point(25, 50),
                AutoSize = true,
                Text = "Eliminar transferencia"
            };

            panel.Controls.Add(BotonDeleteTransferencia);            

            return panel;
        }

        void BuildTablaTransferencias (List<Transferencia> transferencias)
        {
            this.TablaTransferencias = new DataGridView()
            {
                Dock = DockStyle.Left,
                ColumnCount = 6,
                MinimumSize = new Size(2000, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16)
            };
            this.TablaTransferencias.Columns[0].Name = "Id";
            this.TablaTransferencias.Columns[1].Name = "Tipo";
            this.TablaTransferencias.Columns[2].Name = "Cuenta origen";
            this.TablaTransferencias.Columns[3].Name = "Cuenta destino";
            this.TablaTransferencias.Columns[4].Name = "Importe";
            this.TablaTransferencias.Columns[5].Name = "Fecha";

            foreach (var t in transferencias)
            {
                bool aux = false;

                foreach (DataGridViewRow row in this.TablaTransferencias.Rows)
                {
                    if ((int)row.Cells[0].Value == t.Id)
                    {
                        aux = true;
                    }
                }
                if (!aux)
                    this.TablaTransferencias.Rows.Add(
                        t.Id.ToString(),
                        t.Tipo,
                        t.CCCOrigen,
                        t.CCCDestino,
                        t.Importe.ToString(),
                        t.Fecha.ToString("dd-MM-yyyy hh:mm")
                    );
            }
        }
        
        public void AddTransferenciaPanel()
        {
            this.mainPanel.Controls.Clear();

            this.mainPanel.Controls.Add(this.BuildTipo());
            this.mainPanel.Controls.Add(this.BuildCuentaOrigen());
            this.mainPanel.Controls.Add(this.BuildCuentaDestino());
            this.mainPanel.Controls.Add(this.BuildImporte());

            this.mainPanel.Controls.Add(this.BuildBotonAddTransferencia());
            this.mainPanel.Controls.Add(this.BuildBotonHome());

        }

        public Boolean DeleteTransferenciaPanel(string mensaje)
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

        public Panel BuildTipo()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top
            };

            var etiqueta = new Label
            {
                Dock = DockStyle.Left,
                Text = "Tipo"
            };

            this.Tipo = new TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 200
            };

            panel.Controls.Add(this.Tipo);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Panel BuildCuentaOrigen()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top
            };

            var etiqueta = new Label
            {
                Dock = DockStyle.Left,
                Text = "CCC Origen"
            };

            this.CCCOrigen = new TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 200
            };

            panel.Controls.Add(this.CCCOrigen);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Panel BuildCuentaDestino()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top
            };

            var etiqueta = new Label
            {
                Dock = DockStyle.Left,
                Text = "CCC Origen"
            };

            this.CCCDestino = new TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 200
            };

            panel.Controls.Add(this.CCCDestino);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Panel BuildImporte()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top
            };

            var etiqueta = new Label
            {
                Dock = DockStyle.Left,
                Text = "Importe"
            };

            this.Importe = new TextBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 200
            };

            panel.Controls.Add(this.Importe);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Button BuildBotonAddTransferencia()
        {
            Button toret = new Button();
            toret.Text = "Insertar";
            toret.Width = 200;

            this.BotonAddTransferencia = toret;

            return toret;
        }
        public Button BuildBotonHome()
        {
            Button toret = new Button();
            toret.Text = "Home";
            toret.Width = 200;

            this.BotonHome = toret;

            return toret;
        }
    }
}
