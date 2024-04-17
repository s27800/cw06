using cwiczenia06.Models;
using cwiczenia06.Repositories;
using cwiczenia06.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace cwiczenia06.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly AnimalRepository _animalRepository;

    public AnimalsController(AnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public IActionResult GetAnimals()
    {
        var animals = _animalRepository.GetAnimals("name");

        return Ok(animals);
    }

    [HttpGet("{orderBy}")]
    public IActionResult GetAnimals(string orderBy)
    {
        var animals = _animalRepository.GetAnimals(orderBy);

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animalRepository.AddAnimal(animal);

        return Created("api/animals", null);
    }

    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, AnimalToUpdate animal)
    {
        _animalRepository.UpdateAnimal(idAnimal, animal);

        return Ok();
    }

    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        _animalRepository.DeleteAnimal(idAnimal);

        return Ok();
    }
}