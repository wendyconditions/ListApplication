(function () {
    "use strict";

    angular
        .module("ToDoList")
        .factory("listService", listService);

    listService.$inject = ["$http", "$q"];

    function listService($http, $q) {
        var service = {
            loadList: _getList
            , createItem: _postItem
            , completedTask: _deleteTask
            , updateTask: _putTask
        };

        return service;

        ////////////

        function _getList() {
            var settings = {
                method: "GET"
                , url: "/api/lists"
            };
            return $http(settings)
                .then(null, _getListError);
        }

        function _getListError(error) {
            return $q.reject(error.data.message);
        }

        function _postItem(item) {
            var settings = {
                method: "POST"
                , url: "/api/lists/"
                , data: item
            };
            return $http(settings)
                .then(null, _postItemError)
        }

        function _postItemError(error) {
            return $q.reject(error);
        }

        function _deleteTask(id) {
            var settings = {
                method: "DELETE"
                , url: "/api/lists/" + id
            };
            return $http(settings)
                .then(null, _deleteTaskError);
        }

        function _deleteTaskError(error) {
            return $q.reject(error);
        }

        function _putTask(updatedItem) {
            var settings = {
                method: "PUT"
                , url: "/api/lists/" + updatedItem.id
                , data: updatedItem
            };
            return $http(settings)
                .then(null, _putTaskError);
        }

        function _putTaskError(error) {
            return $q.reject(error.data.message);
        }
    }
})();