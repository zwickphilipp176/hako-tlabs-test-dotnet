using FluentValidation;

namespace TestApp.ToDoList.Application.Model.TodoItem.Validation
{
    /// <summary>
    /// Validation rules for <see cref="CreateToDoItem"/>.
    /// </summary>
    public class CreateToDoItemValidator : AbstractValidator<CreateToDoItem>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CreateToDoItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
