'use strict';

happyOffice.controller('personCtrl', ['$scope', 'personViewModel', 'AddDealService', function ($scope, personViewModel, AddDealService) {
    $scope.viewModel = personViewModel;
    var res = window.location.href.split('/');
    $scope.groupId = res[res.length - 1];

    personViewModel.init();
    $scope.NewUser = new Object();
    $scope.NewUser.GroupId = $scope.groupId;
    $scope.NewUser.Name = "";
    $scope.NewUser.Position = "";
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

        data.append("NewUser", angular.toJson($scope.NewUser));
        AddDealService.AddDeal(data).then(function (response) {
            alert("Added Successfully");
        }, function () {

        });
    };
}]);


happyOffice.factory('AddDealService',
    [
        '$http', function ($http) {

            var fac = {};
            fac.AddDeal = function (data) {
                return $http.post("/api/Person/AddPerson/",
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