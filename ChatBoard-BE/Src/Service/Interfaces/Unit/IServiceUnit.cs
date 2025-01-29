namespace Service.Interfaces.Unit
{
    public interface IServiceUnit
    {
        IEmailServices Email { get; }
        IAccountService Account { get; }
    }
}
