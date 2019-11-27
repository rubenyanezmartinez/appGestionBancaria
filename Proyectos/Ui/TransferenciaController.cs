using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class TransferenciaController
    {
        public TransferenciaView View { get; private set; }
        public GestorTransferencias GestorTransferencias { get; private set; }
        public GestorCuentas GestorCuentas { get; private set; }
        public TransferenciaController(GestorTransferencias gestorTransferencias, GestorCuentas gestorCuentas)
        {
            this.GestorTransferencias = gestorTransferencias;
            this.GestorCuentas = gestorCuentas;
            this.View = new TransferenciaView(this.GestorTransferencias.Transferencias);
            this.IniciarBotones();
        }

        private void IniciarBotones()
        {
            this.View.BotonAddPanel.Click += new EventHandler(AccionInsertarTransferencia);
            this.View.BotonDeleteTransferencia.Click += new EventHandler(AccionEliminarTransferencia);
            this.View.BotonModifyPanel.Click += new EventHandler(AccionModificarTransferencia);
            this.View.BotonCerraryGuardar.Click += new EventHandler(AccionGuardarYSalir);
        }

        private void AccionInsertarTransferencia(object sender, EventArgs e)
        {
            this.View.AddTransferenciaPanel();
            this.View.BotonAddTransferencia.Click += new EventHandler(AccionInsertar);
            this.View.BotonHome.Click += new EventHandler(AccionHome);
        }

        private void AccionInsertar(object sender, EventArgs e)
        {
            string Tipo = this.View.Tipo.Text;
            Cuenta CCCOrigen = GestorCuentas.GetCuentaByCCC(this.View.CCCOrigen.Text);
            Cuenta CCCDestino = GestorCuentas.GetCuentaByCCC(this.View.CCCDestino.Text);
            double Importe;
            Double.TryParse(this.View.Importe.Text, out Importe);

            if (Tipo == "")
            {
                Tipo = "puntual";
            }

            if (CCCOrigen == null)
            {
                this.View.AlertError("La cuenta de origen no existe ", this.GestorTransferencias);
            }
            else if (CCCDestino == null)
            {
                this.View.AlertError("La cuenta de destino no existe ", this.GestorTransferencias);
            }
            else if (Importe == 0)
            {
                this.View.AlertError("Importe no válido ", this.GestorTransferencias);
            }
            else
            {
                int Id = 1;

                if (GestorTransferencias.Transferencias.Count() != 0)
                {
                    Id = (GestorTransferencias.Transferencias.Last().Id) + 1;
                }

                this.GestorTransferencias.AddTransferencia(new Transferencia(Id, Tipo, CCCOrigen, CCCDestino, Importe, DateTime.Now));

                this.View.GetTransferenciasMainPanel(this.GestorTransferencias);
            }

            this.IniciarBotones();
        }

        private void AccionModificarTransferencia(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaTransferencias.SelectedRows;

            if(filasSeleccionadas.Count > 0)
            {
                int indice = filasSeleccionadas[0].Index;

                Transferencia transferenciaSeleccionada = this.GestorTransferencias.Transferencias[indice];

                this.View.ModifyTransferenciaPanel(transferenciaSeleccionada);
                this.View.BotonModifyTransferencia.Click += new EventHandler(AccionModificar);
                this.View.BotonHome.Click += new EventHandler(AccionHome);
            }
            else
            {
                this.View.AlertError("Debe seleccionar una transferencia ", this.GestorTransferencias);
            }
        }

        private void AccionModificar(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaTransferencias.SelectedRows;
            int indice = filasSeleccionadas[0].Index;

            Transferencia transferenciaSeleccionada = this.GestorTransferencias.Transferencias[indice];

            string Tipo = this.View.Tipo.Text;
            Cuenta CCCOrigen = GestorCuentas.GetCuentaByCCC(this.View.CCCOrigen.Text);
            Cuenta CCCDestino = GestorCuentas.GetCuentaByCCC(this.View.CCCDestino.Text);
            double Importe;
            Double.TryParse(this.View.Importe.Text, out Importe);
            if (Tipo == "")
            {
                Tipo = "puntual";
            }

            if (CCCOrigen == null)
            {
                this.View.AlertError("La cuenta de origen no existe ", this.GestorTransferencias);
            }
            else if (CCCDestino == null)
            {
                this.View.AlertError("La cuenta de destino no existe ", this.GestorTransferencias);
            }
            else if (Importe == 0)
            {
                this.View.AlertError("Importe no válido ", this.GestorTransferencias);
            }
            else
            {
                Transferencia transferenciaModificada = new Transferencia(
                    transferenciaSeleccionada.Id,
                    Tipo, 
                    CCCOrigen,
                    CCCDestino,
                    Importe,
                    /*DateTime.Now*/
                    transferenciaSeleccionada.Fecha
                );

                this.GestorTransferencias.Modificar(transferenciaModificada);
            
                this.View.GetTransferenciasMainPanel(this.GestorTransferencias);
            }

            this.IniciarBotones();
        }

        private void AccionEliminarTransferencia(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaTransferencias.SelectedRows;

            if(filasSeleccionadas.Count > 0)
            {
                int indice = filasSeleccionadas[0].Index;

                Transferencia transferenciaSeleccionada = this.GestorTransferencias.Transferencias[indice];

                if (this.View.AlertDeleteTransferencia("¿Desea elminar la transferencia con ID " +
                    transferenciaSeleccionada.Id + " ?"))
                {
                    this.GestorTransferencias.RemoveTransferencia(transferenciaSeleccionada);
                }
            }
            else
            {
                this.View.AlertError("Debe seleccionar una transferencia ", this.GestorTransferencias);
            }

            this.View.GetTransferenciasMainPanel(this.GestorTransferencias);
            this.IniciarBotones();
        }

        private void AccionHome(object sender, EventArgs e)
        {
            this.View.GetTransferenciasMainPanel(this.GestorTransferencias);
            this.IniciarBotones();
        }

        private void AccionGuardarYSalir(object sender, EventArgs e)
        {
            this.GestorTransferencias.GuardarTransferencias(this.GestorTransferencias.Transferencias);
            this.View.Close();
        }
    }
}
