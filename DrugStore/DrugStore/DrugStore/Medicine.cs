using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	public class Medicine : ActiveRecord
	{
		public string Name { get; private set; }
		public string Manufacturer { get; private set; }
		public decimal Price { get; private set; }
		public int Amount { get; private set; }
		public bool WithPrescription { get; private set; }

		public Medicine() { }

		public Medicine(int id)
		{
			ID = id;
		}

		public void Reload()
		{
			try
			{
				if (ID == 0) throw new Exception("Invalid Medicine ID");

				Open();
				sqlCommand.CommandText = $"Select * From Medicines Where MedicineID = '{ID}';";
				var sqlReader = sqlCommand.ExecuteReader();
				if (!sqlReader.Read()) { throw new Exception("Medicine doesn't exist!"); }

				do
				{

					Name = sqlReader.GetString(1);
					Manufacturer = sqlReader.GetString(2);
					Price = sqlReader.GetDecimal(3);
					Amount = sqlReader.GetInt32(4);
					WithPrescription = sqlReader.GetBoolean(5);

				} while (sqlReader.Read());

				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				Close();
			}

		}

		public void Update()
		{
			try
			{
				Console.Write("Medicine ID: ");
				string id = Console.ReadLine().ToString();
				Medicine OldMedicine = new Medicine(int.Parse(id));
				OldMedicine.Reload();

				Console.Clear();
				Console.WriteLine("What change?");
				Console.WriteLine("Name: N");
				Console.WriteLine("Manufacturer: M");
				Console.WriteLine("Price: P");
				Console.WriteLine("Amount: A");
				Console.WriteLine("WithPrescription: WP");
				string command = Console.ReadLine().ToString().Trim();
				string param = "";

				switch (command)
				{
					case "N":
						param = "Name";
						Console.Clear();
						Console.WriteLine($"Old value {OldMedicine.Name}");
						Console.Write("New value: ");
						command = Console.ReadLine().ToString().Trim();
						break;
					case "M":
						param = "Manufacturer";
						Console.Clear();
						Console.WriteLine($"Old value {OldMedicine.Manufacturer}");
						Console.Write("New value: ");
						command = Console.ReadLine().ToString().Trim();
						break;
					case "P":
						param = "Price";
						Console.Clear();
						Console.WriteLine($"Old value {OldMedicine.Price}");
						Console.Write("New value: ");
						command = Console.ReadLine().ToString().Trim();
						break;
					case "A":
						param = "Amount";
						Console.Clear();
						Console.WriteLine($"Old value {OldMedicine.Amount}");
						Console.Write("New value: ");
						command = Console.ReadLine().ToString().Trim();
						break;
					case "WP":
						param = "WithPrescription";
						Console.Clear();
						Console.WriteLine($"Old value {OldMedicine.WithPrescription}");
						Console.Write("New value (y/n): ");
						command = Console.ReadLine().ToString().Trim();
						if (command == "y") command = "1";
						else if (command == "n") command = "0";
						else Console.WriteLine("Wrong format"); Update();
						break;
				}

				Open();
				sqlCommand.CommandText = $"Update Medicines Set {param} = '{command}' Where MedicineID = {id};";
				var sqlReaderCheck = sqlCommand.ExecuteNonQuery();
				Close();



				if (Name == "") throw new Exception("Incorect Medicine Name");
			}
			catch (FormatException e)
			{
				Console.WriteLine("Invalid format, try again, " + e.Message);
				Console.Clear();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error, try again, " + e.Message);
				Console.Clear();
			}
		}

		public void Remove()
		{
			try
			{
				if (ID == 0) throw new Exception("Invalid medicine ID");

				Open();

				sqlCommand.CommandText = $"Delete From Medicines Where MedicineID = '{ID}';";
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

			try
			{
				if (!SetParameters(out int WithPrescriptionBite)) throw new Exception("Invalid Parameters, try again");
				Console.Clear();

				Open();
				sqlCommand.CommandText = $"Select * From Medicines Where Name = '{Name}' and Manufacturer = '{Manufacturer}';";
				var sqlReaderCheck = sqlCommand.ExecuteReader();
				if (sqlReaderCheck.Read()) throw new Exception("Medicine exist!");
				Close();

				Open();
				sqlCommand.CommandText = $"Insert into Medicines(Name, Manufacturer, Price, Amount, WithPrescription) Values ('{Name}', '{Manufacturer}', '{Price.ToString("N", new CultureInfo("en-US", false).NumberFormat)}', '{Amount}', '{WithPrescriptionBite}');";
				var sqlReader = sqlCommand.ExecuteNonQuery();
				Console.WriteLine("Number of changed records: " + sqlReader);

				sqlCommand.CommandText = $"Select SCOPE_IDENTITY()";
				var sqlReaderID = sqlCommand.ExecuteReader();
				sqlReaderID.Read();
				ID = int.Parse(sqlReaderID.GetDecimal(0).ToString());
				Console.WriteLine("ID new Medicine : " + ID);

				Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Close();
			}

		}


		private bool SetParameters(out int WithPrescriptionBite)
		{
			WithPrescriptionBite = 0;
			try
			{

				Console.Write("Set name: ");
				Name = Console.ReadLine().ToString();

				if (Name == "") throw new Exception("Incorect Medicine Name");

				Console.Write("Set manufacturer: ");
				Manufacturer = Console.ReadLine().ToString();

				if (Manufacturer == "") throw new Exception("Incorect manufacturer Name");

				Console.Write("With prescription (y/n): ");
				string decision = Console.ReadLine().ToString();

				if (decision == "y")
				{
					WithPrescription = true; WithPrescriptionBite = 1;
				}
				else if (decision == "n")
				{
					WithPrescription = true;
					WithPrescriptionBite = 0;
				}
				else
				{
					throw new Exception("At Prescription choose y or n");
				}

				Console.Write("Set Price: ");
				Price = decimal.Parse(Console.ReadLine());

				Console.Write("Set Amount: ");
				Amount = int.Parse(Console.ReadLine());
				return true;

			}
			catch (FormatException e)
			{
				Console.WriteLine("Incorrect format Data: ", e.Message);
				Console.Clear();
				return false;
			}
			catch (Exception e)
			{
				Console.WriteLine("Incorrect Data: ", e.Message);
				Console.Clear();
				return false;
			}


		}
	}
}
