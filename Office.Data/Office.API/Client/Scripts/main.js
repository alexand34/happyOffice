'use strict';

var happyOffice = angular.module('happyOffice', ['ngRoute']);

happyOffice.config(['$routeProvider', function ($routeProvide) {
        $routeProvide
            .when('/',
                {
                    templateUrl: 'views/home.html',
                    controller: 'homeCtrl'
                })
            .when('/personGroups',
                {
                    templateurl: 'views/PersonGroups/personGroups.html',
                    controller: 'personGroupCtr'
                })
            .when('/personGroups/add',
                {
                    templateurl: 'views/PersonGroups/addPersonGroup.html',
                    controller: 'personGroupCtrl'
                })
            .when('/persons',
                {
                    templateUrl: 'views/persons/persons.html',
                    controller: 'personCtrl'
                })
            .when('/persons/add',
                {
                    templateUrl: 'views/persons/addPerson.html',
                    controller: 'personCtrl'
                })
            .otherwise(
                {
                    redirectTo: '/'
                });

    }
]);

happyOffice.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);