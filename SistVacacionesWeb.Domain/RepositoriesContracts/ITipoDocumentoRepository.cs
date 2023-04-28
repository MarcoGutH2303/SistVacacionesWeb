using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface ITipoDocumentoRepository
    {
        List<TipoDocumentoModel> ListarTipoDocumento(string codEmpresa);
        int GrabarTipoDocumento(TipoDocumentoModel oTipoDocumentoModel);
        TipoDocumentoModel RecuperarTipoDocumento(string codTipoDocumento, string codEmpresa);
        int EliminarTipoDocumentoFisico(string codTipoDocumento, string codEmpresa);
        int EliminarTipoDocumentoLogico(TipoDocumentoModel oTipoDocumentoModel);
    }
}
