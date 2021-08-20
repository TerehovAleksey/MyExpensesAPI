using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models
{
    /*
     * можно дополнительно прикрепить к RefreshToken объектам другие метаданные ,
     * например IP-адрес клиента, пользовательский агент, идентификатор устройства и т. Д.,
     * Чтобы мы могли идентифицировать и отслеживать пользовательские сеансы и обнаруживать мошеннические токены
     */

    public class RefreshToken
    {
        [JsonPropertyName("username")] 
        public string UserName { get; set; }

        [JsonPropertyName("tokenString")] 
        public string TokenString { get; set; }

        [JsonPropertyName("expireAt")] 
        public DateTime ExpireAt { get; set; }
    }
}
