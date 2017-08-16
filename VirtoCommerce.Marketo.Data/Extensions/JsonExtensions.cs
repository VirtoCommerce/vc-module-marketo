using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.Marketo.Data.Extensions
{
    public static class JsonExtensions
    {
        public static StringContent AsJson(this object o)
            => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}
