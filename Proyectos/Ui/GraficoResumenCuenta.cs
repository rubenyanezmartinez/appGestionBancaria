using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Graficos.Core;


namespace Graficos.UI
{
    public class GraficoResumenCuenta : Chart
    {
        public GraficoResumenCuenta(Size s, Cuenta c, List<Transferencia> transferencias, int year) : base(s.Width,s.Height)
        {

            this.Init();
            this.LegendX = year.ToString();
            this.ShowType = SHOW.MONTHS;
            Dictionary<string, int[]> dataToRepresent = FetchNumByMonth(c, transferencias, year);

            this.LegendValues = new Dictionary<string, IEnumerable<int>>()
            {
                {"Depositos", dataToRepresent["DEPOSITOS"] },
                {"Retiradas", dataToRepresent["RETIRADAS"] },
                {"Transferencias", dataToRepresent["TRANSFERENCIAS"] }
            };

            this.LegendValuesX = new List<string>()
            {
                "Ene", "Feb","Mar","Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"
            };
            
            this.Draw();
        }

        public GraficoResumenCuenta(Size s, Cuenta c, List<Transferencia> transferencias) : base(s.Width, s.Height)
        {
            this.Init();
            this.LegendX = "Años";
            this.ShowType = SHOW.YEARS;

            var dataToRepresent = FetchNumYears(c, transferencias);

            this.LegendValues = new Dictionary<string, IEnumerable<int>>()
            {
                {"Depositos", dataToRepresent["DEPOSITOS"] },
                {"Retiradas", dataToRepresent["RETIRADAS"] },
                {"Transferencias", dataToRepresent["TRANSFERENCIAS"] }
            };

            //Add years to legend X
            for(int i = c.FechaApertura.Year; i <= DateTime.Today.Year; i++ )
            {
                this.LegendValuesX.Add(i.ToString().Substring(2));
            }
            this.Draw();
        }

        public void Init()
        {
            this.Dock = DockStyle.Fill;
            this.Type = ChartType.Guided;
            this.LegendY = "Numero movimientos";
        }

        public Dictionary<string, int[]> FetchNumByMonth(Cuenta c, List<Transferencia> transferencias ,int year)
        {
            Dictionary<string, int[]> toret = new Dictionary<string, int[]>();

            var depositosByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var retiradasByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var transferenciasByMonth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            c.Depositos.ForEach((dep) => { if (dep.Fecha.Year == year) depositosByMonth[dep.Fecha.Month - 1]++;  });
            c.Retiradas.ForEach((ret) => { if (ret.Fecha.Year == year) retiradasByMonth[ret.Fecha.Month - 1]++;  });
            transferencias.ForEach((t) => { if (t.Fecha.Year == year) transferenciasByMonth[t.Fecha.Month - 1]++; });

            toret.Add("DEPOSITOS", depositosByMonth);
            toret.Add("RETIRADAS", retiradasByMonth);
            toret.Add("TRANSFERENCIAS", transferenciasByMonth);

            return toret;
        }

        public Dictionary<string, int[]> FetchNumYears(Cuenta c, List<Transferencia> t)
        {
            Dictionary<string, int[]> toret = new Dictionary<string, int[]>();
            var depositosByYear = new int[DateTime.Now.Year - c.FechaApertura.Year +1];
            var retiradasByYear = new int[DateTime.Now.Year - c.FechaApertura.Year +1];
            var transferenciasByYear = new int[DateTime.Now.Year - c.FechaApertura.Year +1];

            c.Depositos.ForEach((dep) => depositosByYear[dep.Fecha.Year - c.FechaApertura.Year]++);
            c.Retiradas.ForEach((ret) => retiradasByYear[ret.Fecha.Year - c.FechaApertura.Year]++);
            t.ForEach((t) => transferenciasByYear[t.Fecha.Year - c.FechaApertura.Year]++);

            toret.Add("DEPOSITOS", depositosByYear);
            toret.Add("RETIRADAS", retiradasByYear);
            toret.Add("TRANSFERENCIAS", transferenciasByYear);

            return toret;
        }

        public SHOW ShowType
        {
            get;set;
        }
        
    }
}
