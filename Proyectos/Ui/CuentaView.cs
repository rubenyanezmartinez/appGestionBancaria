using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;
using System;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class CuentaView : WFrms.Form
    {
        public CuentaView(GestorCuentas gestor)
        {
            this.ShowIndex(gestor);            
        }

        public void ShowIndex(GestorCuentas gestor)
        {
            this.Controls.Clear();
            this.MainPanel = new Panel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(this.BuildTablaCuentas(gestor.Cuentas));
            this.MainPanel.Controls.Add(this.BuildButtonDetalle());
            this.Controls.Add(MainPanel);
            this.MainPanel.AutoScroll = true;

        }

        public void ShowDetalles(Cuenta cuenta)
        {
            this.Controls.Clear();
            this.MainPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(BuildCCCTextBox(cuenta.CCC));
            this.MainPanel.Controls.Add(BuildSaldoTextBox(cuenta.Saldo));
            this.MainPanel.Controls.Add(BuildTipoCb(cuenta.Tipo));
            this.MainPanel.Controls.Add(BuildFechaAperturaCalendar(cuenta.FechaApertura));
            this.MainPanel.Controls.Add(BuildInteresMensualNumeric(cuenta.InteresMensual));
            //this.MainPanel.Controls.Add(BuildTitularesTable(cuenta.Titulares));

            var movimientosPanel = new Panel { Dock = DockStyle.Top };

            movimientosPanel.Controls.Add(BuildRetiradasTable(cuenta.Retiradas));
            movimientosPanel.Controls.Add(BuildDepositosTable(cuenta.Depositos));

            this.MainPanel.Controls.Add(movimientosPanel);
            this.MainPanel.Controls.Add(BuildButtonVolver());
            this.MainPanel.Controls.Add(BuildGuardarButton());

            this.Controls.Add(this.MainPanel);
            this.MainPanel.AutoScroll = true;

        }

        public void ShowAddMovimiento(Boolean isRetirada)
        {
            this.Controls.Clear();
            this.MainPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(BuildClienteTextBox());
            this.MainPanel.Controls.Add(BuildCantidadNumeric());
            this.MainPanel.Controls.Add(BuildFechaMovimientoDate());

            if (isRetirada)
            {
                this.MainPanel.Controls.Add(this.AddRetiradaButton);
            } else
            {
                this.MainPanel.Controls.Add(this.AddDepositoButton);
            }

            this.MainPanel.Controls.Add(BuildButtonVolver());

            this.Controls.Add(this.MainPanel);
        }

        private Panel BuildGuardarButton()
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            this.GuardarButton = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Guardar"
            };

            pnl.Controls.Add(this.GuardarButton);

            return pnl;
        }

        private Panel BuildClienteTextBox()
        {
            var lable = new Label
            {
                Dock = DockStyle.Left,
                Text = "Cliente"
            };

            var pnl = new Panel { Dock = DockStyle.Top };

            this.ClienteTextBox = BuildTextBox("");

            pnl.Controls.Add(lable);
            pnl.Controls.Add(this.ClienteTextBox);

            return pnl;
        }

        private Panel BuildCantidadNumeric()
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            var labelLeft = new Label
            {
                Dock = DockStyle.Left,
                Text = "Cantidad (céntimos)"
            };

            this.CantidadNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right
            };

            pnl.Controls.Add(labelLeft);
            pnl.Controls.Add(this.CantidadNumeric);

            return pnl;
        }

        private Panel BuildFechaMovimientoDate()
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Fecha" };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.FechaMovimientoDate = new DateTimePicker
            {
                Dock = DockStyle.Right
            };

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.FechaMovimientoDate);

            return pnl;
        }

        private Panel BuildButtonVolver()
        {
            var pnl = new Panel { Dock = DockStyle.Bottom };
            this.ButtonVolver = new Button
            {
                Dock = DockStyle.Top,
                Text = "Volver"
            };

            pnl.Controls.Add(this.ButtonVolver);

            return pnl;
        }

        private TextBox BuildTextBox(string value)
        {
            var toret = new TextBox
            {
                Dock = DockStyle.Right,
                Text = value
            };

            return toret;
        }

        private Panel BuildCCCTextBox(string CCC)
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            var label = new Label { Dock = DockStyle.Left, Text = "CCC", Enabled = false };

            this.CCCTextBox = BuildTextBox(CCC);

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.CCCTextBox);
            pnl.MaximumSize = new Size(int.MaxValue, this.CCCTextBox.Height * 2);

            return pnl;
        }
        private Panel BuildSaldoTextBox(int saldo)
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Saldo (en céntimos)" };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.SaldoNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right,
                Minimum = 0,
                Maximum = int.MaxValue,
                Value = saldo
            };

            pnl.Controls.Add(this.SaldoNumeric);
            pnl.Controls.Add(label);

            return pnl;
        }

        private Panel BuildTipoCb(Cuenta.TipoCuenta tipo)
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Tipo de cuenta" };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.TipoCb = new ComboBox {
                Dock = DockStyle.Right,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            string[] arrayTipos = { Cuenta.TipoCuenta.VIVIENDA.ToString(), Cuenta.TipoCuenta.AHORRO.ToString(), Cuenta.TipoCuenta.CORRIENTE.ToString() };
            this.TipoCb.Items.AddRange(arrayTipos);
            this.TipoCb.SelectedItem = tipo.ToString();

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.TipoCb);

            return pnl;
        }

        private Panel BuildFechaAperturaCalendar(DateTime fechaApertura)
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Fecha de apertura" };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.FechaAperturaDatePicker = new DateTimePicker {
                   Dock = DockStyle.Right,
                   Value = fechaApertura
            };

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.FechaAperturaDatePicker);

            return pnl;
        }

        private Panel BuildInteresMensualNumeric(int interes)
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            var labelLeft = new Label
            {
                Dock = DockStyle.Left,
                Text = "Interés mensual (%)"
            };

            this.InteresMensualNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right,
                Value = interes
            };

            pnl.Controls.Add(labelLeft);
            pnl.Controls.Add(this.InteresMensualNumeric);

            return pnl;
        }

        private Panel BuildRetiradasTable(List<Movimiento> retiradas)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "Retiradas" };
            var pnl = new Panel { Dock = DockStyle.Left, MaximumSize = new Size (Screen.PrimaryScreen.WorkingArea.Size.Width / 2, int.MaxValue) };
            this.RetiradasTable = new DataGridView
            { 
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                MinimumSize = new Size(400, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16)
            };

            pnl.MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Size.Width / 2, this.RetiradasTable.Height);
            this.RetiradasTable.Columns[0].Name = "Cliente";
            this.RetiradasTable.Columns[1].Name = "Fecha";
            this.RetiradasTable.Columns[2].Name = "Cantidad";

            foreach(Movimiento retirada in retiradas)
            {
                this.RetiradasTable.Rows.Add(
                    retirada.Cliente.Dni,
                    retirada.Fecha.Date,
                    string.Format("{0:#.00}", (double)retirada.Cantidad / 100) + "€"
                    ) ;
            }

            this.AddRetiradaButton = new Button
            {
                Dock = DockStyle.Top,
                Text = "Nueva Retirada"
            };

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.RetiradasTable);
            pnl.Controls.Add(this.AddRetiradaButton);

            return pnl;
        }

        private Panel BuildDepositosTable(List<Movimiento> depositos)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "Depósitos" };
            var pnl = new Panel { Dock = DockStyle.Right, MaximumSize = new Size (Screen.PrimaryScreen.WorkingArea.Size.Width / 2, int.MaxValue), MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Size.Width / 2, 0) };
            this.DepositosTable = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                MinimumSize = new Size(400, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
            };

            this.DepositosTable.Columns[0].Name = "Cliente";
            this.DepositosTable.Columns[0].ReadOnly = true;
            this.DepositosTable.Columns[1].Name = "Fecha";
            this.DepositosTable.Columns[2].Name = "Cantidad";

            foreach (Movimiento deposito in depositos)
            {
                this.DepositosTable.Rows.Add(
                    deposito.Cliente.Dni,
                    deposito.Fecha.Date,
                    string.Format("{0:#.00}", (double)deposito.Cantidad / 100) + "€"
                    );
            }

            this.AddDepositoButton = new Button
            {
                Dock = DockStyle.Top,
                Text = "Nuevo Depósito"
            };

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.DepositosTable);
            pnl.Controls.Add(this.AddDepositoButton);

            return pnl;
        }

        private Panel BuildTablaCuentas(List<Cuenta> cuentas)
        {
            var pnl = new Panel { Dock = DockStyle.Fill };
            this.TablaCuentas = new DataGridView()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                MinimumSize = new Size(2000, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
            };

            this.TablaCuentas.Columns[0].Name = "CCC";
            this.TablaCuentas.Columns[1].Name = "Tipo cuenta";
            this.TablaCuentas.Columns[2].Name = "Saldo";
            this.TablaCuentas.Columns[3].Name = "Fecha de apertura";
            this.TablaCuentas.Columns[4].Name = "Interés mensual";
            this.TablaCuentas.Columns[5].Name = "Titulares";

            foreach(Cuenta cuenta in cuentas)
            {
                string titulares = "";

                foreach(Cliente titular in cuenta.Titulares)
                {
                    titulares += titular.Nombre + ", ";
                }

                this.TablaCuentas.Rows.Add(
                        cuenta.CCC, 
                        cuenta.Tipo, 
                        string.Format("{0:#.00}", (double) cuenta.Saldo / 100) + "€", 
                        cuenta.FechaApertura.Day + "/" + cuenta.FechaApertura.Month + "/" + cuenta.FechaApertura.Year, 
                        cuenta.InteresMensual + "%", 
                        titulares
                    );

            }
            pnl.Controls.Add(this.TablaCuentas);
            return pnl;
        }

        public Panel BuildButtonDetalle()
        {
            var pnl = new Panel { Dock = DockStyle.Bottom };
            this.ButtonDetalle = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "Ver seleccionada"
            };

            pnl.Controls.Add(this.ButtonDetalle);

            return pnl;
        }

        public void ShowAddCuenta(GestorCuentas gestor)
        {
            //this.ShowDetalles(new Cuenta(gestor.Cuentas.Count));   
        }

        private Panel MainPanel;

        public DateTimePicker FechaAperturaDatePicker;

        public DataGridView TablaCuentas;

        public Button ButtonDetalle;

        public TextBox CCCTextBox;

        public NumericUpDown SaldoNumeric;

        public ComboBox TipoCb;

        public NumericUpDown InteresMensualNumeric;

        public DataGridView DepositosTable;

        public DataGridView RetiradasTable;

        public Button AddRetiradaButton;

        public Button ButtonVolver;

        public Button AddDepositoButton;

        public TextBox ClienteTextBox;

        public NumericUpDown CantidadNumeric;

        public DateTimePicker FechaMovimientoDate;

        public Button GuardarButton;

        public Button AddCuentaButton;

        public Button SaveCuentaButton;
    }
}
