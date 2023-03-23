namespace Task3.Scripts.Modules
{
    public class ShieldRechargeModule: IModule
    {
        private readonly float _rechargeModificationFactor;

        public ShieldRechargeModule(float rechargeModificationFactor)
        {
            _rechargeModificationFactor = rechargeModificationFactor;
        }

        public void Apply(Battleship battleship)
        {
            battleship.Shield.ModifyRechargeValue(_rechargeModificationFactor);
        }

        public void Remove(Battleship battleship)
        {
            battleship.Shield.ModifyRechargeValue(1.0f / _rechargeModificationFactor);
        }
    }
}