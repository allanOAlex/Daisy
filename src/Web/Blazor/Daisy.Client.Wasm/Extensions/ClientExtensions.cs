using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Daisy.Client.Wasm.Extensions
{
    public static class ClientExtensions
    {
        public static string[] HttpCodes { get; set; } = { "401", "403" };
        public static ValueTask FocusAsync(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeVoidAsync("BlazorFocusElement", elementReference);
        }
    }
}
