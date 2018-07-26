using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	class Program
	{

		static void Main(string[] args)
		{
			string command = "";
			ShowMedicines showMedicines = new ShowMedicines();
			do
			{
				switch (command)
				{
					case "AM":
						Console.Clear();
						AddMedicine();
						Console.WriteLine();
						break;
					case "EM":
						Console.Clear();
						EditMedicine();
						break;
					case "DM":
						Console.Clear();
						DeleteMedicine();
						break;
					case "SA":
						Console.Clear();
						showMedicines.SelectAllMedicines();
						break;
					case "O":
						Console.Clear();
						AddOrder();
						break;
					case "DP":
						Console.Clear();
						showMedicines.SelectAllPrescription();
						RemovePrescription();
						break;
					case "DO":
						Console.Clear();
						showMedicines.SelectAllOrders();
						RemoveOrder();
						break;
					case "SM":
						Console.Clear();
						showMedicines.SearchMedicine();
						break;
					default:
						Console.Clear();
						break;
				}

				Console.WriteLine($"Exit: exit");
				Console.WriteLine($"Add Medicine: AM");
				Console.WriteLine($"Edit Medicine: EM");
				Console.WriteLine($"Delete Medicine: DM");
				Console.WriteLine($"Show all Medicine: SA");
				Console.WriteLine($"Order: O");
				Console.WriteLine($"Delete Prescription: DP");
				Console.WriteLine($"Delete Order: DO");
				Console.WriteLine($"Search Medicine: SM");
				Console.WriteLine();
				Console.Write("Write command: ");
				command = Console.ReadLine();

				if (command == "exit") break;

			} while (true);
		}


		private static void AddMedicine()
		{
			Medicine medicine = new Medicine();
			medicine.Save();
		}

		private static void DeleteMedicine()
		{
			Console.Write("Enter ID: ");
			try
			{
				Medicine medicine = new Medicine(int.Parse(Console.ReadLine()));
				medicine.Remove();
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void EditMedicine()
		{
			Medicine medicine = new Medicine();
			medicine.Update();
		}

		private static void RemovePrescription()
		{

			Console.Write("Enter Prescription ID: ");
			try
			{
				Prescription prescription = new Prescription(int.Parse(Console.ReadLine()));
				prescription.Remove();
				Console.Clear();
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
				Console.Clear();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Clear();
			}
		}

		private static void RemoveOrder()
		{
			Console.Write("Enter Order ID: ");
			try
			{
				Order order = new Order(int.Parse(Console.ReadLine()));
				order.Remove();
				Console.Clear();
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
				Console.Clear();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.Clear();
			}
		}

		private static void AddOrder()
		{
			try
			{
				Console.Clear();
				Console.Write("Enter ID medicine: ");
				Medicine medicine = new Medicine(int.Parse(Console.ReadLine()));

				medicine.Reload();
				Order order;

				if (medicine.WithPrescription == true)
				{
					int id = AddPrescription();
					if (id == 0)
					{
						Console.WriteLine("Error, try again");
						return;
					}

					Prescription prescription = new Prescription(id);
					order = new Order(medicine, prescription);
					order.Save();
					return;
				}

				order = new Order(medicine);
				order.Save();
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static int AddPrescription()
		{
			Prescription prescription = new Prescription();
			return prescription.Save();
		}
	}

}
