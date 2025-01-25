
function addToCart(productId) {
    fetch(`/Cart/Add?productId=${productId}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // تحديث عداد السلة
                const cartCount = document.getElementById("cart-count");
                cartCount.innerText = data.cartCount;
            } else {
                alert("حدث خطأ أثناء إضافة المنتج إلى السلة.");
            }
        })
        .catch(error => console.error("Error:", error));
}