using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces.Repositories;
using AgendaApp.Infra.Data.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Infra.Data.Repositories
{
    public class TarefaRepository : BaseRepository<Tarefa, Guid>, ITarefaRepository
    {
        public List<Tarefa> GetAll(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                //LAMBDA
                /*
                return dataContext.Set<Tarefa>()
                    .Where(t => t.DataHora >= dataMin && t.DataHora <= dataMax && t.UsuarioId == usuarioId)
                    .OrderBy(t => t.DataHora)
                    .ToList();
                */

                //LINQ
                /*
                var query = from t in dataContext.Set<Tarefa>()
                            where t.DataHora >= dataMin && t.DataHora <= dataMax && t.UsuarioId == usuarioId
                            orderby t.DataHora
                            select t;

                return query.ToList();
                */

                //SQL
                var query = @"
                    SELECT * FROM TAREFA 
                    WHERE DATAHORA BETWEEN @DataMin AND @DataMax AND USUARIO_ID = @UsuarioId
                    ORDER BY DATAHORA
                ";

                return dataContext.Database
                    .GetDbConnection()
                    .Query<Tarefa>(query, new
                    {
                        @DataMin = dataMin,
                        @DataMax = dataMax,
                        @UsuarioId = usuarioId
                    }).ToList();
            }
        }
    }
}



