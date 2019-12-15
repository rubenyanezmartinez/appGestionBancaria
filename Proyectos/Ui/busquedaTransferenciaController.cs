using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;

namespace Proyectos.Ui
{
    using wf = System.Windows.Forms;
    class busquedaTransferenciaController : wf.Form
    {
        public GestorTransferencias gestorT
        {
            get; private set;
        }
        public GestorCuentas gestorC
        {
            get; private set;
        }

        public GestorClientes gestorCC
        {
            get; set;
        }
        public busquedaTransferenciasView View
        {
            get; set;
        }

        public busquedaTransferenciaController(GestorTransferencias gt, GestorCuentas gc, GestorClientes gcc)
        {
            this.gestorT = gt;

            this.gestorC = gc;

            this.gestorCC = gcc;

            this.View = new busquedaTransferenciasView();

            this.View.searchButton.Click += new EventHandler(buscar);
        }

        void listenerBuscar(object o, EventArgs e)
        {
            this.buscar(o, e);
        }

        void buscar(object sender, EventArgs e)
        {
            string param = this.View.Parametro.Text;
            char operacion = '1';
            int numItem = this.View.operacion.SelectedIndex;

            if (numItem >= 0)
            {
                operacion = ((string)this.View.operacion.Items[numItem])[0];
            }

            if (param != "")
            {
                Cliente c;
                Cuenta cc;
                List<Transferencia> lt;
                foreach (wf.ListViewItem l in this.View.gl.Items)
                {
                    this.View.gl.Items.Remove(l);
                }

                switch (operacion)
                {
                    case '1':
                        cc = this.gestorC.GetCuentaByCCC(param);
                        lt = this.gestorT.GetTransferenciaCuentaEmisor(cc);

                        foreach (var t in lt)
                        {

                            var lvi = new wf.ListViewItem(t.Tipo.ToString());
                            lvi.Tag = t;

                            lvi.SubItems.Add(t.CCCOrigen.CCC.ToString());
                            lvi.SubItems.Add(t.CCCDestino.CCC.ToString());
                            lvi.SubItems.Add(t.Importe.ToString());
                            lvi.SubItems.Add(t.Fecha.ToString());
                            lvi.SubItems.Add(cc.Titulares.First().Dni.ToString());
                            lvi.SubItems.Add(gestorC.GetCuentaByCCC(t.CCCDestino.CCC).Titulares.First().Dni.ToString());
                            this.View.gl.Items.Add(lvi);
                        }

                        break;
                    case '2':
                        cc = this.gestorC.GetCuentaByCCC(param);
                        lt = this.gestorT.GetTransferenciaCuentaReceptor(cc);

                        foreach (var t in lt)
                        {

                            var lvi = new wf.ListViewItem(t.Tipo.ToString());
                            lvi.Tag = t;

                            lvi.SubItems.Add(t.CCCOrigen.CCC.ToString());
                            lvi.SubItems.Add(t.CCCDestino.CCC.ToString());
                            lvi.SubItems.Add(t.Importe.ToString());
                            lvi.SubItems.Add(t.Fecha.ToString());
                            lvi.SubItems.Add(cc.Titulares.First().Dni.ToString());
                            lvi.SubItems.Add(gestorC.GetCuentaByCCC(t.CCCDestino.CCC).Titulares.First().Dni.ToString());
                            this.View.gl.Items.Add(lvi);
                        }
                        break;
                    case '3':
                        c = this.gestorCC.ConsultarPorDni(param);
                        lt = this.gestorT.GetTransferenciaClienteEmisor(c);

                        foreach (var t in lt)
                        {

                            var lvi = new wf.ListViewItem(t.Tipo.ToString());
                            lvi.Tag = t;

                            lvi.SubItems.Add(t.CCCOrigen.CCC.ToString());
                            lvi.SubItems.Add(t.CCCDestino.CCC.ToString());
                            lvi.SubItems.Add(t.Importe.ToString());
                            lvi.SubItems.Add(t.Fecha.ToString());
                            lvi.SubItems.Add(param);
                            lvi.SubItems.Add(gestorC.GetCuentaByCCC(t.CCCDestino.CCC).Titulares.First().Dni.ToString());
                            this.View.gl.Items.Add(lvi);
                        }

                        break;
                    case '4':
                        c = this.gestorCC.ConsultarPorDni(param);
                        lt = this.gestorT.GetTransferenciaClienteReceptor(c);

                        foreach (var t in lt)
                        {

                            var lvi = new wf.ListViewItem(t.Tipo.ToString());
                            lvi.Tag = t;
                            lvi.SubItems.Add(t.CCCOrigen.CCC.ToString());
                            lvi.SubItems.Add(t.CCCDestino.CCC.ToString());
                            lvi.SubItems.Add(t.Importe.ToString());
                            lvi.SubItems.Add(t.Fecha.ToString());
                            lvi.SubItems.Add(gestorC.GetCuentaByCCC(t.CCCDestino.CCC).Titulares.First().Dni.ToString());
                            lvi.SubItems.Add(param);
                            this.View.gl.Items.Add(lvi);
                        }
                        break;
                }
            }

        }
    }
}
