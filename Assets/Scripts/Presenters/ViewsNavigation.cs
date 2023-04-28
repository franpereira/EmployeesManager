using System.Collections.Generic;

namespace Employees.Presenters
{
    public static class ViewsNavigation
    {
        static readonly Stack<INavigable> _stack = new();
        
        public static void NavigateTo(INavigable viewPresenter)
        {
            if (_stack.Count > 0) 
                _stack.Peek().HideUI();
            _stack.Push(viewPresenter);
            viewPresenter.ShowUI();
        }
        
        public static void NavigateBack()
        {
            if (_stack.Count <= 0) return;
            _stack.Pop().HideUI();
            if (_stack.Count > 0)
                _stack.Peek().ShowUI();
        }
        
        public static void NavigateBackToRoot()
        {
            while (_stack.Count > 1)
                _stack.Pop().HideUI();
            _stack.Peek().ShowUI();
        }
    }
}