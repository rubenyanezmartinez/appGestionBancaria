using App_Gestion_Bancaria.Core.Clases;
using Proyectos.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graficos.UI
{
    class GraficoResumenSaldosClienteView : Form
    {
        public List<Cliente> clientes;
        public List<Movimiento> movimientos;
        public List<Cuenta> cuentas;
        public List<Transferencia> transferencias;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        public GraficoResumenSaldosClienteView(Cliente cliente, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {
            

            this.clientes = new List<Cliente>();
            Cliente cliente1 = new Cliente("312A", "Victor", "123456789", "v@v.es", "calle1");
            Cliente cliente2 = new Cliente("312B", "Alex", "978654321", "a@a.es", "calle2");
            this.clientes.Add(cliente1);
            this.clientes.Add(cliente2);
            InitializeComponent();
            //MOCK DATOS


            Cuenta cuenta1 = new Cuenta("123", Cuenta.TipoCuenta.AHORRO, 1200, new DateTime(2018, 12, 21), 12);
            Cuenta cuenta2 = new Cuenta("124", Cuenta.TipoCuenta.AHORRO, 1400, new DateTime(2018, 12, 22), 12);
            cuenta1.Titulares.Add(cliente1);
            cuenta2.Titulares.Add(cliente2);
            Transferencia trans1 = new Transferencia(1, "tipo1", cuenta1, cuenta2, 400, new DateTime(2019, 11, 14));
            Transferencia trans2 = new Transferencia(2, "tipo2", cuenta2, cuenta1, 300, new DateTime(2019, 11, 17));
            Transferencia trans3 = new Transferencia(3, "tipo1", cuenta1, cuenta2, 800, new DateTime(2019, 11, 13));
            Transferencia trans4 = new Transferencia(4, "tipo2", cuenta2, cuenta1, 500, new DateTime(2019, 11, 15));

            Movimiento mov1 = new Movimiento(100, cliente1, new DateTime(2019, 11, 16));
            Movimiento mov2 = new Movimiento(200, cliente1, new DateTime(2019, 11, 18));
            Movimiento mov3 = new Movimiento(300, cliente1, new DateTime(2019, 11, 17));
            Movimiento mov4 = new Movimiento(400, cliente1, new DateTime(2019, 11, 19));
            Movimiento mov5 = new Movimiento(500, cliente2, new DateTime(2019, 11, 17));
            Movimiento mov6 = new Movimiento(600, cliente2, new DateTime(2019, 11, 19));
            Movimiento mov7 = new Movimiento(700, cliente2, new DateTime(2019, 11, 21));
            Movimiento mov8 = new Movimiento(800, cliente2, new DateTime(2019, 11, 22));
            cuentas = new List<Cuenta>();
            movimientos = new List<Movimiento>();
            transferencias = new List<Transferencia>();


            movimientos.Add(mov1);
            movimientos.Add(mov2);
            movimientos.Add(mov3);
            movimientos.Add(mov4);
            movimientos.Add(mov5);
            movimientos.Add(mov6);
            movimientos.Add(mov7);
            movimientos.Add(mov8);

            cuentas.Add(cuenta1);
            cuentas.Add(cuenta2);
            transferencias.Add(trans1);
            transferencias.Add(trans2);
            transferencias.Add(trans3);
            transferencias.Add(trans4);



            //movimientosGenerales(movimientos, cuentas, transferencias);
            movimientosPorCliente(cliente2, movimientos, cuentas, transferencias);
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
        public Ingresos GraficoIngresos { get; set; }

        public Cliente Cliente { get; set; }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(454, 86);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(762, 36);
            this.chart2.Name = "chart2";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            this.chart2.Series.Add(series3);
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(300, 300);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            // 
            // Ingresos
            // 
            this.ClientSize = new System.Drawing.Size(1790, 488);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Name = "Ingresos";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }
      

        //Movimientos por cliente
        void movimientosPorCliente(Cliente cliente, List<Movimiento> movimientos, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {

            chart2.Titles.Add("Ingresos Generales");
            chart2.Series["Series1"].LegendText = "Depositos";
            chart2.Series["Series2"].LegendText = "Transferencias";
            foreach (Cuenta cuenta in cuentas)
            {

                foreach (Movimiento mov in movimientos)
                {
                    if (mov.Cliente.Equals(cliente))
                    {
                        chart2.Series["Series1"].Points.AddXY(mov.Fecha, mov.Cantidad);
                    }



                }
            }
            foreach (Transferencia transferencia in transferencias)
            {
                if (transferencia.CCCOrigen.Titulares.Contains(cliente))
                {

                    chart2.Series["Series2"].Points.AddXY(transferencia.Fecha, transferencia.Importe);
                }
                else if (transferencia.CCCDestino.Titulares.Contains(cliente))
                {

                    chart2.Series["Series2"].Points.AddXY(transferencia.Fecha, transferencia.Importe);

                }

            }

        }
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


            PanelGraficoResumenSaldos.Controls.Add(label);
            PanelGraficoResumenSaldos.Controls.Add(this.SelectVisualization);
            PanelGraficoResumenSaldos.Controls.Add(label2);
            PanelGraficoResumenSaldos.Controls.Add(this.SelectYear);
            PanelGraficoResumenSaldos.Controls.Add(this.Grsc);
            

            this.Controls.Add(PanelGraficoResumenSaldos);
            
        }
    }
}
