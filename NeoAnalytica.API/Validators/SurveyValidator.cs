using FluentValidation;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.Infrastructure;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoAnalytica.API.Validators
{
    public class SurveyValidator : AbstractValidator<SurveyRequest>
    {
        private readonly ISurveyService _surveyService;
        public SurveyValidator(ISurveyService surveyService)
        {
            _surveyService = surveyService;
            RuleFor(x => x.SurveyName).NotEmpty().WithMessage("Survey name is required.");
            RuleFor(x => x.SurveyCategoryID).Custom(async (list, context) =>
            {
                if (context.PropertyValue == null)
                {
                    context.AddFailure("SurveyCategoryID is required.");
                }
                else
                {
                    var category = context.PropertyValue as int?;
                    var surveyCategories = await _surveyService.GetAllSurveyCategories();
                    var categoryValid = surveyCategories.Where(x => x.SurveyCategoryID == category);
                    if (categoryValid == null)
                        context.AddFailure("SurveyCategoryID is invalid.");
                }
            });
        }
    }
}
