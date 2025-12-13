using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ServiceAbstraction.Common
{
    public partial class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        private Error(string code , string description , ErrorType type)
        {
            Code = code;
            Description = description;  
            Type = type;
        
        }

        public static Error Failure(string code = "General.Failure " 
            , string description = "A Failure Has Occurred")
            =>new (code, description , ErrorType.Failure );

        public static Error Vailedation(string code = "General.Vailedation "
           , string description = "A Vailedation Error Has Occurred")
           => new(code, description, ErrorType.Vailedation);

        public static Error NotFound(string code = "General.NotFound "
            , string description = "A 'NotFound' Error Has Occurred")
            => new(code, description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict "
            , string description = "A Conflict Error Has Occurred")
            => new(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code = "General.Unauthorized "
         , string description = "A Unauthorized Error Has Occurred")
         => new(code, description, ErrorType.Unauthorized);
    }
}
