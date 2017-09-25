using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingList.Models.Requests
{
    public class ListAddRequest
    {
        [Required]
        public string ToDoItem { get; set; }
        [Required]
        [MaxLength(4)]
        public int Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}