using UnityYamlReaderSystem;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace UnityYamlReaderSystem
{
    public sealed class UnityObject
    {
        static readonly Regex ClassIdRegex = new Regex(@"(?<=^--- !u!)\d+", RegexOptions.Compiled);
        static readonly Regex FileIdRegex = new Regex(@"(?<=&)\d+", RegexOptions.Compiled);
        static readonly Regex TypeNameRegex = new Regex(@"^\w+(?=:)", RegexOptions.Compiled);
        static readonly Regex StrippedRegex = new Regex(@"(?<=^--- !u!\d+ &\d+ )stripped", RegexOptions.Compiled);

        private readonly Match _yaml;
        public readonly ClassId ClassId;
        public readonly long FileId;

        private string? _TypeName;
        public string TypeName => _TypeName ??= TypeNameRegex.Match(_yaml.Value).Value;

        private bool? _IsStripped;
        public bool IsStripped => _IsStripped ??= StrippedRegex.IsMatch(_yaml.Value);

        public UnityObject(Match yaml)
        {
            _yaml = yaml;
            ClassId = (ClassId)int.Parse(ClassIdRegex.Match(yaml.Value).Value);
            FileId = long.Parse(FileIdRegex.Match(yaml.Value).Value);
        }

        private ImmutableArray<YamlDocument>? _document;

        public ImmutableArray<YamlDocument> Document
        {
            get
            {
                if (_document == null)
                {
                    //get the text except the first line
                    var idx = _yaml.Value.IndexOf("\n", StringComparison.Ordinal);
                    if (idx < 0) throw new Exception("Invalid YAML");
                    var text = _yaml.Value.Substring(idx + 1);
                    var yaml = new YamlStream();
                    yaml.Load(new StringReader(text));
                    _document = yaml.Documents.ToImmutableArray();
                }

                return _document.Value;
            }
        }
    }
}