(function () {

    'use strict';
    angular.module('tenRugsApp').controller('rugsController', RugsController);

    RugsController.$inject = ['$scope', 'rugsService', 'Cropper', '$http'];

    function RugsController($scope, rugsService, Cropper, $http) {
        var vm = this;
        vm.$scope = $scope;
        vm.rugsService = rugsService;
        vm.displayBodyContent = false;
        vm.selectAll = _selectAll;
        vm.createRugs = _createRugs;
        vm.save = _save;
        vm.cancel = _cancel;
        vm.edit = _editRugs;
        vm.delete = _deleteRugs;
        vm.selectedKey = null;

        vm.items = [];
        vm.selectedItem = null;
        vm.message = "Welcome to the Rugs page!!!";
        vm.heading = "Create a new Rugs";


        vm.$scope.readProfile = _readProfile;
        vm.upload = _upload;
        vm.dataURItoBlob = dataURItoBlob;



        render();
        function render() {
            console.log("Rugs index page is ready");
            vm.selectAll();
        };

        function _selectAll() {
            console.log("Executing Rugs Service SelectAll()...");
            $http.get("/api/rugs/")
                      .then(onSelectAllSuccess, onError);
            vm.displayBodyContent = true;
            console.log("Executed Rugs Service SelectAll()");
        };

        function _createRugs() {
            vm.selectedItem = {};
            console.log("New rug has been created!");
        };

       
        function _save() {
            console.log("Executing Rugs Service Save()...");
            if (vm.selectedItem.id) {
                $http.put("/api/rugs/", vm.selectedItem)
                      .then( onUpdateSuccess, onError);               
            } else {
                $http.post("/api/rugs/", vm.selectedItem)
                      .then(onSaveSuccess, onError);
            }
            console.log("Executed Rugs Service Save()");
        };

        function _cancel() {
            vm.selectedItem = null;
        };

        function _editRugs(item) {
            vm.selectedItem = item;
        };

        function _deleteRugs(key, data) {
            vm.selectedKey = key;
            $http.delete('/api/rugs?id=' + data)
                     .then(onDeleteSuccess, onError);
        };

        function onSelectAllSuccess(data) {
            console.log("Rugs Service SelectAll() was successful!");
            console.log(data.data.items);
            vm.items = data.data.items;
            console.log(vm.items);
        };

        function onSaveSuccess(data) {
            vm.selectedItem = null;
            render();
            console.log("Rugs Service Save() was successful!");

        };

        function onUpdateSuccess(data) {
            vm.selectedItem = null;
            render();
            console.log("Rugs Service Update() was successful!");
 
        };

        function onDeleteSuccess(data) {
            vm.items.splice(vm.selectedKey, 1);
            render();
            console.log("Delete Success");              
        };

        function onError(xhr, ajaxOptions, thrownError) {
            //console.log(xhr.responseText);
            console.log("Error");
        };

        function _upload() {
            if (!vm.dataUrl) {
                vm.save();
            } else {
                var fd = new FormData();
                var imgBlob = dataURItoBlob(vm.dataUrl);
                fd.append('file', imgBlob);
                $http.post("/api/upload", fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(_onUploadSuccess, _onUploadError);
            }
        };

        function _onUploadSuccess(data) {
            console.log("uploaded file!");
            console.log(data);
            vm.selectedItem.url = data.data;
            vm.save();
        };

        function _onUploadError(jqXHR) {
            console.log("error uploading image");
            console.log(jqXHR.responseText);
        };


        function dataURItoBlob(dataURI) {
            var binary = atob(dataURI.split(',')[1]);
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
            var array = [];
            for (var i = 0; i < binary.length; i++) {
                array.push(binary.charCodeAt(i));
            }
            return new Blob([new Uint8Array(array)], {
                type: mimeString
            });
        };

        function _readProfile(blob) {
            Cropper.encode((vm.file = blob)).then(function (dataUrl) {
                vm.dataUrl = dataUrl;
            });
        };

    };

})();