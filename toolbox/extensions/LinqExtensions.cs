using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.pscalderonm.toolbox.extensions
{
	public static class LinqExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action)
		{
			foreach (var item in instance)
			{
				action(item);
			}
		}

		public static IEnumerable<O> MapTo<T, O>(this IEnumerable<T> instance, Func<T, O> func)
		{
			foreach (var item in instance)
			{
				yield return func(item);
			}
		}

		public static Task ForEachAsync<T>(this IEnumerable<T> instance, Func<T, Task> action)
		{
			return Task.WhenAll(instance.Select(item => action(item)));
		}

		public static Task MapToAsync<T, O>(this IEnumerable<T> instance, Func<T, Task<O>> func)
		{
			return Task.WhenAll(instance.Select(item => func(item)));
		}

		public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> instance) {
			List<T> items = new List<T>();
			instance.ForEach(list => items.AddRange(list));
			return items as IEnumerable<T>;
		}
	}
}
