'use strict';

happyOffice.controller('personCtrl', ['$scope', 'personViewModel', function ($scope, personViewModel) {
    $scope.hello = personViewModel;

    personViewModel.init();
}]);