using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingList.Interfaces;
using TestingList.Domain;
using TestingList.Extensions;
using TestingList.Models.Requests;

namespace TestingList.Services
{
    public class BaseService : IListService
    {
        //delcaring/instaniating dataprovider
        private IDataProvider _dataProvider;

        public BaseService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public List<ToDoListDomain> GetAll()
        {
            List<ToDoListDomain> list = null;

            _dataProvider.ExecuteCmd("dbo.ToDoList_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ToDoListDomain singleItem = new ToDoListDomain();
                    int startingIndex = 0; //startingOrdinal

                    singleItem.Id = reader.GetSafeInt32(startingIndex++);
                    singleItem.ToDoItem = reader.GetSafeString(startingIndex++);
                    singleItem.Priority = reader.GetSafeInt32(startingIndex++);
                    singleItem.DateCreated = reader.GetSafeDateTime(startingIndex++);
                    singleItem.DateModified = reader.GetSafeUtcDateTimeNullable(startingIndex++);
                    singleItem.DateCompleted = reader.GetSafeUtcDateTimeNullable(startingIndex++);

                    //not going to create list if there's no data / lazy load / if statement
                    if (list == null)
                    {
                        list = new List<ToDoListDomain>();
                    }

                    list.Add(singleItem);
                });

            return list;
        }

        public int Insert(ToDoListDomain model)
        {
            int Id = 0;

            _dataProvider.ExecuteNonQuery("dbo.ToDoList_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ToDoItem", model.ToDoItem);
                   paramCollection.AddWithValue("@Priority", model.Priority);

                   SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   idParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(idParameter);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   Int32.TryParse(param["@Id"].Value.ToString(), out Id);
               });

            return Id;
        }

        public void Update(ToDoListDomain model)
        {

            _dataProvider.ExecuteNonQuery("dbo.ToDoList_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@ToDoItem", model.ToDoItem);
                   paramCollection.AddWithValue("@Priority", model.Priority);
                   paramCollection.AddWithValue("@Id", model.Id);
               });
        }

        public Dictionary<int, DeleteIdsRequest> SoftDelete(DeleteIdsRequest model)
        {
            var results = new Dictionary<int, DeleteIdsRequest>();

            _dataProvider.ExecuteNonQuery("dbo.ToDoList_SoftDeleteTask",
                parameters =>
                {
                    var ids = parameters.AddWithValue("@Id", new IntIdTable(model.Ids));
                    ids.SqlDbType = System.Data.SqlDbType.Structured;
                    ids.TypeName = "dbo.IntIdTable";
                });
            return results;
        }

        public Dictionary<int, DeleteIdsRequest> HardDelete(DeleteIdsRequest model)
        {
            var results = new Dictionary<int, DeleteIdsRequest>();

            _dataProvider.ExecuteNonQuery("dbo.ToDoList_HardDeleteTask",
                parameters =>
                {
                    var ids = parameters.AddWithValue("@Id", new IntIdTable(model.Ids));
                    ids.SqlDbType = System.Data.SqlDbType.Structured;
                    ids.TypeName = "dbo.IntIdTable";
                });
            return results;
        }

        //public void HardDelete(int Id)
        //{
        //    _dataProvider.ExecuteNonQuery("dbo.ToDoList_Delete"
        //      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
        //      {
        //          paramCollection.AddWithValue("@Id", Id);
        //      });
        //}
    }
}