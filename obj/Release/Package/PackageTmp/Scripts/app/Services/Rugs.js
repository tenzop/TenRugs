(function () {

    'use strict';

    angular.module('tenRugsApp')
        .factory('rugsService', rugsServiceFactory);

    function rugsServiceFactory() {

        var aRugsServiceObject = tenRugs.services.rugs;
        console.log('Rugs Service created successfully');
        return aRugsServiceObject;

    };

})();