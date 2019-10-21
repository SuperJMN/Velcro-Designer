namespace Designer.Model.Tests
{
    public class AssignmentStatement : Statement
    {
        public string Verb { get; }
        public string Property { get; }
        public string Value { get; }

        public AssignmentStatement(string verb, string property, string value)
        {
            Verb = verb;
            Property = property;
            Value = value;
        }

        public override string ToString()
        {
            return $"/{Verb}: {Property} = {Value}";
        }
    }
}