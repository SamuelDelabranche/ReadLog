using Microsoft.Extensions.DependencyInjection;
using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Services
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(Type ViewModelType);
    }
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ViewModelFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public ViewModelBase CreateViewModel(Type ViewModelType) => (ViewModelBase)_serviceProvider.GetRequiredService(ViewModelType);
    }
}
