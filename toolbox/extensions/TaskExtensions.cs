using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.pscalderonm.toolbox.extensions {
	public static class TaskExtensions {

		public static Task<T> DefaultValue<T>() {
			return Task.FromResult(default(T));
		}

		public static Task DefaultValue() {
			return Task.FromResult<object>(null);
		}

		public static Task<IEnumerable<T>> DefaultList<T>() {
			IEnumerable<T> list = new List<T>();
			return Task.FromResult(list);
		}
	}
}
