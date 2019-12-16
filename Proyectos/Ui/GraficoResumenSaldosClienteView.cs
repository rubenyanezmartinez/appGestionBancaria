using App_Gestion_Bancaria.Core.Clases;
using Proyectos.Ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graficos.UI
{
    class GraficoResumenSaldosClienteView : Form
    {

        public GraficoResumenSaldosClienteView(Cliente cliente, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {
            this.MaximizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.Size = new System.Drawing.Size(600*2, 600);

            this.Cliente = cliente;
            Cuentas = cuentas;
            Transferencias = transferencias;
            this.Build();
        }

        public TableLayoutPanel PanelGraficoResumenSaldos { get; private set; }
        public ComboBox SelectVisualization { get; private set; }
        public ComboBox SelectYear { get; private set; }
 

        public List<Cuenta> Cuentas { get; }
        public List<Transferencia> Transferencias { get; }
        public GraficoResumenSaldosCliente Grsc { get; set; }
        public Button VolverButton { get; private set; }
        public Cliente Cliente { get; set; }

        private void Build()
        {
            PanelGraficoResumenSaldos = new TableLayoutPanel()
            {
                Dock = DockStyle.Left,
                Width = this.Width / 2
            };

            var label = new Label()
            {
                Text = "Type of visualization",
                Dock = DockStyle.Fill
            };

            var label2 = new Label()
            {
                Text = "Year"
            };
            this.SelectVisualization = new ComboBox();
            this.SelectVisualization.Items.AddRange(new string[] { "Years", "Months" });

            this.SelectYear = new ComboBox();

            var fechaMinima = new List<int>();
            this.Cuentas.ForEach((cuenta) => { if (!fechaMinima.Contains(cuenta.FechaApertura.Year)) fechaMinima.Add(cuenta.FechaApertura.Year); });
            var minYear = fechaMinima.Min();
            for (int i = minYear; i <= DateTime.Today.Year; i++)
            {
                this.SelectYear.Items.Add(i);
            }

           

            this.Grsc = new GraficoResumenSaldosCliente(new System.Drawing.Size(500, 500), this.Cliente, this.Cuentas , this.Transferencias, 2016);

            this.VolverButton = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            PanelGraficoResumenSaldos.Controls.Add(label);
            PanelGraficoResumenSaldos.Controls.Add(this.SelectVisualization);
            PanelGraficoResumenSaldos.Controls.Add(label2);
            PanelGraficoResumenSaldos.Controls.Add(this.SelectYear);
            PanelGraficoResumenSaldos.Controls.Add(this.Grsc);

            PanelGraficoResumenSaldos.Controls.Add(this.VolverButton);

            this.Controls.Add(PanelGraficoResumenSaldos);
        }
    }
}
