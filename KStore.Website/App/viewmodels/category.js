define(['services/datacontext'], function (datacontext) {

    var products = ko.observableArray();
    var highestPrice = ko.observable(8000);
    var lowestPrice = ko.observable(0);
    var brands = ko.observableArray();
    var selectBrands = ko.observableArray();
    var showProducts = ko.observableArray();
    var thiscategory="";
    var keyword = "";

    var vm = {
        activate: activate,
        highestPrice: highestPrice,
        lowestPrice: lowestPrice,
        orderByLowestPrice: orderByLowestPrice,
        orderByHighestPrice: orderByHighestPrice,
        brands: brands,
        filterAfterBrand: filterAfterBrand,
        showProducts: showProducts,
        compositionComplete: compositionComplete,
        clearFilter: clearFilter,
        thiscategory: thiscategory

    };


   

    highestPrice.subscribe(function (newValue) {

        datacontext.getCategoryAllProductsFilterAfterPrice(thiscategory, products, showProducts, brands, highestPrice(), lowestPrice());

    });


    lowestPrice.subscribe(function (newValue) {
        datacontext.getCategoryAllProductsFilterAfterPrice(thiscategory, products, showProducts, brands, highestPrice(), lowestPrice());

    });


    function orderByLowestPrice() {
        showProducts.sort(function (highest, lowest) {

            return highest.price< lowest.price ? -1 : 1;
        });
    }

    function orderByHighestPrice() {
        showProducts.sort(function (highest, lowest) {
            return highest.price> lowest.price ? -1 : 1;
        });
    }



    function activate(category) {
        thiscategory = category;
        $('.btn-selected').removeClass('btn-selected');
        selectBrands([]);

        datacontext.getCategoryAllProducts(category, products, showProducts, brands);
 
    }

      
    

    function compositionComplete() {
                   lowestPrice(0);
        orderByHighestPrice();
        highestPrice(showProducts()[0].price);
           
            $("#slider-range").slider({
                range: true,
                min: 0,
                max: highestPrice(),
                values: [lowestPrice(), highestPrice()],
                slide: function (event, ui) {
                    lowestPrice(ui.values[0]);
                    highestPrice(ui.values[1]);
                }
            });
        
      
    }

    function clearFilter() {
        activate(thiscategory);
    }

    function filterAfterBrand() {

        var brand = this.toString();
        if ($.inArray(brand, selectBrands()) === -1) {
            selectBrands.push(brand);
            $("#" + brand).addClass('btn-selected')
        } else {
            selectBrands.remove(brand);
            $("#" + brand).removeClass('btn-selected')
        }
        
        if (selectBrands().length>0) {
            showProducts([]);
            $(products()).each(function (index,product) {
              
                if ($.inArray(product.brand_Name, selectBrands()) !== -1) {
                   
                    showProducts.push(product);
                }
            });
        } else {
            
            showProducts(products());
        }

    }


    return vm;


});