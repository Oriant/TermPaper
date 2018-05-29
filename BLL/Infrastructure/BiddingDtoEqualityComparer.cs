using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	public class BiddingDtoEqualityComparer : IEqualityComparer<BidDTO>
	{
		public bool Equals(BidDTO x, BidDTO y)
		{
			if (object.ReferenceEquals(x, y)) return true;

			if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

			return x.Sum == y.Sum && x.Id == y.Id;
		}

		public int GetHashCode(BidDTO obj)
		{
			if (object.ReferenceEquals(obj, null)) return 0;

			int hashCodeSum = obj.Sum.GetHashCode();
			int hasCodeId = obj.Id.GetHashCode();

			return hashCodeSum ^ hasCodeId;
		}
	}
}
