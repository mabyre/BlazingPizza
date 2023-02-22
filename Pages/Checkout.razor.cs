using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Pages;

public class CheckoutBase : ComponentBase
{
    protected static Order order => OrderState.Order;
}
