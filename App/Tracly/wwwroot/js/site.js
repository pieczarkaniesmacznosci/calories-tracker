// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#productNameListInput").keyup(function () {
	var searchQuery = $("#productNameListInput").val();

	if (searchQuery.length > 2) {
		loadList(searchQuery);
	} else {
		loadList();
	}
});

function loadList(searchQuery) {
	if (searchQuery === undefined) {
		searchQuery = "";
	}
	var urlBase = "/Product/ProductsList/".concat("?queryString=" + searchQuery);
	$("#divPartial").load(urlBase);
}

function putProduct(productFormData) {
	$.ajax({
		type: "PUT",

		data: productFormData,

		url: "/Product/PutProduct",

		success: function () {
			$("#productModal").modal("hide");
			loadList();
		},

		error: function () {
			alert("ajax failed");
		},

		processData: false,
	});
}

function postProduct(productFormData) {
	$.ajax({
		type: "POST",

		data: productFormData,

		url: "/Product/PostProduct",

		success: function () {
			$("#productModal").modal("hide");
			loadList();
		},

		error: function () {
			alert("ajax failed");
		},

		processData: false,
	});
}

function saveProduct() {
	var productFormData = $("#productForm").serialize();

	var productId = $("#productModal").attr("data-id");

	if (productId === undefined) {
		postProduct(productFormData);
	} else {
		productFormData = productFormData.concat("&id=" + productId);

		putProduct(productFormData);
	}
}

function deleteProduct(id) {
	var urlBase = "/Product/DeleteProduct/";

	var url = urlBase.concat(id);

	$.ajax({
		type: "DELETE",

		url: url,

		success: function () {
			loadList();
		},

		error: function () {
			alert("ajax failed");
		},

		processData: true,
	});
}

function editProduct(id) {
	var urlBase = "/Product/EditProduct/";

	var url = urlBase.concat(id);

	$.ajax({
		type: "PUT",

		url: url,

		success: function () {
			loadList();
		},

		error: function () {
			alert("ajax failed");
		},

		processData: true,
	});
}

function addProductModal() {
	$("#productModal").modal({ show: true });

	document.getElementById("productModalTitle").innerHTML = "Add Product";

	document.getElementById("name").value = document.getElementById(
		"productNameListInput"
	).value;
}

function editProductModal(id) {
	var urlBase = "/Product/GetProduct/";

	var url = urlBase.concat(id);

	$.ajax({
		type: "GET",

		url: url,

		success: function (returnedProduct) {
			$("#productModal").modal({ show: true });

			populateModalInputs(
				returnedProduct["name"],

				returnedProduct["kcal"],

				returnedProduct["protein"],

				returnedProduct["carbohydrates"],

				returnedProduct["fat"]
			);

			$("#productModal").attr("data-id", id);
		},

		error: function () {
			alert("ajax failed");
		},

		processData: true,
	});
}

function populateModalInputs(name, kcal, protein, carbohydrates, fat) {
	document.getElementById("productModalTitle").innerHTML = "Edit Product";

	$("#name").val(name);

	$("#kcal").val(kcal);

	$("#protein").val(protein);

	$("#carbohydrates").val(carbohydrates);

	$("#fat").val(fat);
}
