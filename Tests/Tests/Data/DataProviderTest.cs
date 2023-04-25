using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;
using Tests.Data;


namespace Tests.Data
{
	[TestFixture]
	public class DataProviderTest
	{
		private DataProvider data = new DataProvider();

		[Test]
		public void FilesTest()
		{
			var inputExists = File.Exists(DataProvider.InputFilename);
			var outputExists = File.Exists(DataProvider.OutputFilename);

			Assert.True(inputExists, "Файл input не существует.");
			Assert.True(outputExists, "Файл output не существует.");
		}

		[Test]
		public void InputTest()
		{
			Assert.IsNotEmpty(data.Input, "Свойство Input - пустое.");

			var input = File.ReadAllText(DataProvider.InputFilename);

			Assert.AreEqual(input, data.Input, "Текст в файле не равен записанному в Input.");
		}

		[Test]
		public void OutputTest()
		{
			var oldOutput = File.ReadAllText(DataProvider.OutputFilename);
			File.Delete(DataProvider.OutputFilename);
			const string text = "test";

			data.Output = text;

			Assert.AreEqual(text, data.Output, "Текст в свойстве Output после записи не равен присвоенному.");

			var exists = File.Exists(DataProvider.OutputFilename);

			Assert.True(exists, "Файл не записался.");

			var output = File.ReadAllText(DataProvider.OutputFilename);

			Assert.AreEqual(output, data.Output, "Текст в файле не равен записанному в Output.");

			File.WriteAllText(DataProvider.OutputFilename, oldOutput);
		}
	}
}
