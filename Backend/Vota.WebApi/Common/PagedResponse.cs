using System.ComponentModel;

namespace Vota.WebApi.Common
{
    /// <summary>
    /// Generic paged response model 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        /// <summary>
        /// Page Number
        /// </summary>
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// Page Size
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Total Records
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }
    }
}
