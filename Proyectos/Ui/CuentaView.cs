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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new Size(650, 700);
            this.ShowIndex(gestor);            
        }

        public void ShowIndex(GestorCuentas gestor)
        {
            this.Controls.Clear();
            this.Text = "Gestión de Cuentas";
            this.MainPanel = new Panel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(this.BuildTablaCuentas(gestor.Cuentas));

            var buttonPanel = new Panel { Dock = DockStyle.Bottom };

            buttonPanel.Controls.Add(this.BuildButtonDetalle());
            buttonPanel.Controls.Add(this.BuildAddCuentaButton());

            this.MainPanel.Controls.Add(buttonPanel);

            this.MainPanel.Controls.Add(this.BuildBorrarCuentaButton());


            this.Controls.Add(MainPanel);
        }

        public void ShowDetalles(Cuenta cuenta)
        {
            this.Controls.Clear();
            this.MainPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            this.Text = "Detalles de la cuenta: " + cuenta.CCC;
            this.MainPanel.Controls.Add(BuildCCCTextBox(cuenta.CCC));
            this.MainPanel.Controls.Add(BuildSaldoTextBox(cuenta.Saldo));
            this.MainPanel.Controls.Add(BuildTipoCb(cuenta.Tipo));
            this.MainPanel.Controls.Add(BuildFechaAperturaCalendar(cuenta.FechaApertura));
            this.MainPanel.Controls.Add(BuildInteresMensualNumeric(cuenta.InteresMensual));
            //this.MainPanel.Controls.Add(BuildTitularesTable(cuenta.Titulares));

            var movimientosPanel = new Panel { Dock = DockStyle.Top };

            movimientosPanel.Controls.Add(BuildRetiradasTable(cuenta.Retiradas));
            movimientosPanel.Controls.Add(BuildDepositosTable(cuenta.Depositos));

            movimientosPanel.Height = 150;

            this.MainPanel.Controls.Add(movimientosPanel);

            this.MainPanel.Controls.Add(BuildTitularesTable(cuenta.Titulares));

            //Boton para mostrar el grafico
            this.MostrarGraficoButton = new Button
            {
                Text = "Ver estadisticas de cuenta",
                Dock = DockStyle.Fill
            };
            this.MainPanel.Controls.Add(this.MostrarGraficoButton);

            var buttonPanel = new Panel { Dock = DockStyle.Bottom };
            buttonPanel.Controls.Add(BuildButtonVolver());
            buttonPanel.Controls.Add(BuildGuardarButton());

            this.MainPanel.Controls.Add(buttonPanel);

            this.Controls.Add(this.MainPanel);

        }

        public void ShowAddMovimiento(Boolean isRetirada)
        {
            this.Controls.Clear();
            this.Text = "Nuevo Movimiento: ";
            _ = isRetirada ? this.Text += "RETIRADA" : this.Text += "DEPÓSITO";
            this.MainPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(BuildDniTextBox());
            this.MainPanel.Controls.Add(BuildCantidadNumeric());
            this.MainPanel.Controls.Add(BuildFechaMovimientoDate());

            var buttonPanel = new Panel { Dock = DockStyle.Bottom };
            var pnl = new Panel { Dock = DockStyle.Right };


            if (isRetirada)
            {
                this.AddRetiradaButton.Dock = DockStyle.Fill;
                pnl.Controls.Add(this.AddRetiradaButton);
            } else
            {
                this.AddDepositoButton.Dock = DockStyle.Fill;
                pnl.Controls.Add(this.AddDepositoButton);
            }

            buttonPanel.Controls.Add(pnl);
            buttonPanel.Controls.Add(BuildButtonVolver());

            this.MainPanel.Controls.Add(buttonPanel);

            this.Controls.Add(this.MainPanel);
        }

        public void ShowAddTitular()
        {
            this.Controls.Clear();
            this.Text = "Añadir titular";
            this.MainPanel = new TableLayoutPanel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(this.BuildDniTextBox());

            Panel buttonPanel = new Panel { Dock = DockStyle.Bottom };
            buttonPanel.Controls.Add(this.BuildConfirmarTitularButton());
            buttonPanel.Controls.Add(this.BuildButtonVolver());

            this.MainPanel.Controls.Add(buttonPanel);

            this.Controls.Add(this.MainPanel);
        }

        private Panel BuildDniTextBox()
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            var label = new Label { Dock = DockStyle.Left, Text = "DNI del titular" };
            this.DniTextBox = this.BuildTextBox("");

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.DniTextBox);
            pnl.MaximumSize = new Size(int.MaxValue, this.DniTextBox.Height * 2);

            return pnl;
        }

        private Panel BuildConfirmarTitularButton()
        {
            var pnl = new Panel { Dock = DockStyle.Right };
            this.ConfirmarTitularButton = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Añadir Titular"
            };

            pnl.Controls.Add(this.ConfirmarTitularButton);

            return pnl;
        }

        private Panel BuildBorrarCuentaButton()
        {
            var pnl = new Panel { Dock = DockStyle.Bottom };
            this.BorrarCuentaButton = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Borrar cuenta"
            };

            pnl.Controls.Add(this.BorrarCuentaButton);

            return pnl;
        }

        private Panel BuildAddCuentaButton()
        {
            var pnl = new Panel { Dock = DockStyle.Left };
            this.AddCuentaButton = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Nueva Cuenta"
            };

            pnl.Controls.Add(this.AddCuentaButton);

            return pnl;
        }

        private Panel BuildGuardarButton()
        {
            var pnl = new Panel { Dock = DockStyle.Right };
            this.GuardarButton = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Guardar"
            };

            pnl.Controls.Add(this.GuardarButton);

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
                Dock = DockStyle.Right,
                Maximum = int.MaxValue
            };

            pnl.Controls.Add(labelLeft);
            pnl.Controls.Add(this.CantidadNumeric);
            pnl.MaximumSize = new Size(int.MaxValue, this.CantidadNumeric.Height * 2);

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
            pnl.MaximumSize = new Size(int.MaxValue, this.FechaMovimientoDate.Height * 2);

            return pnl;
        }

        private Panel BuildButtonVolver()
        {
            var pnl = new Panel { Dock = DockStyle.Left };
            this.ButtonVolver = new Button
            {
                Dock = DockStyle.Fill,
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
            var label = new Label { Dock = DockStyle.Left, Text = "CCC"};

            this.CCCTextBox = BuildTextBox(CCC);
            this.CCCTextBox.ReadOnly = true;

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
                Maximum = int.MaxValue,
                Value = saldo
            };

            pnl.Controls.Add(this.SaldoNumeric);
            pnl.Controls.Add(label);
            pnl.MaximumSize = new Size(int.MaxValue, this.SaldoNumeric.Height * 2);

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
            pnl.MaximumSize = new Size(int.MaxValue, this.TipoCb.Height * 2);


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
            pnl.MaximumSize = new Size(int.MaxValue, this.FechaAperturaDatePicker.Height * 2);


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
            pnl.MaximumSize = new Size(int.MaxValue, this.InteresMensualNumeric.Height * 2);


            return pnl;
        }

        private Panel BuildRetiradasTable(List<Movimiento> retiradas)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "Retiradas" };
            var pnl = new Panel { Dock = DockStyle.Left, MaximumSize = new Size (this.ClientSize.Width / 2, int.MaxValue) };
            this.RetiradasTable = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
                Height = 100
            };

            pnl.MinimumSize = new Size(this.Size.Width / 2, this.RetiradasTable.Height);
            this.RetiradasTable.Columns[0].Name = "Cliente";
            this.RetiradasTable.Columns[1].Name = "Fecha";
            this.RetiradasTable.Columns[2].Name = "Cantidad";

            foreach(Movimiento retirada in retiradas)
            {
                this.RetiradasTable.Rows.Add(
                    retirada.Cliente.Nombre,
                    retirada.Fecha.Day + "/" + retirada.Fecha.Month + "/" + retirada.Fecha.Year,
                    string.Format("{0:#.00}", (double)retirada.Cantidad / 100) + "€"
                    ) ;
            }

            this.AddRetiradaButton = new Button
            {
                Dock = DockStyle.Top,
                Text = "Nueva Retirada"
            };

            pnl.MinimumSize = new Size(this.RetiradasTable.Height, 0);

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.RetiradasTable);
            pnl.Controls.Add(this.AddRetiradaButton);

            return pnl;
        }

        private Panel BuildDepositosTable(List<Movimiento> depositos)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "Depósitos" };
            var pnl = new Panel { Dock = DockStyle.Right, MaximumSize = new Size (this.ClientSize.Width / 2, int.MaxValue), MinimumSize = new Size(Screen.PrimaryScreen.WorkingArea.Size.Width / 2, 0) };
            this.DepositosTable = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
                Height = 100
            };

            this.DepositosTable.Columns[0].Name = "Cliente";
            this.DepositosTable.Columns[0].ReadOnly = true;
            this.DepositosTable.Columns[1].Name = "Fecha";
            this.DepositosTable.Columns[2].Name = "Cantidad";

            foreach (Movimiento deposito in depositos)
            {
                this.DepositosTable.Rows.Add(
                    deposito.Cliente.Nombre,
                    deposito.Fecha.Day + "/" + deposito.Fecha.Month + "/" + deposito.Fecha.Year,
                    string.Format("{0:#.00}", (double)deposito.Cantidad / 100) + "€"
                    );
            }

            this.AddDepositoButton = new Button
            {
                Dock = DockStyle.Top,
                Text = "Nuevo Depósito"
            };

            pnl.MinimumSize = new Size(this.Size.Width / 2, this.DepositosTable.Height);
            pnl.Controls.Add(label);
            pnl.Controls.Add(this.DepositosTable);
            pnl.Controls.Add(this.AddDepositoButton);

            return pnl;
        }

        private Panel BuildTitularesTable(List<Cliente> titulares)
        {
            var pnl = new WFrms.Panel { Dock = DockStyle.Top, Height = 150 };

            this.TitularesTable = new DataGridView
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(25, 16),
                Height = 100
            };

            this.TitularesTable.Columns[0].Name = "DNI";
            this.TitularesTable.Columns[1].Name = "Nombre";
            this.TitularesTable.Columns[2].Name = "Email";

            foreach(Cliente titular in titulares)
            {
                this.TitularesTable.Rows.Add(titular.Dni, titular.Nombre, titular.Email);
            }

            this.AddTitularButton = new Button
            {
                Dock = DockStyle.Top,
                Text = "Añadir titular"
            };

            this.RemoveTitularButton = new Button
            {
                Dock = DockStyle.Bottom,
                Text = "Eliminar titular"
            };

            pnl.Controls.Add(this.AddTitularButton);
            pnl.Controls.Add(this.TitularesTable);
            pnl.Controls.Add(this.RemoveTitularButton);

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
            var pnl = new Panel { Dock = DockStyle.Right };
            this.ButtonDetalle = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Ver seleccionada"
            };

            pnl.Controls.Add(this.ButtonDetalle);

            return pnl;
        }

        public void ShowAddCuenta(Cuenta cuenta)
        {
            this.ShowDetalles(cuenta);
            this.Text = "Nueva Cuenta";
            this.GuardarButton.Text = "Guardar";
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

        public NumericUpDown CantidadNumeric;

        public DateTimePicker FechaMovimientoDate;

        public Button GuardarButton;

        public Button AddCuentaButton;

        public Button SaveCuentaButton;

        public Button BorrarCuentaButton;

        public DataGridView TitularesTable;

        public Button AddTitularButton;

        public Button RemoveTitularButton;

        public TextBox DniTextBox;

        public Button ConfirmarTitularButton;

        public Button MostrarGraficoButton { get; set; }
    }
}
