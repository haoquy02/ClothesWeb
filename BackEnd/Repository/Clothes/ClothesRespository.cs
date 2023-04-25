using AutoMapper;
using ClothesWeb.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

namespace ClothesWeb.Repository.Clothes
{
    public class ClothesRespository : IClothesRespository
    {
        private readonly ClothesDBContext _context;
        private readonly IMapper _mapper;
        public ClothesRespository(ClothesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> CreatePost(ClothesDB clothesCreateInfo)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Insert Into Clothes (Clothesname, Prices, Quantity, Image, Type, Size, Material, Color, Gender, Description ) " +
                    "VALUES (@Clothesname, @Prices, @Quantity, @Image, @Type, @Size, @Materia, @Color, @Gender, @Description)";
                command.Parameters.AddWithValue("@Clothesname", clothesCreateInfo.ClothesName);
                command.Parameters.AddWithValue("@Prices", clothesCreateInfo.Prices);
                command.Parameters.AddWithValue("@Quantity", clothesCreateInfo.Quantity);
                command.Parameters.AddWithValue("@Image", clothesCreateInfo.Image);
                command.Parameters.AddWithValue("@Type", clothesCreateInfo.Type);
                command.Parameters.AddWithValue("@Size", clothesCreateInfo.Size);
                command.Parameters.AddWithValue("@Materia", clothesCreateInfo.Material);
                command.Parameters.AddWithValue("@Color", clothesCreateInfo.Color);
                command.Parameters.AddWithValue("@Gender", clothesCreateInfo.Gender);
                command.Parameters.AddWithValue("@Description", clothesCreateInfo.Description);
                await command.ExecuteScalarAsync();
            }
            connection.Close();
            return "Create Post Successful";
        }

        public Task<string> DeletePost(ClothesDB clothesDB)
        {
            throw new NotImplementedException();
        }

        public async Task<ClothesDB> GetPost(ClothesDB clothesInfo)
        {
            var connection = _context.GetDbConnection();
            ClothesDB clothes = new ClothesDB();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From Account Where Username = '" + clothesInfo.ClothesName + "'";
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    clothes = _mapper.Map<ClothesDB>(reader);
                }
            }
            connection.Close();
            return clothes;
        }

        public async Task<string> UpdatePost(ClothesDB clothesUpdateInfo)
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = (SqlConnection)connection;
                command.CommandText = "Update Clothes (Clothesname, Prices, Quantity, Image, Type, Size, Material, Color, Gender, Description ) " +
                    "VALUES (@Clothesname, @Prices, @Quantity, @Image, @Type, @Size, @Materia, @Color, @Gender, @Description) WHERE id = @id";
                command.Parameters.AddWithValue("@id", clothesUpdateInfo.id);
                command.Parameters.AddWithValue("@Clothesname", clothesUpdateInfo.ClothesName);
                command.Parameters.AddWithValue("@Prices", clothesUpdateInfo.Prices);
                command.Parameters.AddWithValue("@Quantity", clothesUpdateInfo.Quantity);
                command.Parameters.AddWithValue("@Image", clothesUpdateInfo.Image);
                command.Parameters.AddWithValue("@Type", clothesUpdateInfo.Type);
                command.Parameters.AddWithValue("@Size", clothesUpdateInfo.Size);
                command.Parameters.AddWithValue("@Materia", clothesUpdateInfo.Material);
                command.Parameters.AddWithValue("@Color", clothesUpdateInfo.Color);
                command.Parameters.AddWithValue("@Gender", clothesUpdateInfo.Gender);
                command.Parameters.AddWithValue("@Description", clothesUpdateInfo.Description);
                await command.ExecuteScalarAsync();
            }
            connection.Close();
            return "Update Post Successful";
        }
    }
}
