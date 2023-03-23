namespace Task3.Scripts.Modules
{
    public interface IModule
    {
        void Apply(Battleship battleship);
        void Remove(Battleship battleship);
    }
}