using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
    public class EmployeDataExecutableTests
    {
	    public int StartExecutableProgram(string programName)
	    {
			var testProgram = new Process();
		    testProgram.StartInfo.FileName = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";
		    testProgram.StartInfo.Arguments = "--help";
		    testProgram.StartInfo.CreateNoWindow = true;
		    testProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			testProgram.StartInfo.UseShellExecute = true;
		    testProgram.Start();
			testProgram.WaitForExit(6000);
		    return testProgram.ExitCode;
	    }

		[Fact]
	    public void ShouldReturnOneAsAnErrorCode_IfExecutableCalledWithTheHelpOption()
		{
			var exitCode = StartExecutableProgram("yes");
			Assert.Equal(1, exitCode);
		}
    }
}
