using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dzakuma.MicroserviceMockup.EmployeeData
{
	public class EmployeeDatabaseMockup
	{
		string _dataSource = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\Data\person-information.json";
		public string OutputPersonnelList()
		{
			var jsonData = JArray.Parse(File.ReadAllText(_dataSource));
			return jsonData.ToString();
		}
	}
}
