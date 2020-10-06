using Confluent.Kafka;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private ProducerBuilder<Null, string> producerBuilder;
        private ProducerConfig producerConfig;

        public PhoneBookRepository(IConfiguration configuration, ProducerConfig config)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            producerConfig = config;
            producerBuilder = new ProducerBuilder<Null, string>(producerConfig);
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
                dbConnection.Execute("INSERT INTO public.pbrecord (username, phonenumber) VALUES(@username,@phonenumber)", item);
                //var insertSQL = string.Format(@"INSERT INTO public.customer(firstname, lastname, email,createtime)                    VALUES('{0}', '{1}', '{2}','{3}');", "Catcher", "Wong", "catcher_hwq@outlook.com", DateTime.Now);
                //var res = conn.Execute(insertSQL);

                //var insertSQL = string.Format(@"INSERT INTO public.pbrecord (username, phonenumber)                    VALUES('{0}', '{1}');", "Wong", "23e423");
                //var res = dbConnection.Execute(insertSQL);
                
            }

            using (var producer = producerBuilder.Build())
            {
                string payload = JsonConvert.SerializeObject(item);
                producer.Produce("phonebook-incoming", new Message<Null, string> { Value = payload });
            }

            return item;
        }

        public IEnumerable<PhoneBook> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<PhoneBook>("SELECT * FROM public.pbrecord");
            }
        }

        public PhoneBook FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<PhoneBook>("SELECT * FROM public.pbrecord WHERE id = @Id", new { Id = id }).FirstOrDefault();
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

        public PhoneBook Update(PhoneBook item)
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
