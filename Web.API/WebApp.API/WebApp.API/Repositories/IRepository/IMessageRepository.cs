

using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Helper;

namespace WebApp.API.Repositories.IRepository
{
    public interface IMessageRepository
    {
        void AddGroup(Group group);
        void RemoveConnection(Models.Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<Group> GetGroupForConnection(string connectionId);
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessagesParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);

    }
}
