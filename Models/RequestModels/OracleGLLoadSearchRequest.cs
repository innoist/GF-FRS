﻿using FRS.Models.Common;

namespace FRS.Models.RequestModels
{
    public class OracleGLEntrySearchRequest : GetPagedListRequest
    {
        public long OracleGLLoadId { get; set; }

        public OrderByOracleGLEntry OrderByColumn
        {
            get
            {
                return (OrderByOracleGLEntry)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
