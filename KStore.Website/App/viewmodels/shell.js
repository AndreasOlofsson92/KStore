define(['durandal/system', 'plugins/router', 'services/logger', 'config', 'services/shoppingcart', 'services/datacontext'],
    function (system, router, logger, config, cart,dbcontext) {

        var categories=ko.observableArray();
        var searchTextProduct = ko.observable("");
        
 
        // Keep a log of the throttled values

        function itemName() {

            if (cart.itemCount() > 1) {
                return cart.itemCount()+ ' Varor ' +cart.totalPrice() + ' Kr.';
            } else if (cart.itemCount()==1) {
                return cart.itemCount() + ' Vara ' + cart.totalPrice() + ' Kr.';
            } else {
                return "Din varukorg är tom"
            }
        }
        function searchProduct() {

            if (searchTextProduct().length > 0) {
                router.navigate('products/' + searchTextProduct());
            }
        }



        searchTextProduct.subscribe(function (val) {

            $("#search-product-text").autocomplete({
                source: function (request, response) {
                    console.log(request)
                    var callback = function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.name,
                                value: item.name
                            }               
                        }));
                    };
                    dbcontext.searchProductNamesPartial(callback, searchTextProduct());
                },
                minLength: 1
            });
        });

        $(document).on('click', '.btn-buy-product', function (e) {
         
            cart.addItemClicked(this, e);
          
            e.preventDefault();
        });
        $(document).on('click', '.btn-shopping-cart', function (e) {
            router.navigate('/checkout');
        });

        $(document).on('click', '.close-cart-overlay', function (e) {

            $('#cart-overlay-wrapper').addClass('display-none');
            e.preventDefault();
        });

        $(document).on('click', '#cart-toggle', function (e) {
            
            $('#cart-overlay-wrapper').toggleClass('display-none');
            e.preventDefault();
        });

        $(document).on('click', '.glyphicon-trash', function (e) {
      
            var id=$(this).parents('.removeItem').find('input[type=hidden]').val();
            cart.removeItem(id);
            
        });

        var shell = {
            cartTotalPrice:cart.totalPrice,
            cartItems:cart.cartItems,
            itemCount:cart.itemCount,
            activate: activate,
            router: router,
            searchTextProduct: searchTextProduct,
            categories: categories,
            searchProduct: searchProduct,
            itemName: itemName
         

        };

     
        
        return shell;



        //#region Internal Methods
        function activate() {
            dbcontext.getCategories(categories);
            console.log(location.hash);
            var paymentSuccessfully = getParameterByName('success');
            console.log(paymentSuccessfully);
            if (paymentSuccessfully=="true") {

                logger.log('Betalning lyckades!', null, 'shell', true);
            }

            return boot();
        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexString = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexString);
            var found = regex.exec(window.location.hash);
            if (found == null)
                return null;
            else
                return decodeURIComponent(found[1].replace(/\+/g, " "));
            }
       
        
        function boot() {
   

            router.on('router:route:not-found', function (fragment) {
                logError('No Route Found', fragment, true);
            });

        
            return router.makeRelative({ moduleId: 'viewmodels' }) // router will look here for viewmodels by convention
                .map(config.routes)            // Map the routes
                .buildNavigationModel() // Finds all nav routes and readies them
                .activate();            // Activate the router
        }
        
        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }

        function logError(msg, data, showToast) {
            logger.logError(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });