using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CoreWebAPI.Models.Response
{
    [DataContract]
    public class AuthResponse
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public Courier Courier { get; set; }
    }
}
