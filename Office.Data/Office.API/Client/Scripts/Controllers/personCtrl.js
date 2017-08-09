'use strict';

happyOffice.controller('personCtrl', ['$scope', 'personViewModel', function ($scope, personViewModel) {
    $scope.viewModel = personViewModel;
    var res = window.location.href.split('/');
    $scope.groupId = res[res.length-1];

    personViewModel.init();
}]);