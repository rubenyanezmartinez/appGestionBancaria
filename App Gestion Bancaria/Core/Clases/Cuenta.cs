using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Clases
{
    public class Cuenta
    {

        public Cuenta(string cCC, TipoCuenta tipo, int saldo, DateTime fechaApertura, int interesMensual)
        {
            CCC = cCC;
            Tipo = tipo;
            Saldo = saldo;
            Titulares = new List<Cliente>();
            FechaApertura = fechaApertura;
            InteresMensual = interesMensual;
            Depositos = new List<Movimiento>();
            Retiradas = new List<Movimiento>();
        }

        public Cuenta() { }

        public Cuenta(string CCC)
        {
            this.CCC = CCC;
            this.FechaApertura = DateTime.Today;
            this.Depositos = new List<Movimiento>();
            this.Retiradas = new List<Movimiento>();
            this.Titulares = new List<Cliente>();

        }

        public XElement ToXML()
        {
            XElement titulares = new XElement("titulares");

            foreach (Cliente cliente in this.Titulares)
            {
                titulares.Add(cliente.ToXml());
            }

            XElement depositos = new XElement("depositos");

            foreach (Movimiento deposito in this.Depositos)
            {
                depositos.Add(deposito.ToXml());
            }

            XElement retiradas = new XElement("retiradas");

            foreach (Movimiento retirada in this.Retiradas)
            {
                retiradas.Add(retirada.ToXml());
            }

            XElement toret = new XElement("cuenta",
                new XElement("tipo", this.Tipo.ToString()),
                titulares,
                new XElement("ccc", this.CCC),
                new XElement("saldo", this.Saldo),
                new XElement("fechaApertura", this.FechaApertura),
                new XElement("interesMensual", this.InteresMensual),
                depositos,
                retiradas
                );

            return toret;
        }

        public override String ToString()
        {
            String titularesString = "";

            foreach (Cliente titular in this.Titulares)
            {
                titularesString += titular.Nombre + ", ";
            }

            String depositosString = "";

            foreach (Movimiento deposito in this.Depositos)
            {
                depositosString += deposito.Cantidad + ", ";
            }

            String retiradasString = "";

            foreach (Movimiento retirada in this.Retiradas)
            {
                retiradasString += retirada.Cantidad + ", ";
            }

            return "CUENTA " + this.Tipo.ToString() + ": " +
                "\n\tCCC: " + this.CCC +
                "\n\tSaldo: " + this.Saldo +
                "\n\tTitulares: " + titularesString +
                "\n\tFecha Apertura: " + this.FechaApertura.ToLongDateString() +
                "\n\tInterés Mensual: " + this.InteresMensual +
                "\n\tDepósitos: " + depositosString +
                "\n\tRetiradas: " + retiradasString;
        }

        public enum TipoCuenta { CORRIENTE, AHORRO, VIVIENDA };

        public String CCC { get; set; }

        public TipoCuenta Tipo { get; set; }

        public int Saldo { get; set; }

        public List<Cliente> Titulares { get; set; }

        public DateTime FechaApertura { get; set; }

        public int InteresMensual { get; set; }

        public List<Movimiento> Depositos { get; set; }

        public List<Movimiento> Retiradas { get; set; }

    }
}
