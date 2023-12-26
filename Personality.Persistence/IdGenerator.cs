using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Personality.Domain;
using Personality.Domain.Base;
using Personality.Persistence.Context;
using System;
using System.Threading.Tasks;

namespace Personality.Persistence
{
    /// <summary>
    /// <inheritdoc cref="IIdGenerator"/>
    /// </summary>
    /// <remarks>
    /// Упрощенная реализация через последовательность
    /// </remarks>
    public class IdGenerator : IIdGenerator
    {
        private readonly DbContextOptions<PersonalityContext> options;

        public IdGenerator(DbContextOptions<PersonalityContext> options)
        {
            this.options = options;
        }

        public async Task<TEntityId> NewIdAsync<TEntityId>() where TEntityId : IEntityId
        {
            await using var context = new PersonalityContext(options);
            return (TEntityId)Activator.CreateInstance(typeof(TEntityId), await NewId(context));
        }

        private async Task<long> NewId(PersonalityContext context)
        {
            var keyParam = new SqlParameter("@key", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output };
            await context.Database.ExecuteSqlRawAsync($"SELECT @key = (NEXT VALUE FOR PersonHiLo)", keyParam);
            return (long)keyParam.Value!;
        }
    }
}
