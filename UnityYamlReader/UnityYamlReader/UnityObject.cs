using UnityYamlReaderSystem;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace UnityYamlReaderSystem
{
    public sealed class UnityObject
    {
        static readonly Regex ClassIdRegex = new Regex(@"(?<=^--- !u!)\d+", RegexOptions.Compiled);
        static readonly Regex FileIdRegex = new Regex(@"(?<=&)\d+", RegexOptions.Compiled);
        static readonly Regex TypeNameRegex = new Regex(@"^\w+(?=:)", RegexOptions.Compiled | RegexOptions.Multiline);
        static readonly Regex StrippedRegex = new Regex(@"(?<=^--- !u!\d+ &\d+ )stripped", RegexOptions.Compiled);

        private readonly Match _yaml;
        public readonly ClassId ClassId;
        public readonly long FileId;

        private string? _TypeName;

        /// <summary>
        /// ex. GameObject
        /// </summary>
        public string TypeName
        {
            get { return _TypeName ??= TypeNameRegex.Match(_yaml.Value).Value; }
        }

        private bool? _IsStripped;
        public bool IsStripped => _IsStripped ??= StrippedRegex.IsMatch(_yaml.Value);

        public UnityObject(Match yaml)
        {
            _yaml = yaml;
            ClassId = (ClassId)int.Parse(ClassIdRegex.Match(yaml.Value).Value);
            FileId = long.Parse(FileIdRegex.Match(yaml.Value).Value);
        }


        private string? _documentText;

        /// <summary>
        /// ex.
        /// <code>
        ///GameObject:
        ///  m_ObjectHideFlags: 0
        ///  m_CorrespondingSourceObject: {fileID: 0}
        ///  m_PrefabInstance: {fileID: 0}
        ///  m_PrefabAsset: {fileID: 0}
        ///  serializedVersion: 6
        ///  m_Component:
        ///  - component: {fileID: 5540805159857226831}
        ///  - component: {fileID: 5540805159857227184}
        ///  m_Layer: 5
        ///  m_Name: Button
        ///  m_TagString: Untagged
        ///  m_Icon: {fileID: 0}
        ///  m_NavMeshLayer: 0
        ///  m_StaticEditorFlags: 0
        ///  m_IsActive: 1
        /// </code>
        /// </summary>
        public string DocumentText
        {
            get
            {
                if (_documentText == null)
                {
                    //get the text except the first line
                    var idx = _yaml.Value.IndexOf("\n", StringComparison.Ordinal);
                    if (idx < 0) throw new Exception("Invalid YAML");
                    _documentText = _yaml.Value.Substring(idx + 1);
                }

                return _documentText;
            }
        }

        private YamlDocument? _document;

        public YamlDocument Document
        {
            get
            {
                if (_document == null)
                {
                    var text = DocumentText;
                    var yaml = new YamlStream();
                    yaml.Load(new StringReader(text));
                    _document = yaml.Documents.First();
                }

                return _document;
            }
        }


        /// <summary>
        /// ex.
        /// <code>
        ///  m_ObjectHideFlags: 0
        ///  m_CorrespondingSourceObject: {fileID: 0}
        ///  m_PrefabInstance: {fileID: 0}
        ///  m_PrefabAsset: {fileID: 0}
        ///  serializedVersion: 6
        ///  m_Component:
        ///  - component: {fileID: 5540805159857226831}
        ///  - component: {fileID: 5540805159857227184}
        ///  m_Layer: 5
        ///  m_Name: Button
        ///  m_TagString: Untagged
        ///  m_Icon: {fileID: 0}
        ///  m_NavMeshLayer: 0
        ///  m_StaticEditorFlags: 0
        ///  m_IsActive: 1
        /// </code>
        /// </summary>
        public string DocumentBodyText
        {
            get
            {
                var idx = DocumentText.IndexOf("\n", StringComparison.Ordinal);
                if (idx < 0) throw new Exception("Invalid YAML");
                return DocumentText.Substring(idx + 1);
            }
        }
    }
}