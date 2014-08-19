define(function () {

    var imageSettings = {
        imageBasePath: '../content/images/products/',
        unknownProduct: 'unknown_product.jpg'
    };

    var remoteServiceName = 'api/breeze';
    

    var routes = [
        {
            route: 'products(/:keyword)',
            moduleId: 'products',
            title: 'Products',
            hash:'#products'
        },

        {
            route: '',
            moduleId: 'home',
            title: 'Hem',
            nav: 1
        },
        {
            route: 'productdetail(/:id)',
            moduleId: 'productdetail',
            visible: false
        },
        {
            route: 'category(/:category)',
            moduleId: 'category',
            title:'category',
            hash: '#category'
        },
        {
            route: 'checkout',
            moduleId: 'checkout',
            title: 'checkout'
        }
    ];


    return {
        routes: routes,
        imageSettings: imageSettings,
        remoteServiceName: remoteServiceName
    }

});
