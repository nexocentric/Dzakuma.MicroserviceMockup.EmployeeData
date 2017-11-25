using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
    public class EmployeDataExecutableTests
    {
	    private string _testProgramName = "Dzakuma.MicroserviceMockup.EmployeeData.exe";
	    private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";

		public int StartExecutableProgram(string programName, string arguments = "")
	    {
		    string assemblyFilePath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
		    var pathInformation = assemblyFilePath.Split(new [] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);

			var testProgram = new Process();
		    testProgram.StartInfo.FileName = _executablePath;
		    testProgram.StartInfo.Arguments = arguments;
		    testProgram.StartInfo.CreateNoWindow = true;
		    testProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			testProgram.StartInfo.UseShellExecute = true;
		    testProgram.Start();
			testProgram.WaitForExit(6000);
		    return testProgram.ExitCode;
	    }

		[Theory]
		[InlineData("--help")]
		[InlineData("--h")]
		[InlineData("-help")]
		[InlineData("-h")]
		[InlineData("/help")]
		[InlineData("/h")]
		public void ShouldReturnOneAsAnErrorCode_IfExecutableCalledWithTheHelpOption(string helpArgument)
		{
			var exitCode = StartExecutableProgram(_testProgramName, helpArgument);
			Assert.Equal(1, exitCode);
		}

	    [Fact]
	    public void ShouldReturnOneAsAnErrorCode_IfExecutableCalledWithNoOptions()
	    {
		    var exitCode = StartExecutableProgram(_testProgramName);
		    Assert.Equal(1, exitCode);
	    }

	    [Theory]
	    [InlineData("--version")]
	    [InlineData("--v")]
	    [InlineData("-version")]
	    [InlineData("-v")]
	    [InlineData("/version")]
	    [InlineData("/v")]
	    public void ShouldReturnTwoAsAnErrorCode_IfExecutableCalledWithTheHelpOption(string helpArgument)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, helpArgument);
		    Assert.Equal(2, exitCode);
	    }

	    [Theory]
	    [InlineData("--test")]
	    [InlineData("--t")]
	    [InlineData("-test")]
	    [InlineData("-t")]
	    [InlineData("/test")]
	    [InlineData("/t")]
	    public void ShouldReturnThreeAsAnErrorCode_IfExecutableCalledWithTheHelpOption(string helpArgument)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, helpArgument);
		    Assert.Equal(3, exitCode);
	    }
	}
}
