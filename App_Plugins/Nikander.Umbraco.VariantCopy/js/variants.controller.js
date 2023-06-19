angular.module("umbraco").controller("Nikander.VariantCopy", function ($scope, $http, navigationService, contentEditingHelper, editorState) {

    var vm = this;
    vm.busy = true;
    vm.success = false;
    vm.error = false;
    vm.errorMessage = "";
    vm.includeChilderen = false;

    $http({
        method: "GET",
        params: {
            nodeId: $scope.currentNode.id
        },
        url: "backoffice/Nikander/VariantCopy/RetrieveCultures/"
    }).then((response) => {
        console.log(response.data);
        vm.contentCultures = response.data;
        vm.publishedCultures = vm.contentCultures.filter(cultureSetting => cultureSetting.IsPublished);
        vm.fromCulture = vm.publishedCultures[0].IsoCode;
        vm.toCulture = vm.contentCultures[0].IsoCode;
        vm.busy = false;
    })

    vm.variantCopy = async function (nodeId) {
        vm.busy = true;

        var result =  await $http({
            method: "GET",
            params: {
                nodeId: nodeId,
                includeChilderen: vm.includeChilderen,
                fromCulture: vm.fromCulture,
                toCulture: vm.toCulture
            },
            url: "backoffice/Nikander/VariantCopy/CreateContentVariants/"
        });

        if (result.data.IsSucces) {
            vm.success = true;
        }
        else {
            vm.errorMessage = result.data.Error.Message;
            vm.error = true;
        }

        vm.busy = false;
    }
    vm.closeDialog = function () {
        navigationService.hideDialog();
        location.reload();
    };
    vm.includeChilderenClick = function () {
        vm.includeChilderen = !vm.includeChilderen
    };
});