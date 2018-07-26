using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	public abstract class ActiveRecord
	{
		static string connectionString = "Integrated Security=SSPI;Data Source=.\\SQLEXPRESS;Initial Catalog=DrugStoreDataBase;";
		public int ID { get; protected set; }
		protected SqlCommand sqlCommand = new SqlCommand();
		protected SqlConnection sqlConnection;

		public virtual void Save()
		{

		}

		protected virtual void Open()
		{
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			sqlCommand.Connection = sqlConnection;
		}

		protected virtual void Close()
		{
			sqlConnection?.Close();
		}

		~ActiveRecord()
		{
			sqlConnection?.Close();
		}
	}
}
