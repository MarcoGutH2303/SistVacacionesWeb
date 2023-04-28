using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.RepositoriesContracts
{
    public interface IConceptoRepository
    {
        List<ConceptoModel> ListarConcepto(string codEmpresa);
        int GrabarConcepto(ConceptoModel oConceptoModel);
        ConceptoModel RecuperarConcepto(string codConcepto, string codEmpresa);
        int EliminarConceptoFisico(string codConcepto, string codEmpresa);
        int EliminarConceptoLogico(ConceptoModel oConceptoModel);
    }
}
