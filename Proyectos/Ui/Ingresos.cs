using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionBancaria.UI
{
    public partial class Ingresos : Form
    {
        public List<Cliente> clientes;
            public List<Movimiento> movimientos;
      


        public Ingresos()
        {
            this.clientes = new List<Cliente>();
            Cliente cliente1 = new Cliente("312A", "Victor", 123456789, "v@v.es", "calle1");
            Cliente cliente2 = new Cliente("312B", "Alex", 978654321, "a@a.es", "calle2");
            this.clientes.Add(cliente1);
            this.clientes.Add(cliente2);
            InitializeComponent();
            //MOCK DATOS
           

            Cuenta cuenta1 = new Cuenta(123, "credito", 1200, "victor", "12-21-2018", 12);
            Cuenta cuenta2 = new Cuenta(124, "credito", 1400, "alex", "12-22-2018", 12);

            Transferencia trans1 = new Transferencia(1, "tipo1", cuenta1, cuenta2, 400, new DateTime(2019, 11, 14));
            Transferencia trans2 = new Transferencia(2, "tipo2", cuenta2, cuenta1, 300, new DateTime(2019, 11, 17));
            Transferencia trans3 = new Transferencia(3, "tipo1", cuenta1, cuenta2, 800, new DateTime(2019, 11, 13));
            Transferencia trans4 = new Transferencia(4, "tipo2", cuenta2, cuenta1, 500, new DateTime(2019, 11, 15));

            Movimiento mov1 = new Movimiento(100, cliente1, new DateTime(2019, 11, 16), cuenta1);
            Movimiento mov2 = new Movimiento(200, cliente1, new DateTime(2019, 11, 18), cuenta1);
            Movimiento mov3 = new Movimiento(300, cliente1, new DateTime(2019, 11, 17), trans1);
            Movimiento mov4 = new Movimiento(400, cliente1, new DateTime(2019, 11, 19), trans2);
            Movimiento mov5 = new Movimiento(500, cliente2, new DateTime(2019, 11, 17), cuenta2);
            Movimiento mov6 = new Movimiento(600, cliente2, new DateTime(2019, 11, 19), cuenta2);
            Movimiento mov7 = new Movimiento(700, cliente2, new DateTime(2019, 11, 21), trans3);
            Movimiento mov8 = new Movimiento(800, cliente2, new DateTime(2019, 11, 22), trans4);

            movimientos = new List<Movimiento>();
           
            movimientos.Add(mov1);
            movimientos.Add(mov2);
            movimientos.Add(mov3);
            movimientos.Add(mov4);
            movimientos.Add(mov5);
            movimientos.Add(mov6);
            movimientos.Add(mov7);
            movimientos.Add(mov8);
            


            movimientosGenerales(movimientos);
            
         
        }
        void operar()
        {
            var array = clientes.ToArray();
            var selected = combo.SelectedIndex;
            Console.WriteLine(array[selected].Dni, movimientos);
            movimientosPorCliente(array[selected].Dni, movimientos);
        }

      

        //Movimientos Generales
        void movimientosGenerales(List<Movimiento> movimientos)
        {

            chart1.Titles.Add("Ingresos Generales");
            chart1.Series["Series1"].LegendText = "Depositos";
            chart1.Series["Series2"].LegendText = "Transferencias";

            foreach (Movimiento mov in movimientos)
            {
              
                if (mov.Tipo.GetType().Name != "Transferencia")
                {
                    chart1.Series["Series1"].Points.AddXY(mov.Fecha, mov.Cantidad);
                }
                else
                {
                    var importeTrans = mov.Tipo.GetType().GetProperty("Importe");
                    var fechaTrans = mov.Tipo.GetType().GetProperty("Fecha");

                    chart1.Series["Series2"].Points.AddXY(fechaTrans.GetValue(mov.Tipo), importeTrans.GetValue(mov.Tipo));
                }
                
            }
        }

        //Movimientos por cliente
        void movimientosPorCliente(string cliente,List<Movimiento> movimientos)
        {

            chart2.Titles.Add("Ingresos del Cliente " + cliente);
            chart2.Series["Series1"].LegendText = "Depositos";
            chart2.Series["Series2"].LegendText = "Transferencias";
            
            foreach (Movimiento mov in movimientos)
            {
                if (mov.Cliente.Dni == cliente)
                {
                    if (mov.Tipo.GetType().Name != "Transferencia")
                    {
                        
                        chart2.Series["Series1"].Points.AddXY(mov.Fecha, mov.Cantidad);
                    }
                    else
                    {
                        var importeTrans = mov.Tipo.GetType().GetProperty("Importe");
                        var fechaTrans = mov.Tipo.GetType().GetProperty("Fecha");
                        Console.WriteLine("holaaaaaaaaaaaa",mov.Fecha);

                        chart2.Series["Series2"].Points.AddXY(fechaTrans.GetValue(mov.Tipo), importeTrans.GetValue(mov.Tipo));
                    }
                }
                

            }

        }

        private void Chart2_Click(object sender, EventArgs e)
        {

        }

    }
}
