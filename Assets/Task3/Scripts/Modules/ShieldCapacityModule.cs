namespace Task3.Scripts.Modules
{
    public class ShieldCapacityModule: IModule
    {
        private readonly float _shieldCapacityModificationFactor;

        public ShieldCapacityModule(float modificationFactor)
        {
            _shieldCapacityModificationFactor = modificationFactor;
        }

        public void Apply(Battleship battleship)
        {
            battleship.Shield.AddCapacity(_shieldCapacityModificationFactor);
        }

        public void Remove(Battleship battleship)
        {
            battleship.Shield.AddCapacity(-_shieldCapacityModificationFactor);
        }
    }
}