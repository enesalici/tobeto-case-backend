using Application.Repos;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AccessToken>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public LoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(i => i.Username == request.Username);

            if (user is null)
                throw new Exception("login failed.");

            bool isPasswordMatch = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordMatch)
                throw new Exception("login failed.");

            return _tokenHelper.CreateToken(user);
        }
    }
}
