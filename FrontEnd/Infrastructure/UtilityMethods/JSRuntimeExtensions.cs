using Microsoft.JSInterop;

namespace FrontEnd.Infrastructure.UtilityMethods;

static internal class JsRuntimeExtensions
{
    public static async Task ToastSuccess(this IJSRuntime jsRuntime, string message)
    {
        const string identifier = "notyf.success";
        await jsRuntime.InvokeVoidAsync(identifier, message);
    }
    
    public static async Task ToastError(this IJSRuntime jsRuntime, string message)
    {
        const string identifier = "notyf.error";
        await jsRuntime.InvokeVoidAsync(identifier, message);
    }
}