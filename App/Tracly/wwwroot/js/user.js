function getCurrentUserWeightData() {
	var urlBase = "/User/UserWeight";
	$.ajax({
		url: urlBase,
		type: "GET",
		success: function (result, status, xhr) {
			populateUserWeightInputs(result);
		},
	});
}

function getCurrentUserNutritionData() {
	var urlBase = "/User/UserNutrition";
	$.ajax({
		url: urlBase,
		type: "GET",
		success: function (result, status, xhr) {
			populateUserNutritionInputs(result);
		},
	});
}

function populateUserWeightInputs(userWeight) {
	$("#currentWeight").val(userWeight.weight);
}

$("#userNutritionAccordion").on("show.bs.collapse", function () {
	getCurrentUserNutritionData();
});

$("#userWeightAccordion").on("show.bs.collapse", function () {
	getCurrentUserWeightData();
});

function populateUserNutritionInputs(userNutrition) {
	$("#currentKcalIntake").val(userNutrition.kcal);
	$("#currentProteinIntake").val(userNutrition.protein);
	$("#currentCarbohydratesIntake").val(userNutrition.carbohydrates);
	$("#currentFatIntake").val(userNutrition.fat);
}
