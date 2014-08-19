define(['services/logger','services/datacontext'], function (logger,datacontext) {
    var title = 'Home',
        newProducts = ko.observableArray(),
        popularProducts = ko.observableArray();

    var vm = {
        activate: activate,
        title: title,
        newProducts: newProducts,
        popularProducts: popularProducts,
        compositionComplete: compositionComplete
    };

    function compositionComplete() {
        $('#slideShow').bxSlider({
            mode: 'fade',
            auto: true,
            pause: 3000,
            adaptiveHeight: true,
       
        });


    }

    return vm;

    //#region Internal Methods
    function activate() {
        datacontext.getNewProductsPartials(newProducts);
        datacontext.getPopularProductsPartials(popularProducts);

        return true;
    }
    //#endregion
});