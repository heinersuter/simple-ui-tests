namespace Alsolos.RegexTester.Controller
{
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class RegexEvaluator
    {
        public string Evaluate(string inputText, Regex regex)
        {
            var sb = new StringBuilder();
            var groups = regex.GetGroupNames();
            if (groups.Length > 1)
            {
                sb.AppendLine(string.Format("Groups: {0}", string.Join(", ", groups.Skip(1))));
                sb.AppendLine();
            }

            foreach (Match match in regex.Matches(inputText))
            {
                sb.AppendLine(string.Format("Match: {0}", match.Value));
                for (var i = 1; i < match.Groups.Count; i++)
                {
                    var g = match.Groups[i];
                    if (g.Success)
                    {
                        sb.AppendLine(string.Format("    Group {0}: {1}", regex.GroupNameFromNumber(i), g.Value));
                    }
                }
            }
            return sb.ToString();
        }
    }
}
