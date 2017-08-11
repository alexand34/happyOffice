'use strict';

happyOffice.controller('homeCtrl', ['$scope', 'homeViewModel', 'AddPersonService', 'groupViewModel', function ($scope, homeViewModel, AddPersonService, groupViewModel) {
    $scope.viewModel = homeViewModel;
    $scope.groupsViewModel = groupViewModel;
    groupViewModel.init();
    homeViewModel.init();
    $scope.groupId = "null";
    $scope.getTheFiles = function ($files) {
        $scope.imagesrc = [];

        for (var i = 0; i < $files.length; i++) {

            var reader = new FileReader();
            reader.fileName = $files[i].name;
            reader.onload = function (event) {
                var image = {};
                image.Name = event.target.fileName;
                image.Size = (event.total / 1024).toFixed(2);
                image.Src = event.target.result;
                $scope.imagesrc.push(image);
                $scope.$apply();
            }
            reader.readAsDataURL($files[i]);
        }

        $scope.Files = $files;

    };
    // Submit Forn data
    $scope.Submit = function () {
        //FILL FormData WITH FILE DETAILS.
        var data = new FormData();

        angular.forEach($scope.Files, function (value, key) {
            data.append(key, value);
        });

        AddPersonService.AddDeal(data, $scope.groupId).then(function(response) {
            $scope.faces = response;
        });
    };
}]);

happyOffice.factory('AddPersonService',
    [
        '$http', function ($http) {

            var fac = {};
            fac.AddDeal = function (data, groupId) {
                return $http.post("/api/Index/recognize/"+groupId,
                    data,
                    {
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).then(function successCallback(response) {
                    return response.data;
                });
            }
            return fac;
        }
    ]);