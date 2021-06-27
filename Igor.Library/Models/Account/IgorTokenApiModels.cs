//using System.Net.Http;
//using Newtonsoft.Json;

//namespace Igor.Library.Models.Account
//{
//    /// <summary>
//    /// Response of token get.
//    /// </summary>
//    public class IgorTokenApiResponse : RestApiResponse
//    {
//        /// <summary>
//        /// Login token.
//        /// </summary>
//        [JsonProperty(PropertyName = "token")]
//        public string Token { get; set; }

//        /// <summary>
//        /// Response status code
//        /// </summary>
//        [JsonProperty(PropertyName = "status")]
//        public string Status { get; set; }
//        /// <summary>
//        /// Error message in case of not logged in.
//        /// </summary>
//        [JsonProperty(PropertyName = "message")]
//        public new string ErrorMessage { get; set; }
//    }
//    /// <summary>
//    /// Request to get the token.
//    /// </summary>
//    public class IgorTokenApiRequest : RestApiRequest
//    {
//        #region Properties

//        /// <summary>
//        /// Nokia user name.
//        /// </summary>
//        [RequestParameterType(Type = RequestParameterTypes.Data)]
//        [JsonProperty(PropertyName = "userName")]
//        public string UserName { get; set; }
//        /// <summary>
//        /// Nokia user password.
//        /// </summary>
//        [RequestParameterType(Type = RequestParameterTypes.Data)]
//        [JsonProperty(PropertyName = "userPassword")]
//        public string Password { get; set; }
//        /// <summary>
//        /// Name of login service.
//        /// </summary>
//        [RequestParameterType(Type = RequestParameterTypes.Data)]
//        [JsonProperty(PropertyName = "serviceName")]
//        public string Service { get; set; } = "SkyLab@test";

//        /// <summary>
//        /// Secret key for service.
//        /// </summary>
//        [RequestParameterType(Type = RequestParameterTypes.Data)]
//        [JsonProperty(PropertyName = "serviceSecret")]
//        public string ServiceSecretKey { get; set; } = "RS140haCnUNB4SxuD2S6b8EdpXxc9t8B659Z496x";

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Default instance.
//        /// </summary>
//        public IgorTokenApiRequest()
//        {
//            Server = "https://cord.emea.nsn-net.net";
//            Url = "api/v1/initialToken";
//            Method = HttpMethod.Post;
//        }

//        #endregion
//    }

//    /// <summary>
//    /// Token from CORD for getting user data.
//    /// </summary>
//    public class IgorToken
//    {
//        /// <summary>
//        /// Login token.
//        /// </summary>
//        public string Token { get; set; }
//        /// <summary>
//        /// Response status code
//        /// </summary>
//        public int StatusCode { get; set; }
//        /// <summary>
//        /// Error message in case of not logged in.
//        /// </summary>
//        public string ErrorMessage { get; set; }
//        /// <summary>
//        /// Check if status code represents OK.
//        /// </summary>
//        public bool IsSuccessfull => StatusCode == 200;
//    }
//}
