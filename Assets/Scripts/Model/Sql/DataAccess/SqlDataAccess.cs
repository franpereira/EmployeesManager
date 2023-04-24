using System;
using System.Collections.Generic;
using System.Data;

namespace Employees.Model.Sql.DataAccess
{
    /// <summary>
    /// Base class with common functionality for SQL data access classes.
    /// </summary>
    /// <typeparam name="TKey">The type of the key for the entity.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class SqlDataAccess<TKey, TEntity>
    {
        readonly SqlExecutor _executor;
        readonly Dictionary<TKey, TEntity> _cache = new();

        protected SqlDataAccess(SqlExecutor executor) => _executor = executor;

        /// <summary>
        /// Creates an entity instance from the current row of the data reader.
        /// </summary>
        /// <param name="reader">The data reader containing the entity data.</param>
        /// <returns>A new instance of the entity.</returns>
        protected abstract TEntity EntityFromReader(IDataReader reader);

        /// <summary>
        /// Gets an entity from the cache or from the database.
        /// </summary>
        /// <param name="key">The key of the entity to get.</param>
        /// <param name="entityFactory">A function that creates a new instance of the entity.</param>
        /// <returns>The entity with the specified key.</returns>
        protected TEntity GetFromCacheOrDb(TKey key, Func<TEntity> entityFactory)
        {
            if (_cache.TryGetValue(key, out TEntity entity))
                return entity;

            entity = entityFactory();
            _cache.Add(key, entity);
            return entity;
        }

        /// <summary>
        /// Queries all entities that match the specified SQL statement.
        /// </summary>
        /// <param name="statement">The statement to execute.</param>
        /// <param name="prepareCommand">An optional delegate that prepares the command before execution.</param>
        /// <returns>A list of all entities found.</returns>
        protected List<TEntity> QueryAllBy(string statement, Action<IDbCommand> prepareCommand = null)
        {
            List<TEntity> entities = new();
            _executor.Execute(statement, prepareCommand, handleResults:
                reader =>
                {
                    while (reader.Read())
                        entities.Add(EntityFromReader(reader));
                }
            );
            return entities;
        }
        
        /// <summary>
        /// Returns the first found entity that matches the specified SQL statement.
        /// Used for queries that are expected to return only one entity.
        /// </summary>
        /// <param name="statement">The SQL statement to execute.</param>
        /// <param name="prepareCommand">An optional delegate that prepares the command before execution.</param>
        /// <returns>The first entity that matches the specified SQL statement, or null if no entities are found.</returns>
        protected TEntity QueryOneBy(string statement, Action<IDbCommand> prepareCommand = null)
        {
            TEntity entity = default;
            _executor.Execute(statement, prepareCommand, handleResults:
                reader =>
                {
                    if (reader.Read())
                        entity = EntityFromReader(reader);
                }
            );
            return entity;
        }
        
        /// <summary>
        /// Execute the specified SQL statement.
        /// Intended for statements that do not return any results (e.g. INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="statement">The SQL statement to execute.</param>
        /// <param name="prepareCommand">An optional delegate that prepares the command before execution.</param>
        protected void NonQuery(string statement, Action<IDbCommand> prepareCommand = null)
        {
            _executor.Execute(statement, prepareCommand);
        }
    }
}