using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

using Xunit.Sdk;

namespace OzonContestFeb23.Tests;

public class JsonFileDataAttribute<T> : DataAttribute
{
    private readonly string _filePath;
    private readonly List<string> _properties;

    public JsonFileDataAttribute(string filePath)
    {
        _filePath = filePath;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var path = Path.IsPathRooted(_filePath)
            ? _filePath
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

        if (!File.Exists(path))
        {
            throw new ArgumentException($"Could not find file at path: {path}");
        }
        
        using var reader = new StreamReader(_filePath);
        var json = reader.ReadToEnd();
        var data = JsonConvert.DeserializeObject<List<T>>(json);

        
        return data.Select(x => new object[]{ x });
    }
}