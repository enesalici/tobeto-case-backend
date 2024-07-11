
namespace Core.Utilities.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
