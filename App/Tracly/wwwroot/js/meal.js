var mealProducts;
var product;

function goToMealsList() {
	$.get("/Meal/RedirectToList");
}

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
			setUpNumeric();
		},
	});
}

$("#productForMealListInput").keyup(function () {
	var searchQuery = $("#productForMealListInput").val();

	if (searchQuery.length > 2) {
		loadProductForMealList(searchQuery);
	} else {
		loadProductForMealList();
	}
});

function loadProductForMealList(searchQuery) {
	if (searchQuery === undefined) {
		searchQuery = "";
	}
	var urlBase = "/Meal/ProductListForMealTable".concat(
		"?queryString=" + searchQuery
	);
	$("#productsListForMealTable").load(urlBase);
}

$("#mealListInput").keyup(function () {
	var searchQuery = $("#mealListInput").val();

	if (searchQuery.length > 2) {
		loadMealList(searchQuery);
	} else {
		loadMealList("");
	}
});

function loadMealList(searchQuery) {
	if (searchQuery === undefined) {
		searchQuery = "";
	}
	var urlBase = "/Meal/MealListTable".concat("?queryString=" + searchQuery);
	$("#divMealsSavedPartial").load(urlBase);
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

function deleteProductFromMeal(id) {
	var reducedProductsList = mealProducts.filter((x) => x.productId != id);
	mealProducts = reducedProductsList;
	loadMealProductList(reducedProductsList);
}

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

function eatNow() {
	var meal = {
		MealName: document.getElementById("mealName").value,
		IsSaved: false,
		DateEaten: new Date().toISOString(),
		MealProducts: mealProducts.map((mp) => {
			var newWeight = $(`[data-meal-product-id="${mp.productId}"]`).val();
			return {
				ProductId: mp.productId,
				Weight: newWeight,
			};
		}),
	};
	var urlBase = "/Meal/PostMeal";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: meal,
		success: function (result, status, xhr) {
			loadConsumedMeals();
			//mealDetails();
		},
		error: function () {
			alert("ajax failed");
		},
	});
}

function eatNowSavedMeal(mealId) {
	var mealLog = {
		MealId: mealId,
		DateEaten: new Date().toISOString(),
	};

	var urlBase = "/Meal/EatSavedMeal";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: mealLog,
		success: function (result, status, xhr) {
			loadSavedMeals();
			//mealDetails();
		},
		error: function () {
			alert("ajax failed");
		},
	});
}

function deleteConsumedMeal(mealLogId) {
	var urlBase = "/Meal/DeleteConsumedMeal";
	$.ajax({
		url: urlBase,
		type: "DELETE",
		data: { mealLogId: mealLogId },
		success: function (result, status, xhr) {
			loadConsumedMeals();
			//mealDetails();
		},
		error: function () {
			alert("ajax failed");
		},
	});
}

function deleteSavedMeal(mealId) {
	var urlBase = "/Meal/DeleteSavedMeal";
	$.ajax({
		url: urlBase,
		type: "DELETE",
		data: { mealId: mealId },
		success: function (result, status, xhr) {
			loadSavedMeals();
		},
		error: function () {
			alert("ajax failed");
		},
	});
}

function saveForLater() {
	var meal = {
		MealName: document.getElementById("mealName").value,
		IsSaved: true,
		MealProducts: mealProducts.map((mp) => {
			var newWeight = $(`[data-meal-product-id="${mp.productId}"]`).val();
			return {
				ProductId: mp.productId,
				Weight: newWeight,
			};
		}),
	};
	var urlBase = "/Meal/PostMeal";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: meal,
		success: function (result, status, xhr) {
			goToMealsList();
		},
		error: function () {
			alert("ajax failed");
		},
	});
	goToMealsList();
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
