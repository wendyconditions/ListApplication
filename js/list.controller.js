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

        /////////////

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

        function _btnComplete(data) {
            console.log(data);
            var completedItems = [];

            for (var i = 0; i < data.length; i++) {
                if (data[i].isComplete) {
                    completedItems.push(data[i].id);
                }
            }
            console.log(completedItems);
            listService.completedTask(completedItems).then(_completedSuccess, _completedError);
        }

        function _completedSuccess() {
            console.log("soft delted success api");
            _init();
        }

        function _completedError() {
            console.log("error happened soft delete api");
        }
    }

})();