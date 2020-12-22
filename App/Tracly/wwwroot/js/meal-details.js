function mealDetails(id) {
	$.get("/Meal/MealDto", { id: id }, function (data) {
		mealProducts = data.mealProducts;
		$("#mealName").val(data.mealName);
	});
}

$(function () {
	var saveEditBtn = $("#saveEdit");
	var mealId = null;
	if (saveEditBtn) {
		mealId = saveEditBtn.attr("data-access-meal-id");
	}

	mealDetails(mealId);
});
