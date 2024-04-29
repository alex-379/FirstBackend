namespace FirstBackend.Core.Dtos
{
    public class SaltDto
    {
        public Guid UserId { get; set; }

        public string Salt { get; set; }
    }
}
