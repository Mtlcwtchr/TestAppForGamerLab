using UnityEngine;

namespace Task3.Scripts.Modules
{
    public class ModuleFactory : MonoBehaviour
    {
        [SerializeField] private ModulesConfig _modulesConfig;

        private static ModuleFactory _instance;
        public static ModuleFactory Instance => _instance;

        private IModule _shieldRechargeModule;
        private IModule _shieldCapacityModule;
        private IModule _hullCapacityModule;
        private IModule _weaponsRechargeModule;

        private void Awake()
        {
            _instance = this;

            _shieldRechargeModule = new ShieldRechargeModule(_modulesConfig.shieldRechargeFactor);
            _shieldCapacityModule = new ShieldCapacityModule(_modulesConfig.shieldCapacityFactor);
            _hullCapacityModule = new HullCapacityModule(_modulesConfig.hullCapacityFactor);
            _weaponsRechargeModule = new WeaponsRechargeModule(_modulesConfig.weaponsRechargeFactor);
        }

        public IModule Create(Module module) =>
            module switch
            {
                Module.ShieldRecharge => _shieldRechargeModule,
                Module.ShieldCapacity => _shieldCapacityModule,
                Module.HullCapacity => _hullCapacityModule,
                Module.WeaponRecharge => _weaponsRechargeModule,
                _ => null
            };
    }
}