using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PhoneBookPersistense.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PhoneBookPersistense.Repository.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private string ConnectionString;

        public Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(ConnectionString);
            }
        }

        public T Add(T item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var id = dbConnection.Execute("INSERT INTO public.pbrecord (username, phonenumber) VALUES(@username,@phonenumber) RETURNING id", item);

            return FindByID(id);
            }
        }

        public IEnumerable<T> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>("SELECT * FROM public.pbrecord");
            }
        }

        public T FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>("SELECT * FROM public.pbrecord WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM public.pbrecord WHERE id=@Id", new { Id = id });
            }
        }

        public T Update(T item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE public.pbrecord SET username = @username, phonenumber= @phonenumber WHERE id = @Id", item);

                return item;
            }
        }
    }
}
