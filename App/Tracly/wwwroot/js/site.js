// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function saveProduct() {
    var productFormData = $('#productForm').serialize();

    $.ajax({
        type: "POST",
        data: productFormData,
        url: "/Product/PostProduct",
        success: function () {
            $("#productModal").hide();
        },
        error: function () {
            alert("ajax failed");
        },
        processData: false
    })
}