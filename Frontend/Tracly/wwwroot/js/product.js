// PRODUCTS LIST GENERATION

var products;

$("#productNameListInput").keyup(function () {
	var searchQuery = $("#productNameListInput").val();

	if (searchQuery.length > 2) {
		getProducts(searchQuery);
	} else {
		getProducts("");
	}
});

function getProducts(searchQuery) {
	$.ajax({
		url: "/Product/ProductsList/".concat("?queryString=" + searchQuery),
		type: "GET",
		success: function (result, status, xhr) {
			loadProductsTable(result);
		},
	});
}

function loadProductsTable(productsForTable) {
	var urlBase = "/Product/ProductsListTable";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: { products: productsForTable },
		success: function (result, status, xhr) {
			$("#productsListTable").html(result);
			registerFocusout();
			setUpNumeric();
		},
	});
}

function putProduct(productFormData) {
	$.ajax({
		type: "PUT",

		data: productFormData,

		url: "/Product/PutProduct",

		success: function () {
			$("#productModal").modal("hide");
			getProducts("");
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
			getProducts("");
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
			getProducts("");
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

function showProductModal(inputField) {
	$("#productModal").modal({ show: true });
	$("#productModal").on("hidden.bs.modal", function () {
		$("#productForm").validate().resetForm();
		$("#productForm .is-invalid").removeClass("is-invalid");
	});
	document.getElementById("productModalTitle").innerHTML = "Add Product";

	document.getElementById("name").value = document.getElementById(
		inputField
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
			$("#productModal").on("hidden.bs.modal", function () {
				$("#productForm").validate().resetForm();
				$("#productForm .is-invalid").removeClass("is-invalid");
			});

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
						type: "POST",
						data: {
							productName: function () {
								return $("#name").val();
							},
							productId: function () {
								return $("#productModal").attr("data-id");
							},
						},
					},
				},
				kcal: { required: true },
				protein: { required: true },
				carbohydrates: { required: true },
				fat: { required: true },
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
