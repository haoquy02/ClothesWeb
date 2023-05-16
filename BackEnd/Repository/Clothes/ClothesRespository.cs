using AutoMapper;
using ClothesWeb.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using System.Data;

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
        public async Task<string> CreateClothes(ClothesDB clothesCreateInfo)
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

        public Task<string> DeleteClothes(ClothesDB clothesDB)
        {
            throw new NotImplementedException();
        }

        public async Task<ClothesDB> GetClothes(int clothesID)
        {
            var connection = _context.GetDbConnection();
            ClothesDB clothes = new();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From Clothes Where id = " + clothesID + "";
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
        public async Task<ClothesDB> GetClothesCartById(int clothesID)
        {
            ClothesDB clothesCart = new(); ;
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From Clothes Where id = " + clothesID + "";
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    clothesCart = _mapper.Map<ClothesDB>(reader);
                }
            }
            connection.Close();
            return clothesCart;
        }

        public async Task<string> UpdateClothes(ClothesDB clothesUpdateInfo)
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
        public async Task<List<ClothesDB>> GetAllClothes()
        {
            var connection = _context.GetDbConnection();
            connection.Open();
            List<ClothesDB> listClothes = new();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select * From Clothes";
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    listClothes.Add(_mapper.Map<IDataReader, ClothesDB>(reader));
                }
            }
            connection.Close();
            return listClothes;
        }

        public async Task<bool> UpdateQuantity(int quantity, int id)
        {
            var maxQuantity = 0;
            var connection = _context.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "Select Quantity From Clothes Where id = " + id + "";
                var reader = command.ExecuteReader();
                await reader.ReadAsync();
                if (reader.HasRows)
                {
                    maxQuantity = (int)reader["Quantity"];
                }
            }
            connection.Close();
            connection.Open();
            if (maxQuantity > 0 && maxQuantity >= quantity)
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = (SqlConnection)connection;
                    command.CommandText =
                     "Update Clothes SET Quantity= @Quant WHERE id = @id";
                    command.Parameters.AddWithValue("@Quant", maxQuantity - quantity);
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteScalarAsync();
                }
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
