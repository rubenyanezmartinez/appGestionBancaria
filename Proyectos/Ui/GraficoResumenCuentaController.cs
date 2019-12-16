using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using App_Gestion_Bancaria.Core.Clases;
using App_Gestion_Bancaria.Core.Gestores;
using Proyectos.Ui;

namespace Graficos.UI
{
    class GraficoResumenCuentaController : Form
    {

        public GraficoResumenCuentaController(Cuenta cuenta)
        {
            
            this.MaximizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            //Buscamos las transferencias
            var transferencias = new GestorTransferencias().Transferencias.FindAll((transferencia) => transferencia.CCCDestino.CCC == cuenta.CCC || transferencia.CCCOrigen.CCC == cuenta.CCC);
            this.View = new GraficoResumenCuentaView(cuenta, transferencias);

            this.View.SelectYear.SelectedIndex = 0;
            this.View.SelectVisualization.SelectedIndexChanged += SelectVisualization_SelectedIndexChanged;
            this.View.SelectYear.SelectedIndexChanged += SelectYear_SelectedIndexChanged;
            this.View.VolverButton.Click += VolverButton_Click;


        }

        private void VolverButton_Click(object sender, EventArgs e)
        {
            this.View.Close();
        }

        private void SelectYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Actualizamos el primer grafico
            var graphic = new GraficoResumenCuenta(new System.Drawing.Size(500, 500), this.View.Cuenta, this.View.Transferencias, (int)this.View.SelectYear.Items[this.View.SelectYear.SelectedIndex]);
            this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.Grc);
            this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.VolverButton);
            this.View.Grc = graphic;
            this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.Grc);
            this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.VolverButton);

        }

        private void SelectVisualization_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.View.SelectVisualization.SelectedIndex)
            {
                case 0:
                    //Actualizamos el primer grafico
                    this.View.SelectYear.Hide();
                    this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.Grc);
                    this.View.PanelGraficoResumenCuenta.Controls.Remove(this.View.VolverButton);
                    this.View.Grc = new GraficoResumenCuenta(new System.Drawing.Size(500, 500), this.View.Cuenta, this.View.Transferencias);
                    this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.Grc);
                    this.View.PanelGraficoResumenCuenta.Controls.Add(this.View.VolverButton);
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
