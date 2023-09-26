using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EndpointConstants
    {
        public const string brokerAddress = "http://127.0.0.1:5001";

        public const string subscriberAddress = "http://127.0.0.1:0"; // .0 sistemul automat gaseste un port liber
    }
}
