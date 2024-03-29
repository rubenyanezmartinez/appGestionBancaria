﻿using App_Gestion_Bancaria.Core.Acceso_a_datos;
using App_Gestion_Bancaria.Core.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Gestion_Bancaria.Core.Gestores
{
    public class GestorClientes
    {
        public const string EtiquetaFichero = "clientes.xml";
        public List<Cliente> ContenedorClientes { get; private set; }

        public GestorClientes ()
        {
            this.ContenedorClientes = new List<Cliente>();
            RecuperarClientes();
        }

        //Recupera los datos del fichero XML en caso de existir. Devuelve T en caso de existir clientes almacenados.
        public void RecuperarClientes()
        {
            this.ContenedorClientes = ClientesRepositorio.Leer(EtiquetaFichero);
        }
        
        //Guarda la lista de clientes en el archivo XML
        public void GuardarClientes()
        {
            ClientesRepositorio.Guardar(EtiquetaFichero, this.ContenedorClientes);
        }

        //Insertar cliente pasandole el cliente ya creado. Devuelve T si se ha insertado y F en caso contrario.
        public Boolean Insertar(Cliente cliente)
        {
            string dni = cliente.Dni;
            string telefono = cliente.Telefono;
            string email = cliente.Email;

            if (this.ConsultarPorDni(dni) == null 
                && this.ConsultarPorEmail(email) == null
                && this.ConsultarPorTelefono(telefono) == null)
            {
                this.ContenedorClientes.Add(cliente);
                return true;
            }
            return false;
        }

        //Insertar cliente pasandole todos los atributos necesarios. Devuelve T si se ha insertado y F en caso contrario.
        public Boolean Insertar(string dni, string nombre, string telefono, string email, string direccionPostal)
        {
            if (this.ConsultarPorDni(dni) == null
                && this.ConsultarPorEmail(email) == null
                && this.ConsultarPorTelefono(telefono) == null)
            {
                Cliente cliente = new Cliente(dni, nombre, telefono, email, direccionPostal);
                this.ContenedorClientes.Add(cliente);
                return true;
            }

            return false;
        }

        //Eliminar el cliente con el dni indicado. Devuelve T en caso de existir y eliminarse correctamente, F en caso contrario.
        public Boolean Eliminar(string dni)
        {
            Cliente consulta = this.ConsultarPorDni(dni);

            if (consulta != null)
            {
                this.ContenedorClientes.Remove(consulta);
                return true;
            }

            return false;
        }

        //Edita los parametros pasados que no sean null.
        public void Editar(string dni, string nombre, string telefono, string email, string direccionPostal)
        {
            Cliente consultaDni = this.ConsultarPorDni(dni);
            Cliente consultaTelefono = this.ConsultarPorTelefono(telefono);
            Cliente consultaEmail = this.ConsultarPorEmail(email);
            this.ContenedorClientes.Remove(consultaDni);

            string dniNuevo = dni;
            string nombreNuevo = nombre;
            string telefonoNuevo = telefono;
            string emailNuevo = email;
            string direccionPostalNueva = direccionPostal;

            if (consultaTelefono != null)
            {
                telefonoNuevo = consultaDni.Telefono;
            }
            if (consultaEmail != null)
            {
                emailNuevo = consultaDni.Email;
            }

            Cliente almacenar = new Cliente(dniNuevo, nombreNuevo, telefonoNuevo, emailNuevo, direccionPostalNueva);

            this.ContenedorClientes.Add(almacenar);
        }

        //Devuelve el cliente con ese DNI:
        public Cliente ConsultarPorDni (string dni)
        {
            Cliente toret = null;

            foreach (Cliente cliente in this.ContenedorClientes)
            {
                if (cliente.Dni == dni)
                {
                    toret = cliente;
                }
            }

            return toret;
        }

        //Devuelve el cliente con ese Telefono:
        public Cliente ConsultarPorTelefono(string telefono)
        {
            Cliente toret = null;

            foreach (Cliente cliente in this.ContenedorClientes)
            {
                if (cliente.Telefono == telefono)
                {
                    toret = cliente;
                }
            }

            return toret;
        }

        //Devuelve el cliente con ese Email:
        public Cliente ConsultarPorEmail(string email)
        {
            Cliente toret = null;

            foreach (Cliente cliente in this.ContenedorClientes)
            {
                if (cliente.Email == email)
                {
                    toret = cliente;
                }
            }

            return toret;
        }

    }
}
