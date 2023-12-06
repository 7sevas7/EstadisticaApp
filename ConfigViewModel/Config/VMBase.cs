using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EstadisticaApp.ConfigViewModel.Config
{
    public abstract partial class VMBase : ObservableObject, IModelBase
    {
        [RelayCommand]
        public virtual async Task Loaded()
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }
        
        //Configuración de los objetos para la vista 
        protected virtual void NotifyStateChanged() => OnPropertyChanged((string?)null);

        
        public virtual async Task OnInitializedAsync()
        {
            await Loaded().ConfigureAwait(true);
        }

       
    }
}
