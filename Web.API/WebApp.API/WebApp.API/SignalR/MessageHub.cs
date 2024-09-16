//using AutoMapper;
//using Microsoft.AspNetCore.SignalR;
//using WebApp.API.Models.DTOs;
//using WebApp.API.Models;
//using WebApp.API.Repositories.IRepository;
//using WebApp.API.Extensions;

//namespace WebApp.API.SignalR
//{
//    public class MessageHub : Hub
//    {

//        private readonly IUnitOfWork unitOfWork;


//        private readonly IMapper mapper;
//        private readonly IHubContext<PresenceHub> presenceHub;
//        private readonly PresenceTracker tracker;

//        private readonly IUserRepository _userRepository;

//        public MessageHub(IUnitOfWork unitOfWork, IMapper mapper,
//             IHubContext<PresenceHub> presenceHub,
//            PresenceTracker tracker,
//            IUserRepository userRepository)
//        {


//            this.unitOfWork = unitOfWork;
//            this.mapper = mapper;
//            this.presenceHub = presenceHub;
//            this.tracker = tracker;
//            _userRepository = userRepository;
//        }
//        public override async Task OnConnectedAsync()
//        {
//            try
//            {
//                var httpContext = Context.GetHttpContext();
//                var otherUser = httpContext.Request.Query["user"].ToString();
//                var groupName = GetGroupName(Context.User.GetUsername(), otherUser);

//                // Add to group
//                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
//                var group = await AddToGroup(groupName);
//                await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

//                // Get message thread
//                var messages = await unitOfWork.MessageRepository
//                    .GetMessageThread(Context.User.GetUsername(), otherUser);

//                // Complete any changes in the unit of work
//                if (unitOfWork.hasChanges())
//                {
//                    await unitOfWork.Complete();
//                }

//                // Send message thread to caller
//                await Clients.Caller.SendAsync("ReceiveMessageThread", messages);

//                Console.WriteLine($"Sent message thread to group {groupName}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
//                throw; // Optionally, rethrow if you want the connection to close
//            }
//        }


//        public override async Task OnDisconnectedAsync(Exception exception)
//        {
//            var group = await RemoveFromMessageGroup();
//            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
//            await base.OnDisconnectedAsync(exception);
//        }


//        public async Task SendMessage(CreateMessageDto createMessageDto)
//        {
//            var username = Context.User.GetUsername();


//            if (username == createMessageDto.RecipientUsername.ToLower())
//                throw new HubException("Cannot send message to self");

//            var sender = await _userRepository.GetUserByUsernameAsync(username);
//            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

//            if (recipient == null) throw new HubException("Not found user");

//            var message = new Message
//            {
//                SenderId = sender.Id,
//                RecipientId = recipient.Id,
//                SenderUsername = sender.Username, // Assuming you want to store these usernames
//                RecipientUsername = recipient.Username, // Assuming you want to store these usernames
//                Content = createMessageDto.Content,
//                MessageSent = DateTime.UtcNow, // Set the current time as the message sent time
//                SenderDeleted = false, // Default value
//                RecipientDeleted = false // Default value
//            };

//            var groupName = GetGroupName(sender.Username, recipient.Username);

//            var group = await unitOfWork.MessageRepository.GetMessageGroup(groupName);
//            if (group.Connections.Any(x => x.Username == recipient.Username))
//            {
//                Console.WriteLine(recipient.Username);
//                message.DateRead = DateTime.Now;
//            }
//            else
//            {

//                //For notifications
//                var connections = await tracker.GetConnectionsForUser(recipient.Username);
//                if (connections != null)
//                {
//                    //checking if they are connected
//                    await presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
//                        new { username = sender.Username });
//                }
//            }
//            unitOfWork.MessageRepository.AddMessage(message);

//            if (await unitOfWork.Complete())
//            {
//                Console.WriteLine($"Sending message to group {groupName}");
//                await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
//            }
//        }

//        private async Task<Group> AddToGroup(string groupName)
//        {
//            var group = await unitOfWork.MessageRepository.GetMessageGroup(groupName);
//            var connection = new Connection(Context.ConnectionId, Context.User.GetUsername());

//            if (group == null)
//            {
//                group = new Group(groupName);
//                unitOfWork.MessageRepository.AddGroup(group);

//            }

//            group.Connections.Add(connection);
//            if (await unitOfWork.Complete()) return group;

//            throw new HubException("Failed to join group");
//        }




//        private async Task<Group> RemoveFromMessageGroup()
//        {
//            var group = await unitOfWork.MessageRepository.GetGroupForConnection(Context.ConnectionId);
//            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
//            unitOfWork.MessageRepository.RemoveConnection(connection);
//            if (await unitOfWork.Complete()) return group;

//            throw new HubException("Failed to remove from group");
//        }
//        private string GetGroupName(string caller, string other)
//        {
//            var stringCompare = string.CompareOrdinal(caller, other) < 0;
//            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
//        }
//    }
//}
