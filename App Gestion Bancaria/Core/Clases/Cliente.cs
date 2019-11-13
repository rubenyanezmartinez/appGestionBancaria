using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App_Gestion_Bancaria.Core.Clases
{
    public class Cliente
    {
        public string Dni { get; private set; }
        
        public string Nombre { get; private set; }

        public string Telefono { get; private set; }

        public string Email { get; private set; }

        public string DireccionPostal { get; private set; }

        public Cliente(string dni, string nombre, string telefono, string email, string direccionPostal)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Email = email;
            this.DireccionPostal = direccionPostal;
        }

        public XElement ToXml()
        {
            XElement toret = new XElement("cliente", 
                new XElement("dni", this.Dni),
                new XElement("nombre", this.Nombre),
                new XElement("telefono", this.Telefono),
                new XElement("email", this.Email),
                new XElement("direccionPostal", this.DireccionPostal)
                );

            return toret;
        }

    }
}
