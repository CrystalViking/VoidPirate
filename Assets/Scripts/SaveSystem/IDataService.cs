

public interface IDataService 
{
    bool SaveData<T>(string RelativePath, T data, bool encrypted);

    T LoadData<T>(string RelativePath, bool encrypted);
}
