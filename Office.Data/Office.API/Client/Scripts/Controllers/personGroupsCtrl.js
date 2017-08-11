'use strict';

happyOffice.controller('personGroupCtrl', ['$scope', 'groupViewModel', function ($scope, groupViewModel) {
    $scope.viewModel = groupViewModel;

    groupViewModel.init();
}]);