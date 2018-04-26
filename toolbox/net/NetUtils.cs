using System.Net;

namespace com.pscalderonm.toolbox.net {
	public static class NetUtils
    {
		public static string GetPublicIPv4Address() {
			var address = IPAddress.Parse(new WebClient().DownloadString("http://icanhazip.com"));
			return address?.MapToIPv4().ToString() ?? IPAddress.Loopback.MapToIPv4().ToString();			
		}
	}
}
