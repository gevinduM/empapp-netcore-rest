using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Classes
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 5;

        const int  _minPage = 1;
        private int _pageSize = 1;

        public int page
        {
            get
            {

                return _pageSize;
            }
            set
            {
                _pageSize = Math.Max(_minPage, value);
            }
        }

        public int size {

            get {

                return _size;
            }
            set {
                _size = Math.Min(_maxSize, value);
            }
        }
    }
}
