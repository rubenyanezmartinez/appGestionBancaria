using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        public GraficoResumenSaldosClienteView(Cliente cliente, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {
            

            this.MaximizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.Size = new System.Drawing.Size(600*2, 600);

            this.Cliente = cliente;
            Cuentas = cuentas;
            Transferencias = transferencias;
            InitializeComponent();
            movimientosPorCliente(Cliente, Cuentas, Transferencias);
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
                Width = this.Width / 2,
                Padding = new Padding(10)
            };

            var label = new Label()
            {
                Text = "Tipo de visualización",
                Dock = DockStyle.Fill
            };

            var label2 = new Label()
            {
                Text = "Año"
            };
            this.SelectVisualization = new ComboBox();
            this.SelectVisualization.Items.AddRange(new string[] { "Años", "Meses" });

            this.SelectYear = new ComboBox();

            var fechaMinima = new List<int>();
            this.Cuentas.ForEach((cuenta) => { if (!fechaMinima.Contains(cuenta.FechaApertura.Year)) fechaMinima.Add(cuenta.FechaApertura.Year); });
            var minYear = fechaMinima.Min();
            for (int i = minYear; i <= DateTime.Today.Year; i++)
            {
                this.SelectYear.Items.Add(i);
            }

           

            this.Grsc = new GraficoResumenSaldosCliente(new System.Drawing.Size(600, 600), Cliente, Cuentas , Transferencias, minYear);

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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
          
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();

            //this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(854, 86);
            this.chart2.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart2.Series.Add(series1);
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(600, 600);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chart1";

            // 
            // Ingresos
            // 
            this.ClientSize = new System.Drawing.Size(1790, 488);

            this.Controls.Add(this.chart2);
            this.Name = "Ingresos";
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();

            this.ResumeLayout(false);

        }
        //Movimientos por cliente
        void movimientosPorCliente(Cliente cliente, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {

            chart2.Titles.Add("Ingresos del cliente "+ cliente.Dni);
            chart2.Series["Series1"].LegendText = "Depositos";
            chart2.Series["Series2"].LegendText = "Transferencias";
            foreach(Cuenta c in cuentas)
            {
                if(c.Titulares.FindAll((titular) => titular.Dni == cliente.Dni).Count > 0)
                {
                    continue;
                }
                for (int i = 1; i <= 12; i++)
                {
                    
                    double importeTotal = 0;
                    var gestorCuentas = new App_Gestion_Bancaria.Core.Gestores.GestorCuentas();
                    var gestorClientes = new GestorClientes();

                    c.Depositos.ForEach((deposito) => {
                        if (deposito != null && deposito.Fecha.Date.Month == i)
                           
                        {
                            importeTotal += deposito.Cantidad;
                        }
                    });

                    chart2.Series["Series1"].Points.AddXY(i, importeTotal);

                }
            }
            /* foreach (Cuenta cuenta in cuentas)
             {

                     
                         chart2.Series["Series1"].Points.AddXY(cuenta.Titulares,cuenta.Saldo);
                     




             }*/
            for (int i = 1; i <= 12; i++)
            {
                double importeTotal = 0;
                var gestorCuentas = new App_Gestion_Bancaria.Core.Gestores.GestorCuentas();
                var gestorClientes = new GestorClientes();
                transferencias.ForEach((transferencia) => {
                    if (transferencia != null 
                        && transferencia.Fecha.Date.Month == i 
                        && ((gestorCuentas.GetCuentaByCCC(transferencia.CCCOrigen.CCC).Titulares.FindAll((titular) => titular.Dni == cliente.Dni)).Count > 0)  
                        || gestorCuentas.GetCuentaByCCC(transferencia.CCCDestino.CCC).Titulares.FindAll((titular) => titular.Dni == cliente.Dni).Count > 0)
                    {
                        importeTotal += transferencia.Importe;
                    }
                });

                chart2.Series["Series2"].Points.AddXY(i, importeTotal);

            }
            /*foreach (Transferencia transferencia in transferencias)
            {
                if (transferencia.CCCOrigen.Titulares.Contains(cliente))
                {

                    chart2.Series["Series2"].Points.AddXY(transferencia.Fecha, transferencia.Importe);
                }
                else if (transferencia.CCCDestino.Titulares.Contains(cliente))
                {

                    chart2.Series["Series2"].Points.AddXY(transferencia.Fecha, transferencia.Importe);

                }

            }*/

        }
    }
}
