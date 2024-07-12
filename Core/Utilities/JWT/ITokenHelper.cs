
using Core.Base.Entities;


namespace Core.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(BaseEntity<Guid> user);
    }
}
