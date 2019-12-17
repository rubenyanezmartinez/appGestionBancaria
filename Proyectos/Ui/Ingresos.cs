using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proyectos.Ui
{
    public partial class Ingresos : Form
    {
       

       

        public Ingresos(List<Cuenta> cuentas, List<Transferencia> transferencias)
        {
            this.Cuentas = cuentas;
            InitializeComponent();
            TableLayoutPanel table = new TableLayoutPanel() { Dock = DockStyle.Bottom};
            
            this.Volver = new Button()
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            table.Controls.Add(this.Volver);
            this.Controls.Add(table);
            
            this.Volver.Click += Volver_Click;




            movimientosGenerales( cuentas, transferencias);
            

        }

        private void Volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   


        //Movimientos Generales
        void movimientosGenerales( List<Cuenta> cuentas, List<Transferencia> transferencias)
        {
            Console.WriteLine(transferencias.Count);
            chart1.Titles.Add("Ingresos Generales");
            chart1.Series["Series1"].LegendText = "Depositos";
            chart1.Series["Series2"].LegendText = "Transferencias";
            foreach (Cuenta cuenta in cuentas)
            {
                for (int i = 1; i <= 12; i++)
                {
                    double importeTotal = 0;
                    cuenta.Depositos.ForEach((deposito) => {
                        if (deposito.Fecha.Date.Month == i)
                        {
                            importeTotal += deposito.Cantidad;
                        }
                    });
                    chart1.Series["Series1"].Points.AddXY(i, importeTotal);

                }

            
            }
            
            for (int i=1; i<= 12; i++)
            {
                double importeTotal = 0;
                transferencias.ForEach((transferencia) => {
                    if (transferencia.Fecha.Date.Month == i)
                    {
                        importeTotal += transferencia.Importe;
                    }
                });
                chart1.Series["Series2"].Points.AddXY(i, importeTotal);

            }
        }

      

        private void Chart2_Click(object sender, EventArgs e)
        {

        }
        public Cliente Cliente
        {
            get; set;
        }

        public List<Cuenta> Cuentas
        {
            get; set;
        }



    }
}
