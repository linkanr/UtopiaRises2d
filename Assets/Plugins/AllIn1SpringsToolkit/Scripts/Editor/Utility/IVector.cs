namespace AllIn1SpringsToolkit
{
	public interface IVector
	{
		int GetSize();

		float this[int index]
		{
			get;
			set;
		}
	}
}