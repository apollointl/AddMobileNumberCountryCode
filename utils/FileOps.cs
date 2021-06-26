using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AddMobileNumberCountryCode.Utils
{
	public static class FileOps
	{
		public static IEnumerable<string> Prepend61(string[] lines, int columnIndex) =>
			lines.Where(x => x.Contains(','))
				.Select(
					line =>
					{
						var values = line.Split(',');
						var mobileNumber = values[columnIndex];
						if (mobileNumber.StartsWith("4")) values[columnIndex] = "61" + mobileNumber;
						else if (mobileNumber.StartsWith("04")) values[columnIndex] = "61" + mobileNumber.Substring(1);
						return string.Join(",", values);
					}
				);

		public static (string, string) GetDirFileName(string path)
		{
			var fileName =
				Path.GetFileName(path).Split(new string[] { ".csv" }, StringSplitOptions.RemoveEmptyEntries)[0]
				+ " (Updated).csv";
			var fileDir = Path.GetDirectoryName(path);
			return (fileDir, fileName);
		}
	}
}
