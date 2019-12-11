using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Graficos.Core;

namespace Graficos.UI
{
    class GraficoResumenCuentaController : Form
    {

        public GraficoResumenCuentaController(Cuenta cuenta, List<Transferencia> transferencias)
        {
            this.View = new GraficoResumenCuentaView(cuenta, transferencias);

            this.View.SelectYear.SelectedIndex = 0;
            this.View.SelectVisualization.SelectedIndexChanged += SelectVisualization_SelectedIndexChanged;
            this.View.SelectYear.SelectedIndexChanged += SelectYear_SelectedIndexChanged;


        }

        private void SelectYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Actualizamos el primer grafico
            var graphic = new GraficoResumenCuenta(MainView.graphicsSize, this.View.Cuenta, this.View.Transferencias, (int)this.View.SelectYear.Items[this.View.SelectYear.SelectedIndex]);
            this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.Grc);
            this.View.Grc = graphic;
            this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.Grc);

        }

        private void SelectVisualization_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.View.SelectVisualization.SelectedIndex)
            {
                case 0:
                    //Actualizamos el primer grafico
                    this.View.SelectYear.Hide();
                    this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.Grc);
                    this.View.Grc = new GraficoResumenCuenta(MainView.graphicsSize, this.View.Cuenta, this.View.Transferencias);
                    this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.Grc);
                    break;
                case 1:
                    this.View.SelectYear.Show();
                    this.View.SelectYear.SelectedIndex = 0;

                    break;
            }
        }

        public GraficoResumenCuentaView View
        {
            get; set;
        }
    }

}
