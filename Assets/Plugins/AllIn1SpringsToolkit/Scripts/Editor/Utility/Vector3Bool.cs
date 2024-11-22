namespace AllIn1SpringsToolkit
{
	public struct Vector3Bool : IVectorBool
	{
		public static readonly Vector3Bool AllTrue = new Vector3Bool(true, true, true);
		public static readonly Vector3Bool AllFalse = new Vector3Bool(false, false, false);

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
				}
			}
		}

		public bool x;
		public bool y;
		public bool z;

		public Vector3Bool(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public int GetSize()
		{
			return 3;
		}

		public Vector3Bool And(Vector3Bool vectorBoolToCompare)
		{
			Vector3Bool res = new Vector3Bool(vectorBoolToCompare.x && x, vectorBoolToCompare.y && y, vectorBoolToCompare.z && z);
			return res;
		}

		public static Vector3Bool operator !(Vector3Bool a)
		{
			Vector3Bool res = new Vector3Bool(!a.x, !a.y, !a.z);
			return res;
		}
	}
}