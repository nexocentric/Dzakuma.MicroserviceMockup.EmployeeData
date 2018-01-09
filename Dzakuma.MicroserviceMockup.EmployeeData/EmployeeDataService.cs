using System;
using Mono.Options;
using NLog;
using Dzakuma.MicroserviceMockup.Standardization;

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
		private static bool _outputHtml = false;

		private static EmployeeDatabaseMockup _databaseConnection = new EmployeeDatabaseMockup();
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

		public static int Main(string[] args)
		{
			try
			{
				if (!ValidateProgramArguments(args))
				{
					_displayHelp = true;
				}

				if (_displayHelp)
				{
					return DisplayHelp();
				}

				if (_displayVersionInformation)
				{
					//TODO: Illustrate how the void is bad and how it can be
					//      improved by returning something
					DisplayVersionInformation();
					return (int)RetunCodes.VersionInformationDisplayed;
				}

				if (_runTests)
				{
					return RunTests();
				}

				if (!_outputMoviePreferences && !_outputGeneralInformation && !_outputPersonnelList)
				{
					Console.Error.Write(EncodeTextForCommandLine(OutputSinglePersonById(_selectedId)));
				}
				Console.Error.Write(EncodeTextForCommandLine(OutputMoviePreferencesById(_selectedId, _outputMoviePreferences)));
				Console.Error.Write(EncodeTextForCommandLine(OutputGeneralInformationById(_selectedId, _outputGeneralInformation)));

				if (_outputHtml) { Console.Error.Write(OutputPersonnelList(_outputPersonnelList, _outputHtml)); }
				Console.Error.Write(EncodeTextForCommandLine(OutputPersonnelList(_outputPersonnelList, _outputHtml)));
			
				return (int)RetunCodes.NormalOperation;
			}
			catch (Exception anomaly)
			{
				_internalLogger.Fatal(anomaly, "An unhandled error was trapped by the main process of this program.");
				return (int)RetunCodes.FatalError;
			}
		}

		public static bool ValidateProgramArguments(string[] args)
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

				//TODO: Implement this as a fix for one of the failing tests
				//      this illustrates what good test coverage can do
				//      correct answer is args.length
				/*if (extraParameters.Count == 0)
				{
					_internalLogger.Trace("This program requires arguments.");
					return false;
				}*/

				if (!_displayVersionInformation && !_runTests && !_outputPersonnelList && _selectedId == 0)
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

		public static OptionSet InitializeProgramOptions()
		{
			return new OptionSet
			{
				{ "v|version", "Version information for this program.", o => { if (o != null) { _displayVersionInformation = true; } } },
				{ "h|help", "Displays this message and exits.", o => { if (o != null) { _displayHelp = true; } } },
				{ "t|test", "Runs diagnostic tests on this service.", o => { if (o != null) { _runTests = true; } } },
				{ "i|id=", "Gets information for the person matching the specified ID.", (uint o) => _selectedId = o },
				{ "m|movie", "Gets the movie preferences of the specified ID. ID must be specified.", o => { if (o != null) { _outputMoviePreferences = true; } } },
				{ "g|general", "Gets general information for the specified ID. ID must be specified.", o => { if (o != null) { _outputGeneralInformation = true; } } },
				{ "a|all", "Gets a list of all personnel.", o => { if (o != null) { _outputPersonnelList = true; } } },
				//TODO: implement this test for the "WEB SERVICE side of things"
				//{ "html", "Formats the output as HTML for a page.", o => { if (o != null) { _outputHtml = true; } } },
			};
		}

		public static int DisplayHelp()
		{
			DisplayVersionInformation();
			Console.Error.WriteLine();

			//short application usage message
			Console.Error.WriteLine("Usage: Dzakuma.MicroserviceMockup.EmployeeData.exe -[options]");
			Console.Error.WriteLine("Returns the specified employee data to the command line in JSON format.");
			Console.Error.WriteLine();
			Console.Error.WriteLine("If no parameters are specified displays help message.");
			Console.Error.WriteLine();

			// output the options
			Console.Error.WriteLine("Options:");
			_programOptions.WriteOptionDescriptions(Console.Error);

			return (int)RetunCodes.HelpDisplayed;
		}

		//TODO: explain why void is bad
		public static void DisplayVersionInformation()
		{
			Console.Error.Write("Program: ");
			Console.Error.Write(typeof(EmployeeDataService).Assembly.GetName().Name + " ");
			Console.Error.WriteLine(typeof(EmployeeDataService).Assembly.GetName().Version);
		}

		public static string EncodeTextForCommandLine(string text)
		{
			return TransportSafeString.Encode(text);
		}

		public static int RunTests()
		{
			Console.Error.Write("Mockup of tests run on the platform.");
			return (int) RetunCodes.TestsRun;
		}

		public static string OutputSinglePersonById(uint selectedId)
		{
			if (!(selectedId > 0)) { return ""; }
			return _databaseConnection.OutputSinglePerson(selectedId);
		}

		public static string OutputMoviePreferencesById(uint selectedId, bool outputMoviePreferences)
		{
			if (!(selectedId > 0)) { return ""; }
			if (!outputMoviePreferences) { return ""; }
			return _databaseConnection.OutputMoviePreferences(selectedId);
		}

		public static string OutputGeneralInformationById(uint selectedId, bool outputGeneralInformation)
		{
			if (!(selectedId > 0)) { return ""; }
			if (!outputGeneralInformation) { return ""; }
			return _databaseConnection.OutputGeneralInformation(selectedId);
		}

		public static string OutputPersonnelList(bool outputPersonnelList, bool outputHtml = false)
		{
			if (!outputPersonnelList) { return ""; }
			//TODO: part of the output html test
			//if (outputHtml) { return _databaseConnection.OutputPersonnelListHtml(); }
			return _databaseConnection.OutputPersonnelList();
		}
	}
}
