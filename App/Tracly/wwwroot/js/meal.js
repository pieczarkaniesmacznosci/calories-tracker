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
	var reducedProductsList = mealProducts.mealProducts.filter(
		(x) => x.id != id
	);
	mealProducts.mealProducts = reducedProductsList;
	loadMealProductList(reducedProductsList);
}

var mealProducts;
var product;
$(document).ready(function () {
	if (window.location.href.indexOf("/meal/details") > -1) {
		var id = 1;
		mealDetails(id);
	}
});

function mealDetails(id) {
	$.get("/Meal/MealDto", { id: id }, function (data) {
		mealProducts = data;
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

function addProductToMeal(productId) {
	mealProducts.mealProducts.push({
		productId: product.Id,
		product: product,
		weight: 100,
	});
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
