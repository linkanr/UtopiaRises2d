namespace AllIn1SpringsToolkit
{
	public interface IVectorBool
	{
		int GetSize();

		bool this[int index]
		{
			get;
			set;
		}
	}
}