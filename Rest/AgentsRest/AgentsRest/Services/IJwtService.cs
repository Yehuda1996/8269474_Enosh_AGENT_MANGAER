namespace AgentsRest.Services
{
    public interface IJwtService
    {
        string CreateToken(string name);
    }
}
