using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Exceptions
{
   public class UnAuthorizedException :Exception
    {
        public UnAuthorizedException() :base()
        {  
        }

        public UnAuthorizedException(string ?message) : base(message) 
        {
            
        }

        public UnAuthorizedException(string ?message ,Exception ex)
        {
        
        }
    }
}
