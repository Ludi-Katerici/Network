using System.Text;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Refit;

namespace FrontEnd.Infrastructure.UtilityMethods;

public static class ValidationExtensions
{
    public static bool ValidateFormModel<TModel>(this AbstractValidator<TModel> validator, TModel model, EditContext formContext, ValidationMessageStore validationMessageStore)
    {
        validationMessageStore.Clear();

        var result = validator.Validate(model);

        foreach (var error in result.Errors)
        {
            validationMessageStore.Add(formContext.Field(error.PropertyName), error.ErrorMessage);
        }

        formContext.Validate();
        formContext.NotifyValidationStateChanged();

        return result.IsValid;
    }

    public static async Task<bool> ValidateFormModelAsync<TModel>(this AbstractValidator<TModel> validator, TModel model, EditContext formContext, ValidationMessageStore validationMessageStore)
    {
        validationMessageStore.Clear();

        var result = await validator.ValidateAsync(model);

        foreach (var error in result.Errors)
        {
            if (error.ErrorMessage.Contains("Моля"))
            {
                error.ErrorMessage = string.Empty;
            }
            validationMessageStore.Add(formContext.Field(error.PropertyName), error.ErrorMessage);
        }

        formContext.Validate();
        formContext.NotifyValidationStateChanged();

        return result.IsValid;
    }

    static internal void ConstructValidationMessages<TFormModel>(this ErrorResponse? errorResponse,
        EditContext formContext, ValidationMessageStore validationMessageStore, IDictionary<string, string>? mapping = null)
    {
        if (errorResponse is null || errorResponse.Errors.Count is 0)
        {
            validationMessageStore.Add(formContext.Field(string.Empty), "An unknown error occurred.");

            formContext.Validate();
            formContext.NotifyValidationStateChanged();
            return;
        }

        var formModelProperties = typeof(TFormModel).GetProperties().Select(x => x.Name).ToList();

        foreach (var errorProperty in errorResponse.Errors.Keys)
        {
            var modelProperty = formModelProperties.FirstOrDefault(modelProperty => modelProperty.Equals(errorProperty, StringComparison.InvariantCultureIgnoreCase));

            if (modelProperty is null && mapping is not null)
            {
                modelProperty = mapping.FirstOrDefault(x => x.Value.Equals(errorProperty, StringComparison.InvariantCultureIgnoreCase)).Key;
            }

            var fieldIdentifier = formContext.Field(modelProperty ?? string.Empty);

            foreach (var error in errorResponse.Errors[errorProperty])
            {
                validationMessageStore.Add(fieldIdentifier, error);
            }
        }

        formContext.Validate();
        formContext.NotifyValidationStateChanged();
    }

    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    static internal async Task<ErrorResponse> GetErrorResponse(this ApiException? exception)
    {
        if (exception?.Content is null)
        {
            return new ErrorResponse();
        }

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(exception.Content));
        var response = await JsonSerializer.DeserializeAsync<ErrorResponse>(stream, JsonSerializerOptions);

        return response ?? new ErrorResponse();
    }
}