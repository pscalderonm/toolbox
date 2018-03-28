using System;
using System.Data;

namespace com.pscalderonm.toolbox.extensions
{
	public static class DataExtensions
	{
		public static T? GetNul<T>(this IDataReader reader, string columnName) where T : struct
		{
			return reader[columnName] == DBNull.Value
			? null
			: new T?((T)Convert.ChangeType(reader[columnName], typeof(T)));
		}

		public static T Get<T>(this IDataReader reader, string columnName)
		{
			return reader[columnName] == DBNull.Value
			? default(T)
			: (T)Convert.ChangeType(reader[columnName], typeof(T));
		}

		public static string Get(this IDataReader reader, string columnName)
		{
			return Get<string>(reader, columnName);
		}
	}
}
