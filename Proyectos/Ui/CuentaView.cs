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
            this.WindowState = FormWindowState.Maximized;
            this.Size = new Size(650, 700);
            this.ShowIndex(gestor);            
        }

        public void ShowIndex(GestorCuentas gestor)
        {
            this.Controls.Clear();
            this.Text = "Gestión de Cuentas";
            this.MainPanel = new Panel { Dock = DockStyle.Fill };
            this.MainPanel.Controls.Add(this.Titulo);
            this.MainPanel.Controls.Add(this.BuildTablaCuentas(gestor.Cuentas));
           

            var buttonPanel = new Panel { Dock = DockStyle.Bottom , Font = new Font("Arial", 12, FontStyle.Regular), };

            buttonPanel.Controls.Add(this.BuildButtonDetalle());
            //buttonPanel.Controls.Add(this.BuildAddCuentaButton());
            buttonPanel.Controls.Add(this.BuildVerIngresosButton());
            this.MainPanel.Controls.Add(buttonPanel);
            

            //this.MainPanel.Controls.Add(this.BuildBorrarCuentaButton());


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


            //this.MainPanel.Controls.Add(this.MostrarGraficoButton);

            var buttonPanel = new Panel { Dock = DockStyle.Bottom };
            buttonPanel.Controls.Add(BuildButtonVolverVerEstadisticas());
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
                
                pnl.Controls.Add(this.AddRetiradaButton);
            } else
            {
                
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
            var label = new Label { Dock = DockStyle.Left, Text = "DNI del titular", Font = new Font("Arial", 12, FontStyle.Regular) };
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
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
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
                Location = new Point(400, 0),
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Borrar cuenta",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            this.VolverButton = new Button
            {
                Location = new Point(936, 0),
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.Controls.Add(this.BorrarCuentaButton);
            pnl.Controls.Add(this.VolverButton);

            return pnl;
        }

        private Panel BuildAddCuentaButton()
        {
            var pnl = new Panel { Dock = DockStyle.Left, Size = new Size(600, 50) };
            this.AddCuentaButton = new Button
            {
                Location = new Point(pnl.Width - 200, 00),
                Size = new Size(200, 30),
                FlatStyle = FlatStyle.Flat,
                Text = "Nueva Cuenta",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.Controls.Add(this.AddCuentaButton);

            return pnl;
        }

        private Panel BuildGuardarButton()
        {
            var pnl = new Panel { Dock = DockStyle.Right };
            this.GuardarButton = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Guardar",
                Font = new Font("Arial", 12, FontStyle.Regular),
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
                Text = "Cantidad (céntimos)",
                Width=300,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            this.CantidadNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right,
                Maximum = int.MaxValue,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            pnl.Controls.Add(labelLeft);
            pnl.Controls.Add(this.CantidadNumeric);
            pnl.MaximumSize = new Size(int.MaxValue, this.CantidadNumeric.Height * 2);

            return pnl;
        }

        private Panel BuildFechaMovimientoDate()
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Fecha", Font = new Font("Arial", 12, FontStyle.Regular) };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.FechaMovimientoDate = new DateTimePicker
            {
                Dock = DockStyle.Right,
                Font = new Font("Arial", 12, FontStyle.Regular),
                Width=300,
            };

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.FechaMovimientoDate);
            pnl.MaximumSize = new Size(int.MaxValue, this.FechaMovimientoDate.Height * 2);

            return pnl;
        }

        private Panel BuildButtonVolverVerEstadisticas()
        {
            var pnl = new Panel { Dock = DockStyle.Fill};
            var pnl1 = new Panel { Dock = DockStyle.Left, Size = new Size(500, 200)};
            var pnl2 = new Panel { Dock = DockStyle.Right,Size=new Size(650,200) };
            this.ButtonVolver = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            //Boton para mostrar el grafico
            this.MostrarGraficoButton = new Button
            {
                Width = 300,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Ver estadisticas de cuenta",
                Font = new Font("Arial", 12, FontStyle.Regular),

            };
            pnl1.Controls.Add(this.MostrarGraficoButton);
            pnl2.Controls.Add(this.ButtonVolver);
            pnl.Controls.Add(pnl1);
            pnl.Controls.Add(pnl2);



            return pnl;
        }

        private Panel BuildButtonVolver()
        {
            var pnl = new Panel { Dock = DockStyle.Fill };

            this.ButtonVolver = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular)
            };


            pnl.Controls.Add(this.ButtonVolver);

            return pnl;
        }

        private TextBox BuildTextBox(string value)
        {
            var toret = new TextBox
            {
                Dock = DockStyle.Right,
                Text = value,
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            return toret;
        }

        private Panel BuildCCCTextBox(string CCC)
        {
            var pnl = new Panel { Dock = DockStyle.Top };
            var label = new Label { Dock = DockStyle.Left, Text = "CCC", Font = new Font("Arial", 12, FontStyle.Regular)};

            this.CCCTextBox = BuildTextBox(CCC);
            this.CCCTextBox.ReadOnly = true;

            pnl.Controls.Add(label);
            pnl.Controls.Add(this.CCCTextBox);
            pnl.MaximumSize = new Size(int.MaxValue, this.CCCTextBox.Height * 2);

            return pnl;
        }
        private Panel BuildSaldoTextBox(int saldo)
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Saldo (en céntimos)" , Font = new Font("Arial", 12, FontStyle.Regular),Width=200 };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.SaldoNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right,
                Maximum = int.MaxValue,
                Value = saldo,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            pnl.Controls.Add(this.SaldoNumeric);
            pnl.Controls.Add(label);
            pnl.MaximumSize = new Size(int.MaxValue, this.SaldoNumeric.Height * 2);

            return pnl;
        }
        Label Titulo = new Label() { Text = "GESTIÓN DE CUENTAS", Size = new System.Drawing.Size(1000, 100), Font = new Font("Arial", 35, FontStyle.Regular) };
       
        private Panel BuildTipoCb(Cuenta.TipoCuenta tipo)
        {
            var label = new Label { Dock = DockStyle.Left, Text = "Tipo de cuenta", Font = new Font("Arial", 12, FontStyle.Regular), Width=200 };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.TipoCb = new ComboBox {
                Dock = DockStyle.Right,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 12, FontStyle.Regular)
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
            var label = new Label { Dock = DockStyle.Left, Text = "Fecha de apertura", Font = new Font("Arial", 12, FontStyle.Regular), Width=200 };
            var pnl = new Panel { Dock = DockStyle.Top };

            this.FechaAperturaDatePicker = new DateTimePicker {
                   Dock = DockStyle.Right,
                   Value = fechaApertura,
                   Font = new Font("Arial", 12, FontStyle.Regular),
                   Width=300,
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
                Text = "Interés mensual (%)",
                Font = new Font("Arial", 12, FontStyle.Regular),
                Width=200,
            };

            this.InteresMensualNumeric = new NumericUpDown
            {
                Dock = DockStyle.Right,
                Value = interes,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            pnl.Controls.Add(labelLeft);
            pnl.Controls.Add(this.InteresMensualNumeric);
            pnl.MaximumSize = new Size(int.MaxValue, this.InteresMensualNumeric.Height * 2);


            return pnl;
        }

        private Panel BuildRetiradasTable(List<Movimiento> retiradas)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "Retiradas" , Font = new Font("Arial", 12, FontStyle.Regular), Width=200 };
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
                //Location = new Point(25, 16),
                Height = 100,
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
                    "-"+string.Format("{0:#.00}", (double)retirada.Cantidad / 100) + "€"
                    ) ;
            }

            this.AddRetiradaButton = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Dock = DockStyle.Top,
                Text = "Nueva Retirada",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.MinimumSize = new Size(this.RetiradasTable.Height, 0);

            //pnl.Controls.Add(label);
            pnl.Controls.Add(this.RetiradasTable);
            pnl.Controls.Add(this.AddRetiradaButton);

            return pnl;
        }

        private Panel BuildDepositosTable(List<Movimiento> depositos)
        {
            var label = new Label { Dock = DockStyle.Top, Text = "   Depósitos", Font = new Font("Arial", 12, FontStyle.Regular), Width=200 };
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
                //Location = new Point(25, 16),
                Height = 100,
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
                    "+"+string.Format("{0:#.00}", (double)deposito.Cantidad / 100) + "€"
                    );
            }

            this.AddDepositoButton = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Dock = DockStyle.Top,
                Text = "Nuevo Depósito",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.MinimumSize = new Size(this.Size.Width / 2, this.DepositosTable.Height);
           // pnl.Controls.Add(label);
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
                //Location = new Point(25, 16),
                Height = 100,
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Dock = DockStyle.Top,
                Text = "Añadir titular",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            this.RemoveTitularButton = new Button
            {
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Dock = DockStyle.Bottom,
                Text = "Eliminar titular",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.Controls.Add(this.AddTitularButton);
            pnl.Controls.Add(this.TitularesTable);
            pnl.Controls.Add(this.RemoveTitularButton);

            return pnl;
        }

        private Panel BuildTablaCuentas(List<Cuenta> cuentas)
        {
            var pnl = new Panel { Dock = DockStyle.Fill,Location=new Point(0,100) };
            this.TablaCuentas = new DataGridView()
            {
                Margin= new Padding(20,20,20,20),
                //Dock = DockStyle.Fill,
                ColumnCount = 6,
                MinimumSize = new Size(2000, 1000),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(0, 100),
                Font = new Font("Arial", 12, FontStyle.Regular),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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

            var pnl = new Panel { Dock = DockStyle.Left,Size=new Size(600,300) };
            this.AddCuentaButton = new Button
            {
                Location = new Point(10, 45),
                Size = new Size(200, 30),
                FlatStyle = FlatStyle.Flat,
                Text = "Nueva Cuenta",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            this.ButtonDetalle = new Button
            {
                Location= new Point(10,10),
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Ver seleccionada",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            this.BorrarCuentaButton = new Button
            {
                Location = new Point(215, 10),
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Borrar cuenta",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            this.VolverButton = new Button
            {
                Location = new Point(215, 45),
                Width = 200,
                FlatStyle = FlatStyle.Flat,
                Height = 30,
                Text = "Volver",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            pnl.Controls.Add(this.ButtonDetalle);
            pnl.Controls.Add(this.AddCuentaButton);
            pnl.Controls.Add(this.BorrarCuentaButton);
            pnl.Controls.Add(this.VolverButton);




            return pnl;
        }

        public void ShowAddCuenta(Cuenta cuenta)
        {
            this.ShowDetalles(cuenta);
            this.Text = "Nueva Cuenta";
            this.GuardarButton.Text = "Guardar";
            this.GuardarButton.Font = new Font("Arial", 12, FontStyle.Regular);
        }
        private Panel BuildVerIngresosButton()
        {
            var pnl = new Panel { Dock = DockStyle.Left };
            this.VerIngresosButton = new Button
            {
                Location = new Point(0,45),
                Size = new Size(200, 30),
                FlatStyle = FlatStyle.Flat,
                Text = "Ver Ingresos",
                Font = new Font("Arial", 12, FontStyle.Regular),
            };

            pnl.Controls.Add(this.VerIngresosButton);

            return pnl;
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

        public Button VolverButton;

        public DataGridView TitularesTable;

        public Button AddTitularButton;

        public Button RemoveTitularButton;

        public TextBox DniTextBox;

        public Button ConfirmarTitularButton;

        public Button MostrarGraficoButton { get; set; }
        public Button VerIngresosButton { get;  set; }
    }
}
