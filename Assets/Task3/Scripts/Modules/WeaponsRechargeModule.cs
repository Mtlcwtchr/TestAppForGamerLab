namespace Task3.Scripts.Modules
{
    public class WeaponsRechargeModule: IModule
    {
        private readonly float _rechargeModificationFactor;

        public WeaponsRechargeModule(float rechargeModificationFactor)
        {
            _rechargeModificationFactor = rechargeModificationFactor;
        }

        public void Apply(Battleship battleship)
        {
            battleship.WeaponsHandler.ModifyWeaponsRechargeSpeed(_rechargeModificationFactor);
        }

        public void Remove(Battleship battleship)
        {
            battleship.WeaponsHandler.ModifyWeaponsRechargeSpeed(1.0f / _rechargeModificationFactor);
        }
    }
}