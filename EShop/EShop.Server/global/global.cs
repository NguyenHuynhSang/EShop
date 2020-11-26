using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.global
{
    public static class global
    {
        public static String[] Origins =
        {
            "http://eshopadmin.netlify.app",
            "http://localhost:3000",
            "https://eshopadmin.netlify.app/auth/login"
        };

        public static readonly string ApiCorsPolicy = "ApiCorsPolicy";

    }
}
