using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public struct CustomVector4 : IVector
	{
		public Vector4 vector4;

		public CustomVector4(Vector4 vector4)
		{
			this.vector4 = vector4;
		}

		public float this[int index]
		{
			get
			{
				return vector4[index];
			}
			set
			{
				vector4[index] = value;
			}
		}

		public int GetSize()
		{
			int res = 4;
			return res;
		}
	}
}