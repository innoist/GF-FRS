using System.Collections.Generic;

namespace FRS.Web.Models
{
    public class LoadSearchRequestResponse
    {
        public IEnumerable<Load> Loads { get; set; }
    }
}