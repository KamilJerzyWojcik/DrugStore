using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore
{
	public interface IActiveRecord
	{
		void Reload();
		void Remove();
	}
}
