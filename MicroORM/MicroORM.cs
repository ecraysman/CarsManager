
using PetaPoco;
using PetaPoco.Providers;
using MySql.Data.MySqlClient;
using System.Data;
//using PetaPoco.Utilities;



namespace ORM
{
 /// <summary>
 /// Wrapper de petaPoco
 /// 
 /// </summary>
    public class MicroORM
    {

        private PetaPoco.IDatabase _database;

        public PetaPoco.IDatabase EzeORM
        {

            get {
                int maxAttempts = 5;
                while (true)
                {
                    try
                    {

                        if (_database.Connection.State == ConnectionState.Closed)
                        {
                            _database.Connection.Open();
                        }

                        return _database;
                    }
                    catch (MySqlException ex)
                    {
                        //If the error is a temp, wait and retry, max 5 times
                        if (maxAttempts > 0 && (ex.Number == 0 || ex.Number == 1042 || ex.Number == 1043 || ex.Number == 1044 || ex.Number == 1045))
                        {
                            maxAttempts--;
                            Thread.Sleep(50);
                        }
                        else
                        {
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                //return _database;
            
            }




        }


        //public Database GetDatabase()
        //{
            
        //}


        public MicroORM()
        {
            _database = new Database(new  MySqlConnection("Server=localhost;Port=3307;Database=CarsManager;Uid=mysqluser;Pwd=mysqlpass;"));
            //_database = new Database((IDatabaseBuildConfiguration)DatabaseConfiguration.Build()
            //    .UsingProvider<MySqlDatabaseProvider>()
            //    .UsingConnectionString("Server=localhost;Port=3307;Database=CarsManager;Uid=mysqluser;Pwd=mysqlpass;")
            //    .Create());
            //_database = new Database(DatabaseConfiguration.Build()
            //    .UsingProvider<MySqlDatabaseProvider>()
            //    .UsingConnectionString("Server=localhost:3307;Database=CarsManager;Uid=mysqluser;Pwd=mysqlpass;")
            //    .Create());
            //PetaPoco.IDatabaseBuildConfiguration
            // dbConfig = (IDatabaseBuildConfiguration)DatabaseConfiguration.Build()
            //    .UsingProvider<MySqlDatabaseProvider>()
            //    .UsingConnectionString("Server=localhost:3307;Database=CarsManager;Uid=mysqluser;Pwd=mysqlpass;")
            //    .Create();
            //_database = new Database(dbConfig);

            //Create models?

            
        }



   


    }
}
