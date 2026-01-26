

namespace _4EPlatForm.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IDbContextTransaction _transaction;
        public Guid _loggedInUserId;
        public UnitOfWork(AppDBContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            _loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
            repositories = new Hashtable();

        }
        Hashtable repositories;
        public IGRepository<TEntity> Repository<TEntity>() where TEntity : AuditEntity<Guid>
        {
            var key = typeof(TEntity).Name;
            if (!repositories.ContainsKey(key))
            {
                var repository = new GRepository<TEntity>(_context);
                repositories.Add(key, repository);
            }
            return repositories[key] as IGRepository<TEntity>;


        }
        public IGRepository<TEntity> LK_Repository<TEntity>() where TEntity : AuditEntity<int>
        {
            var key = typeof(TEntity).Name;
            if (!repositories.ContainsKey(key))
            {
                var repository = new GRepository<TEntity>(_context);
                repositories.Add(key, repository);
            }
            return repositories[key] as IGRepository<TEntity>;


        }
        #region Transaction
        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();
        public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();
        public void Commit() => _transaction.Commit();
        public async Task CommitAsync() => await _transaction.CommitAsync();
        public void Rollback() => _transaction?.Rollback();
        public async Task RollbackAsync() => await _transaction.RollbackAsync();

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
        public async Task DisposeAsync()
        {
            await _transaction.DisposeAsync();
            await _context.DisposeAsync();
        }
        #endregion


        #region SaveChanges

        public void SaveChanges()
        {
            try
            {
                var entries = _context.ChangeTracker.Entries()
                       .Where(e => (e.Entity is BaseEntity<Guid>) && (
                         e.State == EntityState.Added
                                  || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((AuditEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((AuditEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;


                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((AuditEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        ((AuditEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }

                }
                if (_context.Database.CurrentTransaction == null)
                    BeginTransaction();
                _context.SaveChanges();
                //Commit();
            }
            catch (Exception)
            {
                Rollback();

            }

        }
        #endregion
        #region SaveChanges async
        public async Task SaveChangesAsync()
        {
            try
            {

                var entries = _context.ChangeTracker.Entries()
.Where(e => (e.Entity is BaseEntity<Guid>) && (
     e.State == EntityState.Added
     || e.State == EntityState.Modified));

                foreach (var entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Modified)
                    {
                        ((AuditEntity<Guid>)entityEntry.Entity).UpdatedOn = DateTime.Now;
                        ((AuditEntity<Guid>)entityEntry.Entity).UpdatedBy = _loggedInUserId;
                        entityEntry.Property("CreatedOn").IsModified = false;
                        entityEntry.Property("CreatedBy").IsModified = false;


                    }

                    if (entityEntry.State == EntityState.Added)
                    {
                        ((AuditEntity<Guid>)entityEntry.Entity).CreatedOn = DateTime.Now;
                        ((AuditEntity<Guid>)entityEntry.Entity).CreatedBy = _loggedInUserId;
                    }
                }
                if (_context.Database.CurrentTransaction == null)
                    await BeginTransactionAsync();
                await _context.SaveChangesAsync();
                //Commit();
            }
            catch (Exception ex)
            {
                await RollbackAsync();

            }

        }
        #endregion

        void AddProperty(ExpandoObject expando, string propertyName, object value)
        {
            // Cast ExpandoObject to IDictionary to add properties dynamically
            var expandoDict = (IDictionary<string, object>)expando;
            expandoDict[propertyName] = value;
        }
        private Guid GetPropertyValueAsGuid(object entity, string propertyName)
        {
            if (entity.GetType().GetProperty(propertyName) != null)
            {
                return (Guid)(entity.GetType().GetProperty(propertyName)?.GetValue(entity) ?? default(Guid));
            }
            return default(Guid);
        }
    }
}

