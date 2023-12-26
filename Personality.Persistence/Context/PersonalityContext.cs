using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personality.Persistence.EventStore.Entity;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;
using Personality.Reading.Projecting.PersonStateModel.Projection;

namespace Personality.Persistence.Context
{
    public class PersonalityContext :
        DbContext,
        Reading.Persistence.Repository.IUnitOfWork,
        Writing.Persistence.IUnitOfWork
    {
        //очередь пакетных задач для запуска в транзакции
        private readonly Queue<Func<Task>> batchTasks = new();
        //наблюдатели за контекстом
        private readonly List<IContextObserver> observers = new();

        public PersonalityContext(DbContextOptions<PersonalityContext> options)
            : base(options)
        {
        }

        //события лица
        public virtual DbSet<PersonEventItem> PersonEvents { get; set; }

        //модель чтения истории изменений данных лица
        public virtual DbSet<PersonalDataHistoryProjection> PersonalDataHistories { get; set; }
        public virtual DbSet<IdentityDocumentHistoryProjection> IdentityDocumentHistories { get; set; }

        //модель чтения состояний лица
        public virtual DbSet<PersonStateProjection> PersonStates { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.HasSequence<int>("PersonHiLo", "dbo")
                        .StartsAt(1)
                        .IncrementsBy(10);

            modelBuilder.HasSequence<int>("ProjectionHistoryIdentityDocumentHiLo", "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.HasSequence<int>("ProjectionHistoryPersonalDataHiLo", "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.HasSequence<int>("ProjectionPersonStateHiLo", "dbo")
                .StartsAt(1)
                .IncrementsBy(1);
        }

        /// <summary>
        /// Добавить наблюдателя
        /// </summary>
        /// <param name="observer">Наблюдатель</param>
        public void AddObserver(IContextObserver observer)
        {
            if (observer != null && !observers.Contains(observer))
                observers.Add(observer);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);

            while (batchTasks.Count != 0)
            {
                await batchTasks.Dequeue().Invoke();
            }

            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            observers.ForEach(o => o.HandleCommit());
            observers.Clear();
        }

        /// <summary>
        /// Выполнить очистку данных
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        public void Clear<T>() where T : class
        {
            batchTasks.Enqueue(() => Set<T>().ExecuteDeleteAsync());
        }
    }
}
