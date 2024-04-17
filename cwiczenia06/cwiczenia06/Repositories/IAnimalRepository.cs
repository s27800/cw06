using cwiczenia06.Models;
using cwiczenia06.Models.DTOs;

namespace cwiczenia06.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string par);
    void AddAnimal(Animal animal);
    void UpdateAnimal(int idAnimal, AnimalToUpdate animal);
    void DeleteAnimal(int idAnimal);
}