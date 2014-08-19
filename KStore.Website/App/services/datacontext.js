define(['services/model', 'config', 'services/logger', 'durandal/system', 'services/breeze.partial-entities'],
    function (model, config, logger, system, partialMapper) {

        var EntityQuery = breeze.EntityQuery,
            manager = configureBreezeManager();
        var Predicate = breeze.Predicate;


        var entityNames = model.entityNames;

        var datacontext = {
            getProductsFilterAfterPrice: getProductsFilterAfterPrice,
            getProductsPatials: getProductsPatials,
            getNewProductsPartials: getNewProductsPartials,
            getPopularProductsPartials: getPopularProductsPartials,
            getProductById: getProductById,
            searchProductsPartial: searchProductsPartial,
            getCategories: getCategories,
            filterAfterPrice: filterAfterPrice,
            getCategoryAllProducts: getCategoryAllProducts,
            searchProductNamesPartial: searchProductNamesPartial,
            getCategoryAllProductsFilterAfterPrice: getCategoryAllProductsFilterAfterPrice
        };

        function searchProductNamesPartial(callback, keyword) {
            var query = EntityQuery.from('Products').select('name').where('name', 'startsWith', keyword);

            return manager.executeQuery(query).then(querySucceded).fail(queryFailed);

            function querySucceded(data) {

                callback(data.results);


            }
        }


        function getProductsFilterAfterPrice(keyword, productsObservable, showProductsObservable, brandsObservable, highestPrice, lowestPrice) {
            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath,brand.name').expand('Brand');

            var p1 = Predicate.create('price', '>=', lowestPrice);
            var p2 = Predicate.create('price', '<=', highestPrice);
            var p3 = Predicate.create('name', 'startsWith', keyword)
            var pred = Predicate.and([p1, p2, p3]);

            var query = query.where(pred);
            return manager.executeQuery(query).then(querySucceded).fail(queryFailed);

            function querySucceded(data) {

                if (productsObservable) {

                    productsObservable(data.results);
                    showProductsObservable(data.results);

                    var brands = ko.utils.arrayMap(productsObservable(), function (product) {

                        return product.brand_Name;
                    });
                    brandsObservable(ko.utils.arrayGetDistinctValues(brands));
                }

            }
        }

        function getCategoryAllProducts(category, productsObservable, showProductsObservable, brandsObservable) {
            console.log(category);
            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath,brand.name').where('category.name', '==', category).expand('Brand');

            return manager.executeQuery(query).then(querySucceded).fail(queryFailed);

            function querySucceded(data) {

                if (productsObservable) {
                    productsObservable(data.results);
                    showProductsObservable(data.results);

                    var brands = ko.utils.arrayMap(productsObservable(), function (product) {

                        return product.brand_Name;
                    });
                    brandsObservable(ko.utils.arrayGetDistinctValues(brands));

                }

            }
        }

        function getCategoryAllProductsFilterAfterPrice(category, productsObservable, showProductsObservable, brandsObservable, highestPrice, lowestPrice) {
            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath,brand.name').expand('Brand');

            var p1 = Predicate.create('price', '>=', lowestPrice);
            var p2 = Predicate.create('price', '<=', highestPrice);
            var p3 = Predicate.create('category.name', '==', category);
            var pred = Predicate.and([p1, p2, p3]);

            var query = query.where(pred);
            return manager.executeQuery(query).then(querySucceded).fail(queryFailed);

            function querySucceded(data) {

                if (productsObservable) {

                    productsObservable(data.results);
                    showProductsObservable(data.results);

                    var brands = ko.utils.arrayMap(productsObservable(), function (product) {

                        return product.brand_Name;
                    });
                    brandsObservable(ko.utils.arrayGetDistinctValues(brands));
                }

            }
        }


        function getCategories(categoriesObservable) {
            var query = EntityQuery.from('ProductCategories').select('name');

            return manager.executeQuery(query).then(querySucceded);

            function querySucceded(data) {

                if (categoriesObservable) {

                    categoriesObservable(data.results);

                }

            }
        }

        function filterAfterPrice(keyword, highestPrice, lowestPrice, productsObservable) {
            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath').expand('Brand');

            var p1 = Predicate.create('price', '>=', lowestPrice);
            var p2 = Predicate.create('price', '<=', highestPrice);
            var p3 = Predicate.create('category.name', '==', category)
            var pred = Predicate.and([p1, p2, p3]);

            var query = query.where(pred);

            var data = manager.executeQuery(query).then(querySucceded);

            console.log(data.results);
            function querySucceded(data) {

                productsObservable(data.results);

            }
        }
        function getProductsPatials(productsObservable) {

            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath');

            return manager.executeQuery(query).then(querySucceded);

            function querySucceded(data) {

                if (productsObservable) {
                    productsObservable(data.results);
                    console.log(productsObservable());

                }

            }


        };

        function searchProductsPartial(keyword, productsObservable, showProductsObservable, brandsObservable) {

            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath,brand.name').where('name', 'startsWith', keyword).expand('Brand');

            return manager.executeQuery(query).then(querySucceded).fail(queryFailed);

            function querySucceded(data) {

                if (productsObservable) {
                    productsObservable(data.results);
                    showProductsObservable(data.results);

                    var brands = ko.utils.arrayMap(productsObservable(), function (product) {

                        return product.brand_Name;
                    });
                    brandsObservable(ko.utils.arrayGetDistinctValues(brands));

                }

            }
        }




        function getNewProductsPartials(newProductsObservable) {

            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath').orderBy('created').take(3);

            return manager.executeQuery(query).then(querySucceded);

            function querySucceded(data) {
                if (newProductsObservable) {
                    newProductsObservable(data.results);
                }
            }
        }
        function getPopularProductsPartials(popularProductsObservable) {

            var query = EntityQuery.from('Products').select('id,name,description,deliveryTime,stockStatus,price,imagePath').orderByDesc('views').take(3);

            return manager.executeQuery(query).then(querySucceded);

            function querySucceded(data) {
                if (popularProductsObservable) {
                    popularProductsObservable(data.results);
                }
            }


        }


        function getProductById(productId, productObservable) {
            manager.fetchEntityByKey('Product', productId).then(fetchSucceded).fail(queryFailed);

            function fetchSucceded(data) {
                var e = data.entity;
                productObservable(e);
            }
        }

        function getLocal(resource) {
            var query = EntityQuery.from(resource);
            return manager.executeQueryLocally(query);
        }


        function queryFailed(error) {
            console.log(error.message);
            logger.logError(error.message, error, system.getModuleId(datacontext), true)
        }

        function configureBreezeManager() {

            breeze.NamingConvention.camelCase.setAsDefault();
            var mgr = new breeze.EntityManager(config.remoteServiceName)
            //model.configureMetaDataStore(mgr.metadataStore);
            return mgr;
        }



        return datacontext;


    });