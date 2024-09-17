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
        private static readonly Type[] EnumerationTypes = { typeof(PaymentRequest) };
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public PaymentRequestContext( IBus messageBus)
        {
            _connectionString = @"Server = (localdb)\\MSSQLLocalDB; Integrated Security = true; Database = PaymentRequestContext;";
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
            modelBuilder.Entity<PaymentRequest>(x =>
            {
                x.ToTable("PaymentRequest").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("PaymentRequestID");
                x.Property(p => p.Amount)
                    .HasConversion(p => p.Value, p => Amount.Create(p).Value);
                x.Property(p => p.Currency).HasColumnName("Currency");
                x.Property(p => p.Status)
                    .HasConversion<int>();
                x.Property(p => p.PayeeId).HasColumnName("PayeeId");
                x.Property(p => p.PayerID).HasColumnName("PayerID");
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
                enumerationEntry.State = EntityState.Unchanged;
            }

            List<Entity> entities = ChangeTracker
                .Entries()
                .Where(x => x.Entity is Entity)
                .Select(x => (Entity)x.Entity)
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