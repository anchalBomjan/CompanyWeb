using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using WebApp.API.Extensions;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;

using WebApp.API.Helper;
using WebApp.API.Repositories;

namespace WebApp.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        
        private readonly IMapper mapper;
        private readonly IUserRepository _userRepository;
      

        public MessageController(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
           
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userRepository = userRepository;
          
        }



        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.GetUsername().ToLower();

            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("Cannot send message to self");

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername.ToLower());

            if (sender == null) return NotFound("Sender not found");
            if (recipient == null) return NotFound("Recipient not found");

            var message = new Message
            {
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                SenderUsername = sender.Username, // Assuming you want to store these usernames
                RecipientUsername = recipient.Username, // Assuming you want to store these usernames
                Content = createMessageDto.Content,
                MessageSent = DateTime.UtcNow, // Set the current time as the message sent time
                SenderDeleted = false, // Default value
                RecipientDeleted = false // Default value
            };

            unitOfWork.MessageRepository.AddMessage(message);
            var result = await unitOfWork.Complete();

            if (result)
            {
                var messageDto = mapper.Map<MessageDto>(message);
                return Ok(messageDto);
            }
            return BadRequest("Failed to send message");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser([FromQuery]
        MessagesParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var messages = await unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            return Ok(messages);
        }
        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername(); // Ensure the user is authenticated
            var messages = await unitOfWork.MessageRepository.GetMessageThread(currentUsername, username);

            return Ok(messages);  // Return the messages between the current user and the specified user
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var username = User.GetUsername();

            var message = await unitOfWork.MessageRepository.GetMessage(id);

            if (message.Sender.Username != username && message.Recipient.Username != username)
                return Unauthorized();

            if (message.Sender.Username == username) message.SenderDeleted = true;

            if (message.Recipient.Username == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                unitOfWork.MessageRepository.DeleteMessage(message);

            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting the message");
        }
     


    }
}



