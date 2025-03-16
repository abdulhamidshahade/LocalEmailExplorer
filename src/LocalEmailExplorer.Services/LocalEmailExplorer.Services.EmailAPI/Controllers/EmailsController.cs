using AutoMapper;
using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;
using LocalEmailExplorer.Services.EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalEmailExplorer.Services.EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmailsController> _logger;

        public EmailsController(IEmailService emailService, ILogger<EmailsController> logger,
            IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetEmails()
        {
            try
            {
                var emails = _emailService.GetEmailsAsync();

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
            
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<ResponseDto>> GetEmilById(string id)
        {
            
            try
            {
                var email = await _emailService.GetEmailByIdAsync(id);

                var emailDto = _mapper.Map<EmailDto>(email);

                return new ResponseDto
                {
                    Data = emailDto,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }


        [HttpGet]
        [Route("recoveryEmail/{recoveryEmail}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByRecoveryEmail(string recoveryEmail)
        {
            try
            {
                var emails = await _emailService.GetEmailsByRecoveryEmailAsync(recoveryEmail);

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("phoneNumber/{phoneNumber}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByPhoneNumber(string phoneNumber)
        {
            try
            {
                var emails = await _emailService.GetEmailsByPhoneAsync(phoneNumber);

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateEmail(CreateEmailDto emailDto)
        {
            try
            {
                var createdEmail = await _emailService.CreateEmailAsync(emailDto);

                return new ResponseDto
                {
                    Data = createdEmail,
                    IsSuccess = true,
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<ResponseDto>> UpdateEmail(string id, UpdateEmailDto emailDto)
        {
            try
            {
                var updatedEmail = await _emailService.UpdateEmailAsync(emailDto, id);

                return new ResponseDto
                {
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<ResponseDto>> DeleteEmail(string id, DeleteEmailDto emailDto)
        {
            try
            {
                var deletedEmail = await _emailService.DeleteEmailAsync(emailDto);

                return new ResponseDto
                {
                    IsSuccess = true,
                    StatusCode = 200,
                };
            }
            catch(Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = ex.Message
                };
            }
        }


    }
}
