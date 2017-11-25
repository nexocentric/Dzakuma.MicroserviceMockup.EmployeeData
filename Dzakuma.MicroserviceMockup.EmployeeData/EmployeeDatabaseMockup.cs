using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dzakuma.MicroserviceMockup.EmployeeData
{
	public class EmployeeDatabaseMockup
	{
		string _dataSource = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\Data\person-information.json";
		string _jsonObjectSource = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\Data\person-information-non-array.json";

		public string OutputPersonnelList()
		{
			var jsonData = JArray.Parse(File.ReadAllText(_dataSource));
			return jsonData.ToString();
		}

		public string OutputSinglePerson(uint selectedId)
		{
			var information = SelectSinglePerson(selectedId);
			return information.ToString();
		}

		public string OutputMoviePreferences(uint selectedId)
		{
			var information = SelectSinglePerson(selectedId);
			var selectedInformation = new Dictionary<string, string>
			{
				{ "id", (string)information["id"] },
				{ "most_favorite_movie", (string)information["most_favorite_movie"] },
				{ "second_favorite_movie", (string)information["second_favorite_movie"] },
				{ "most_hated_movie", (string)information["most_hated_movie"] },
			};
			return JsonConvert.SerializeObject(selectedInformation);
		}

		public string OutputGeneralInformation(uint selectedId)
		{
			var information = SelectSinglePerson(selectedId);
			var selectedInformation = new Dictionary<string, string>
			{
				{ "id", (string)information["id"] },
				{ "first_name", (string)information["first_name"] },
				{ "last_name", (string)information["last_name"] },
				{ "gender", (string)information["gender"] },
				{ "department", (string)information["department"] },
			};
			return JsonConvert.SerializeObject(selectedInformation);
		}

		public JObject SelectSinglePerson(uint selectedId)
		{
			int numberOfLinesToSkip = (int) (selectedId - 1);
			string data = File.ReadLines(_jsonObjectSource).Skip(numberOfLinesToSkip).Take(1).First();
			var jsonObject = JObject.Parse(data);
			return jsonObject;
		}
	}
}
