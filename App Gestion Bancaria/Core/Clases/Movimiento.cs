using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Clases
{
    public class Movimiento
    {
        public XElement ToXml()
        {
            return new XElement("movimiento", 
                new XElement("cantidad", this.Cantidad),
                new XElement("fecha", this.Fecha.ToString()),
                this.Cliente.ToXml());

        }

        public Movimiento(int cantidad, Cliente cliente, DateTime Fecha)
        {
            this.Cliente = cliente;
            this.Cantidad = cantidad;
            this.Fecha = Fecha;
        }
        public int Cantidad { get; set; }

        public Cliente Cliente { get; set; }

        public DateTime Fecha { get; set; }
    }
}
