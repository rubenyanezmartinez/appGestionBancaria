using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Graficos.UI
{
    class GraficoResumenCuentaView : Form
    {

        //public GraficoResumenCuentaView(Cuenta cuenta, List<Transferencia> transferencias)
        //{
        //    this.Size = new System.Drawing.Size(600 *2, 600);
        //    this.Cuenta = cuenta;
        //    Transferencias = transferencias;
        //    this.Build();
        //}

        //public ComboBox SelectVisualization { get; private set; }
        //public ComboBox SelectYear { get; private set; }
        //public Cuenta Cuenta { get; private set; }
        //public List<Transferencia> Transferencias { get; }
        //public TableLayoutPanel PanelGraficoResumenCuenta { get; private set; }
        //public GraficoResumenCuenta Grc { get; set; }

        //private void Build()
        //{

        //    PanelGraficoResumenCuenta = new TableLayoutPanel()
        //    {
        //        Dock = DockStyle.Left,
        //        Width = this.Width / 2
        //    };

        //    var label = new Label()
        //    {
        //        Text = "Type of visualization",
        //        Dock = DockStyle.Fill
        //    };

        //    var label2 = new Label()
        //    {
        //        Text = "Year"
        //    };
        //    this.SelectVisualization = new ComboBox();
        //    this.SelectVisualization.Items.AddRange(new string[] { "Years", "Months" });

        //    this.SelectYear = new ComboBox();
        //    for (int i = this.Cuenta.FechaApertura.Year; i <= DateTime.Today.Year; i++)
        //    {
        //        this.SelectYear.Items.Add(i);
        //    }

        //    this.Grc = new GraficoResumenCuenta(new System.Drawing.Size(500, 500), this.Cuenta, this.Transferencias);
        //    this.Grc.Draw();


        //    PanelGraficoResumenCuenta.Controls.Add(label);
        //    PanelGraficoResumenCuenta.Controls.Add(this.SelectVisualization);
        //    PanelGraficoResumenCuenta.Controls.Add(label2);
        //    PanelGraficoResumenCuenta.Controls.Add(this.SelectYear);
        //    PanelGraficoResumenCuenta.Controls.Add(this.Grc);

        //    this.Controls.Add(PanelGraficoResumenCuenta);
        //}
    }
}
