using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApi.Repository
{
    public class PhoneBookRepository : IRepository<PhoneBook>
    {
        private string connectionString;

        public PhoneBookRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public PhoneBook Add(PhoneBook item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                //dbConnection.Open();
                //dbConnection.Execute("INSERT INTO phonebook ([Id], name,number) VALUES(@id,@Name,@Number)", item);
                //var insertSQL = string.Format(@"INSERT INTO public.customer(firstname, lastname, email,createtime)                    VALUES('{0}', '{1}', '{2}','{3}');", "Catcher", "Wong", "catcher_hwq@outlook.com", DateTime.Now);
                //var res = conn.Execute(insertSQL);

                var insertSQL = string.Format(@"INSERT INTO public.pbrecord (username, phonenumber)                    VALUES('{0}', '{1}');", "Wong", "23e423");
                var res = dbConnection.Execute(insertSQL);

                return item;
            }

        }

        public IEnumerable<PhoneBook> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<PhoneBook>("SELECT * FROM PhoneBook");
            }
        }

        public PhoneBook FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<PhoneBook>("SELECT * FROM PhoneBook WHERE [Id] = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM PhoneBook WHERE Id=@Id", new { Id = id });
            }
        }

        public PhoneBook Update(PhoneBook item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE PhoneBook SET id = @Id,  name  = @Name, number= @Number WHERE id = @Id", item);

                return item;
            }
        }
    }
}
