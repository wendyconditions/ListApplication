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

    }
})();

(function () {
    "use strict";

    angular
        .module("ToDoList")
        .controller("listController", listController);

    listController.$inject = ["listService"];

    function listController(listService) {
        var vm = this;
        vm.$onInit = _init;
        vm.data = {};
        vm.name = "Wendy";
        vm.btnAdd = _btnAdd;


        function _init() {
            listService.loadList().then(_loadSuccess);
        }

        function _loadSuccess(response) {
            console.log(response);
            var response = response.data.items
            console.log(response);
            for (var i = 0; i < response.length; i++) {
                if (response[i].priority == 1) {
                    vm.backgroundRed = true;
                } else if (response[i].priority == 2) {
                    vm.backgroundGreen = true;
                } else if (response[i].priority == 3) {
                    vm.backgroundYellow = true;
                }
            }
            vm.items = response;
            
        }

        function _btnAdd() {
            listService.createItem(vm.data).then(_addSuccess, null);
        }

        function _addSuccess() {
            _init();
            console.log("New item created");
        }
    }

})();