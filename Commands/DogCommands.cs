using Microsoft.EntityFrameworkCore;
using MinimalAPI.Model;

namespace MinimalAPI.Commands
{
    public class DogCommands:IDogCommands
    {
        private readonly DataContext _dataContext;

        public DogCommands(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddDog(Dog dog)
        {
            try
            {
                await _dataContext.Dogs.AddAsync(dog);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Dog>> GetAllDogs()
        {
            return await _dataContext.Dogs.AsNoTracking().ToListAsync();
        }

        public async Task<Dog> GetDogById(Guid id)
        {
            return await _dataContext.Dogs.FindAsync(id);
        }

        public async Task<bool> UpdateDog(Dog dog, Guid id)
        {
            var dogInput = await _dataContext.Dogs.FindAsync(id);

            if(dogInput == null)
            {
                return false;
            }

            dogInput.Name = dog.Name;
            dogInput.Color = dog.Color;
            dogInput.Breed = dog.Breed;

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteDog(Guid id)
        {
            var dogInput = await _dataContext.Dogs.FindAsync(id);

            if (dogInput == null)
            {
                return false;
            }

            _dataContext.Dogs.Remove(dogInput);
            return true;
        }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
