using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpgsqlBenchRetrieve {
	class Program {
		static void Main(string[] args) {
			var summary = BenchmarkRunner.Run<Bench>();
			Console.ReadLine();
		}
	}
}
