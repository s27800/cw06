using cwiczenia06.Models;

namespace cwiczenia06.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
}