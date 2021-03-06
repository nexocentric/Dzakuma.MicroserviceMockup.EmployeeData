﻿using System;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
	public class EmployeeDataServiceTests
	{
		public class ValidateProgramArguments
		{
			[Fact]
			public void ShouldReturn_False_IfCalledWithTooManyArguments()
			{
				string arguments = "-a -b -c -d -e -f -g -i -j -k -l -m -n -o -p -q";
				Assert.False(EmployeeDataService.ValidateProgramArguments(arguments.Split(' ')));
			}

			[Fact]
			public void ShouldReturn_False_IfCalledWithNoArguments()
			{
				Assert.False(EmployeeDataService.ValidateProgramArguments(new string[] {}));
			}

			[Fact]
			public void ShouldReturn_False_IfCalledWithCompletelyBadArguments()
			{
				string arguments = "- -";
				Assert.False(EmployeeDataService.ValidateProgramArguments(arguments.Split(' ')));
			}

			[Theory]
			[InlineData("-v -i")]
			[InlineData("-h -i")]
			[InlineData("-t -i")]
			[InlineData("-m -i")]
			[InlineData("-g -i")]
			[InlineData("-a -i")]
			public void ShouldReturn_True_IfCalledWithTheAppropriateArguments(string arguments)
			{
				Assert.False(EmployeeDataService.ValidateProgramArguments(arguments.Split(' ')));
			}
		}

		public class InitializeProgramOptions
		{
			[Fact]
			public void ShouldReturn_SevenOptions_WhenCalled()
			{
				Assert.Equal(
					7,
					EmployeeDataService.InitializeProgramOptions().Count
				);
			}
		}

		public class DisplayVersionInformation
		{
			//TODO: implement display version information tests
			/*[Fact]
			public void ShouldReturn_VersionDisplayedEnum_WhenCalled()
			{
				EmployeeDataService.InitializeProgramOptions();
				Assert.Equal(
					(int)EmployeeDataService.RetunCodes.VersionInformationDisplayed,
					EmployeeDataService.DisplayVersionInformation()
				);
			}*/
		}

		public class DisplayHelp
		{
			[Fact]
			public void ShouldReturn_HelpDisplayedEnum_WhenCalled()
			{
				EmployeeDataService.InitializeProgramOptions();
				Assert.Equal(
					(int)EmployeeDataService.RetunCodes.HelpDisplayed,
					EmployeeDataService.DisplayHelp()
				);
			}
		}

		public class RunTests
		{
			[Fact]
			public void ShouldReturn_TestsRunEnum_WhenCalled()
			{
				EmployeeDataService.InitializeProgramOptions();
				Assert.Equal(
					(int)EmployeeDataService.RetunCodes.TestsRun,
					EmployeeDataService.RunTests()
				);
			}
		}

		public class EncodeTextForCommandLine
		{
			[Fact]
			public void ShouldReturn_Base64EncodedString_WhenPassedAnyString()
			{
				string inputString = "something";
				string encodedString = EmployeeDataService.EncodeTextForCommandLine(inputString);

				//if an exception is thrown here there is a problem with the
				//conversion class
				byte[] HashBytes = Convert.FromBase64String(encodedString);
				Assert.NotEqual(encodedString, inputString);
			}
		}

		public class OutputSinglePersonById
		{
			[Fact]
			public void ShouldReturn_BlankString_WhenGivenBadId()
			{
				Assert.Empty(EmployeeDataService.OutputSinglePersonById(0));
			}
		}

		public class OutputMoviePreferenceById
		{
			[Fact]
			public void ShouldReturn_BlankString_WhenGivenBadId()
			{
				Assert.Empty(EmployeeDataService.OutputMoviePreferencesById(0, true));
			}

			[Fact]
			public void ShouldReturn_BlankString_WhenOutputFlagIsFalse()
			{
				Assert.Empty(EmployeeDataService.OutputMoviePreferencesById(1, false));
			}
		}

		public class OutputGeneralInformationById
		{
			[Fact]
			public void ShouldReturn_BlankString_WhenGivenBadId()
			{
				Assert.Empty(EmployeeDataService.OutputGeneralInformationById(0, true));
			}

			[Fact]
			public void ShouldReturn_BlankString_WhenOutputFlagIsFalse()
			{
				Assert.Empty(EmployeeDataService.OutputGeneralInformationById(1, false));
			}
		}

		public class OutputPersonnelList
		{
			[Fact]
			public void ShouldReturn_BlankString_WhenOutputFlagIsFalse()
			{
				Assert.Empty(EmployeeDataService.OutputPersonnelList(false));
			}
		}

		//TODO: part of the output html test
		/*public class OutputPersonnelListHtml
		{
			[Fact]
			public void ShouldReturn_BlankString_WhenOutputAndHtmlFlagsAreFalse()
			{
				//this tests the optional false
				Assert.Empty(EmployeeDataService.OutputPersonnelList(false));
				//this tests the explicit false
				Assert.Empty(EmployeeDataService.OutputPersonnelList(false, false));
			}
		}*/
	}
}
