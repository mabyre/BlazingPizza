using BlazingPizza.Services;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Shared;

public partial class OrderReview : ComponentBase
{
    [Parameter] 
    public Order Order { get; set; }
}
