using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Shared;

public partial class AddressEditor : ComponentBase
{
    [Parameter] 
    public Address Address { get; set; }

    // InputText has no @ref="startName"
    // 
    //private ElementReference startName;

    //protected override async Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        await startName.FocusAsync();
    //    }
    //}
}
