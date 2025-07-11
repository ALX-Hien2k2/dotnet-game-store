using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    // Implement method "InvokeAsync" of class "IEndpointFilter"
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // Get the argument of type T (your DTO)
        var dto = context.Arguments.OfType<T>().FirstOrDefault();
        if (dto is null)
        {
            return Results.BadRequest();
        }

        // Run validation
        var validationResults = new List<ValidationResult>();  // a list to collect any errors.
        var validationContext = new ValidationContext(dto);  // provides metadata about the object to validate (its type, services, etc).

        /*
        Validator.TryValidateObject() is a built-in .NET method that:
            - Checks all [Required], [StringLength], [Range], etc. on your DTO.
            - Adds any validation errors into validationResults.
            - Returns true if no errors.
        */

        bool isValid = Validator.TryValidateObject(
            dto,
            validationContext,
            validationResults,
            validateAllProperties: true
        );

        if (!isValid)
        {
            // Create a cleaner, more readable error format
            var errors = validationResults
                .SelectMany(result =>
                    result.MemberNames.Select(member => new
                    {
                        field = member,
                        error = result.ErrorMessage ?? "Validation error."
                    }))
                .ToArray();

            return Results.BadRequest(new
            {
                message = "Validation Failed",
                errors
            });
        }
        
        // Everything is valid, continue to your handler
        return await next(context);
    }
}
