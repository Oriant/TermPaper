using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	public class LotDtoEqualityComparer : IEqualityComparer<LotDTO>
	{
		public bool Equals(LotDTO x, LotDTO y)
		{
			if (object.ReferenceEquals(x, y)) return true;

			if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

			return x.Name == y.Name && x.Id == y.Id && x.IsConfirmed == y.IsConfirmed;
		}

		public int GetHashCode(LotDTO obj)
		{
			if (object.ReferenceEquals(obj, null)) return 0;

			int hashCodeName = obj.Name == null ? 0 : obj.Name.GetHashCode();
			int hasCodeId = obj.Id.GetHashCode();

			return hashCodeName ^ hasCodeId;
		}
	}
}
