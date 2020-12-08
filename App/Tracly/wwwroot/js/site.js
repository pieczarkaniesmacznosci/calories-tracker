// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#productNameListInput").keyup(function () {
	var searchQuery = $("#productNameListInput").val();

	if (searchQuery.length > 2) {
		loadList(searchQuery);
	} else {
		loadList("");
	}
});

function loadList(searchQuery) {
	if (searchQuery === undefined) {
		searchQuery = "";
	}
	var urlBase = "/Product/ProductsList/".concat("?queryString=" + searchQuery);
	$("#divProductsPartial").load(urlBase);
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
	if (!$("#productForm").valid()) {
		return;
	}

	var productFormData = $("#productForm").serialize();

	var productId = $("#productModal").attr("data-id");

	if (productId === undefined) {
		postProduct(productFormData);
		loadList();
	} else {
		productFormData = productFormData.concat("&id=" + productId);

		putProduct(productFormData);
		loadList();
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
	$("#productModal").on("hidden.bs.modal", function () {
		$("#productForm").validate().resetForm();
		$("#productForm .is-invalid").removeClass("is-invalid");
	});
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

function setUpNumeric() {
	$(".numeric").numeric({
		decimal: ".",
		negative: false,
		precision: 2,
	});
}

$(function () {
	setUpNumeric();
	$.validator.setDefaults({
		errorClass: "text-danger",
		highlight: function (element) {
			$(element).addClass("is-invalid");
		},
		unhighlight: function (element) {
			$(element).removeClass("is-invalid");
		},
	});
	var $productForm = $("#productForm");
	if ($productForm.length) {
		$productForm.validate({
			rules: {
				name: {
					required: true,
					minlength: 3,
					remote: {
						url: "/Product/ProductNameValid",
						async: false,
						type: "post",
						data: {
							productName: function () {
								return $("#name").val();
							},
						},
					},
				},
				kcal: {
					required: true,
				},
				protein: {
					required: true,
				},
				carbohydrates: {
					required: true,
				},
				fat: {
					required: true,
				},
			},
			messages: {
				name: {
					required: "Product name is required!",
					remote: "Product already exists!",
				},
				kcal: {
					required: "Kcal is required!",
				},
				protein: {
					required: "Insert protein content!",
				},
				carbohydrates: {
					required: "Insert carbohydrates content!",
				},
				fat: {
					required: "Insert fat content!",
				},
			},
		});
	}
});
