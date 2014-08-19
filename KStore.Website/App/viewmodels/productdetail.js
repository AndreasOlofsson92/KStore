define(['services/datacontext','plugins/router'],
    function (datacontext, router) {

        var product = ko.observable();


        var title = 'Product details';


        function activate(routeData) {

            var id = parseInt(routeData);

            datacontext.getProductById(id, product);
        }


        var vm = {
            activate: activate,
            title: title,
            product: product
            
        };
        return vm;

    });