using UnityEngine;

namespace AllIn1SpringsToolkit
{
	public struct CustomVector3 : IVector
	{
		public Vector3 vector3;

		public CustomVector3(Vector3 vector3)
		{
			this.vector3 = vector3;
		}

		public float this[int index]
		{
			get
			{
				return vector3[index];
			}
			set
			{
				vector3[index] = value;
			}
		}

		public int GetSize()
		{
			int res = 3;
			return res;
		}
	}
}