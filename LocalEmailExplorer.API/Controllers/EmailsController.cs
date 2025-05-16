using AutoMapper;
using LocalEmailExplorer.API.DTOs;
using LocalEmailExplorer.Application.DTOs.EmailDtos;
using LocalEmailExplorer.Application.Services.Interfaces.EmailServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalEmailExplorer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        private readonly ILogger<EmailsController> _logger;

        public EmailsController(IEmailService emailService, 
                                ILogger<EmailsController> logger
                                )
        {
            _emailService = emailService;

            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetEmails([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            //var emails = await _emailService.GetEmailsAsync();

            var paginatedEmails = await _emailService.GetEmailsPagedAsync(pageNumber, pageSize);

            //var emailsDto = _mapper.Map<List<EmailDto>>(emails);

            if (paginatedEmails == null)
            {
                _logger.LogError("Error getting all emails.");

                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred"
                });
            }

            return Ok(new ResponseDto
            {
                Data = new
                {
                    Items = paginatedEmails.Items,
                    TotalCount = paginatedEmails.TotalCount,
                    PageNumber = paginatedEmails.PageNumber,
                    PageSize = paginatedEmails.PageSize,
                    TotalPages = paginatedEmails.TotalPages
                },
                IsSuccess = true,
                StatusCode = 200
            });
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ResponseDto>> GetEmailById(int id)
        {


            var email = await _emailService.GetEmailByIdAsync(id);

            if (email == null)
            {
                _logger.LogError($"Error getting email by Id '{id}'", id);

                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }



            return Ok(new ResponseDto
            {
                Data = email,
                IsSuccess = true,
                StatusCode = 200
            });
        }


        [HttpGet]
        [Route("recovery-email/{recoveryEmail}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByRecoveryEmail(string recoveryEmail)
        {

            var emails = await _emailService.GetEmailsByRecoveryEmailAsync(recoveryEmail);

            if (emails != null)
            {
                return Ok(new ResponseDto
                {
                    Data = emails,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }

            _logger.LogError($"Error getting emails by recovery email: '{recoveryEmail}", recoveryEmail);
            return StatusCode(500, new ResponseDto
            {
                IsSuccess = false,
                StatusCode = 500,
                StatusMessage = "An unexpected error occurred"
            });

        }

        [HttpGet]
        [Route("phone-number/{phoneNumber}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByPhoneNumber(string phoneNumber)
        {

            var emails = await _emailService.GetEmailsByPhoneAsync(phoneNumber);

            if (emails != null)
            {
                return Ok(new ResponseDto
                {
                    Data = emails,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }

            _logger.LogError($"Error getting emails by phone number: '{phoneNumber}'", phoneNumber);
            return StatusCode(500, new ResponseDto
            {
                IsSuccess = false,
                StatusCode = 500,
                StatusMessage = "An expeteced error occurred"
            });

        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateEmail(CreateEmailDto emailDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmail = await _emailService.CreateEmailAsync(emailDto);

            if (createdEmail != null)
            {
                return CreatedAtAction(nameof(GetEmailById), new { id = createdEmail.Id }, new ResponseDto
                {
                    Data = createdEmail,
                    IsSuccess = true,
                    StatusCode = 201
                });
            }

            _logger.LogError($"Error creating email address");
            return StatusCode(500, new ResponseDto
            {
                IsSuccess = false,
                StatusCode = 500,
                StatusMessage = "Error creating email address"
            });

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateEmail(int id, UpdateEmailDto emailDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedEmail = await _emailService.UpdateEmailAsync(id, emailDto);

            if (updatedEmail)
            {
                return Ok(new ResponseDto
                {
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            else
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    StatusMessage = "En error occurred while updating the email content"
                });
            }
        }


        [HttpDelete]
        [Route("email-address/{emailAddress}")]
        public async Task<ActionResult<ResponseDto>> DeleteEmail(string emailAddress)
        {
            
                var email = await _emailService.GetEmailByEmailAddressAsync(emailAddress, track:false);

                var deletedEmail = await _emailService.DeleteEmailAsync(emailAddress);

                if (deletedEmail)
                {
                    return Ok(new ResponseDto
                    {
                        IsSuccess = true,
                        StatusCode = 200,
                    });
                }
                else
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        StatusMessage = "En error exists while deleting the email"
                    });
                }
        }

        [HttpGet]
        [Route("email-address/{emailAddress}")]
        public async Task<ActionResult<ResponseDto>> GetEmailByEmailAddressAsync(string emailAddress)
        {

            var email = await _emailService.GetEmailByEmailAddressAsync(emailAddress, false);

            if (email == null)
            {
                return NotFound(new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    StatusMessage = $"Email with address '{emailAddress}' not found."
                });
            }
            return Ok(new ResponseDto
            {
                Data = email,
                IsSuccess = true,
                StatusCode = 200
            });
        }
    }

}
