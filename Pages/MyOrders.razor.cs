using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Pages;

public class MyOrdersBase : ComponentBase
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected List<OrderWithStatus> ordersWithStatus = new List<OrderWithStatus>();

    protected override async Task OnParametersSetAsync()
    {
        ordersWithStatus = await HttpClient.GetFromJsonAsync<List<OrderWithStatus>>(
            $"{NavigationManager.BaseUri}orders");
    }
}
