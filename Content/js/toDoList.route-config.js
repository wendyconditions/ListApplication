(function () {
    "use strict";

    var app = angular.module('ToDoList');

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];

    function _configureStates($stateProvider, $urlRouterProvider) {

        // For any unmatched url, redirect to /state1
        $urlRouterProvider.otherwise("/tasks");

        // Registering states
        $stateProvider
            .state({
                name: 'home',
                component: 'home',
                url: '/tasks'
            })
            .state({
                name: 'completed',
                component: 'completed',
                url: '/completed'
            })
    }

    // Definining state components and directing to templates
    app.component('home', {
        templateUrl: 'tasks.html'
        , controller: 'listController as vm'
    });

    app.component('completed', {
        templateUrl: 'completed.html'
        , controller: 'listController as vm'
    });
})();
