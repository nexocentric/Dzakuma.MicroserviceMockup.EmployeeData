using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
	public class EmployeeDatabaseMockupTests
	{
		public class OutputPersonnelList
		{
			[Fact]
			public void ShouldReturnJsonArrayString_WhenCalled()
			{
				Assert.Matches(
					@"^\[\s*\{\s*[\S\s]*\}\s*\]\s*",
					new EmployeeDatabaseMockup().OutputPersonnelList()
				);
			}
		}

		public class OutputSinglePersion
		{
			[Fact]
			public void ShouldReturnJsonObjectString_OfAllPersonData_WhenCalled()
			{
				Assert.Matches(
					@"^\{[\s\S]+id[\s\S]+first_name[\s\S]+last_name[\s\S]+gender[\s\S]+most_fa[\s\S]+second_fa[\s\S]+mos[\s\S]+favorite_animal[\s\S]+department[\s\S]+\}[\s\S]*",
					new EmployeeDatabaseMockup().OutputSinglePerson(1)
				);
			}
		}

		public class OutputMoviePreferences
		{
			[Fact]
			public void ShouldReturnJsonObjectString_OfMoviePreferences_WhenCalled()
			{
				Assert.Matches(
					@"^\{[\s\S]+most_fa[\s\S]+second_fa[\s\S]+mos[\s\S]+\}[\s\S]*",
					new EmployeeDatabaseMockup().OutputMoviePreferences(1)
				);
			}
		}

		public class OutputGeneralInformation
		{
			[Fact]
			public void ShouldReturnJsonObjectString_OfGeneralInformation_WhenCalled()
			{
				Assert.Matches(
					@"^\{[\s\S]+id[\s\S]+first_name[\s\S]+last_name[\s\S]+gender[\s\S]+department[\s\S]+\}[\s\S]*",
					new EmployeeDatabaseMockup().OutputGeneralInformation(1)
				);
			}
		}
	}
}
