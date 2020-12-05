var mealProducts;
var product;

function loadMealsList(isSaved) {
	var list;
	if (isSaved) {
		list = "#divMealsSavedPartial";
	} else {
		list = "#divMealsConsumedPartial";
	}
	var urlBase = "/Meal/MealsList/".concat("?saved=" + isSaved);
	$(list).load(urlBase);
}
$(function () {
	loadMealsList(false);
});

function deleteProductFromMeal(id) {
	var reducedProductsList = mealProducts.filter((x) => x.id != id);
	mealProducts = reducedProductsList;
	loadMealProductList(reducedProductsList);
}

$(document).ready(function () {
	if (window.location.href.indexOf("/meal/details") > -1) {
		mealDetails();
	}
});

function mealDetails(id) {
	$.get("/Meal/MealDto", { id: id }, function (data) {
		mealProducts = data.mealProducts;
	});
}

function loadMealProductList(products) {
	var list = "#mealProductListTable";
	var urlBase = "/Meal/GenerateMealProductListTable";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: { mealProducts: products },
		success: function (result, status, xhr) {
			$(list).html(result);
		},
	});
}

$("#productForMealListInput").keyup(function () {
	var searchQuery = $("#productForMealListInput").val();

	if (searchQuery.length > 2) {
		loadProductForMealList(searchQuery);
	} else {
		return;
	}
});

function loadProductForMealList(searchQuery) {
	if (searchQuery === undefined) {
		searchQuery = "";
	}
	var urlBase = "/Meal/ProductListForMealTable".concat(
		"?queryString=" + searchQuery
	);
	console.log(urlBase);
	$("#productsListForMealTable").load(urlBase);
}

function getProduct(id) {
	var urlBase = "/Product/GetProduct/";
	var url = urlBase.concat(id);

	$.ajax({
		type: "GET",
		url: url,
		success: function (returnedProduct) {
			product = returnedProduct;
		},
	});
}

function addProductToMeal(productId) {
	var urlBase = "/Product/GetProduct/";

	var url = urlBase.concat(productId);

	$.ajax({
		type: "GET",
		url: url,
		success: function (product) {
			mealProducts.push({
				productId: product.id,
				product: product,
				weight: 100,
			});
			loadMealProductList(mealProducts);
		},
	});
}
// ------------ VALIDATION RULES ------------
$(function () {
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
				mealName: {
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

function addProductMealModal() {
	$("#productModal").modal({ show: true });
	$("#productModal").on("hidden.bs.modal", function () {
		$("#productForm").validate().resetForm();
		$("#productForm .is-invalid").removeClass("is-invalid");
	});
	document.getElementById("productModalTitle").innerHTML = "Add Product";

	document.getElementById("name").value = document.getElementById(
		"productForMealListInput"
	).value;
}

function eatNow() {}
function saveForLater() {}