using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.Utilities
{
    public class JReader
    {
        public string extractData(string tokenName)
        {
            string myJsonString = File.ReadAllText("Utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName)!.Value<string>()!;
        }

        public string[] extractDataArray(string tokenName)
        {
            string myJsonString = File.ReadAllText("Utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            List<string> productsList = jsonObject.SelectTokens(tokenName)!.Values<string>()!.ToList()!;
            return productsList.ToArray();
        }
    }
}
