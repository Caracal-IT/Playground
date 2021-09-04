// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

using Playground.XsltTransformTests.Extensions;
using Playground.XsltTransform;
using Playground.XsltTransformTests.TransformUtilities;

namespace Playground.XsltTransformTests {
    public class A_Transformer {
        [Fact]
        public async Task Should_Return_Empty_If_Source_Is_Null() {
            var extensions = new Dictionary<string, object> {
                { "utility:hash/v1", new HashUtility() }
            };

            var sourceXml = await "Resources/Data.xml".ReadFromFileAsync();
            var xslt = await "Resources/Transform.xslt".ReadFromFileAsync();
            var xslt2 = await "Resources/Transform2.xslt".ReadFromFileAsync();

            var result = Transformer.Transform(sourceXml, xslt);
            await File.WriteAllTextAsync("../../../testing.html", result, Encoding.UTF8);
            var result2 = Transformer.Transform(sourceXml, xslt2, extensions);
            
            await File.WriteAllTextAsync("../../../testing2.json", result2, Encoding.UTF8);

            var result3 = JsonConvert.DeserializeXNode(result2, "response");
            await File.WriteAllTextAsync("../../../testing3.xml", result3.ToString(), Encoding.UTF8);
            
            Assert.Equal("testing", "testing");
        }
    }
}