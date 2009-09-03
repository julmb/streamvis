namespace Extensions
{
	public struct Range<T>
	{
		readonly T start;
		readonly T end;
		
		public T Start { get { return start; } }
		public T End { get { return end; } }
		
		public Range(T start, T end)
		{
			this.start = start;
			this.end = end;
		}
	}
}