namespace SnowmanLabsChallenge.Application.Filters
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using System;

    public class BaseFilter : IFilter
    {
        public int? Id { get; set; }

        public Guid? Uuid { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? Active { get; set; }

        /// <summary>
        ///     Page Number. Minimum value is 1.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        ///     Page Size. Default Value is 25.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        ///     Indicator if need to paginate the results list.
        /// </summary>
        public bool HasPagination { get; set; }

        private string _sortBy;

        /// <summary>
        ///     Field order. Defaut value is Id.
        /// </summary>
        public string SortBy
        {
            get
            {
                return this._sortBy ?? "id";
            }
            set
            {
                this._sortBy = value;
            }
        }

        /// <summary>
        ///     Order direction. May assume two values: ASC or DESC.
        /// </summary>
        /// <value>
        ///     May assume two values:
        ///         1. ASC
        ///             Ascending order
        ///         2. DESC
        ///             Descending order
        /// </value>
        public string SortDirection { get; set; }
    }
}
