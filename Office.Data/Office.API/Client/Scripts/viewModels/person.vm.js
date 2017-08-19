'use strict';

happyOffice.service('personViewModel', ['$http', 'personSvc', function ($http, personSvc) {
    var viewModel = {
        init: function () {
            $('#spinner').show();
            var group = window.location.href.split('/');
            personSvc.getAll(group[group.length-1]).then(function (result) {
                _.extend(viewModel, result);
            }).then(function () {
                $('#spinner').hide();
            });
        },
        getPerson: function (id) {
            personSvc.getPerson(id).then(function (res) {
                _.extend(viewModel, res);
            });
        },
        AddPerson: function (groupId, name, position) {
            $('#spinner').show();
            personSvc.addPerson(groupId, name, position).then(function () {
                location.href = "/Client/#/persons/";
            });
        },
        deletePerson: function (id) {
            personSvc.deletePerson(id);
        }
    };
    return viewModel;
}
]);