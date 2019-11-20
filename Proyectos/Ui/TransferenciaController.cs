using App_Gestion_Bancaria.Core.Gestores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyectos.Ui
{
    using WFrms = System.Windows.Forms;
    public class TransferenciaController
    {
        public TransferenciaView View { get; private set; }
        public GestorTransferencias Gestor { get; private set; }
        public TransferenciaController()
        {
            this.Gestor = new GestorTransferencias();
            this.View = new TransferenciaView(this.Gestor.Transferencias);
        }
    }
}
