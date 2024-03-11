using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserAuth.BusinessLogic.Dto
{
    public class UserDto
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string FullName {  get; set; }
        public string EMail { get; set; }  
        
    }
}
