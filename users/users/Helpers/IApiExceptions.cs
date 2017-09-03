using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace users
{
    public interface IApiExceptions
    {
        string ErrorDescription { get; set; }        
        HttpStatusCode HttpStatus { get; set; }        
        string ReasonPhrase { get; set; }
    }
}
