namespace AllIn1SpringsToolkit
{
	public struct Vector4Bool : IVectorBool
	{
		public static readonly Vector4Bool AllTrue = new Vector4Bool(true, true, true, true);
		public static readonly Vector4Bool AllFalse = new Vector4Bool(false, false, false, false);

		public bool this[int index]
		{
			get
			{
				bool res = false;
				switch (index)
				{
					case 0:
						res = x;
						break;
					case 1:
						res = y;
						break;
					case 2:
						res = z;
						break;
					case 3:
						res = w;
						break;
				}

				return res;
			}
			set
			{
				switch (index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					case 2:
						z = value;
						break;
					case 3:
						w = value;
						break;
				}
			}
		}

		public bool x;
		public bool y;
		public bool z;
		public bool w;

		public Vector4Bool(bool x, bool y, bool z, bool w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public int GetSize()
		{
			return 4;
		}

		public static Vector4Bool operator !(Vector4Bool a)
		{
			Vector4Bool res = new Vector4Bool(!a.x, !a.y, !a.z, !a.w);
			return res;
		}

		public Vector4Bool And(Vector4Bool vectorBoolToCompare)
		{
			Vector4Bool res = new Vector4Bool(vectorBoolToCompare.x && x, vectorBoolToCompare.y && y, vectorBoolToCompare.z && z, vectorBoolToCompare.w && w);
			return res;
		}
	}
}