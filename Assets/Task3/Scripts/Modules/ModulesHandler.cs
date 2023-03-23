using System.Collections.Generic;
using UnityEngine;

namespace Task3.Scripts.Modules
{
    public class ModulesHandler : MonoBehaviour
    {
        [SerializeField] private int modulesCapacity;

        private List<IModule> _modules;

        private void Awake()
        {
            _modules = new List<IModule>(modulesCapacity);
        }

        public bool TryAddModule(IModule module)
        {
            if (!CanAddModule())
            {
                return false;
            }

            _modules.Add(module);
            return true;
        }

        public bool TryRemoveModule(IModule module)
        {
            return _modules.Remove(module);
        }

        public bool CanAddModule()
        {
            return _modules.Count < modulesCapacity;
        }
    }
}
