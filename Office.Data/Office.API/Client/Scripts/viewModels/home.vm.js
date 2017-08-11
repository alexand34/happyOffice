'use strict';

happyOffice.service('homeViewModel', ['$http', 'homeSvc', function ($http, homeSvc) {
        var viewModel = {
            init: function () {
                homeSvc.init().then(function(result) {
                    _.extend(viewModel, result);
                });
            },
            trainAll: function () {
                homeSvc.trainAll();
            }
        };
        return viewModel;
    }]);