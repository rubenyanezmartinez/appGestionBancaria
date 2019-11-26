using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using App_Gestion_Bancaria.Core.Clases;

namespace App_Gestion_Bancaria.Core.Acceso_a_datos
{
    public static class CuentasRepositorio
    {

        public static void Guardar(List<Cuenta> listaCuentas)
        {
            XElement cuentasXML = new XElement("cuentas");

            foreach (Cuenta cuenta in listaCuentas)
            {
                cuentasXML.Add(cuenta.ToXML());
            }

            cuentasXML.Save(CUENTAS_FICHERO);
        }

        public static List<Cuenta> Leer()
        {
            List<Cuenta> toret = new List<Cuenta>();
            XElement cuentasXML = null;

            try
            {
                cuentasXML = XElement.Load(CUENTAS_FICHERO);
            }
            catch (Exception e)
            {
                return null;
            }

            foreach (XElement cuenta in cuentasXML.Elements())
            {
                Cuenta nuevaCuenta = new Cuenta(cuenta.Element("ccc").Value, (Cuenta.TipoCuenta)Enum.Parse(typeof(Cuenta.TipoCuenta),
                    cuenta.Element("tipo").Value), Int32.Parse(cuenta.Element("saldo").Value),
                    Convert.ToDateTime(cuenta.Element("fechaApertura").Value), Int32.Parse(cuenta.Element("interesMensual").Value));

                foreach (XElement titular in cuenta.Element("titulares").Elements())
                {
                    nuevaCuenta.Titulares.Add(new Cliente(titular.Element("dni").Value, titular.Element("nombre").Value,
                                                titular.Element("telefono").Value, titular.Element("email").Value, titular.Element("direccionPostal").Value));
                }

                foreach (XElement deposito in cuenta.Element("depositos").Elements())
                {
                    XElement clienteXML = deposito.Element("cliente");
                    Cliente cliente = new Cliente(clienteXML.Element("dni").Value, clienteXML.Element("nombre").Value,
                                                clienteXML.Element("telefono").Value, clienteXML.Element("email").Value, clienteXML.Element("direccionPostal").Value);
                    nuevaCuenta.Depositos.Add(new Movimiento(Int32.Parse(deposito.Element("cantidad").Value), cliente, Convert.ToDateTime(deposito.Element("fecha").Value)));
                }

                foreach (XElement retirada in cuenta.Element("retiradas").Elements())
                {
                    XElement clienteXML = retirada.Element("cliente");
                    Cliente cliente = new Cliente(clienteXML.Element("dni").Value, clienteXML.Element("nombre").Value,
                                                clienteXML.Element("telefono").Value, clienteXML.Element("email").Value, clienteXML.Element("direccionPostal").Value);
                    nuevaCuenta.Retiradas.Add(new Movimiento(Int32.Parse(retirada.Element("cantidad").Value), cliente, Convert.ToDateTime(retirada.Element("fecha").Value)));
                }

                toret.Add(nuevaCuenta);
            }

            return toret;
        }

        public const String CUENTAS_FICHERO = "cuentas.xml";
    }
}
