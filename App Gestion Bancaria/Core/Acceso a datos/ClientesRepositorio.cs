using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App_Gestion_Bancaria.Core.Acceso_a_datos
{
    public static class ClientesRepositorio
    {
        public static List<Cliente> Leer(string etiquetFichero)
        {
            List<Cliente> toret = new List<Cliente>();

            XmlDocument docXml = new XmlDocument();
            docXml.Load(etiquetFichero);

            if (docXml != null)
            {
                foreach (XmlNode clientes in docXml.DocumentElement.ChildNodes)
                {
                    string dni = "";
                    string nombre = "";
                    string telefono = "";
                    string email = "";
                    string direccionPostal = "";

                    foreach (XmlNode cliente in clientes.ChildNodes)
                    {

                        switch (cliente.Name)
                        {
                            case "dni":
                                dni = cliente.InnerText;
                                break;

                            case "nombre":
                                nombre = cliente.InnerText;
                                break;

                            case "telefono":
                                telefono = cliente.InnerText;
                                break;

                            case "email":
                                email = cliente.InnerText;
                                break;

                            case "direccionPostal":
                                direccionPostal = cliente.InnerText;
                                break;
                        }

                    }

                    Cliente c = new Cliente(dni, nombre, telefono, email, direccionPostal);
                    toret.Add(c);
                }
            }

            return toret;
        }

        public static void Guardar(string etiquetFichero, List<Cliente> clientes)
        {
            using (var xmlWriter = new XmlTextWriter(etiquetFichero, Encoding.UTF8))
            {

                xmlWriter.WriteStartElement("clientes");

                foreach (var cliente in clientes)
                {
                    xmlWriter.WriteStartElement("cliente");


                    xmlWriter.WriteStartElement("dni");
                    xmlWriter.WriteString(cliente.Dni);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("nombre");
                    xmlWriter.WriteString(cliente.Nombre);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("telefono");
                    xmlWriter.WriteString(cliente.Telefono);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("email");
                    xmlWriter.WriteString(cliente.Email);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("direccionPostal");
                    xmlWriter.WriteString(cliente.DireccionPostal);
                    xmlWriter.WriteEndElement();


                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }
    }
}
