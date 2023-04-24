using System.Text;
using UnityYamlReaderSystem;

namespace TestProject1;

public class Tests
{
    private string ButtonPrefab;
    private string CanvasPrefab;
    
    [SetUp]
    public void Setup()
    {
        ButtonPrefab = Encoding.UTF8.GetString(Resource.Properties.Resources.exampleButton);
        CanvasPrefab = Encoding.UTF8.GetString(Resource.Properties.Resources.exampleCanvas);
    }

    [Test]
    public void TypeName()
    {
        var yaml = new UnityYaml(ButtonPrefab);
        var objects = yaml.unityObjects;
        var name = objects[0].TypeName;
        Assert.AreEqual("GameObject",name);
    }
    
    [Test]
    public void IsStripped()
    {
        var yaml = new UnityYaml(ButtonPrefab);
        var objects = yaml.unityObjects;
        var isStripped = objects[0].IsStripped;
        Assert.AreEqual(false,isStripped);
    }
    
    [Test]
    public void IsStrippedCanvas()
    {
        var yaml = new UnityYaml(CanvasPrefab);
        var objects = yaml.unityObjects;
        var isStripped = objects.Last().IsStripped;
        Assert.AreEqual(true,isStripped);
    }
    
}