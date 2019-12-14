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
    using wf = System.Windows.Forms;
    class MainView : wf.Form
    {
        private TableLayoutPanel mainPanel;
        public wf.Button gClientes, gCuentas, gTransferencias;

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
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.gClientes = new Button
            {
                Text = "Gestionar Clientes",
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            this.gCuentas = new Button
            {
                Text = "Gestionar Cuentas",
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            this.gTransferencias = new Button
            {
                Text = "Gestionar Transferencias",
                Width = 200,
                Height = 30,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.mainPanel.Controls.Add(this.gClientes);
            this.mainPanel.Controls.Add(this.gCuentas);
            this.mainPanel.Controls.Add(this.gTransferencias);

            this.Controls.Add(mainPanel);
        }

        
    }
}
