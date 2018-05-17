using com.pscalderonm.toolbox.net;
using System;

namespace playground {
	class Program {
		static void Main(string[] args) {

			Console.WriteLine(NetUtils.GetPublicIPv4Address());

			Console.ReadKey();
		}
	}
}
