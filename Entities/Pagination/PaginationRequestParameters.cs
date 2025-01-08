using System;
using System.Collections.Generic;
using System.Linq;	
using System.Text;
using System.Threading.Tasks;

namespace Entities.Pagination
{
    public abstract class PaginationRequestParameters
    {
		const int maxPageSize = 50;
		//
        public int PageNumber { get; set; }
		private int _pageSize;

		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value >maxPageSize ? maxPageSize : value; }//eger istenilen sayı 50 den buyukse sade elli dönderecegim ama kucuse istelilen sayıyı dönecegim
		}

	}
}
