using System;
using System.Linq;
using Mirzipan.Extensions.Collections;
using Mirzipan.Heist;
using Mirzipan.Heist.Processors;
using Reflex.Attributes;
using Reflex.Core;

namespace Mirzipan.Framed.Reactive
{
    internal sealed class ReactiveSystems : IStartable, IDisposable
    {
        private IReactToAction[] _actions;
        private IReactToCommand[] _commands;

        [Inject]
        private Container _container;
        [Inject]
        private IServerProcessor _processor;
        
        #region Lifecycle

        public void Start()
        {
            _actions = _container.All<IReactToAction>().OrderBy(e => e.Priority, false).ToArray();
            _commands = _container.All<IReactToCommand>().OrderBy(e => e.Priority, false).ToArray();
               
            _processor.OnActionProcessed += OnActionProcessed;
            _processor.OnCommandExecuted += OnCommandExecuted;
        }

        public void Dispose()
        {
            _container = null;
            _processor.OnActionProcessed -= OnActionProcessed;
            _processor.OnCommandExecuted -= OnCommandExecuted;
            _processor = null;
        }

        #endregion Lifecycle

        #region Bindings

        private void OnActionProcessed(IAction action)
        {
            for (var i = 0; i < _actions.Length; i++)
            {
                IReactToAction entry = _actions[i];
                entry.ReactTo(action);
            }
        }

        private void OnCommandExecuted(ICommand command)
        {
            for (var i = 0; i < _commands.Length; i++)
            {
                IReactToCommand entry = _commands[i];
                entry.ReactTo(command);
            }
        }

        #endregion Bindings
    }
}