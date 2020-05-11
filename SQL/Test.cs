using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Data;
using System.Net.Sockets;

namespace SQL
{
    class Test
    {
        void Run()
        {
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo(Const.Host, Const.Port, Const.User, Const.Pass);
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);
            using (SshClient client = new SshClient(connectionInfo))
            {
                try
                {
                    client.Connect();
                    if (client.IsConnected) { Console.WriteLine("SSH Connection is active."); }
                    else { Console.WriteLine("SSH Connection failed."); }
                    ForwardedPortLocal portFwdL = new ForwardedPortLocal(Const.SqlIPA, Const.SqlPort, Const.SqlHost, Const.SqlPort);
                    client.AddForwardedPort(portFwdL);
                    portFwdL.Start();
                    if (portFwdL.IsStarted)
                        Console.WriteLine("port forwarding is started");
                    else Console.WriteLine("port forwarding failed.");

                    string strConnection = Const.SqlConn;
                    MySqlConnection conn = new MySqlConnection(strConnection);
                    string myTableName = "tbl_users";
                    MySqlDataAdapter myDataAdapter = new MySqlDataAdapter();
                    myDataAdapter.SelectCommand = new MySqlCommand(Const.SqlSelect, conn);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(myDataAdapter);
                    try
                    {
                        conn.Open();
                        Console.WriteLine("SQL connection is active.");
                        DataSet ds = new DataSet("users");
                        myDataAdapter.Fill(ds, myTableName);
                        myDataAdapter.Update(ds, myTableName);
                        Console.WriteLine(ds.GetXml());
                    }
                    catch(MySqlException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally { conn.Close(); }
                    client.Disconnect();
                    Console.WriteLine("SSH Disconnect");
                }
                catch(SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            new Test().Run();
            Console.ReadKey();
        }
    }
}
