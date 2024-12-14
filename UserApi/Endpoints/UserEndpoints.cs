using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using UserApi.DTO_s;
using UserApi.Entities;
using UserApi.Repositories;

namespace UserApi.Endpoints
{
    public static class UserEndpoints
    {
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateUser);
            return group;
        }

        static async Task<IResult> CreateUser(
            UserDataDTO createUserDTO,
            IUserRepository userRepository,
            IMapper mapper)
        {
            try
            {
                var user = mapper.Map<UserData>(createUserDTO);
                var id = await userRepository.AddUserAsync(user);
                var userDTO = mapper.Map<UserDataDTO>(user);

                return TypedResults.Created($"/users/{id}", userDTO);
            }
            catch (ArgumentException ex)
            {
                // Manejo de la excepción si el número de teléfono ya existe
                return Results.BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción
                return Results.Json(new { message = "Ocurrió un error interno.", details = ex.Message }, statusCode: 500);
            }
        }
    }
}

