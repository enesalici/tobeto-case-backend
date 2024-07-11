using Application.Repos;
using AutoMapper;
using Core.Utilities.Hashing;
using Domain.Entities;
using MediatR;
using SystemTask = System.Threading.Tasks.Task; // Alias for System.Threading.Tasks.Task

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async SystemTask Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(request);

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _userRepository.AddAsync(user);
        }
    }
}
