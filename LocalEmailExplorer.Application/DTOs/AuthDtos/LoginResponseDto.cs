namespace LocalEmailExplorer.Application.Dtos.AuthDtos
{
    public class LoginResponseDto
    {
        public ApplicationUserDto User { get; set; }
        public string Token { get; set; }
    }
}
