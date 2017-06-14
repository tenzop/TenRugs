(function () {

    'use strict';

    angular.module('tenRugsApp')
        .factory('peopleService', peopleServiceFactory);

    function peopleServiceFactory() {

        var aPeopleServiceObject = tenRugs.services.people;
        console.log('People Service created successfully');
        return aPeopleServiceObject;

    };

})();