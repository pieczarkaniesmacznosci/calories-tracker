var nutritionFormValidator;
var weightFormValidator;

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

function PostUserNutrition() {
	if (!$("#nutritionForm").valid()) {
		return;
	}
	var userNutrition = $("#nutritionForm").serialize();
	var urlBase = "/User/PostUserNutrition";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: userNutrition,
		success: function () {
			getCurrentUserNutritionData();
		},
	});
}

function PostUserWeight() {
	if (!$("#weightForm").valid()) {
		return;
	}
	var userWeight = $("#weight").serialize();
	var urlBase = "/User/PostUserWeight";
	$.ajax({
		url: urlBase,
		type: "POST",
		data: userWeight,
		success: function () {
			getCurrentUserWeightData();
		},
	});
}

function populateUserWeightInputs(userWeight) {
	$("#currentWeight").text(userWeight.weight);
}

$("#userNutritionAccordion").on("show.bs.collapse", function () {
	getCurrentUserNutritionData();
});

$("#userWeightAccordion").on("show.bs.collapse", function () {
	getCurrentUserWeightData();
});

function populateUserNutritionInputs(userNutrition) {
	$("#currentKcalIntake").text(userNutrition.kcal);
	$("#currentProteinIntake").text(userNutrition.protein);
	$("#currentCarbohydratesIntake").text(userNutrition.carbohydrates);
	$("#currentFatIntake").text(userNutrition.fat);
}

$(".collapsible").on("hide.bs.collapse", function () {
	nutritionFormValidator.resetForm();
	weightFormValidator.resetForm();
	$("#nutritionForm")[0].reset();
	$("#weightForm")[0].reset();
});

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
	var $weightForm = $("#weightForm");
	if ($weightForm.length) {
		weightFormValidator = $weightForm.validate({
			rules: {
				weight: { required: true },
			},
			messages: {
				weight: {
					required: "Weight is required!",
				},
			},
		});
	}
	var $nutritionForm = $("#nutritionForm");
	if ($nutritionForm.length) {
		nutritionFormValidator = $nutritionForm.validate({
			rules: {
				kcal: { required: true },
				protein: { required: true },
				carbohydrates: { required: true },
				fat: { required: true },
			},
			messages: {
				kcal: {
					kcal: "Kcal intake is required!",
				},
				protein: {
					protein: "Protein intake is required!",
				},
				carbohydrates: {
					carbohydrates: "Carbohydrates intake is required!",
				},
				fat: {
					fat: "Fat intake is required!",
				},
			},
		});
	}
});
