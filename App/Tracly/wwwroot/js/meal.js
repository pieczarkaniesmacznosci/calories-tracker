var mealProducts;
var products;
var product;

function goToMealsList() {
	window.location.replace("/meal/list");
}

// MEAL LIST GENERATION

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

// PRODUCTS FOR MEAL LIST GENERATION

$("#productForMealListInput").keyup(function () {
	var searchQuery = $("#productForMealListInput").val();

	if (searchQuery.length > 2) {
		getProductsForMeal(searchQuery);
	} else {
		getProductsForMeal();
	}
});

function getProductsForMeal(searchQuery) {
	$.ajax({
		url: "/Product/ProductsList/".concat("?queryString=" + searchQuery),
		type: "GET",
		success: function (result, status, xhr) {
			products = result;
			filterAvailableProducts(mealProducts);
			loadProductsForMealTable(products);
		},
	});
}

function loadProductsForMealTable(productsForTable) {
	var urlBase = "/Meal/ProductForMealTable";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: { products: productsForTable },
		success: function (result, status, xhr) {
			$("#productsListForMealTable").html(result);
			registerFocusout();
			setUpNumeric();
		},
	});
}

function filterAvailableProducts(mealProductsToGenerate) {
	products = products.reduce((reducedProducts, product) => {
		var foundProduct = mealProductsToGenerate.find(
			(x) => x.productId === product.id
		);
		if (!!foundProduct) {
			return reducedProducts;
		} else {
			return [...reducedProducts, product];
		}
	}, []);
}

// MEALPRODUCTS GENERATION

function loadMealProductList(mealProductsToGenerate) {
	filterAvailableProducts(mealProductsToGenerate);
	var list = "#mealProductListTable";
	var urlBase = "/Meal/GenerateMealProductListTable";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: { mealProducts: mealProductsToGenerate },
		success: function (result, status, xhr) {
			$(list).html(result);
			registerFocusout();
			setUpNumeric();
		},
	});
}

// function editMealWindow(id) {
// 	var urlBase = "/Meal/MealDto/";

// 	var url = urlBase.concat(id);

// 	$.ajax({
// 		type: "GET",

// 		url: url,

// 		success: function (returnedProduct) {
// 			$("#productModal").modal({ show: true });
// 			$("#productModal").on("hidden.bs.modal", function () {
// 				$("#productForm").validate().resetForm();
// 				$("#productForm .is-invalid").removeClass("is-invalid");
// 			});

// 			populateModalInputs(
// 				returnedProduct["name"],
// 				returnedProduct["kcal"],
// 				returnedProduct["protein"],
// 				returnedProduct["carbohydrates"],
// 				returnedProduct["fat"]
// 			);

// 			$("#productModal").attr("data-id", id);
// 		},

// 		error: function () {
// 			alert("ajax failed");
// 		},

// 		processData: true,
// 	});
// }

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
			var searchQuery = $("#productForMealListInput").val();
			getProductsForMeal(searchQuery);
		},
	});
}

function deleteProductFromMeal(id) {
	var reducedProductsList = mealProducts.filter((x) => x.productId != id);
	mealProducts = reducedProductsList;
	loadMealProductList(reducedProductsList);
}

// function addProductMealModal() {
// 	$("#productModal").modal({ show: true });
// 	$("#productModal").on("hidden.bs.modal", function () {
// 		$("#productForm").validate().resetForm();
// 		$("#productForm .is-invalid").removeClass("is-invalid");
// 	});
// 	document.getElementById("productModalTitle").innerHTML = "Add Product";

// 	document.getElementById("name").value = document.getElementById(
// 		"productForMealListInput"
// 	).value;
// }

function saveEdit(mealLogId) {
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
	var mealLog = {
		Meal: meal,
		Id: mealLogId,
	};
	var urlBase = "/Meal/EditEatenMeal";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: mealLog,
		success: function (result, status, xhr) {
			goToMealsList();
		},
		error: function () {
			alert("ajax failed");
		},
	});
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
			goToMealsList();
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
		},
		error: function () {
			alert("ajax failed");
		},
	});
}

function editConsumedMeal(mealLogId) {
	var urlBase = "/Meal/Details";
	$.ajax({
		url: urlBase,
		type: "GET",
		data: { id: mealLogId },
		success: function (result, status, xhr) {},
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
			goToMealsList();
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
}

function registerFocusout() {
	$(".meal-product-weight-input").focusout(function () {
		var id = $(this).attr("data-meal-product-id");

		mealProducts = mealProducts.map((x) => {
			if (x.productId === Number(id)) {
				return {
					...x,
					weight: $(this).val(),
				};
			} else {
				return x;
			}
		});
	});
}
