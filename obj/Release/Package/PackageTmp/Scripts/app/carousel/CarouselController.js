(function () {

    'use strict';
    angular.module('tenRugsApp').controller('carouselController', CarouselController);

    CarouselController.$inject = ['$scope'];

    function CarouselController($scope) {
        var vm = this;
        vm.$scope = $scope;
    }
});