using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Pages;

public class OderDetailBase : ComponentBase
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Parameter] 
    public int OrderId { get; set; }

    protected OrderWithStatus orderWithStatus;

    protected bool invalidOrder = false;

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            orderWithStatus = await HttpClient.GetFromJsonAsync<OrderWithStatus>(
                $"{NavigationManager.BaseUri}orders/{OrderId}");
        }
        catch (Exception ex)
        {
            invalidOrder = true;
            Console.Error.WriteLine(ex);
        }
    }
}
