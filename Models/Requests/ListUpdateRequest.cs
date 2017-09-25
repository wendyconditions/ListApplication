using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingList.Models.Requests
{
    public class ListUpdateRequest : ListAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}