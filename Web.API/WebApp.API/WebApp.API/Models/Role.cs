namespace WebApp.API.Models
{
    public class Role
    {

        public int Id { get; set; }
        public string RoleName { get; set; }
        /// <summary>
        /// /
        /// </summary>
        /// 



        ///    while    doing   with    entityframework  with database first     we  implement this relation   due to the   hybrid   of   dapper and entityframework   for (  Message)

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();


    }
}
