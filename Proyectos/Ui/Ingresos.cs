using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proyectos.Ui
{
    public partial class Ingresos : Form
    {
        public List<Cliente> clientes;
        public List<Movimiento> movimientos;
        public List<Cuenta> cuentas;
        public List<Transferencia> transferencias;



        public Ingresos(Cliente c)
        {
            this.Cliente = c;
           
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



            movimientosGenerales(movimientos, cuentas, transferencias);
            movimientosPorCliente(cliente1, movimientos, cuentas, transferencias);

        }
        /*void operar()
        {
            var array = clientes.ToArray();
            var selected = combo.SelectedIndex;
           
            
        }*/



        //Movimientos Generales
        void movimientosGenerales(List<Movimiento> movimientos, List<Cuenta> cuentas, List<Transferencia> transferencias)
        {

            chart1.Titles.Add("Ingresos Generales");
            chart1.Series["Series1"].LegendText = "Depositos";
            chart1.Series["Series2"].LegendText = "Transferencias";
            foreach (Cuenta cuenta in cuentas)
            {

                foreach (Movimiento mov in movimientos)
                {


                    chart1.Series["Series1"].Points.AddXY(mov.Fecha, mov.Cantidad);

                    // var importeTrans = mov.Tipo.GetType().GetProperty("Importe");
                    //var fechaTrans = mov.Tipo.GetType().GetProperty("Fecha");




                }
            }
            foreach (Transferencia transferencia in transferencias)
            {
                chart1.Series["Series2"].Points.AddXY(transferencia.Fecha, transferencia.Importe);
            }
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

        private void Chart2_Click(object sender, EventArgs e)
        {

        }
        public Cliente Cliente
        {
            get; set;
        }
        


    }
}
