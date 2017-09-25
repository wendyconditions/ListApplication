using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingList.Models.Requests
{
    public class DeleteIdsRequest
    {
        public int[] Ids { get; set; }
    }
}