using MinimalAPI.Model;

namespace MinimalAPI.Commands
{
    public interface IDogCommands
    {
        Task<bool> AddDog(Dog dog);

        Task<List<Dog>> GetAllDogs();

        Task<Dog> GetDogById(Guid id);

        Task<bool> UpdateDog(Dog dog, Guid id);

        Task<bool> DeleteDog(Guid id);

        Task Save();
    }
}
