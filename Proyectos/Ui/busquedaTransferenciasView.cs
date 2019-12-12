using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Proyectos.Ui
{
    using wf = System.Windows.Forms;
    class busquedaTransferenciasView : wf.Form
    {

        wf.TableLayoutPanel mainPanel
        {
            get; set;
        }

        public busquedaTransferenciasView()
        {
            this.Build();
            this.AutoScroll = true;
            this.Size = new System.Drawing.Size(925, 600);
            this.FormBorderStyle = wf.FormBorderStyle.FixedSingle;

        }

        public void Build()
        {
            var mainPanel = new wf.TableLayoutPanel()
            {
                Dock = wf.DockStyle.Fill
            };

            wf.ListView lv = new wf.ListView()
            {
                Dock = wf.DockStyle.Fill,
                Height = 400
            };

            lv.View = wf.View.Details;

            lv.FullRowSelect = true;
            lv.GridLines = true;


            lv.Columns.Add("Tipo", 100);
            lv.Columns.Add("Cuenta Origen", 200);
            lv.Columns.Add("Cuenta Destino", 200);
            lv.Columns.Add("Importe", 80);
            lv.Columns.Add("Fecha", 120);
            lv.Columns.Add("Cliente Emisor", 100);
            lv.Columns.Add("Cliente Destino", 100);


            mainPanel.Controls.Add(lv);

            wf.FlowLayoutPanel flp = new wf.FlowLayoutPanel()
            {
                Anchor = wf.AnchorStyles.None,
                AutoSize = true
            };

            this.searchButton = new wf.Button
            {
                Text = "Buscar",
                AutoSize = true,

            };

            this.gl = lv;

            this.operacion = new wf.ComboBox
            {
                Dock = wf.DockStyle.Right,
                DropDownStyle = wf.ComboBoxStyle.DropDownList
            };

            this.operacion.Items.AddRange(
                new String[] { "1-Cuenta emisor", "2-Cuenta receptor", "3-Cliente emisor", "4-Cliente receptor" }
            );

            this.operacion.SelectedIndex = 0;

            this.Parametro = new wf.TextBox
            {
                Dock = wf.DockStyle.Fill,
                TextAlign = wf.HorizontalAlignment.Right,
                Size = new System.Drawing.Size(200, 30),
            };

            flp.Controls.Add(this.operacion);
            flp.Controls.Add(this.Parametro);
            flp.Controls.Add(searchButton);
            mainPanel.Controls.Add(flp);

            this.Controls.Add(mainPanel);

        }


        public wf.TextBox Parametro
        {
            get; private set;
        }
        public wf.ComboBox operacion
        {
            get; private set;
        }

        public wf.Button searchButton
        {
            get; private set;
        }

        public wf.ListView gl
        {
            get; set;
        }

    }
}
