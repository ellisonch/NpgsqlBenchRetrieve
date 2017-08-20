using BenchmarkDotNet.Attributes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpgsqlBenchRetrieve {
	public class Bench {
		private string _username = "xxx";
		private string _password = "xxx";
		private string _dbname = "xxx";

		private readonly NpgsqlCommand _command;

		public Bench() {
			var connStringTemplate = "Server=127.0.0.1;Port=5432;User Id={0};Password={1};Database={2};Timeout=120;CommandTimeout=3600;Pooling=false";
			var conn = new NpgsqlConnection(string.Format(connStringTemplate, _username, _password, _dbname));

			conn.Open();

			_command = new NpgsqlCommand(@"
				select
					generate_series(1, 10000) as a,
					generate_series(1, 10000) as b,
					generate_series(1, 10000) as c, 
					generate_series(1, 10000) as d
			", conn);
			_command.Prepare();
		}

		[Benchmark]
		public void ReadWithBrackets() {
			using (NpgsqlDataReader dr = _command.ExecuteReader()) {
				while (dr.Read()) {
					var a = (int)dr[0];
					var b = (int)dr[1];
					var c = (int)dr[2];
					var d = (int)dr[3];
					if (a != b || b != c || c != d) {
						throw new Exception();
					}
				}
			}
		}

		[Benchmark]
		public void ReadWithGetInt() {
			using (NpgsqlDataReader dr = _command.ExecuteReader()) {
				while (dr.Read()) {
					var a = dr.GetInt32(0);
					var b = dr.GetInt32(1);
					var c = dr.GetInt32(2);
					var d = dr.GetInt32(3);
					if (a != b || b != c || c != d) {
						throw new Exception();
					}
				}
			}
		}

	}
}
