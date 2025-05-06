public interface ISaveable
{
    public void Load(SaveSlot saveSlot);
    public SaveSlot Save(SaveSlot saveSlot);
}