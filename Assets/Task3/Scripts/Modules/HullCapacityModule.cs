namespace Task3.Scripts.Modules
{
    public class HullCapacityModule: IModule
    {
        private readonly float _hullCapacityModificationFactor;

        public HullCapacityModule(float modificationFactor)
        {
            _hullCapacityModificationFactor = modificationFactor;
        }

        public void Apply(Battleship battleship)
        {
            battleship.Hull.AddCapacity(_hullCapacityModificationFactor);
        }

        public void Remove(Battleship battleship)
        {
            battleship.Hull.AddCapacity(-_hullCapacityModificationFactor);
        }
    }
}