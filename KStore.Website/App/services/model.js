define(['config'], function (config) {

    var imageSettings = config.imageSettings;

    var entityNames = {
        product:'Product'
    };

    function configureMetaDataStore(metadataStore) {
        metadataStore.registerEntityTypeCtor(
            'Product', function () { this.isPartial=false},productInitializer
            );
    }

    function productInitializer(product) {

    }
    var model = {
        configureMetaDataStore: configureMetaDataStore,
        entityNames:entityNames
    };

    return model;
});