'use strict';

happyOffice.service('personViewModel', ['$http', 'personSvc', function ($http, personSvc) {
    var viewModel = {
        init: function () {
            var group = window.location.href.split('/');
            personSvc.getAll(group[group.length-1]).then(function (result) {
                _.extend(viewModel, result);
            });
        },
        getPerson: function (id) {
            personSvc.getPerson(id).then(function (res) {
                _.extend(viewModel, res);
            });
        },
        AddPerson: function(groupId, name, position) {
            personSvc.addPerson(groupId, name, position);
        },
        deletePerson: function (id) {
            personSvc.deletePerson(id);
        }
    };
    return viewModel;
}
]);