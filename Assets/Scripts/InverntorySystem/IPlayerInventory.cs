public interface IPlayerInventory
{
    void AddItem(Weapon newItem);
    int GetInventorySize();
    Weapon GetItem(int index);
    void RemoveItem(int index);
}