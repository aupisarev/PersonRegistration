using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Personality.Persistence.Context;
using Personality.Reading.Projecting;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;
using Personality.Reading.Projecting.PersonStateModel.Projection;

namespace Personality.Persistence
{
    /// <summary>
    /// <inheritdoc cref="IProjectionIdGenerator"/>
    /// </summary>
    /// <remarks>
    /// Упрощенная реализация через последовательность
    /// </remarks>
    public class ProjectionIdGenerator : IProjectionIdGenerator
    {
        private readonly DbContextOptions<PersonalityContext> options;

        public ProjectionIdGenerator(DbContextOptions<PersonalityContext> options)
        {
            this.options = options;
        }

        public async Task<long> NewIdAsync<T>() where T : IProjection
        {
            await using var context = new PersonalityContext(options);
            return await NewId<T>(context);
        }

        private async Task<long> NewId<T>(PersonalityContext context) where T : IProjection
        {
            var sql = string.Empty;

            if (typeof(T) == typeof(IdentityDocumentHistoryProjection))
                sql = "SELECT @key = (NEXT VALUE FOR ProjectionHistoryIdentityDocumentHiLo)";
            else if (typeof(T) == typeof(PersonalDataHistoryProjection))
                sql = "SELECT @key = (NEXT VALUE FOR ProjectionHistoryPersonalDataHiLo)";
            else if(typeof(T) == typeof(PersonStateProjection))
                sql = "SELECT @key = (NEXT VALUE FOR ProjectionPersonStateHiLo)";

            var keyParam = new SqlParameter("@key", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output };
            await context.Database.ExecuteSqlRawAsync(sql, keyParam);
            return (long)keyParam.Value!;
        }
    }
}
