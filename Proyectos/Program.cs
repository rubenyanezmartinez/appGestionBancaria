using App_Gestion_Bancaria.Core.Gestores;
using Proyectos.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClienteController().View);
            //Application.Run(new ProductosPersonaController().View);
            //GestorTransferencias gestorTransferencias = new GestorTransferencias();
            //GestorCuentas gestorCuentas = new GestorCuentas();
            //Application.Run(new TransferenciaController(gestorTransferencias, gestorCuentas).View);
<<<<<<< HEAD
            //Application.Run(new CuentaController().View);
            //Application.Run(new ClienteController().View);
=======

>>>>>>> 5141d03ba43d00cb6614dcf13235c6abfe2585b1
        }
    }
}
