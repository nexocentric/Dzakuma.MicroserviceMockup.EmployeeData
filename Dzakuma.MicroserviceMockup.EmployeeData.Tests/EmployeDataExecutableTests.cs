using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
    public class EmployeDataExecutableTests
    {
	    private string _testProgramName = "Dzakuma.MicroserviceMockup.EmployeeData.exe";
	    private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";

	    public int StartExecutableProgram(string programName, string arguments = "")
	    {
		    string tempData;
		    return StartExecutableProgram(programName, arguments, out tempData);
	    }

		public int StartExecutableProgram(string programName, string arguments, out string standardErrorOutput)
	    {
		    string assemblyFilePath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
		    var pathInformation = assemblyFilePath.Split(new [] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);

			var testProgram = new Process();
		    testProgram.StartInfo.FileName = _executablePath;
		    testProgram.StartInfo.Arguments = arguments;
		    testProgram.StartInfo.CreateNoWindow = true;
		    testProgram.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			testProgram.StartInfo.UseShellExecute = false;
		    testProgram.StartInfo.RedirectStandardError = true;
		    testProgram.Start();
			testProgram.WaitForExit(6000);

		    standardErrorOutput = testProgram.StandardError.ReadToEnd().Trim();

		    return testProgram.ExitCode;
	    }

		[Theory]
		[InlineData("--help")]
		[InlineData("--h")]
		[InlineData("-help")]
		[InlineData("-h")]
		[InlineData("/help")]
		[InlineData("/h")]
		public void ShouldReturn_HelpDispayedErrorCode_IfExecutableCalledWithHelpArgument(string argument)
		{
			var exitCode = StartExecutableProgram(_testProgramName, argument);
			Assert.Equal((int)EmployeeDataService.RetunCodes.HelpDisplayed, exitCode);
		}

	    [Fact]
	    public void ShouldReturn_HelpDisplayedErrorCode_IfExecutableCalledWithNoArguments()
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
	    public void ShouldReturn_VersionInformationDisplayedErrorCode_IfExecutableCalledWithVersionArgument(string arguments)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, arguments);
			Assert.Equal((int)EmployeeDataService.RetunCodes.VersionInformationDisplayed, exitCode);
		}

	    [Theory]
	    [InlineData("--test")]
	    [InlineData("--t")]
	    [InlineData("-test")]
	    [InlineData("-t")]
	    [InlineData("/test")]
	    [InlineData("/t")]
	    public void ShouldReturn_TestsRunErrorCode_IfExecutableCalledWithTestArgument(string arguments)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, arguments);
			Assert.Equal((int)EmployeeDataService.RetunCodes.TestsRun, exitCode);
		}

	    [Theory]
	    [InlineData("--all")]
	    public void ShouldReturn_NormalOperationErrorCode_IfExecutableCalledStandardParameters(string arguments)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, arguments);
		    Assert.Equal((int)EmployeeDataService.RetunCodes.NormalOperation, exitCode);
	    }

		[Theory]
	    [InlineData("--all")]
	    [InlineData("-a")]
	    [InlineData("/a")]
	    [InlineData("--general -id 1")]
	    [InlineData("-g -i=1")]
	    [InlineData("/g /i 1")]
		[InlineData("--movie -id 1")]
	    [InlineData("-m -i=1")]
	    [InlineData("/m /i 1")]
		public void ShouldReturn_Base64EncodedDataString_IfExecutableCalledWithOutputArguments(string arguments)
	    {
		    var exitCode = StartExecutableProgram(_testProgramName, arguments, out var encodedString);
		    byte[] HashBytes = Convert.FromBase64String(encodedString);
			Assert.NotEmpty(encodedString);
			Assert.Equal((int)EmployeeDataService.RetunCodes.NormalOperation, exitCode);
	    }
	}
}
