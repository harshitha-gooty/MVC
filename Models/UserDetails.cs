using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean IsUserValid { get; set; }
    }
}
