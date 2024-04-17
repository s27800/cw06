using cwiczenia06.Models;
using cwiczenia06.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace cwiczenia06.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string par)
    {
        //Otworzenie polaczenia
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Definicja query
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal ORDER BY @par";
        command.Parameters.AddWithValue("par", par);
        
        //Wykonanie commandow
        var reader = command.ExecuteReader();
        var animals = new List<Animal>();

        int idAnimalOriginal = reader.GetOrdinal("IdAnimal");
        int nameOriginal = reader.GetOrdinal("Name");
        int descOriginal = reader.GetOrdinal("Desc");
        int catOriginal = reader.GetOrdinal("Cat");
        int areaOriginal = reader.GetOrdinal("Area");
        
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOriginal),
                Name = reader.GetString(nameOriginal),
                Desc = reader.GetString(descOriginal),
                Cat = reader.GetString(catOriginal),
                Area = reader.GetString(areaOriginal)
            });
        }

        return animals;
    }
    
    public void AddAnimal(Animal animal)
    {
        // Otwieramy połaczenie do bazy danych
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        // Definiujemy query
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES(@animalName, @animalDesc, @animalCat, @animalArea);";
        command.Parameters.AddWithValue("animalName", animal.Name);
        command.Parameters.AddWithValue("animalDesc", animal.Desc);
        command.Parameters.AddWithValue("animalCat", animal.Cat);
        command.Parameters.AddWithValue("animalArea", animal.Area);

        command.ExecuteNonQuery();
    }

    public void UpdateAnimal(int idAnimal, AnimalToUpdate animal)
    {
        //Otworzenie polaczenia
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        //Definicja query
        using SqlCommand command1 = new SqlCommand();
        command1.Connection = connection;
        command1.CommandText = "UPDATE Animal SET Name = @name WHERE IdAnimal = @idAnimal";
        command1.Parameters.AddWithValue("name", animal.Name);
        command1.Parameters.AddWithValue("idAnimal", idAnimal);
        
        using SqlCommand command2 = new SqlCommand();
        command2.Connection = connection;
        command2.CommandText = "UPDATE Animal SET Description = @desc WHERE IdAnimal = @idAnimal";
        command2.Parameters.AddWithValue("desc", animal.Desc);
        command2.Parameters.AddWithValue("idAnimal", idAnimal);
        
        using SqlCommand command3 = new SqlCommand();
        command3.Connection = connection;
        command3.CommandText = "UPDATE Animal SET Cat = @cat WHERE IdAnimal = @idAnimal";
        command3.Parameters.AddWithValue("cat", animal.Cat);
        command3.Parameters.AddWithValue("idAnimal", idAnimal);
        
        using SqlCommand command4 = new SqlCommand();
        command4.Connection = connection;
        command4.CommandText = "UPDATE Animal SET Area = @area WHERE IdAnimal = @idAnimal";
        command4.Parameters.AddWithValue("area", animal.Area);
        command4.Parameters.AddWithValue("idAnimal", idAnimal);
        
        //Wykonanie commandow
        command1.ExecuteNonQuery();
        command2.ExecuteNonQuery();
        command3.ExecuteNonQuery();
        command4.ExecuteNonQuery();
    }

    public void DeleteAnimal(int idAnimal)
    {
        //Otworzenie polaczenia
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        // Definiujemy query
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        
        //Wykonanie commandow
        command.ExecuteNonQuery();
    }
}