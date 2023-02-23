using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingPizza.Pages;

public class CheckoutBase : ComponentBase
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected Order order => OrderState.Order;

    protected bool isSubmitting;

    protected bool isErrorName = false;
    protected bool isError = false;

    protected async Task PlaceOrder()
    {
        var response = await HttpClient.PostAsJsonAsync(NavigationManager.BaseUri + "orders", OrderState.Order);
        var newOrderId = await response.Content.ReadFromJsonAsync<int>();
        OrderState.ResetOrder();
        NavigationManager.NavigateTo($"myorders/{newOrderId}");
    }
    protected async Task CheckSubmission(EditContext editContext)
    {
        isSubmitting = true;
        var model = editContext.Model as Address;
        isErrorName = string.IsNullOrWhiteSpace(model?.Name);
        isError = string.IsNullOrWhiteSpace(model?.Line1)
            || string.IsNullOrWhiteSpace(model?.PostalCode);
        if (!isError && !isErrorName)
        {
            await PlaceOrder();
        }
        isSubmitting = false;
    }
}
