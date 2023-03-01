using Microsoft.AspNetCore.Components;

namespace Daisy.Client.Wasm.Components
{
    public class RedirectToAccessDenied : ComponentBase
    {
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo("/accessdenied");
        }
    }
}
