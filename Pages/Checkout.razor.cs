using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingPizza.Pages;

public class CheckoutBase : ComponentBase, IDisposable
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected Order order => OrderState.Order;

    protected bool isError = true; // show div alerte and button disabled

    protected EditContext editContext;

    protected override void OnInitialized()
    {
        editContext = new(order.DeliveryAddress);
        editContext.OnFieldChanged += HandleFieldChanged;
    }

    public void Dispose()
    {
        editContext.OnFieldChanged -= HandleFieldChanged;
    }

    private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
    {
        isError = !editContext.Validate();
        StateHasChanged();
    }

    protected async Task PlaceOrder()
    {
        var response = await HttpClient.PostAsJsonAsync(NavigationManager.BaseUri + "orders", OrderState.Order);
        var newOrderId = await response.Content.ReadFromJsonAsync<int>();
        OrderState.ResetOrder();
        NavigationManager.NavigateTo($"myorders/{newOrderId}");
    }
}
