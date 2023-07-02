using System.Collections;
using System.Collections.Generic;
using System;

namespace Game
{
    public class UIActionBase : Object
    {}

    public class UIAction : UIActionBase
    {
        private Action _action;
        public void Register(Action action)
        {
            _action += action;
        }
        public void Unregister(Action action)
        {
            _action -= action;
        }
        public void Invoke()
        {
            _action?.Invoke();
        }
    }

    public class UIAction<T> : UIActionBase
    {
        private Action<T> _action;
        public void Register(Action<T> action)
        {
            _action += action;
        }
        public void Unregister(Action<T> action)
        {
            _action -= action;
        }
        public void Invoke(T value)
        {
            _action?.Invoke(value);
        }
    }

    public class UIAction<T, T2> : UIActionBase
    {
        private Action<T, T2> _action;
        public void Register(Action<T, T2> action)
        {
            _action += action;
        }
        public void Unregister(Action<T, T2> action)
        {
            _action -= action;
        }
        public void Invoke(T value, T2 value2)
        {
            _action(value, value2);
        }
    }

}
