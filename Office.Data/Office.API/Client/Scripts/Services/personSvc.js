'use strict';

happyOffice.service('personSvc',
    ['$q', '$http', function ($q, $http) {

        var personSvc = function () {
            this.getAll = function (group) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: '/api/Person/getAll/'+group
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
                }).then(function success(res) {
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

            this.addPerson = function (groupId, name, position) {
                var deferred = $q.defer();
                var data = new Object();
                data.GroupId = groupId;
                data.Name = name;
                data.Position = position;
                $http({
                    method: 'POST',
                    url: '/api/Person/add/',
                    data: data
                }).then(function success(res) {
                    deferred.resolve(res);
                });

                return deferred.promise;
            };

        };
        return new personSvc();
    }]);