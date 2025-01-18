namespace DemoS2.Models.ViewModels
{
    public class UserClaimsViewModel
    {
        public string UserID {  get; set; }
        public List<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
