define(function () {
    var mapper = {
        mapDtosToEntities: mapDtosToEntities
    };

    return mapper;

    function mapDtosToEntities(manager, dtos, entityName) {
        // Map an array of DTO's for a type of entity 
        // to an array of Breeze entities that are managed by 
        // the entity manager and are observables
        var entityArray = dtos.map(dtoToEntityMapper);
        return entityArray;

        function dtoToEntityMapper(dto) {
            var metadataStore = manager.metadataStore,
                entityType = metadataStore.getEntityType(entityName),
                id = dto.id,
                key = new breeze.EntityKey(entityType, id),
                entity = manager.getEntityByKey(key);
            if (!entity) {
                // We don't have it, so create it as a partial
                entity = entityType.createEntity({ id: id, isPartial: true });
                manager.attachEntity(entity);
            }
            mapToEntity(entity, dto);
            entity.entityAspect.setUnchanged();
            return entity;
        }

        function mapToEntity(entity, dto) {
            // entity is an object with observables, dto is json
            for (var prop in dto) {
                if (dto.hasOwnProperty(prop)) {
                    entity[prop](dto[prop]);
                }
            }
            return entity;
        }
    }

});