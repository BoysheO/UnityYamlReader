// See https://aka.ms/new-console-template for more information

using System.Text;
using UnityYamlReader.Model;
using UnityYamlReaderSystem;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


var go = new object[2]
{
    new GameObject(),
    ClassId.Terrain,
};
var goYaml = new Serializer().Serialize(go);

Console.WriteLine("Hello, World!");
var text = Encoding.UTF8.GetString(Resource.Properties.Resources.exampleButton);
var yaml = new UnityYaml(text);
var str = yaml.unityObjects[1].DocumentBodyText;
var ins = UnityYaml.Deserializer.Deserialize<RectTransform>(str);
Console.WriteLine("end");