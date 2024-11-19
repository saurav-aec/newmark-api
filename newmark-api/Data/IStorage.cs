public interface IStorage {
    public Task<List<Property>> ReadDataAsync();
}