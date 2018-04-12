using System;

namespace com.pscalderonm.toolbox.common {
	public static class DateTimeConverter {
		private static readonly DateTime BaseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static long ConvertToEpoch(DateTime dateTime) {
			TimeSpan span = dateTime - BaseDateTime;
			return (long)span.TotalSeconds;
		}

		public static DateTime ConvertToDateTime(long epoch) {
			return BaseDateTime.AddSeconds(epoch);
		}
	}
}
