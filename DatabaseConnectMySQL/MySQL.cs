using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DatabaseConnectMySQL
{
    static class MySQL
    {

        /*
         *To change between Console and Application type debug, right click Project and go into Settings.
         *Under 'Output Type', select 'Console Application' or 'Windows Application'.
         * 
        */

        [STAThread]
        static void Main(string[] args)
        {
            string cs = @"server=localhost;userid=Ekwok;password=Ekindle18!;database=experimentation";

            //InsertOneLine(cs);

            Select(cs);



            Thread.Sleep(10000);
            


            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */
        }

        private static void Select(string connection)
        {
            using (var con = new MySqlConnection(connection))
            {
                con.Open();

                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, con);

                var version = cmd.ExecuteScalar().ToString();

                Console.WriteLine($"MySQL version : {con.ServerVersion}");

                string select = "SELECT * FROM periodic_table";
                using (var displayresults = new MySqlCommand(select, con))
                {
                    using (MySqlDataReader rdr = displayresults.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine(" {0} {1} {2} {3}", rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetMySqlDecimal(3));
                        }
                    }
                }


                con.Close();
            }
        }
        
        private static void Insert(string connection)
        {
            using (var con = new MySqlConnection(connection))
            {
                con.Open();

                //change this to user input


                var ins = "INSERT INTO periodic_table(atomicNumber, symbol, elementName, atomicWeight) VALUES(@atomicNumber, @symbol, @elementName, @atomicWeight)";
                using (var insertLine = new MySqlCommand(ins, con))
                {
                    insertLine.Parameters.AddWithValue("@atomicNumber", 6);
                    insertLine.Parameters.AddWithValue("@symbol", 'O');
                    insertLine.Parameters.AddWithValue("@elementName", "Oxygen");
                    insertLine.Parameters.AddWithValue("@atomicWeight", 16.00);
                    insertLine.Prepare();

                    insertLine.ExecuteNonQuery();

                }

                Console.WriteLine("Row Inserted");


                con.Close();
            }
        }

        private static void InsertOneLine(string connection)
        {
            using (var con = new MySqlConnection(connection))
            {
                con.Open();

                Console.WriteLine("Please provide a query for the MySQL Database. The query needs to be one line containing fields to be added to the table.");
                var ins = Console.ReadLine();

                using (var insertOneLine = new MySqlCommand(ins, con))
                {
                    insertOneLine.ExecuteNonQuery();
                }

                con.Close();

            }

        }


        private static void Update(string connection)
        {
            using (var con = new MySqlConnection(connection))
            {
                con.Open();

                var update = "UPDATE periodic_table SET elementName='Lithium' WHERE atomicNumber = 3";
                using (var updateLine = new MySqlCommand(update, con))
                {
                    updateLine.Prepare();

                    updateLine.ExecuteNonQuery();
                }


                con.Close();
            }
        }



        //SQL Query Function Template. Copy and paste when creating a new MySQL Query Function
        /*
        private static void SqlFunctionTemplate(string connection)
        {
            using (var con = new MySqlConnection(connection))
            {
                con.Open();

                con.Close();

            }

        }
        */

    }
}
