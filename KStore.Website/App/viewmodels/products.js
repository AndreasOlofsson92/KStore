define(['services/datacontext'],
    function (datacontext) {

        var products = ko.observableArray();
        var highestPrice = ko.observable(5000);
        var lowestPrice = ko.observable(0);
        var thiskeyword="";
        var currency = ko.observable('kr.');
        var selectBrands = ko.observableArray();
        var showProducts = ko.observableArray();
        var brands = ko.observableArray();

        highestPrice.subscribe(function(newValue) {
            datacontext.getProductsFilterAfterPrice(thiskeyword, products, showProducts, brands, highestPrice(), lowestPrice());

        });

        
        lowestPrice.subscribe(function (newValue) {
            datacontext.getProductsFilterAfterPrice(thiskeyword,products, showProducts, brands, highestPrice(), lowestPrice());

        });


        var title = 'Products';
        var vm = {
            highestPrice: highestPrice,
            lowestPrice:lowestPrice,
            activate: activate,
            products: products,
            orderByLowestPrice: orderByLowestPrice,
            orderByHighestPrice: orderByHighestPrice,
            currency: currency,
            compositionComplete: compositionComplete,
            filterAfterBrand: filterAfterBrand,
            brands: brands,
            clearFilter: clearFilter

        };

        function orderByLowestPrice() {
            products.sort(function (highest, lowest) {
                return highest.price < lowest.price ? -1 : 1;
            });
        }

        function orderByHighestPrice() {
            products.sort(function (highest, lowest) {
                return highest.price > lowest.price ? -1 : 1;
            });
        }

        function clearFilter() {
            activate(keyword);
        }

        function refresh(){
            datacontext.getProductsPatials(products);
            
        }

        function activate(keyword) {
            if (keyword === null) {
                refresh();
            } else {
                $('.btn-selected').removeClass('btn-selected');
                selectBrands([]);
                datacontext.searchProductsPartial(keyword, products, showProducts,brands);
              
            }
            thiskeyword = keyword;


        }
        
        function compositionComplete() {
 
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
   
        function filterAfterBrand() {

            var brand = this.toString();
            if ($.inArray(brand, selectBrands()) === -1) {
                selectBrands.push(brand);
                $("#" + brand).addClass('btn-selected')
            } else {
                selectBrands.remove(brand);
                $("#" + brand).removeClass('btn-selected')
            }

            if (selectBrands().length > 0) {
                showProducts([]);
                $(products()).each(function (index, product) {

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
