using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingList.Domain
{
    public class ToDoListDomain
    {
        public string ToDoItem { get; set; }
        public int Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int Id { get; set; }
    }
}