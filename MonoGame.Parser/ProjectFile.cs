namespace Designer.Model.Tests
{
    public class ProjectFile
    {
        public Section[] Sections { get; }

        public ProjectFile(Section[] sections)
        {
            Sections = sections;
        }
    }
}