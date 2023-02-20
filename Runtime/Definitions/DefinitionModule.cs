using System.Collections.Generic;
using Mirzipan.Framed.Modules;

namespace Mirzipan.Framed.Definitions
{
    public class DefinitionModule: CoreModule
    {
        #region Lifecycle

        protected override void OnInit()
        {
            // TODO: init definition storage
        }

        protected override void OnLoad()
        {
            // TODO: process definitions
        }

        protected override void OnUnload()
        {
            // TODO: dispose definitions
        }

        #endregion Lifecycle

        #region Queries

        public IEnumerable<T> Get<T>() where T : Definition
        {
            // TODO
            yield break;
        }

        public T Get<T>(int id) where T : Definition
        {
            // TODO
            return null;
        }

        public bool TryGet<T>(int id, out T definition) where T : Definition
        {
            // TODO
            definition = null;
            return false;
        }

        public bool Contains<T>(int id) where T : Definition
        {
            // TODO
            return false;
        }

        #endregion Queries
    }
}