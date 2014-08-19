define([ 'services/logger'],function (logger) {

    var cart = new cart();
 
    var kStore={
        removeItem:cart.removeItem,
        addItemClicked:cart.addItemClicked,
        empty: cart.empty,
        itemCount: cart.itemCount,
        cartItems: cart.items,
        totalPrice: cart.totalPrice,
        summa:cart.summa
        
    };
    function cartItem(args) {
       
        var self = this;
         self.id = args.id || 0;
         self.quantity = args.quantity || 1;
         self.name = args.name;
         self.price = args.price;
         self.image = args.image;
    }


    function cart() {

        var self = this;
        self.items = ko.observableArray();


        //add items
        self.summa = function () {
            
            var summa = 0;
            var item = self.findById(id);
            if (item) {
                summa += parseInt(item.price.trim().split(' ')[0]) * parseInt(item.quantity);
            }
            return summa;
        };

        self.removeItem = function (id) {
            console.log(id);
            var item = self.findById(id);
            if (item) {
                self.items.remove(item);
            };
        };

        self.addItemClicked= function(data, ev) {

            var product = $(ev.currentTarget).attr("id");
            var image = $(data).parent().parent().parent().parent().find('img').attr('src');
            var name = $(data).parent().parent().find('.product-name').text();
            var price = $(data).parent().find('p.product-price').text();
           
            logger.log('Din varukorg har uppdaterats', null, name, true);
            if (product) {
                self.addItem({ id: product ,name:name, price:price, image:image});
            }
        }


        self.addItem = function (item) {
            var existing = self.findById(item.id);
            if (existing) {
                existing.quantity += 1;

                var oldItems = self.items.removeAll();
                self.items(oldItems);
            } else {
                var newItem = new cartItem(item);
                self.items.push(newItem);

            }

            return existing;
        };

        self.empty=function () {
            self.items.removeAll();
        };

        self.itemCount=function () {
            return self.items().length;
        };

        self.findById = function (id) {

           return ko.utils.arrayFirst(self.items(), function (item) {
                console.log(item);
             return   item.id === id;
            });
        };

        self.totalPrice = function () {
            var totalPrice = 0;

            ko.utils.arrayFirst(self.items(), function (item) {

                totalPrice += parseInt(item.price.trim().split(' ')[0]) * parseInt(item.quantity);
                
            

            });

            return totalPrice;
        }

        self.items.subscribe(function (item) {
            localStorage.setItem("KStoreCart", JSON.stringify(item));
        });

    }
        //find items
        //remove
        //totaling
        //storage
        //send off

        //debugg

    return kStore;


});