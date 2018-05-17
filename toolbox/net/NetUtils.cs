using System.Net;
using System.Text.RegularExpressions;

namespace com.pscalderonm.toolbox.net {
	public static class NetUtils {

		static readonly Regex IPRegex = new Regex(@"((\d{1,3}\.?){4})");

		public static string GetPublicIPv4Address() {
			string ipAddress = new WebClient().DownloadString("http://icanhazip.com");
			var match = IPRegex.Match(ipAddress);
			if (!match.Success) return string.Empty;
			var address = IPAddress.Parse(match.Result("$1"));
			return address?.MapToIPv4().ToString() ?? IPAddress.Loopback.MapToIPv4().ToString();
		}

		public static bool TryGetPublicIPv4Address(out string ipV4Address) {
			ipV4Address = GetPublicIPv4Address();
			return !string.IsNullOrEmpty(ipV4Address);
		}
	}
}
