using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Clases
{
    public class Transferencia
    {
        public int Id { get; private set; }
        public String Tipo { get; private set; }
        public Cuenta CCCOrigen { get; private set; }
        public Cuenta CCCDestino { get; private set; }
        public double Importe { get; private set; }
        public DateTime Fecha { get; set; }

        
        public Transferencia (int id, String tipo, Cuenta CCCOrigen, Cuenta CCCDestino, double importe, DateTime fecha)
        {
            this.Id = id;
            this.Tipo = tipo;
            this.CCCOrigen = CCCOrigen;
            this.CCCDestino = CCCDestino;
            this.Importe = importe;
            this.Fecha = fecha;
        }

        public XElement ToXML()
        {

            XElement toret = new XElement("transferencia",
                new XElement("id", this.Id.ToString()),
                new XElement("tipo", this.Tipo.ToString()),
                new XElement("cccorigen", this.CCCOrigen.CCC),
                new XElement("cccdestino", this.CCCDestino.CCC),
                new XElement("importe", this.Importe.ToString()),
                new XElement("fecha", this.Fecha.ToString())
                );

            return toret;
        }

    }
}
