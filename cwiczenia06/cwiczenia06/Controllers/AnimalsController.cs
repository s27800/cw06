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
        var animals = _animalRepository.GetAnimals();
        
        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        //Otworzenie polaczenia
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Definicja query
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (animalName, '', ', ''):";
        command.Parameters.AddWithValue("animalValue", animal.Name);

        command.ExecuteNonQuery();

        return Created("api/animals", null);
    }
}