using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;

namespace Proyectos.Ui
{
    using wf = System.Windows.Forms;
    class MainController : wf.Form
    {
        public MainView View { get; set; }

        public GestorClientes gestorClientes;
        public GestorCuentas gestorCuentas;
        public GestorTransferencias gestorTransferencias;

        public MainController(GestorClientes Clientes, GestorCuentas Cuentas, GestorTransferencias Transferencias)
        {
            this.View = new MainView();

            this.gestorClientes = Clientes;

            this.gestorCuentas = Cuentas;

            this.gestorTransferencias = Transferencias;

            this.bindBotones();
        }

        public void bindBotones()
        {
            this.View.gClientes.Click += (sender, e) => this.gestionClientes();
            this.View.gCuentas.Click += (sender, e) => this.gestionCuentas();
            this.View.gTransferencias.Click += (sender, e) => this.gestionTransferencias();
        }
        private void gestionClientes()
        {
            this.View.Hide();

            new ClienteController(this.gestorClientes).View.ShowDialog();

            this.View.Show();
        }

        private void gestionCuentas()
        {
            this.View.Hide();

            new CuentaController().View.ShowDialog();

            this.View.Show();
        }

        private void gestionTransferencias()
        {
            this.View.Hide();

            new TransferenciaController(this.gestorTransferencias, this.gestorCuentas, this.gestorClientes).View.ShowDialog();

            this.View.Show();
        }
    }
}
