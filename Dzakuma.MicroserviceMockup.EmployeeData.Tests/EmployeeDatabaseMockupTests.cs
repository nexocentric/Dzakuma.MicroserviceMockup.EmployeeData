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
	}
}
