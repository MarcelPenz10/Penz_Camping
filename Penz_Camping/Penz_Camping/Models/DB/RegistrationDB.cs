using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Penz_Camping.Models.DB
{
    public class RegistrationDB :IRegistrierung
    {
        private string _connectionString = "Server=localhost;Database=db_registrierung;Uid=root;Pwd=Messi10neymar11";
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

        public bool Insert(User user)
        {
            if (user == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();

            cmdInsert.CommandText = "Insert Into users Values(null, @vorname, @nachname, @gender, @birthdate, @username, sha2(@password, 512), sha2(@passwordAdmin, 512))";

            DbParameter paramVN = cmdInsert.CreateParameter();
            paramVN.ParameterName = "vorname";
            paramVN.Value = user.Vorname;
            paramVN.DbType = DbType.String;

            DbParameter paramNN = cmdInsert.CreateParameter();
            paramNN.ParameterName = "nachname";
            paramNN.Value = user.Nachname;
            paramNN.DbType = DbType.String;

            DbParameter paramGender = cmdInsert.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.Value = user.Gender;
            paramGender.DbType = DbType.Int32;

            DbParameter paramBDate = cmdInsert.CreateParameter();
            paramBDate.ParameterName = "birthdate";
            paramBDate.Value = user.Birthdate;
            paramBDate.DbType = DbType.Date;

            DbParameter paramUsername = cmdInsert.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdInsert.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = user.Password;
            paramPwd.DbType = DbType.String;

            DbParameter paramPwdA = cmdInsert.CreateParameter();
            paramPwdA.ParameterName = "passwordAdmin";
            paramPwdA.Value = user.PasswordAdmin;
            paramPwdA.DbType = DbType.String;

            cmdInsert.Parameters.Add(paramVN);
            cmdInsert.Parameters.Add(paramNN);
            cmdInsert.Parameters.Add(paramGender);
            cmdInsert.Parameters.Add(paramBDate);
            cmdInsert.Parameters.Add(paramUsername);
            cmdInsert.Parameters.Add(paramPwd);
            cmdInsert.Parameters.Add(paramPwdA);

            return cmdInsert.ExecuteNonQuery() == 1;




        }

        public User Login(UserLogin user)
        {
            DbCommand cmdLogin = this._connection.CreateCommand();
            cmdLogin.CommandText = "SELECT * FROM users WHERE username=@username AND password=sha2(@password, 512) OR passwordAdmin=sha2(@passwordAdmin,512)";

            DbParameter paramUsername = cmdLogin.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdLogin.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = user.Password;
            paramPwd.DbType = DbType.String;

            DbParameter paramPwdA = cmdLogin.CreateParameter();
            paramPwdA.ParameterName = "passwordAdmin";
            paramPwdA.Value = user.PasswordAdmin;
            paramPwdA.DbType = DbType.String;

            cmdLogin.Parameters.Add(paramUsername);
            cmdLogin.Parameters.Add(paramPwd);
            cmdLogin.Parameters.Add(paramPwdA);

            using (DbDataReader reader = cmdLogin.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
                return new User
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Vorname = Convert.ToString(reader["vorname"]),
                    Nachname = Convert.ToString(reader["nachname"]),
                    Gender = (Gender)Convert.ToInt32(reader["gender"]),
                    Birthdate = Convert.ToDateTime(reader["birthdate"]),
                    Username = Convert.ToString(reader["username"]),
                    Password = "",
                    PasswordAdmin = ""
                };
            }
        }


    }
}