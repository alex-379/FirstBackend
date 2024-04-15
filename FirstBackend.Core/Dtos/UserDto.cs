namespace FirstBackend.Core.Dtos
{
    public class UserDto:IdContainer
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
