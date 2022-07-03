using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adspowers.AdspowersAPI.JSON
{
    public class FingerprintConfig
    {
        public List<string> language { get; set; }
        public string ua { get; set; }
        public string flash { get; set; }
        public string scan_port_type { get; set; }
        public string screen_resolution { get; set; }
        public List<string> fonts { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string webrtc { get; set; }
        public string do_not_track { get; set; }
        public string hardware_concurrency { get; set; }
        public string device_memory { get; set; }
    }

    public class CreateConfig
    {
        public string name { get; set; }
        public string group_id { get; set; }
        public string domain_name { get; set; }
        public List<string> repeat_config { get; set; }
        public string country { get; set; }
        public FingerprintConfig fingerprint_config { get; set; }
        public UserProxyConfig user_proxy_config { get; set; }
    }
    public class UserProxyConfig
    {
        public string proxy_soft { get; set; }
    }

}
