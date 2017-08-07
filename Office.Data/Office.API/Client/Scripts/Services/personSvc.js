'use strict';

happyOffice.service('personSvc',
    ['$q', '$http', function ($q, $http) {

        var personSvc = function () {
            this.getAll = function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: '/api/getAll'
                }).then(function successCallback(response){
                    deferred.resolve(response);
                });

                return deferred.promise;
            };

            this.getPerson = function (id) {
                var deferred = $q.defer();
                $http({
                    method: 'GEET',
                    url: 'api/get/' + id,
                    data: data
                }).success(function (res) {
                    deferred.resolve(res);
                });

                return deferred.promise;
            };

            this.deletePerson = function (id) {
                var deferred = $q.defer();
                $http({
                    method: 'DELETE',
                    url: '/api/delete/' + id
                }).success(function (res) {
                    deferred.resolve(res);
                });

                return deferred.promise;
            };

            this.addPerson = function (data) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: 'api/add/',
                    data: data
                }).success(function (res) {
                    deferred.resolve(res);
                });

                return deferred.promise;
            };
        };
        return new personSvc();
    }]);