using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Igor.Library.Models
{
    public class IgorRole : IdentityRole
    {
        [MaxLength(10)]
        public string ShortName { get; set; }

        public IgorRole() : base() { }
        public IgorRole(string name) : base(name) { }
        public IgorRole(string name, string shortName) : base(name)
        {
            ShortName = shortName;
        }
    }
}
