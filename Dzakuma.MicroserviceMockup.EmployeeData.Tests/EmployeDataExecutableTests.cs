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
		public void ShouldReturnHelpDispayedErrorCode_IfExecutableCalledWithHelpArgument(string argument)
		{
			var exitCode = StartExecutableProgram(_testProgramName, argument);
			Assert.Equal((int)EmployeeDataService.RetunCodes.HelpDisplayed, exitCode);
		}

	    [Fact]
	    public void ShouldReturnHelpDisplayedErrorCode_IfExecutableCalledWithNoArguments()
	    {
		    var exitCode = StartExecutableProgram(_testProgramName);
			Assert.Equal((int)EmployeeDataService.RetunCodes.HelpDisplayed, exitCode);
		}

	    [Theory]
	    [InlineData("--version")]
	    [InlineData("--v")]
	    [InlineData("-version")]
	    [InlineData("-v")]
	    [InlineData("/version")]
	    [InlineData("/v")]
	    public void ShouldReturnVersionInformationDisplayedErrorCode_IfExecutableCalledWithVersionArgument(string argument)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, argument);
			Assert.Equal((int)EmployeeDataService.RetunCodes.VersionInformationDisplayed, exitCode);
		}

	    [Theory]
	    [InlineData("--test")]
	    [InlineData("--t")]
	    [InlineData("-test")]
	    [InlineData("-t")]
	    [InlineData("/test")]
	    [InlineData("/t")]
	    public void ShouldReturnTestsRuneErrorCode_IfExecutableCalledWithTestArgument(string argument)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, argument);
			Assert.Equal((int)EmployeeDataService.RetunCodes.TestsRun, exitCode);
		}
	}
}
