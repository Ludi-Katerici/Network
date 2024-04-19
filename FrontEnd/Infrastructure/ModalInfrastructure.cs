using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Infrastructure;

public static class ModalInfrastructure
{
    public static readonly ModalOptions Default = new()
    {
        Position = ModalPosition.Middle,
        HideHeader = true,
        HideCloseButton = true,
        ActivateFocusTrap = true,
        AnimationType = ModalAnimationType.FadeInOut,
        DisableBackgroundCancel = true
    };
    
    public static IModalReference Show<TComponent>(this IModalService modalService, params (string name, object value)[] parameters) 
        where TComponent : IComponent
    {
        var modalParameters = new ModalParameters();
        foreach (var (name, value) in parameters)
        {
            modalParameters.Add(name, value);
        }
        return modalService.Show<TComponent>(string.Empty, modalParameters, Default);
    }
}