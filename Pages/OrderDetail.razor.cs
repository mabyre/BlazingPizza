using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingPizza.Pages;

public class OrderDetailBase : ComponentBase, IDisposable
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Parameter] 
    public int OrderId { get; set; }

    protected OrderWithStatus orderWithStatus;

    protected bool invalidOrder = false;

    //protected override async Task OnParametersSetAsync()
    //{
    //    try
    //    {
    //        orderWithStatus = await HttpClient.GetFromJsonAsync<OrderWithStatus>(
    //            $"{NavigationManager.BaseUri}orders/{OrderId}");
    //    }
    //    catch (Exception ex)
    //    {
    //        invalidOrder = true;
    //        Console.Error.WriteLine(ex);
    //    }
    //}

    protected bool IsOrderIncomplete => orderWithStatus is null || orderWithStatus.IsDelivered == false;

    PeriodicTimer timer = new(TimeSpan.FromSeconds(3));

    protected override async Task OnParametersSetAsync() =>
    await GetLatestOrderStatusUpdatesAsync();

    protected override Task OnAfterRenderAsync(bool firstRender) =>
    firstRender ? StartPollingTimerAsync() : Task.CompletedTask;

    async Task GetLatestOrderStatusUpdatesAsync()
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

    async Task StartPollingTimerAsync()
    {
        while (IsOrderIncomplete && await timer.WaitForNextTickAsync())
        {
            await GetLatestOrderStatusUpdatesAsync();
            StateHasChanged();
        }
    }

    public void Dispose() => timer.Dispose();
}
