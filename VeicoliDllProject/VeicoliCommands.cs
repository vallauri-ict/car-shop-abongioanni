using System;
using System.Data;
using System.Data.OleDb;

namespace VenditaVeicoliDllProject {

    /*********************SQL COMMANDS TO AVOID INJECTION*********************/

    public class VeicoliCommands {
        private abstract class VeicoloCommand {

            internal OleDbCommand cmd;
            public VeicoloCommand(OleDbConnection c) { cmd = new OleDbCommand { Connection = c }; }
            public OleDbCommand GetCommand() { return cmd; }

            public void AggiungiParametri(Veicolo v)
            {
                
                cmd.Parameters.Add("@targa", OleDbType.VarChar, 255).Value = v.Targa;
                cmd.Parameters.Add("@automoto", OleDbType.Boolean).Value = (v is Auto);
                cmd.Parameters.Add("@marca", OleDbType.VarChar, 255).Value = v.Marca;
                cmd.Parameters.Add("@modello", OleDbType.VarChar, 255).Value = v.Modello;
                cmd.Parameters.Add("@cilindrata", OleDbType.Integer).Value = v.Cilindrata;
                cmd.Parameters.Add("@potenzakw", OleDbType.Double).Value = v.PotenzaKw;
                cmd.Parameters.Add("@immatricolazione", OleDbType.DBDate).Value = v.Immatricolazione;
                cmd.Parameters.Add("@usato", OleDbType.Boolean).Value = v.IsUsato;
                cmd.Parameters.Add("@kmzero", OleDbType.Boolean).Value = v.IsKmZero;
                cmd.Parameters.Add("@kmpercorsi", OleDbType.Integer).Value = v.KmPercorsi;
                cmd.Parameters.Add("@colore", OleDbType.VarChar, 255).Value = v.Colore;
                cmd.Parameters.Add("@prezzo", OleDbType.Double).Value = v.Prezzo;
                cmd.Parameters.Add("@imagepath", OleDbType.VarChar, 255).Value = v.ImagePath;
                string c = (v is Auto ? (v as Auto).NumeroAirBag.ToString() : (v as Moto).MarcaSella);
                cmd.Parameters.Add("@caratteristica", OleDbType.VarChar, 255).Value = c;
            }
        }

        private class InsertVeicoloCommand : VeicoloCommand {
            public InsertVeicoloCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "INSERT INTO Veicoli VALUES (@targa, @automoto,@marca, @modello, @cilindrata, @potenzakw," +
                    "@immatricolazione,@usato,@kmzero,@kmpercorsi,@colore,@prezzo,@imagepath,@caratteristica);";
            }
        }
        private class UpdateVeicoloCommand : VeicoloCommand {
            public UpdateVeicoloCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "UPDATE Veicoli SET Targa=@targa,AutoMoto=@automoto,Marca=@marca, Modello=@modello, Cilindrata=@cilindrata,PotenzaKw=@potenzakw," +
                    "Immatricolazione=@immatricolazione,Usato=@usato,KmZero=@kmzero," +
                    "KmPercorsi=@kmpercorsi,Colore=@colore,Prezzo=@prezzo,ImagePath=@imagepath,Caratteristica=@caratteristica WHERE Targa=@targa;";
            }
        }
        private class DeleteVeicoliCommand : VeicoloCommand {
            public DeleteVeicoliCommand(OleDbConnection c) : base(c)
            {
                cmd.CommandText = "DELETE FROM Veicoli WHERE Targa=@targa;";
            }
            public new void AggiungiParametri(Veicolo v)
            {
                cmd.Parameters.Add("@targa", OleDbType.VarChar, 255).Value = v.Targa;
            }
        }

        public VeicoliCommands() { }

        public void CreateTable(string connString)
        {
            string sql = $"CREATE TABLE Veicoli (" +
                $"Targa varchar(255) NOT NULL PRIMARY KEY,"+
                $"AutoMoto bit,"+
                $"Marca varchar(255)," +
                $"Modello varchar(255)," +
                $"Cilindrata int," +
                $"PotenzaKw decimal," +
                $"Immatricolazione datetime," +
                $"Usato bit," +
                $"KmZero bit," +
                $"KmPercorsi int," +
                $"Colore varchar(255)," +
                $"Prezzo decimal," +
                $"ImagePath varchar(255)," +
                $"Caratteristica varchar(255)" +
                $");";
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void DropTable(string connString)
        {
            string sql = $"DROP TABLE Veicoli;";
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public SerializableBindingList<Veicolo> GetVeicoliList(DataTable t)
        {
            SerializableBindingList<Veicolo> l = new SerializableBindingList<Veicolo>();
            foreach (DataRow r in t.Rows)
            {
                if (Convert.ToInt32(r["AutoMoto"]) == 1)
                    l.Add(new Auto(Veicolo.SetArray(r)));
                else
                    l.Add(new Moto(Veicolo.SetArray(r)));
            }
            return l;
        }

        public DataTable GetRows(string connString, string sqlString="SELECT * FROM Veicoli")
        {
            DataTable t = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(t);
            }
            return t;
        }

        public void Insert(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                InsertVeicoloCommand i = new InsertVeicoloCommand(connection);
                OleDbCommand cmd = i.GetCommand();
                i.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                DeleteVeicoliCommand d = new DeleteVeicoliCommand(connection);
                OleDbCommand cmd = d.GetCommand();
                d.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Veicolo v, string connString)
        {
            using (OleDbConnection connection = new OleDbConnection(connString)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                UpdateVeicoloCommand u = new UpdateVeicoloCommand(connection);
                OleDbCommand cmd = u.GetCommand();
                u.AggiungiParametri(v);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
