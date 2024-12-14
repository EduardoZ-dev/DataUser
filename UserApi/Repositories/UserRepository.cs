using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UserApi.Entities;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddUserAsync(UserData user)
        {
            // Validación del nombre
            if (!Regex.IsMatch(user.Name, @"^[a-zA-Z\s]+$"))
            {
                _logger.LogWarning($"El nombre '{user.Name}' contiene caracteres inválidos.");
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");
            }

            // Validación del número de teléfono
            if (!Regex.IsMatch(user.PhoneNumber, @"^[\d+ -]+$"))
            {
                _logger.LogWarning($"El número de teléfono '{user.PhoneNumber}' contiene caracteres inválidos.");
                throw new ArgumentException("El número de teléfono solo puede contener dígitos, '+', espacios o guiones.");
            }

            // Validar si el teléfono ya existe
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);

            if (existingUser != null)
            {
                _logger.LogWarning($"El número de teléfono '{user.PhoneNumber}' ya está registrado.");
                throw new ArgumentException("El número de teléfono ya está registrado.");
            }

            // Agregar el usuario a la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Bienvenido, {user.Name}. Estamos complacidos de tenerlo con nosotros. Su número telefónico es {user.PhoneNumber}.");

            return user.Id;
        }

        public void ValidatePhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^\+?[0-9]+([ -][0-9]+)*$");

            if (!regex.IsMatch(phoneNumber))
            {
                throw new ArgumentException("El número de teléfono no es válido. Debe empezar con '+' y solo contener números, espacios o guiones.");
            }
        }
    }
}