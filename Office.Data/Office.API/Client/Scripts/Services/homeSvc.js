'use strict';

happyOffice.service('homeSvc',
    ['$q', '$http', function($q, $http) {
        var homeSvc = function () {
            this.init = function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: '/api/Index/status'
                }).then(function successCallback(response) {
                    deferred.resolve(response);
                });

                return deferred.promise;
            };
            this.trainAll = function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: '/api/Index/trainAll'
                }).then(function successCallback(response) {
                    deferred.resolve(response);
                });

                return deferred.promise;
            };
        }
        return new homeSvc();
    }]);