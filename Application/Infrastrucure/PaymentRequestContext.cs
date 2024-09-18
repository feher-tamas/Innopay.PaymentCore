using Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionExtensions.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Domain.Common;
using FT.CQRS;

namespace Application.Infrastrucure
{
    public sealed class PaymentRequestContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;
        private IBus _messageBus;
        private static readonly Type[] EnumerationTypes = { typeof(Domain.PaymentRequest) };
        public DbSet<Domain.PaymentRequest> PaymentRequests { get; set; }
        public PaymentRequestContext( IBus messageBus)
        {
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Encrypt=True";
            _useConsoleLogger = true;
            _messageBus = messageBus;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();

            if (_useConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.PaymentRequest>(x =>
            {
                x.ToTable("PaymentRequest")
                .HasKey(k => k.Id);
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.Property(p => p.Amount)
                    .HasConversion(p => p.Value, p => Amount.Create(p).Value);
                x.Property(p => p.Currency).HasColumnName("Currency");
                x.Property(p => p.Status)
                    .HasConversion<int>();
                x.Property(p => p.PayeeId).HasColumnName("PayeeId");
                x.Property(p => p.PayerId).HasColumnName("PayerID");
                x.Property(p => p.CreatedAt).HasColumnName("CreatedAt");
                x.Property(p => p.UpdateAt).HasColumnName("UpdateAt");
            });      
        }
        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> enumerationEntries = ChangeTracker.Entries()
                .Where(x => EnumerationTypes.Contains(x.Entity.GetType()));

            foreach (EntityEntry enumerationEntry in enumerationEntries)
            {
                // Csak akkor állítsd az állapotot, ha az Id végleges értéket tartalmaz
                if (!enumerationEntry.Property("Id").IsTemporary)
                {
                    enumerationEntry.State = EntityState.Unchanged;
                }
            }

            List<EntityBase> entities = ChangeTracker
                .Entries()
                .Where(x => x.Entity is EntityBase)
                .Select(x => (EntityBase)x.Entity)
                .ToList();

            int result = base.SaveChanges();

            foreach (EntityBase entity in entities)
            {
                _messageBus.Send(entity.DomainEvents);
                entity.ClearDomainEvents();
            }
            return result;
        }
    }
}