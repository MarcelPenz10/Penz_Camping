using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Penz_Camping.Models.DB
{
    public class ReservierungDB :IReservierung
    {
        public string _connectionString = "Server=localhost;Database=db_camping;Uid=root;Pwd=Messi10neymar11";
        public MySqlConnection _connection;

        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }
        }

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }

        public bool Insert(Reservierungsanfrage reservierungsanfrage)
        {
            if (reservierungsanfrage == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();
            cmdInsert.CommandText = "Insert Into AnfragenR Values(@vorname, @nachname, @kreditkartennummer, @ersterTagBuchung, @letzterTagBuchung, @paket, sha2(@password, 256), @bearbeitet)";

            DbParameter paramVN = cmdInsert.CreateParameter();
            paramVN.ParameterName = "vorname";
            paramVN.Value = reservierungsanfrage.Vorname;
            paramVN.DbType = DbType.String;

            DbParameter paramNN = cmdInsert.CreateParameter();
            paramNN.ParameterName = "nachname";
            paramNN.Value = reservierungsanfrage.Nachname;
            paramNN.DbType = DbType.String;

            DbParameter paramPaket = cmdInsert.CreateParameter();
            paramPaket.ParameterName = "paket";
            paramPaket.Value = reservierungsanfrage.Paket;
            paramPaket.DbType = DbType.Int32;

            DbParameter paramETB = cmdInsert.CreateParameter();
            paramETB.ParameterName = "ersterTagBuchung";
            paramETB.Value = reservierungsanfrage.ErsterTagBuchung;
            paramETB.DbType = DbType.Date;

            DbParameter paramLTB = cmdInsert.CreateParameter();
            paramLTB.ParameterName = "letzterTagBuchung";
            paramLTB.Value = reservierungsanfrage.LetzterTagBuchung;
            paramLTB.DbType = DbType.Date;

            DbParameter paramKNr = cmdInsert.CreateParameter();
            paramKNr.ParameterName = "kreditkartennummer";
            paramKNr.Value = reservierungsanfrage.Kreditkartennummer;
            paramKNr.DbType = DbType.Int32;

            DbParameter paramPwd = cmdInsert.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = reservierungsanfrage.Password;
            paramPwd.DbType = DbType.String;

            DbParameter paramBear = cmdInsert.CreateParameter();
            paramBear.ParameterName = "bearbeitet";
            paramBear.Value = reservierungsanfrage.Bearbeitet;
            paramBear.DbType = DbType.Boolean;

            cmdInsert.Parameters.Add(paramVN);
            cmdInsert.Parameters.Add(paramNN);
            cmdInsert.Parameters.Add(paramPaket);
            cmdInsert.Parameters.Add(paramETB);
            cmdInsert.Parameters.Add(paramLTB);
            cmdInsert.Parameters.Add(paramKNr);
            cmdInsert.Parameters.Add(paramPwd);
            cmdInsert.Parameters.Add(paramBear);

            return cmdInsert.ExecuteNonQuery() == 1;

        }

        public List<Reservierungsanfrage> GetAllRes()


        {

            List<Reservierungsanfrage> reservierungsanfrage = new List<Reservierungsanfrage>();


            DbCommand cmdSelect = this._connection.CreateCommand();
            cmdSelect.CommandText = "SELECT * FROM AnfragenR";

            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    reservierungsanfrage.Add(new Reservierungsanfrage
                    {
                       

                        Vorname = Convert.ToString(reader["vorname"]),
                        Nachname = Convert.ToString(reader["nachname"]),
                        Kreditkartennummer = Convert.ToInt32(reader["kreditkartennummer"]),
                        Paket = (Paket)Convert.ToInt32(reader["paket"]),
                        ErsterTagBuchung = Convert.ToDateTime(reader["ersterTagBuchung"]),
                        LetzterTagBuchung = Convert.ToDateTime(reader["letzterTagBuchung"]),
                        Bearbeitet = Convert.ToBoolean(reader["bearbeitet"]),
                        Password = ""
                        
                    });
                }
            }
            return reservierungsanfrage;
        }

        public bool UpdateAnfrageStatus(int knr, bool newStatus)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE AnfragenR SET bearbeitet=@newStatus WHERE kreditkartennummer=@Kreditkartennummer";

            DbParameter paramKnr = cmdUpdate.CreateParameter();
            paramKnr.ParameterName = "Kreditkartennummer";
            paramKnr.Value = knr;
            paramKnr.DbType = DbType.Int32;


            DbParameter paramStatus = cmdUpdate.CreateParameter();
            paramStatus.ParameterName = "newStatus";
            paramStatus.Value = newStatus;
            paramStatus.DbType = DbType.Boolean;

            cmdUpdate.Parameters.Add(paramKnr);
            cmdUpdate.Parameters.Add(paramStatus);

            return cmdUpdate.ExecuteNonQuery() == 1;
        }

        public bool AnfrageLöschen(int knr)
        {
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM AnfragenR WHERE kreditkartennummer=@Kreditkartennummer";

            DbParameter paramKnr = cmdDel.CreateParameter();
            paramKnr.ParameterName = "Kreditkartennummer";
            paramKnr.Value = knr;
            paramKnr.DbType = DbType.Int32;

            cmdDel.Parameters.Add(paramKnr);

            return cmdDel.ExecuteNonQuery() == 1;
        }

    }
}