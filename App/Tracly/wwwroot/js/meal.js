var mealProducts;
var products;
var product;

function goToMealsList() {
	$.get("/Meal/RedirectToList");
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
