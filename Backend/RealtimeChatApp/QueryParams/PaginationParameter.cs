using System.ComponentModel.DataAnnotations;

namespace RealtimeChatApp.QueryParams
{
    public class PaginationParameter
    {
        private int _pageNumber = 1;

        private int _pageSize = 100;

        [Range(1,int.MaxValue)]
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid pagination Page parameter: must be non-negative.");
                _pageNumber = value;
            }
        }

        [Range(1,int.MaxValue)]
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid pagination Size parameter: must be non-negative.");
                _pageSize = value;
            }
        }
    }
}
