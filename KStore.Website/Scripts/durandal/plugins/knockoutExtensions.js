define(['knockout'], function (ko) {

    var install = function () {

         
        var previousElement = null;
        ko.bindingHandlers.slideVisible = {
            init: function (element, valueAccessor) {
                var value = valueAccessor();
                $(element).toggle(ko.utils.unwrapObservable(value));
            },
            update: function (element, valueAccessor) {

                var value = ko.utils.unwrapObservable(valueAccessor());
                if (value) {
                    if (previousElement == null) { // initial fade
                        $(element).show();
                    }
                    else {
                        //uses CSS3 Transform for smooth mobile performance
                        $(previousElement).transition({ x: '-100%' }, function () { $(this).hide(); });
                        $(element).css({ x: '100%' });
                        $(element).show().transition({ x: '0%' }, function () {
                            //Callback | transition finished code here
                        });
                    }
                    previousElement = element;
                }
            }
        };
    };

    return {
        install: install
    };
});
