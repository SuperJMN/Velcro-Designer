namespace Designer.Model.Tests
{
    public class RegularStatement : Statement
    {
        public string Verb { get; }
        public string Value { get; }

        public RegularStatement(string verb, string value)
        {
            Verb = verb;
            Value = value;
        }

        public override string ToString()
        {
            return $"/{Verb}: {Value}";
        }
    }
}