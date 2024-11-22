using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public struct CustomVector2 : IVector
	{
		public Vector2 vector2;

		public CustomVector2(Vector2 vector2)
		{
			this.vector2 = vector2;
		}

		public float this[int index]
		{
			get
			{
				return vector2[index];
			}
			set
			{
				vector2[index] = value;
			}
		}
		public int GetSize()
		{
			return 2;
		}
	}
}
