using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Graficos.Core;
using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;

namespace Graficos.UI
{
    class GraficoResumenSaldosClienteController : Form
    {

        public GraficoResumenSaldosClienteController(Cliente c)
        {
            this.Cliente = c;
            var cuentas = new GestorCuentas().Cuentas;
            var transferencias = new GestorTransferencias().Transferencias;
            this.View = new GraficoResumenSaldosClienteView(clientes, cuentas, transferencias);
            this.View.SelectYear.SelectedIndex = 0;
            this.View.SelectVisualization.SelectedIndexChanged += SelectVisualization_SelectedIndexChanged;
            this.View.SelectYear.SelectedIndexChanged += SelectYear_SelectedIndexChanged;

            this.View.SelectVisualization.SelectedIndex = 0;
        }

        

        private void SelectYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //Actualizamos el segundo grafico
            var graphic2 = new GraficoResumenSaldosCliente(MainView.graphicsSize, Cliente, this.View.Cuentas, this.View.Transferencias, (int)this.View.SelectYear.Items[this.View.SelectYear.SelectedIndex]);
            this.View.PanelGraficoResumenSaldos.Controls.Remove(this.View.Grsc);
            this.View.Grsc = graphic2;
            this.View.PanelGraficoResumenSaldos.Controls.Add(this.View.Grsc);
        }

        private void SelectVisualization_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.View.SelectVisualization.SelectedIndex)
            {
                case 0:
                    //Actualizamos el segundo grafico
                    this.View.PanelGraficoResumenSaldos.Controls.Remove(this.View.Grsc);
                    this.View.Grsc = new GraficoResumenSaldosCliente(MainView.graphicsSize, this.Cliente, this.View.Cuentas, this.View.Transferencias);
                    this.View.PanelGraficoResumenSaldos.Controls.Add(this.View.Grsc);

                    break;
                case 1:
                    this.View.SelectYear.Show();
                    this.View.SelectYear.SelectedIndex = 0;

                    break;
            }
        }

        public Cliente Cliente
        {
            get;set;
        }

        public GraficoResumenSaldosClienteView View
        {
            get;set;
        }

    }
}
