using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BlazingPizza.Pages;

public class IndexBase : ComponentBase
{
    [Inject]
    protected HttpClient HttpClient { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected OrderState OrderState { get; set; }

    [Inject]
    protected IJSRuntime JavaScript { get; set; }

    protected List<PizzaSpecial> specials = new();

    protected Order order => OrderState.Order;

    protected override async Task OnInitializedAsync()
    {
        specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>(NavigationManager.BaseUri + "specials");
    }

    protected async Task RemovePizzaConfirmation(Pizza removePizza)
    {
        if (await JavaScript.InvokeAsync<bool>(
            "confirm",
            $"Do you want to remove the \"{removePizza.Special!.Name}\" from your order?"))
        {
            OrderState.RemoveConfiguredPizza(removePizza);
        }
    }
}

