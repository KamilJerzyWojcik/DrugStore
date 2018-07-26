using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	public class Order : ActiveRecord, IActiveRecord
	{
		public int MedicineID { get; private set; }
		public int PrescrionID { get; private set; }
		public DateTime Date { get; private set; }
		public int Amount { get; set; }
		private Medicine _medicine = new Medicine();
		private Prescription _prescription = new Prescription();

		public Order(Medicine medicine, Prescription prescription)
		{
			try
			{
				if (medicine.ID == 0) throw new Exception("Medicine ID doesn't exist");

				MedicineID = medicine.ID;

				if (prescription.ID == 0) throw new Exception("Prescription ID doesn't exist");
				PrescrionID = prescription.ID;

				_medicine = medicine;

			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public Order(Medicine medicine)
		{
			try
			{
				if (medicine.ID == 0) throw new Exception("Medicine ID doesn't exist");

				MedicineID = medicine.ID;

				_medicine = medicine;

			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		public Order(int id)
		{
			ID = id;
		}

		public void Reload()
		{
			try
			{
				if (ID == 0) throw new Exception("Invalid order ID");

				Open();
				sqlCommand.CommandText = $"Select * From Orders Where OrdersID = '{ID}';";
				var sqlReader = sqlCommand.ExecuteReader();
				if (!sqlReader.Read()) { throw new Exception("Order doesn't exist!"); }

				do
				{

					MedicineID = sqlReader.GetInt32(1);
					PrescrionID = sqlReader.GetInt32(2);
					Date = sqlReader.GetDateTime(3);
					Amount = sqlReader.GetInt32(4);

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
				if (ID == 0) throw new Exception("Invalid order ID");

				Open();

				sqlCommand.CommandText = $"Delete From Orders Where OrderID = '{ID}';";
				var sqlReaderDelete = sqlCommand.ExecuteNonQuery();

				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				Close();
			}
		}

		public override void Save()
		{
			SetParameters();
			Console.Clear();
			try
			{

				Open();
				sqlCommand.CommandText = $"Select * From Orders Where MedicineID = '{MedicineID}' and PrescriptionID = '{PrescrionID}';";
				var sqlReaderCheck = sqlCommand.ExecuteReader();
				if (sqlReaderCheck.Read()) throw new Exception("Order exist!");
				Close();
				Open();
				if (_medicine.WithPrescription)
				{
					sqlCommand.CommandText = $"Insert into Orders(MedicineID, PrescriptionID, Date, Amount) Values ('{MedicineID}', '{PrescrionID}', '{Date}', '{Amount}');";
				}
				else
				{
					sqlCommand.CommandText = $"Insert into Orders(MedicineID, Date, Amount) Values ('{MedicineID}', '{Date}', '{Amount}');";
				}

				var sqlReader = sqlCommand.ExecuteNonQuery();
				Console.WriteLine("Number of changed records: " + sqlReader);

				sqlCommand.CommandText = $"Select SCOPE_IDENTITY()";
				var sqlReaderID = sqlCommand.ExecuteReader();
				sqlReaderID.Read();
				ID = int.Parse(sqlReaderID.GetDecimal(0).ToString());
				Console.WriteLine("ID new Order : " + ID);

				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Close();
			}

		}

		private void SetParameters()
		{
			try
			{
				Console.Write("Set Date (yyyy-mm-dd): ");
				Date = DateTime.Parse(Console.ReadLine());

				Console.Write("Set Amount: ");
				Amount = int.Parse(Console.ReadLine());

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
	}
}
