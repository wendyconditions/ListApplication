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
        vm.btnComplete = _btnComplete;
        vm.btnUpdate = _btnUpdate;

        /////////////

        function _init() {
            listService.loadList().then(_loadSuccess);
        }

        function _loadSuccess(response) {
            var response = response.data.items
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

        function _btnAdd(data) {
            if (vm.data.id) {
                listService.updateTask(data).then(_taskSuccess, null);
            } else {
                listService.createItem(vm.data).then(_taskSuccess, null);
            }
        }

        function _taskSuccess() {
            _init();
            vm.data = {};
        }

        function _btnComplete(data) {
            var completedItems = [];

            for (var i = 0; i < data.length; i++) {
                if (data[i].isComplete) {
                    completedItems.push(data[i].id);
                }
            }
            listService.completedTask(completedItems).then(_completedSuccess, _completedError);
        }

        function _completedSuccess() {
            _init();
        }

        function _completedError() {
            // Currently in progress
            console.log("handle this error");
        }

        function _btnUpdate(item) {
            vm.data = {
                toDoItem: item.toDoItem
                , priority: item.priority
                , id: item.id
            };
            return vm.data;
        }
    }
})();