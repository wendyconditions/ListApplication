using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingList.Domain;
using TestingList.Models.Requests;

namespace TestingList.Interfaces
{
    public interface IListService
    {
        List<ToDoListDomain> GetAll();
        int Insert(ToDoListDomain model);
        void Update(ToDoListDomain model);
        Dictionary<int, DeleteIdsRequest> SoftDelete(DeleteIdsRequest model);
        //int SoftDelete(DeleteIdsRequest model);
        void HardDelete(int Id);
       // int Delete(DeleteIdsRequest model);

    }
}
