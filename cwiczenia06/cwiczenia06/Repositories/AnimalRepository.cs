using cwiczenia06.Models;
using Microsoft.Data.SqlClient;

namespace cwiczenia06.Repositories;

public class AnimalRepository : IAnimalRepository
{
    public IEnumerable<Animal> GetAnimals()
    {
        //Otworzenie polaczenia
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Definicja query
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal";
        
        //Wykonanie commandow
        var reader = command.ExecuteReader();
        var animals = new List<Animal>();

        int idAnimalOriginal = reader.GetOrdinal("IdAnimal");
        int nameOriginal = reader.GetOrdinal("Name");
        
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOriginal),
                Name = reader.GetString(nameOriginal)
            });
        }


        return animals;
    }
}