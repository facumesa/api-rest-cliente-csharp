using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.InterfacesRepo
{
    public interface IRepositorioObservaciones : IRepositorio<Observacion>
    {
        bool ExisteObservacionDuplicada(Observacion obs);

        IEnumerable<ObjetoCeleste> RankingObjetosMasObservados();
    }
}
