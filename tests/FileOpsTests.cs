using System;
using Xunit;
using static AddMobileNumberCountryCode.Utils.FileOps;

namespace AddMobileNumberCountryCode.Tests
{
	public class FileOpsTests
	{
		[Fact]
		public void CanGetDirFileName()
		{
			var (fileDir, fileName) = GetDirFileName(@"C:\Users\ahad\Data.csv");
			Assert.Equal(@"C:\Users\ahad", fileDir);
			Assert.Equal("Data (Updated).csv", fileName);
		}

		[Fact]
		public void CanAppend61()
		{
			var original = new string[]
			{
				"Adnan,0482,Bankstown,adnan@gmail.com",
				"Boris,482,Lakemba,boris@hotmail.com",
				"Rahul,234,Newcastle,rahul@yahoo.com",
			};

			var modified =  new string[]
			{
				"Adnan,61482,Bankstown,adnan@gmail.com",
				"Boris,61482,Lakemba,boris@hotmail.com",
				"Rahul,234,Newcastle,rahul@yahoo.com",
			};

			Assert.Equal(modified, Prepend61(original, 1));
		}
	}
}
