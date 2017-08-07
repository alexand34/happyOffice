'use strict';

happyOffice.service('personViewModel', ['$http', 'personSvc', function ($http, personSvc) {
    var viewModel = {
        init: function () {
            personSvc.getAll().then(function (result) {
                _.extend(viewModel, result);
            });
        },
        getPerson: function (id) {
            personSvc.getPerson(id).then(function (res) {
                _.extend(viewModel, res);
            });
        },
        deletePerson: function (id) {
            personSvc.deletePerson(id);
        }
    };
    return viewModel;
}
]);