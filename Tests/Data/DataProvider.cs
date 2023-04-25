using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Tests.Data
{
	class DataProvider
	{
		public const string InputFilename = "../../Assets/input.txt";

		public const string OutputFilename = "../../Assets/output.txt";

		public string Input { get; } = File.ReadAllText(InputFilename);

		public string Output
		{
			get
			{
				return _output;
			}
			set
			{
				File.WriteAllText(OutputFilename, value);
				_output = value;
			}
		}

		private string _output = null;
	}
}
