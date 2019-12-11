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
            GestorClientes gestorClientes= new GestorClientes();
            GestorTransferencias gestorTransferencias = new GestorTransferencias();
            GestorCuentas gestorCuentas = new GestorCuentas();
            //Application.Run(new ClienteController(gestorClientes).View);
            //Application.Run(new ProductosPersonaController().View);
            Application.Run(new busquedaTransferenciaController(gestorTransferencias, gestorCuentas, gestorClientes).View);
            //Application.Run(new TransferenciaController(gestorTransferencias, gestorCuentas, gestorClientes).View);
            //Application.Run(new CuentaController().View);

        }
    }
}
