(
    function (app) {
        app.factory('notification-service', notificationService);

        function notificationService() {
            toastr.options = {
                "debug": false,
                "positionClass": "toast-bottom-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": 2000,
                "extendedTimeOut": 1000
            };

            return {

                displaySuccess: displaySuccess,
                displayError: displayError,
                displayWarning: displayWarning,
                displayInfo: displayInfo
            }

            function displaySuccess(message) {
                toastr.success(message);
            }

            function displayError(message) {
                if (Array.isArray(message)) {
                    message.forEach(function (err) {
                        toastr.error(err);
                    })
                }else  {
                    toastr.error(message);
                }
            }

            function displayWarning(message) {
                toastr.warning(message);
            }

            function displayInfo(message) {
                toastr.info(message);
            }

        }
    }
)(angular.module('eshop-common'))