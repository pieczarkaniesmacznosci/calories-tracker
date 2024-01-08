﻿using CaloriesAPI.Dtos;
using MediatR;

namespace CaloriesAPI.Mediator.Command
{
    public class EditMealCommand : IRequest
    {
        public int UserId { get; set; }
        public int MealId { get; set; }
        public MealDto Meal { get; set; }
    }
}