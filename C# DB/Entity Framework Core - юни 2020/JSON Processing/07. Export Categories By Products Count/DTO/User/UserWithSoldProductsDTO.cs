using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DTO.User
{
   public class UserWithSoldProductsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lasttName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public UserSoldProductDTO[] SoldProducts { get; set; }
    }
}
