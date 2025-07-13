namespace System.Collections.Generic
{
	using System.Collections;

	public static class EnumerableExtensions
	{
		/// <summary>
		/// Returns true if the enumerable is not null and has at least one item
		/// in the collection; otherwise, false.
		/// </summary>
		public static bool IsNullOrEmpty(this IEnumerable enumerable) =>
			enumerable is null || !enumerable.GetEnumerator().MoveNext();
	}
}

