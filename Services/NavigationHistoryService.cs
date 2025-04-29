using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Services
{
    public class NavigationHistoryService
    {
        public bool IsNavigatingBack { get; private set; } = false;

        public void MarkAsNavigatingBack()
        {
            IsNavigatingBack = true;
        }

        public void ClearNavigatingBack()
        {
            IsNavigatingBack = false;
        }

        private readonly Stack<string> _history = new();
        public void AddPage(string uri)
        {
            if (_history.Count == 0 || _history.Peek() != uri)
                _history.Push(uri);
        }

        public string? GoBack()
        {
            if (_history.Count > 1)
            {
                _history.Pop(); // Remove a atual
                return _history.Peek(); // Retorna a anterior
            }

            return null;
        }
        public void Clear()
        {
            _history.Clear();
        }
    }
}
