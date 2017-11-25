using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Options;
using NLog;
using NLog.Targets;

namespace Dzakuma.MicroserviceMockup.EmployeeData
{
	public class EmployeeDataService
	{
		private static bool _outputPersonnelList = false;
		private static bool _outputMoviePreferences = false;
		private static bool _outputGeneralInformation = false;
		private static bool _displayVersionInformation = false;
		private static bool _runTests = false;
		private static bool _displayHelp = false;
		private static uint _selectedId;

		private static OptionSet _programOptions;
		private static Logger _internalLogger = LogManager.GetCurrentClassLogger();

		public enum RetunCodes
		{
			FatalError = -1,
			NormalOperation = 0,
			HelpDisplayed,
			VersionInformationDisplayed,
			TestsRun
		}

		static int Main(string[] args)
		{
			try
			{
				if (!ValidateProgramArguments(args))
				{
					_displayHelp = true;
				}

				if (_displayHelp)
				{
					DisplayHelp();
					return (int)RetunCodes.HelpDisplayed;
				}

				if (_displayVersionInformation)
				{
					DisplayVersionInformation();
					return (int)RetunCodes.VersionInformationDisplayed;
				}

				if (_runTests)
				{
					RunTests();
					return (int)RetunCodes.TestsRun;
				}

				OutputSinglePersonById(_selectedId);
				OutputMoviePreferences(_selectedId, _outputMoviePreferences);
				OutputGeneralInformation(_selectedId, _outputGeneralInformation);
				OutputPersonnelList(_outputPersonnelList);

				return (int)RetunCodes.NormalOperation;
			}
			catch (Exception anomaly)
			{
				_internalLogger.Fatal(anomaly, "An unhandled error was trapped by the main process of this program.");
				return (int)RetunCodes.FatalError;
			}
		}

		static bool ValidateProgramArguments(string[] args)
		{
			try
			{
				_programOptions = InitializeProgramOptions();
				var extraParameters = _programOptions.Parse(args);
				if (extraParameters.Count > 0)
				{
					_internalLogger.Trace("The following arguments were extra.");
					return false;
				}

				if (!_outputPersonnelList && _selectedId == 0)
				{
					_internalLogger.Trace("No appropriate options were selected.");
					return false;
				}

				return true;
			}
			catch (OptionException anomaly)
			{
				_internalLogger.Warn(anomaly, "Invalid arguments were passed to the program.");
				return false;
			}
		}

		static OptionSet InitializeProgramOptions()
		{
			return new OptionSet
			{
				{ "v|version", "Version information for this program.", o => { if (o != null) { _displayVersionInformation = true; } } },
				{ "h|help", "Displays this message and exits.", o => { if (o != null) { _displayHelp = true; } } },
				{ "t|test", "Runs diagnostic tests on this service.", o => { if (o != null) { _runTests = true; } } },
				{ "i|id", "Gets information for the person matching the specified ID.", (uint o) => _selectedId = o },
				{ "m|movie", "Gets the movie preferences of the specified ID. ID must be specified.", o => { if (o != null) { _outputMoviePreferences = true; } } },
				{ "g|general", "Gets general information for the specified ID. ID must be specified.", o => { if (o != null) { _outputGeneralInformation = true; } } },
				{ "a|all", "Gets a list of all personnel.", o => { if (o != null) { _outputPersonnelList = true; } } },
			};
		}

		private static void DisplayHelp()
		{
			//short application usage message
			Console.Error.WriteLine("Usage: Dzakuma.MicroserviceMockup.EmployeeData.exe -[options]");
			Console.Error.WriteLine("Returns the specified employee data to the command line in JSON format.");
			Console.Error.WriteLine();
			Console.Error.WriteLine("If no parameters are specified displays help message.");
			Console.Error.WriteLine();

			// output the options
			Console.Error.WriteLine("Options:");
			_programOptions.WriteOptionDescriptions(Console.Error);
		}

		private static void DisplayVersionInformation()
		{
			throw new NotImplementedException();
		}

		private static void RunTests()
		{
			throw new NotImplementedException();
		}

		private static void OutputSinglePersonById(uint selectedId)
		{
			throw new NotImplementedException();
		}

		private static void OutputMoviePreferences(uint selectedId, bool outputMoviePreferences)
		{
			throw new NotImplementedException();
		}

		private static void OutputGeneralInformation(uint selectedId, bool outputGeneralInformation)
		{
			throw new NotImplementedException();
		}

		private static void OutputPersonnelList(bool outputPersonnelList)
		{
			throw new NotImplementedException();
		}
	}
}
