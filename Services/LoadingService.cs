using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Services
{
    public class LoadingService
    {
        public event Action? OnLoadingStateChanged;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnLoadingStateChanged?.Invoke();
                }
            }
        }

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;
    }
}
