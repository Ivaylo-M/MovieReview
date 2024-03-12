namespace Application.Services.Contracts
{
    using Domain;

    public interface ITokenService
    {
        string CreateToken(User user);

        string GenerateKey();
    }
}
