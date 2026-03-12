namespace Domain.Dtos
{
    public class LoginDtoOut
    {
        public bool IsLogged { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public DateTime? ExpiresIn { get; set; }
    }
}