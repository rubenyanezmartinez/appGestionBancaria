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
    class MainView : Form
    {
        private TableLayoutPanel mainPanel;
        public Button gClientes, gCuentas, gTransferencias;

        public MainView()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Build();
            
        }

        public void Build()
        {
            this.mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            Label title = new Label
            {
                Text = "APP GESTION BANCARIA",
                Width = 400,
                Height = 60,
                Font = new Font("Arial", 24, FontStyle.Regular)
            };

            this.gClientes = new Button
            {
                Text = "Gestionar Clientes",
                Width = 400,
                Height = 60,
                Font = new Font("Arial", 24, FontStyle.Regular)
            };
            this.gCuentas = new Button
            {
                Text = "Gestionar Cuentas",
                Width = 400,
                Height = 60,
                Font = new Font("Arial", 24, FontStyle.Regular)
            };
            this.gTransferencias = new Button
            {
                Text = "Gestionar Transferencias",
                Width = 400,
                Height = 60,
                Font = new Font("Arial", 24, FontStyle.Regular)
            };

            FlowLayoutPanel flp = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            FlowLayoutPanel flp1 = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            FlowLayoutPanel flp2 = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            FlowLayoutPanel flp3 = new FlowLayoutPanel()
            {
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            flp.Controls.Add(title);
            flp1.Controls.Add(this.gClientes);
            flp2.Controls.Add(this.gCuentas);
            flp3.Controls.Add(this.gTransferencias);

            this.mainPanel.Controls.Add(flp);
            this.mainPanel.Controls.Add(flp1);
            this.mainPanel.Controls.Add(flp2);
            this.mainPanel.Controls.Add(flp3);

            this.Controls.Add(mainPanel);
        }

        
    }
}
