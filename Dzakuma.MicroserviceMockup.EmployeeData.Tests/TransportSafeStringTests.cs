using System;
using Xunit;

namespace Dzakuma.MicroserviceMockup.EmployeeData.Tests
{
	public class TransportSafeStringTests
	{
		public class Encode
		{
			[Fact]
			public void ShouldReturn_Base64EncodedString_WhenPassedAnyString()
			{
				string inputString = "something";
				string encodedString = TransportSafeString.Encode(inputString);

				//if an exception is thrown here there is a problem with the
				//conversion class
				byte[] HashBytes = Convert.FromBase64String(encodedString);
				Assert.NotEqual(encodedString, inputString);
			}
		}

		public class Decode
		{
			[Fact]
			public void ShouldReturn_DecodedString_WhenPassedAnEncodedString()
			{
				string inputString = "something";
				string encodedString = TransportSafeString.Encode(inputString);
				string decodedString = TransportSafeString.Decode(encodedString);
				Assert.Equal(inputString, decodedString);
			}

			//TODO: You should probably put a test here to check what happens if
			//      a non base64 string is attempted to be decoded
		}
	}
}
