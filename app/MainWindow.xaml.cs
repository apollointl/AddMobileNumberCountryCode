using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static AddMobileNumberCountryCode.Utils.FileOps;

namespace AddMobileNumberCountryCode.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string FileName { get; private set; }
		public string FileHeader { get; private set; }
		public string[] FileData { get; set; }
		public int ColumnIndex { get; set; }

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OpenFile(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog { Filter = "CSV files|*.csv", };

			var result = openFileDialog.ShowDialog();
			if (result == true)
			{
				FileName = openFileDialog.FileName;
				fileNameTxt.Text = FileName;
				var content = File.ReadAllLines(FileName);
				FileHeader = content[0];
				columnHeadersComboBox.ItemsSource = FileHeader.Split(',');
				FileData = content.Skip(1).ToArray();
				columnHeadersComboBox.IsEnabled = true;
			}
		}

		private void SetColumnIndex(object sender, SelectionChangedEventArgs e)
		{
			ColumnIndex = columnHeadersComboBox.SelectedIndex;
			colFirstValueTxt.Text = "First value in this column: " + FileData[0].Split(',')[ColumnIndex];
			convertButton.IsEnabled = true;
		}

		private void Convert(object sender, RoutedEventArgs e)
		{
			var lines = Prepend61(FileData, ColumnIndex);
			var newFileLines = new List<string>();
			newFileLines.Add(FileHeader);
			newFileLines.AddRange(lines);
			var msg = "Save the new data in a new file?";
			var msgBox = MessageBox.Show(this, msg, "Result", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (msgBox == MessageBoxResult.Yes)
			{
				var (fileDir, fileName) = GetDirFileName(FileName);
				var SaveFile = new SaveFileDialog
				{
					FileName = fileName,
					InitialDirectory = fileDir,
					Filter = "CSV files|*.csv",
				};

				if (SaveFile.ShowDialog() == true)
					File.WriteAllLines(Path.Join(fileDir, fileName), newFileLines.ToArray());
			}
		}
	}
}
