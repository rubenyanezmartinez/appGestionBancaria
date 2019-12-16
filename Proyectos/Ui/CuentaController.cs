using System;
using App_Gestion_Bancaria.Core.Gestores;
using App_Gestion_Bancaria.Core.Clases;
using System.Windows.Forms;

namespace Proyectos.Ui
{
    public class CuentaController
    {
        public CuentaController()
        {
            this.Gestor = new GestorCuentas();
            this.View = new CuentaView(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void IniciarBotonesIndex()
        {
            this.View.ButtonDetalle.Click += new System.EventHandler(AccionButtonDetalle);
            this.View.AddCuentaButton.Click += new System.EventHandler(AddCuenta);
            this.View.BorrarCuentaButton.Click += new System.EventHandler(EliminarCuenta);
            this.View.VolverButton.Click += new System.EventHandler(Volver);
        }

        private void IniciarBotonesDetalles()
        {
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolver);
            this.View.AddRetiradaButton.Click += new System.EventHandler(AddRetirada);
            this.View.AddDepositoButton.Click += new System.EventHandler(AddDeposito);
            this.View.GuardarButton.Click += new System.EventHandler(CambiarCuenta);
        }

        private void IniciarBotonesAddCuenta()
        {
            this.View.GuardarButton.Click += new System.EventHandler(SaveCuenta);
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolver);
            this.View.AddRetiradaButton.Click += new System.EventHandler(AddRetirada);
            this.View.AddDepositoButton.Click += new System.EventHandler(AddDeposito);
        }

        private void AddCuenta(object sender, System.EventArgs e)
        {
            this.CuentaSeleccionada = new Cuenta(this.Gestor.GetNextCCC());
            this.View.ShowAddCuenta(this.CuentaSeleccionada);
            this.IniciarBotonesAddCuenta();
        }

        private void SaveCuenta(object sender, System.EventArgs e)
        {
            this.CuentaSeleccionada.Saldo = Decimal.ToInt32(this.View.SaldoNumeric.Value);
            this.CuentaSeleccionada.InteresMensual = Decimal.ToInt32(this.View.InteresMensualNumeric.Value);
            this.CuentaSeleccionada.Tipo = (Cuenta.TipoCuenta) Enum.Parse(typeof(Cuenta.TipoCuenta), this.View.TipoCb.SelectedItem.ToString());
            this.CuentaSeleccionada.FechaApertura = this.View.FechaAperturaDatePicker.Value;
            this.Gestor.Cuentas.Add(this.CuentaSeleccionada);
            this.Gestor.Guardar();
            this.View.ShowIndex(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void CambiarCuenta(object sender, System.EventArgs e)
        {
            this.CuentaSeleccionada.Saldo = Decimal.ToInt32(this.View.SaldoNumeric.Value);
            this.CuentaSeleccionada.InteresMensual = Decimal.ToInt32(this.View.InteresMensualNumeric.Value);
            this.CuentaSeleccionada.Tipo = (Cuenta.TipoCuenta)Enum.Parse(typeof(Cuenta.TipoCuenta), this.View.TipoCb.SelectedItem.ToString());
            this.CuentaSeleccionada.FechaApertura = this.View.FechaAperturaDatePicker.Value;

            this.Gestor.Guardar();
            
            //this.View.ShowIndex(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void Volver(object sender, System.EventArgs e)
        {
            Console.WriteLine("VOLVER");
            this.View.Close();
        }

        private void AccionButtonVolver(object sender, System.EventArgs e)
        {
            Console.WriteLine("VOLVER");
            this.View.ShowIndex(this.Gestor);
            this.IniciarBotonesIndex();
        }

        private void AddRetirada(object sender, System.EventArgs e)
        {
            this.View.ShowAddMovimiento(true);
            this.View.AddRetiradaButton.Click += new System.EventHandler(NewRetirada);
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolverMovimiento);
        }

        private void NewRetirada(object sender, System.EventArgs e)
        {
            this.CuentaSeleccionada.Retiradas.Add(new Movimiento(Decimal.ToInt32(this.View.CantidadNumeric.Value), new Cliente("", this.View.ClienteTextBox.Text, "", "", ""), this.View.FechaMovimientoDate.Value));
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.View.AddRetiradaButton.Click -= new System.EventHandler(NewRetirada);
            this.IniciarBotonesDetalles();
        }

        private void AddDeposito(object sender, System.EventArgs e)
        {
            this.View.ShowAddMovimiento(false);
            this.View.AddDepositoButton.Click -= new System.EventHandler(AddDeposito);
            this.View.AddDepositoButton.Click += new System.EventHandler(NewDeposito);
            this.View.ButtonVolver.Click += new System.EventHandler(AccionButtonVolverMovimiento);
        }

        private void NewDeposito(object sender, EventArgs e)
        {
            this.CuentaSeleccionada.Depositos.Add(new Movimiento(Decimal.ToInt32(View.CantidadNumeric.Value), new Cliente("", this.View.ClienteTextBox.Text, "", "", ""), this.View.FechaMovimientoDate.Value));
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.View.AddDepositoButton.Click -= new System.EventHandler(NewDeposito);
            this.IniciarBotonesDetalles();
        }

        private void AccionButtonVolverMovimiento(object sender, EventArgs e)
        {
            this.View.ShowDetalles(this.CuentaSeleccionada);
            this.IniciarBotonesDetalles();
        }

        private void AccionButtonDetalle(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaCuentas.SelectedRows;
            if(filasSeleccionadas.Count != 1)
            {
                MessageBox.Show("Selecciona una única cuenta", "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                this.CuentaSeleccionada = Gestor.GetCuentaByCCC(this.View.TablaCuentas.SelectedRows[0].Cells[0].Value.ToString());
                this.View.ShowDetalles(this.CuentaSeleccionada);
                this.IniciarBotonesDetalles();
            }
        }

        private void EliminarCuenta(object sender, System.EventArgs e)
        {
            DataGridViewSelectedRowCollection filasSeleccionadas = this.View.TablaCuentas.SelectedRows;
            if (filasSeleccionadas.Count != 1)
            {
                MessageBox.Show("Selecciona una única cuenta", "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.CuentaSeleccionada = Gestor.GetCuentaByCCC(this.View.TablaCuentas.SelectedRows[0].Cells[0].Value.ToString());
                this.Gestor.Cuentas.Remove(this.CuentaSeleccionada);
                this.View.ShowIndex(this.Gestor);
                this.Gestor.Guardar();
                this.IniciarBotonesIndex();
            }
        }

        public CuentaView View { get; set; }

        public GestorCuentas Gestor { get; set; }

        public Cuenta CuentaSeleccionada { get; set; }

    }

}
