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
        private string _connectionString = "Server=localhost;Database=db_camping;Uid=root;Pwd=Messi10neymar11";
        private MySqlConnection _connection;

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
            cmdInsert.CommandText = "Insert Into Reservierungsanfragen Values(@vorname, @nachname, @kreditkartennummer, @ersterTagBuchung, @letzterTagBuchung, @paket)";

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

            cmdInsert.Parameters.Add(paramVN);
            cmdInsert.Parameters.Add(paramNN);
            cmdInsert.Parameters.Add(paramPaket);
            cmdInsert.Parameters.Add(paramETB);
            cmdInsert.Parameters.Add(paramLTB);
            cmdInsert.Parameters.Add(paramKNr);

            return cmdInsert.ExecuteNonQuery() == 1;

        }
    }
}