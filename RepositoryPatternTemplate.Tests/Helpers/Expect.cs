using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace PikiouAPI.Tests.Helpers
{
    public class Expect : Assert
    {
        /// <summary>
        /// Checks if two objects have the exactly same attributes
        /// </summary>
        public static void DeepEqual(object expected, object actual)
        {
            Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }
        
        /// <summary>
        /// Checks if object coerced to a string and string match exactly
        /// </summary>
        public static void DeepEqual(object expected, string actual)
        {
            Equal(JsonConvert.SerializeObject(expected), actual);
        }
        
        /// <summary>
        /// Checks if object coerced to a string and string match exactly
        /// </summary>
        public static void DeepEqualLowerCaseFields(object expected, string actual)
        {
            Equal(JsonConvert.SerializeObject(expected, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            }), actual);
        }
        
        /// <summary>
        /// Checks if string and an object coerced to a string match exactly
        /// </summary>
        public static void DeepEqual(string expected, object actual)
        {
            Equal(expected, JsonConvert.SerializeObject(actual));
        }
    }
}