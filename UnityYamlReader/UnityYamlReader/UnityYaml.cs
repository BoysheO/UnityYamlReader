using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace UnityYamlReaderSystem
{
    public class UnityYaml
    {
        private readonly string rawString;
        static readonly Regex YamlVersionRegex = new Regex(@"(?<=%YAML )[0-9.]+", RegexOptions.Compiled);
        static readonly Regex TagRegex = new Regex(@"(?<=%TAG !u! tag:)[a-zA-Z.0-9]+,\d+(?=:)", RegexOptions.Compiled);

        static readonly Regex ObjectRegex =
            new Regex(@"(?<=\n)?---(.|\n)*?(?=(((?<=\n)---)|$))",
                RegexOptions.Compiled | RegexOptions.Singleline);

        static readonly Regex StrippedRegex = new Regex(@"stripped", RegexOptions.Compiled);

        public string YamlVersion
        {
            get
            {
                var verStr = YamlVersionRegex.Match(rawString).Value;
                return verStr;
            }
        }

        public string Tag
        {
            get
            {
                var match = TagRegex.Match(rawString);
                return match.Value;
            }
        }

        public readonly ImmutableArray<UnityObject> unityObjects;

        public UnityYaml(string rawString)
        {
            this.rawString = rawString;
            var matches = ObjectRegex.Matches(rawString);
            var builder = ImmutableArray.CreateBuilder<UnityObject>();
            foreach (Match match in matches)
            {
                builder.Add(new UnityObject(match));
            }

            unityObjects = builder.ToImmutable();
        }
    }
}