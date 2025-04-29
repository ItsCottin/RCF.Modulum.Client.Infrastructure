using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Services
{
    public class MenuService
    {
        public event Action? OnMenuChanged;

        public void NotifyMenuChanged()
        {
            OnMenuChanged?.Invoke();
        }
    }
}
