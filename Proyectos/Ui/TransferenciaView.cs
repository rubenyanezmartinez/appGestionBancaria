using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
        public ComboBox Tipo { get; private set; }
        public TextBox CCCOrigen { get; private set; }
        public TextBox CCCDestino { get; private set; }
        public TextBox Importe { get; private set; }
        public Button BotonHome { get; private set; }
        public Button BotonAddTransferencia { get; private set; }
        public Button BotonDeleteTransferencia { get; private set; }
        public Button BotonCerraryGuardar { get; private set; }
        public Button BotonModifyTransferencia { get; private set; }
        public Button BotonAddPanel { get; private set; }
        public Button BotonModifyPanel { get; private set; }

        public void GetTransferenciasMainPanel(GestorTransferencias gestor)
        {
            this.mainPanel.Controls.Clear();
            mainPanel.Controls.Add(PanelTransferencias(gestor.Transferencias));
            this.Controls.Add(mainPanel);
        }

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

            BotonAddPanel = new Button()
            {
                Location = new Point(25, 16),
                Text = "Añadir transferencia",
                Width = 120
            };

            panel.Controls.Add(BotonAddPanel);

            BotonDeleteTransferencia = new Button()
            {
                Location = new Point(25, 50),
                Text = "Eliminar transferencia",
                Width = 120
            };

            panel.Controls.Add(BotonDeleteTransferencia);


            BotonModifyPanel = new Button()
            {
                Location = new Point(160, 16),
                Text = "Modificar transferencia",
                Width = 120
            };

            panel.Controls.Add(BotonModifyPanel);

            BotonCerraryGuardar = new Button()
            {
                Location = new Point(160, 50),
                Text = "Cerrar y guardar",
                Width = 120
            };

            panel.Controls.Add(BotonCerraryGuardar);

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
                    if (row.Cells[0].Value.Equals(t.Id))
                    {
                        aux = true;
                    }
                }
                if (!aux)
                    this.TablaTransferencias.Rows.Add(
                        t.Id.ToString(),
                        t.Tipo,
                        t.CCCOrigen.CCC,
                        t.CCCDestino.CCC,
                        t.Importe.ToString("N", CultureInfo.CreateSpecificCulture("es-ES")) + "€",
                        t.Fecha.ToString("dd-MM-yyyy hh:mm")
                    );
            }
        }
        
        public void AddTransferenciaPanel()
        {
            this.mainPanel.Controls.Clear();

            this.mainPanel.Controls.Add(this.BuildTipo(""));
            this.mainPanel.Controls.Add(this.BuildCuentaOrigen(""));
            this.mainPanel.Controls.Add(this.BuildCuentaDestino(""));
            this.mainPanel.Controls.Add(this.BuildImporte(""));

            this.mainPanel.Controls.Add(this.BuildBotonAddTransferencia());
            this.mainPanel.Controls.Add(this.BuildBotonHome());

        }

        public void ModifyTransferenciaPanel(Transferencia transferencia)
        {
            this.mainPanel.Controls.Clear();

            this.mainPanel.Controls.Add(this.BuildTipo(transferencia.Tipo));
            this.mainPanel.Controls.Add(this.BuildCuentaOrigen(transferencia.CCCOrigen.CCC));
            this.mainPanel.Controls.Add(this.BuildCuentaDestino(transferencia.CCCDestino.CCC));
            this.mainPanel.Controls.Add(this.BuildImporte(transferencia.Importe.ToString()));

            this.mainPanel.Controls.Add(this.BuildBotonModifyTransferencia());
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

        public Panel BuildTipo(String tipo)
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

            this.Tipo = new ComboBox
            {
                Dock = WFrms.DockStyle.Left,
                Width = 200,
                Text = tipo
            };
            this.Tipo.Items.AddRange ( new String[] { "puntual", "periodica" });

            panel.Controls.Add(this.Tipo);
            panel.Controls.Add(etiqueta);

            return panel;
        }
 
        public Panel BuildCuentaOrigen(String cccOrigen)
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
                Width = 200,
                Text = cccOrigen
            };

            panel.Controls.Add(this.CCCOrigen);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Panel BuildCuentaDestino(String cccDestino)
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
                Width = 200,
                Text = cccDestino
            };

            panel.Controls.Add(this.CCCDestino);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Panel BuildImporte(String importe)
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
                Width = 200, 
                Text = importe
            };

            panel.Controls.Add(this.Importe);
            panel.Controls.Add(etiqueta);

            return panel;
        }
        public Button BuildBotonAddTransferencia()
        {
            this.BotonAddTransferencia = new Button()
            {
                Text = "Insertar",
                Width = 120
            };            

            return BotonAddTransferencia;
        }
        public Button BuildBotonHome()
        {
            BotonHome = new Button()
            {
                Text = "Home",
                Width = 120
            };

            return BotonHome;
        }
        public Button BuildBotonModifyTransferencia()
        {
            this.BotonModifyTransferencia = new Button()
            {
                Text = "Modificar",
                Width = 120
            };

            return BotonModifyTransferencia;
        }
        public Boolean AlertDeleteTransferencia(string mensaje)
        {

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(mensaje, "ERROR", buttons);
            if (result == DialogResult.No)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void AlertError(string mensaje, GestorTransferencias gestor)
        {

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            result = MessageBox.Show(mensaje, "ERROR", buttons);
            if (result == DialogResult.OK)
            {
                this.GetTransferenciasMainPanel(gestor);
            }

        }
    }
}
