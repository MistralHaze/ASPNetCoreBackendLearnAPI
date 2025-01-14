using BackendLearnUdemy.DataTransferObjects;
using FluentValidation;

namespace BackendLearnUdemy.Validators
{
    public class BeerUpdateValidator:AbstractValidator<BeerUpdateDTO>
    {
        public BeerUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull();

            //Same as BeerInsertValidator, 
            string errMessage = "Name must not be empty";
            RuleFor(x => x.Name).NotEmpty().WithMessage(errMessage);
            RuleFor(x => x.Name).Length(1, 15).WithMessage("name must be shorter than 15 char");

            RuleFor(x => x.BrandId).NotNull();
            RuleFor(x => x.BrandId).GreaterThan(0);

            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("{PropertyName} must be bigger than 0");

        }

    }
}
