using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Acceso_a_datos
{
    public class TransferenciasRepositorio
    {
        private static readonly String NombreFicheroXML = "Transferencias.xml";
        public void Guardar(List<Transferencia> transferencias)
        {
            XElement transferenciasXML = new XElement("transferencias");

            foreach (Transferencia transferencia in transferencias)
            {
                transferenciasXML.Add(transferencia.ToXML());
            }

            transferenciasXML.Save(NombreFicheroXML);
        }

        public List<Transferencia> Leer()
        {
            List<Transferencia> toret = new List<Transferencia>();
            XElement transferenciasXML = null;

            try
            {
                transferenciasXML = XElement.Load(NombreFicheroXML);
                foreach (XElement transferencia in transferenciasXML.Elements())
                {
                    int id = Int32.Parse(transferencia.Element("id").Value);
                    String tipo = transferencia.Element("tipo").Value;
                    Cuenta cuentaOrigen = new Cuenta()
                    {
                        CCC = transferencia.Element("cccorigen").Value
                    };
                    Cuenta cuentaDestino = new Cuenta()
                    {
                        CCC = transferencia.Element("cccdestino").Value
                    };

                    float importe = float.Parse(transferencia.Element("importe").Value);
                    DateTime fecha = DateTime.Parse(transferencia.Element("fecha").Value);

                    Transferencia nuevaTransferencia = new Transferencia(id, tipo, cuentaOrigen, cuentaDestino, importe, fecha);

                    toret.Add(nuevaTransferencia);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error al recuperar transferencias de fichero");
            }

            return toret;
        }
    }
}
