
using System.ComponentModel;

namespace EstadisticaApp.ConfigViewModel.Config
{
    public  interface IModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loaded();
    }
}
