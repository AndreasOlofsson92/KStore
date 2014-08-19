define(['services/shoppingcart'], function (cart) {

    var vm = {
        activate:activate,
        cartItems: cart.cartItems,
        totalPrice: cart.totalPrice,
        summa:cart.summa
    };




    $(document).on('click', '.btn-checkout-paypal', function () {

        var cartItems = [];
        
        $.each(cart.cartItems(), function (i, item) {
            var cartItem = { productId: item.id, name: item.name, quantity: item.quantity, price: item.price.trim().split(' ')[0] };
            cartItems.push(cartItem);
        });

        $.ajax({
            type: 'post',
            url: '/paypal/SetExpressCheckout',
            contentType: "application/json; charset=utf-8",
            data:JSON.stringify(cartItems)

        }).done(function (response) {
            console.log(response.url);
            window.location = response.url;
        });
    });

    function activate() {
        
    }

    return vm;

});