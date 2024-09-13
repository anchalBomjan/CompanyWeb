namespace WebApp.API.Repositories.IRepository
{
    public interface IUnitOfWork
    {

       
        IMessageRepository MessageRepository { get; }
     

        Task<bool> Complete();

        bool hasChanges();
    }
}
