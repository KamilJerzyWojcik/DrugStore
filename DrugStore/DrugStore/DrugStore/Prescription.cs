using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	public class Prescription : ActiveRecord, IActiveRecord
	{
		public string CustomerName { get; private set; }
		public string PESEL { get; private set; }
		public string PrescriptionNumber { get; private set; }


		public Prescription() { }

		public Prescription(int id)
		{
			ID = id;
		}

		public void Reload()
		{
			try
			{
				if (ID == 0) throw new Exception("Invalid prescription ID");

				Open();
				sqlCommand.CommandText = $"Select * From Prescriptions Where PrescriptionID = '{ID}';";
				var sqlReader = sqlCommand.ExecuteReader();
				if (!sqlReader.Read()) { throw new Exception("Prescription doesn't exist!"); }

				do
				{
					CustomerName = sqlReader.GetString(1);
					PESEL = sqlReader.GetString(2);
					PrescriptionNumber = sqlReader.GetString(3);
				} while (sqlReader.Read());

				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				Close();
			}
		}

		public void Remove()
		{
			try
			{
				if (ID == 0) throw new Exception("Invalid prescription ID");

				Open();

				sqlCommand.CommandText = $"Delete From Prescriptions Where PrescriptionID = '{ID}';";
				var sqlReaderDelete = sqlCommand.ExecuteNonQuery();
				Console.WriteLine("prescription ID: " + ID + " Was deleted");
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				Close();
			}
		}

		public new int Save()
		{
			SetParameters();
			Console.Clear();
			try
			{

				Open();
				sqlCommand.CommandText = $"Select * From Prescriptions Where PrescriptionNumber = '{PrescriptionNumber}';";
				var sqlReaderCheck = sqlCommand.ExecuteReader();
				if (sqlReaderCheck.Read()) throw new Exception("Prescription exist!");
				Close();

				Open();
				sqlCommand.CommandText = $"Insert into Prescriptions(CustomerName, PESEL, PrescriptionNumber) Values ('{CustomerName}', '{PESEL}', '{PrescriptionNumber}');";
				var sqlReader = sqlCommand.ExecuteNonQuery();
				Console.WriteLine("Number of changed records: " + sqlReader);

				sqlCommand.CommandText = $"Select SCOPE_IDENTITY()";
				var sqlReaderID = sqlCommand.ExecuteReader();
				sqlReaderID.Read();
				ID = int.Parse(sqlReaderID.GetDecimal(0).ToString());
				//Console.WriteLine("ID new prescription: " + ID);

				Close();
				return ID;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Close();
				return 0;
			}
		}

		private void SetParameters()
		{
			try
			{

				Console.Write("Set customer first and last name: ");
				CustomerName = Console.ReadLine().ToString().Trim();
				if (CustomerName == "" || !CustomerName.Contains(" ")) throw new Exception("Incorect Customer Name");

				Console.Write("Set customer PESEL: ");
				PESEL = Console.ReadLine().ToString().Trim();
				if (!CheckPesel(PESEL)) throw new Exception("Incorect PESEL");

				Console.Write("Set prescription number: ");
				PrescriptionNumber = Console.ReadLine().ToString().Trim();
				if (PrescriptionNumber.Length != 20) throw new Exception("Incorect prescription number");

			}
			catch (FormatException e)
			{
				Console.WriteLine("Incorrect Data: ", e.Message);
				Save();
			}
			catch (Exception e)
			{
				Console.WriteLine("Incorrect Data: ", e.Message);
				Save();
			}
		}

		private bool CheckPesel(string PESEL)
		{
			if (PESEL == "") return false;
			if (PESEL.Length != 11) return false;

			int[] wages = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
			int result = 0;
			bool boolResult = false;

			try
			{
				for (int i = 0; i <= wages.Length - 1; i++)
				{
					result += int.Parse(PESEL[i].ToString()) * wages[i];
				}
				result = result % 10;
				result = 10 - result;
				result = result % 10;
				if (result == int.Parse((PESEL[10].ToString()))) boolResult = true;
			}
			catch (FormatException e)
			{
				Console.WriteLine("Incorect PESEL format: " + e.Message);
				return false;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				return false;
			}

			return boolResult;
		}
	}
}
