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
        private IReactToCommand[] _systems;

        [Inject]
        private Container _container;
        [Inject]
        private IServerProcessor _processor;
        
        #region Lifecycle

        public void Start()
        {
            _systems = _container.All<IReactToCommand>().OrderBy(e => e.Priority, false).ToArray();
               
            _processor.OnCommandExecuted += OnCommandExecuted;
        }

        public void Dispose()
        {
            _container = null;
            _processor.OnCommandExecuted -= OnCommandExecuted;
            _processor = null;
        }

        #endregion Lifecycle

        #region Bindings

        private void OnCommandExecuted(ICommand command)
        {
            for (var i = 0; i < _systems.Length; i++)
            {
                IReactToCommand entry = _systems[i];
                entry.ReactTo(command);
            }
        }

        #endregion Bindings
    }
}