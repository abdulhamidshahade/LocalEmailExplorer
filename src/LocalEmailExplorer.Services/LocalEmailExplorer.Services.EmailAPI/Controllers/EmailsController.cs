using AutoMapper;
using LocalEmailExplorer.Services.EmailAPI.Halpers.Exceptions;
using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;
using LocalEmailExplorer.Services.EmailAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
                var emails = await _emailService.GetEmailsAsync();

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return Ok(new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting all emails.");

                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred"
                });
            }
            
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<ResponseDto>> GetEmailById(string id)
        {
            
            try
            {
                var email = await _emailService.GetEmailByIdAsync(id);

                if(email == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        StatusMessage = $"Email with Id '{id}' not found"
                    });
                }

                var emailDto = _mapper.Map<EmailDto>(email);

                return Ok(new ResponseDto
                {
                    Data = emailDto,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error getting email by Id '{id}'", id);

                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }
        }


        [HttpGet]
        [Route("recovery-email/{recoveryEmail}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByRecoveryEmail(string recoveryEmail)
        {
            try
            {
                var emails = await _emailService.GetEmailsByRecoveryEmailAsync(recoveryEmail);

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return Ok(new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error getting emails by recovery email: '{recoveryEmail}", recoveryEmail);
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred"
                });
            }
        }

        [HttpGet]
        [Route("phone-number/{phoneNumber}")]
        public async Task<ActionResult<ResponseDto>> GetEmailsByPhoneNumber(string phoneNumber)
        {
            try
            {
                var emails = await _emailService.GetEmailsByPhoneAsync(phoneNumber);

                var emailsDto = _mapper.Map<List<EmailDto>>(emails);

                return Ok(new ResponseDto
                {
                    Data = emailsDto,
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error getting emails by phone number: '{phoneNumber}'", phoneNumber);
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An expeteced error occurred"
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateEmail(CreateEmailDto emailDto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdEmail = await _emailService.CreateEmailAsync(emailDto);

                var createdEmailDto = _mapper.Map<EmailDto>(createdEmail);

                return CreatedAtAction(nameof(GetEmailById), new { id = createdEmail.Id }, new ResponseDto
                {
                    Data = createdEmailDto,
                    IsSuccess = true,
                    StatusCode = 201
                });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating email.");

                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<ResponseDto>> UpdateEmail(string id, UpdateEmailDto emailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedEmail = await _emailService.UpdateEmailAsync(emailDto, id);

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
            catch(EmailNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Email with ID '{id}' not found for update.", id);
                return NotFound(new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    StatusMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating email with ID: {id}", id);
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }
        }

        [HttpDelete]
        [Route("{emailAddress}")]
        public async Task<ActionResult<ResponseDto>> DeleteEmail(string emailAddress)
        {
            try
            {
                var email = await _emailService.GetEmailByEmailAddressAsync(emailAddress, track:false);

                if(email == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        StatusMessage = $"Email with address '{emailAddress}' not found."
                    });
                }

                var emailDto = _mapper.Map<DeleteEmailDto>(email);

                var deletedEmail = await _emailService.DeleteEmailAsync(emailDto);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting email with address: {emailAddress}", emailAddress);
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }
        }

        [HttpGet]
        [Route("email-address/{emailAddress}")]
        public async Task<ActionResult<ResponseDto>> GetEmailByEmailAddressAsync(string emailAddress)
        {
            try
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
                    Data = _mapper.Map<EmailDto>(email),
                    IsSuccess = true,
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting email by email address: {emailAddress}", emailAddress);
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    StatusMessage = "An unexpected error occurred."
                });
            }
        }
    }
}
