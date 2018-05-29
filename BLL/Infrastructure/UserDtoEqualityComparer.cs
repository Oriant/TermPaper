using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	public class UserDtoEqualityComparer : IEqualityComparer<UserDTO>
	{
		public bool Equals(UserDTO x, UserDTO y)
		{
			if (object.ReferenceEquals(x, y)) return true;

			if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

			return x.Name == y.Name && x.Id == y.Id;
		}

		public int GetHashCode(UserDTO obj)
		{
			if (object.ReferenceEquals(obj, null)) return 0;

			int hashCodeName = obj.Name == null ? 0 : obj.Name.GetHashCode();
			int hasCodeId = obj.Id.GetHashCode();

			return hashCodeName ^ hasCodeId;
		}
	}
}
