'use strict';

happyOffice.service('groupSvc',
    ['$q', '$http', function ($q, $http) {
        var groupSvc = function() {
            this.getAll = function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: '/api/PersonGroups/getAll'
                }).then(function successCallback(response) {
                    deferred.resolve(response);
                });

                return deferred.promise;
            };
            this.add = function(id, name) {
                var group = new Object();
                group.Name = name;
                group.PersonGroupId = id;
                $http({
                    method: 'POST',
                    url: '/api/PersonGroups/add',
                    data: group
                });
            };
            this.delete = function(id) {
                $http({
                    method: 'GET',
                    url: '/api/PersonGroups/delete/'+id
                });
            }
        }
        return new groupSvc();
    }]);