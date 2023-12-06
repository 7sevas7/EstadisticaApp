

using EstadisticaApp.ConfigViewModel.Config;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace EstadisticaApp.ConfigViewModel
{
    //Sera una clase en la cual recibira una clase para la creacion de un vie model desde la vista
                            //La Interfaz devuelve objetos que herdadn de ComponentBase 
    public abstract class CreateViewModel<TViewModel> : ComponentBase where TViewModel : IModelBase
    {
        //Recodemos que una clase abstracta solo sirve para crear clase a tra vez de ella no implemente nada como tal asi que es importante decir que es para sorbre escribir en caso de que se imlementen funciones
        [Inject]
        [NotNull]
#pragma warning disable CS8618
        protected TViewModel ViewModel { get; set; }//Este es el objeto que devolvera 
#pragma warning restore CS8618
        protected override void OnInitialized()
        {
            // Cause changes to the ViewModel to make Blazor re-render
            ViewModel.PropertyChanged += (_, _) => StateHasChanged();
            base.OnInitialized();
        }
        protected override Task OnInitializedAsync()
        {
            return ViewModel.OnInitializedAsync();
        }
    }
}
