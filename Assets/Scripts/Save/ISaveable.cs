public interface ISaveable
{
    public void Register();
    public void Load(SaveSlot saveSlot);
    public SaveSlot Save(SaveSlot saveSlot);
}