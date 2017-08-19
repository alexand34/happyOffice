'use strict';

happyOffice.service('groupViewModel',
    ['$http', 'groupSvc', function ($http, groupSvc) {
        var viewModel = {
            init: function () {
                $('#spinner').show();
                var url = window.location.href;
                var res = url.split("/");
                groupSvc.getAll(res[res.length-1]).then(function (result) {
                    _.extend(viewModel, result);
                }).then(function () {
                    $('#spinner').hide();
                });
            },
            AddPersonGroup: function() {
                groupSvc.add(viewModel.id, viewModel.name).then(function (res) {
                    window.location.href = "/Client/#/";
                });
            },
            deletePersonGroup: function(id) {
                groupSvc.delete(id);
                location.reload();
            }
        }
        return viewModel;
    }]);
