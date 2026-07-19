using FluentValidation;
using TestApp.ToDoList.Application.Model.TodoItem;

namespace TestApp.ToDoList.Application.Model.TodoItem.Validation
{
    /// <summary>
    /// Validation rules for <see cref="EditToDoItem"/>.
    /// </summary>
    public class EditToDoItemValidator : AbstractValidator<EditToDoItem>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public EditToDoItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
