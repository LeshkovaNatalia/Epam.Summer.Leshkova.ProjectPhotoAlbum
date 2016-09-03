using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{

    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }    
    }

    public class PagedList<T>
    {
        public List<T> Content { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }
        public int TotalRecords { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalItems { get; set; }
        public int TotalPages
            => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }
    public class IndexViewModel
    {
        public IEnumerable<PhotoViewModel> Photos { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}