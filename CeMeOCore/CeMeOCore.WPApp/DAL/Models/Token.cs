using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeOCore.WPApp.DAL.Models
{
    class Token
    {
        /*
         
        {
            "access_token": "kAJlF3EpyOr6DTi_krbt79bjVIUfbjQLns-1wczDuQHJHS_mzYKoHXe6QQuYjtUUrCtNo_9vuujRDYNFuW6Ycxm7KW822JFpM0qB48EE1qk2nHNzje9lgxnikunRWlOa4EUYkKmvftzZclA-ZWJTcNfMdsVS3NKYrrkGabinxm-67-fGTb_nDPNzbO68SaLS6SJigQxaiNOjkXu24wTXDUh6hErQ80Cod95HbVZnNu8SiwMg83jri6EDb2reHmwgaVVmIety9DP4uS1_EsVvKgz5qdROqYDbp8H48GsC9h-HB46TacMwTPZ28c3BQ6piTwjVhr4SLRAUfa4vRI4aEM1k3qVKDIN9ZAeSMbD3YnUfQ-xCJJQmpWlbyy9AuHgHlU6ilH6MQsB7Rv-FCac0xxREZ9elqtzWsC3ke9dXAI4",
            "token_type": "bearer",
            "expires_in": 1209599,
            "userName": "User",
            ".issued": "Thu, 23 Jan 2014 18:37:36 GMT",
            ".expires": "Thu, 06 Feb 2014 18:37:36 GMT"
        }
          
         */

        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
        public string userName { get; set; }
        public DateTime issued { get; set; }
        public DateTime expires { get; set; }
    }
}
