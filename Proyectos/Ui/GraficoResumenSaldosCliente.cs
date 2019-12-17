using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using App_Gestion_Bancaria.Core.Clases;

namespace Proyectos.Ui
{
    public enum SHOW
    {

        YEARS, MONTHS

    }
    public class GraficoResumenSaldosCliente : Chart
    {
        public SHOW ShowType { get; private set; }

        //Las cuentas tienen que ser las del cliente c
        public GraficoResumenSaldosCliente(Size graphicSize, Cliente client, List<Cuenta> allCuentas, List<Transferencia> allTransferencias, int yearToRepresent) : base(graphicSize.Width, graphicSize.Height)
        {
            this.Init();
            this.LegendX = yearToRepresent.ToString();
            this.ShowType = SHOW.MONTHS;

            var data = FetchImportClientByMonth(client, allTransferencias, allCuentas , yearToRepresent);

            this.LegendValues = new Dictionary<string, IEnumerable<int>>()
            {
                {"Depositos", data["DEPOSITOS"] },
                {"Retiradas", data["RETIRADAS"] },
                {"Transferencias", data["TRANSFERENCIAS"] }
            };

            this.LegendValuesX = new List<string>()
            {
                "Ene", "Feb","Mar","Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
            };

            this.Draw();
        }

        public GraficoResumenSaldosCliente(Size graphicSize, Cliente client, List<Cuenta> allCuentas, List<Transferencia> allTransferencias) : base(graphicSize.Width, graphicSize.Height)
        {
            this.Init();
            this.LegendX = "Años";
            this.ShowType = SHOW.YEARS;

            var data = FetchImportClientYears(client, allTransferencias, allCuentas);

            this.LegendValues = new Dictionary<string, IEnumerable<int>>()
            {
                {"Depositos", data["DEPOSITOS"] },
                {"Retiradas", data["RETIRADAS"] },
                {"Transferencias", data["TRANSFERENCIAS"] }
            };

            var cuentasCliente = allCuentas.FindAll((cuenta) => ContainsCliente(cuenta, client));

            int minFechaAperturaCuentas = this.GetMinFechaAperturaCuentas(cuentasCliente);

            for (int i = minFechaAperturaCuentas; i <= DateTime.Today.Year; i++)
            {
                this.LegendValuesX.Add(i.ToString().Substring(2));
            }
            this.Draw();
        }


        public void Init()
        {
            this.Dock = DockStyle.Fill;
            this.Type = ChartType.Guided;
            this.LegendY = "Importes en €";
        }

        
        public Dictionary<string, int[]> FetchImportClientByMonth(Cliente cliente, List<Transferencia> transferencias, List<Cuenta> cuentas, int year)
        {
            var depositosByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var retiradasByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var transferenciasHechasByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var cuentasCliente = cuentas.FindAll((cuenta) => ContainsCliente(cuenta, cliente));

            foreach(Cuenta c in cuentasCliente)
            {
                
                c.Depositos.FindAll((dep) => dep.Cliente.Dni == cliente.Dni && dep.Fecha.Year == year).ForEach((dep) => depositosByMonth[dep.Fecha.Month - 1] += dep.Cantidad);
                c.Retiradas.FindAll((ret) => ret.Cliente.Dni == cliente.Dni && ret.Fecha.Year == year).ForEach((ret) => retiradasByMonth[ret.Fecha.Month - 1] += ret.Cantidad);
                transferencias.FindAll((trn) => CuentasIguales(trn.CCCOrigen, c) && trn.Fecha.Year == year).ForEach((trn) => transferenciasHechasByMonth[trn.Fecha.Month - 1] += (int)trn.Importe);
            }

            return new Dictionary<string, int[]>()
            {
                {"DEPOSITOS", depositosByMonth },
                {"RETIRADAS", retiradasByMonth },
                {"TRANSFERENCIAS", transferenciasHechasByMonth }
            };
        }
        bool CuentasIguales(Cuenta c1, Cuenta c2)
        {
            return c1.CCC == c2.CCC;
        }

        bool ContainsCliente(Cuenta c, Cliente cli)
        {
            foreach(Cliente p in c.Titulares)
            {
                if(p.Dni == cli.Dni)
                    return true;
            }
            return false;
        }

        void InitializeArray(ref int[] array)
        {
            for(int i = 0; i< array.Length; i++)
            {
                array[i] = 0;
            }
        }

        public Dictionary<string, int[]> FetchImportClientYears(Cliente cliente, List<Transferencia> transferencias, List<Cuenta> cuentas)
        {
            Dictionary<string, int[]> toret = new Dictionary<string, int[]>();

            var cuentasCliente = cuentas.FindAll((cuenta) => ContainsCliente(cuenta, cliente));

            int minFechaAperturaCuentas = this.GetMinFechaAperturaCuentas(cuentasCliente);
         

            var depositosByYear = new int[DateTime.Now.Year - minFechaAperturaCuentas + 1];
            var retiradasByYear = new int[DateTime.Now.Year - minFechaAperturaCuentas + 1];
            var transferenciasHechasByYear = new int[DateTime.Now.Year - minFechaAperturaCuentas + 1];
            InitializeArray(ref depositosByYear);
            InitializeArray(ref retiradasByYear);
            InitializeArray(ref transferenciasHechasByYear);

            foreach (Cuenta c in cuentasCliente)
            {
              
                
                c.Depositos.ForEach((dep) => depositosByYear[dep.Fecha.Year - minFechaAperturaCuentas] += dep.Cantidad);
                c.Retiradas.ForEach((ret) => retiradasByYear[ret.Fecha.Year - minFechaAperturaCuentas] += ret.Cantidad);
                transferencias.FindAll((t) => t.CCCOrigen.Equals(c)).ForEach((t) => transferenciasHechasByYear[t.Fecha.Year - minFechaAperturaCuentas] += (int)t.Importe);
                
            }


            

            toret.Add("DEPOSITOS", depositosByYear);
            toret.Add("RETIRADAS", retiradasByYear);
            toret.Add("TRANSFERENCIAS", transferenciasHechasByYear);

            return toret;
        }

        public int GetMinFechaAperturaCuentas(List<Cuenta> cuentas)
        {
            var fechasCuentas = new List<int>();

            cuentas.ForEach((c) => { if (!fechasCuentas.Contains(c.FechaApertura.Date.Year)) fechasCuentas.Add(c.FechaApertura.Date.Year); });
            
            return fechasCuentas.Count == 0 ? DateTime.Today.Year : fechasCuentas.Min();
        }

    }
}
