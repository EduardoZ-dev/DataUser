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

        static async Task<Created<UserDataDTO>> CreateUser(
            UserDataDTO createUserDTO,
            IUserRepository userRepository,
            IMapper mapper)
            
        {
            var user = mapper.Map<UserData>(createUserDTO);
            var id = await userRepository.AddUserAsync(user);
            var userDTO = mapper.Map<UserDataDTO>(user);

            // Simula el "envío" de un mensaje, imprimiéndolo en los logs
            

            return TypedResults.Created($"/users/{id}", userDTO);
        }
    }
}
