namespace BusinesLogicLayer.Abstract
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
