using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	class ShowMedicines : ActiveRecord
	{
		public void SelectAllMedicines()
		{
			try
			{
				Open();
				sqlCommand.CommandText = "Select * From Medicines;";
				var sqlReader = sqlCommand.ExecuteReader();

				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();
				Console.Write("ID | ".PadLeft(7));
				Console.Write("Name | ".PadLeft(25));
				Console.Write("Manufacturer | ".PadLeft(20));
				Console.Write("Price | ".PadLeft(10));
				Console.Write("Amount | ".PadLeft(10));
				Console.Write("Prescription| ".PadLeft(15));
				Console.WriteLine();
				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();

				while (sqlReader.Read())
				{
					Console.Write($"{sqlReader.GetInt32(0)}  | ".PadLeft(7));
					Console.Write($"{sqlReader.GetString(1)} | ".PadLeft(25));
					Console.Write($"{sqlReader.GetString(2)} | ".PadLeft(20));
					Console.Write($"{sqlReader.GetDecimal(3)} | ".PadLeft(10));
					Console.Write($"{sqlReader.GetInt32(4)} | ".PadLeft(10));
					Console.Write($"{sqlReader.GetBoolean(5)} | ".PadLeft(15));
					Console.WriteLine();
				}

				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public void SelectAllPrescription()
		{
			try
			{
				Open();
				sqlCommand.CommandText = "Select * From Prescriptions;";
				var sqlReader = sqlCommand.ExecuteReader();

				Console.Write("".PadLeft(74, '.'));
				Console.WriteLine();
				Console.Write("ID | ".PadLeft(7));
				Console.Write("Customer name | ".PadLeft(25));
				Console.Write("PESEL | ".PadLeft(20));
				Console.Write("Prescription Number  | ".PadLeft(20));
				Console.WriteLine();
				Console.Write("".PadLeft(74, '.'));
				Console.WriteLine();

				while (sqlReader.Read())
				{
					Console.Write($"{sqlReader.GetInt32(0)}  | ".PadLeft(7));
					Console.Write($"{sqlReader.GetString(1)} | ".PadLeft(25));
					Console.Write($"{sqlReader.GetString(2)} | ".PadLeft(20));
					Console.Write($"{sqlReader.GetString(3)} | ".PadLeft(20));
					Console.WriteLine();
				}

				Console.Write("".PadLeft(74, '.'));
				Console.WriteLine();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public void SelectAllOrders()
		{
			try
			{
				Open();
				sqlCommand.CommandText = "Select * From Orders;";
				var sqlReader = sqlCommand.ExecuteReader();

				Console.Write("".PadLeft(65, '.'));
				Console.WriteLine();
				Console.Write("ID | ".PadLeft(7));
				Console.Write("Date | ".PadLeft(15));
				Console.Write("Amount | ".PadLeft(10));
				Console.Write("Medicine ID | ".PadLeft(10));
				Console.Write("Prescription ID | ".PadLeft(20));
				Console.WriteLine();
				Console.Write("".PadLeft(65, '.'));
				Console.WriteLine();

				while (sqlReader.Read())
				{
					Console.Write($"{sqlReader.GetInt32(0)}  | ".PadLeft(7));
					Console.Write($"{sqlReader.GetDateTime(1).ToString("yyyy-MM-dd")} | ".PadLeft(15));
					Console.Write($"{sqlReader.GetInt32(2)} | ".PadLeft(10));
					Console.Write($"{sqlReader.GetInt32(3)} | ".PadLeft(15));
					Console.Write($"{sqlReader.GetInt32(3)} | ".PadLeft(19));
					Console.WriteLine();
				}

				Console.Write("".PadLeft(65, '.'));
				Console.WriteLine();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public void SearchMedicine()
		{
			Console.WriteLine("Search medicime: ");
			Console.WriteLine("first letter - A%");
			Console.WriteLine("last letter - %A");
			Console.WriteLine("letter in name - %A%");
			Console.Write("Search: ");
			try
			{
				string partName = Console.ReadLine();

				if (partName == "" || partName == null) throw new Exception();

				Open();
				sqlCommand.CommandText = $"Select * From Medicines Where Name like '{partName}';";
				var sqlReader = sqlCommand.ExecuteReader();

				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();
				Console.Write("ID | ".PadLeft(7));
				Console.Write("Name | ".PadLeft(25));
				Console.Write("Manufacturer | ".PadLeft(20));
				Console.Write("Price | ".PadLeft(10));
				Console.Write("Amount | ".PadLeft(10));
				Console.Write("Prescription| ".PadLeft(15));
				Console.WriteLine();
				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();

				while (sqlReader.Read())
				{
					Console.Write($"{sqlReader.GetInt32(0)}  | ".PadLeft(7));
					Console.Write($"{sqlReader.GetString(1)} | ".PadLeft(25));
					Console.Write($"{sqlReader.GetString(2)} | ".PadLeft(20));
					Console.Write($"{sqlReader.GetDecimal(3)} | ".PadLeft(10));
					Console.Write($"{sqlReader.GetInt32(4)} | ".PadLeft(10));
					Console.Write($"{sqlReader.GetBoolean(5)} | ".PadLeft(15));
					Console.WriteLine();
				}

				Console.Write("".PadLeft(86, '.'));
				Console.WriteLine();
				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}
	}
}
