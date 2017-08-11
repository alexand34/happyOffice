'use strict';

happyOffice.service('groupViewModel',
    ['$http', 'groupSvc', function ($http, groupSvc) {
        var viewModel = {
            init: function () {
                var url = window.location.href;
                var res = url.split("/");
                groupSvc.getAll(res[res.length-1]).then(function (result) {
                    _.extend(viewModel, result);
                });
            },
            AddPersonGroup: function() {
                groupSvc.add(viewModel.id, viewModel.name);
            },
            deletePersonGroup: function(id) {
                groupSvc.delete(id);
                location.reload();
            }
        }
        return viewModel;
    }]);
