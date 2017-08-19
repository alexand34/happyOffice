'use strict';

happyOffice.service('homeViewModel', ['$http', 'homeSvc', function ($http, homeSvc) {
        var viewModel = {
            init: function () {
                homeSvc.init().then(function(result) {
                    _.extend(viewModel, result);
                }).then(function (res) {
                    $('#spinner').hide();
                });
            },
            trainAll: function () {
                $('#spinner').show();
                homeSvc.trainAll().then(function (res) {
                    $('#spinner').hide();
                });
            }
        };
        return viewModel;
    }]);