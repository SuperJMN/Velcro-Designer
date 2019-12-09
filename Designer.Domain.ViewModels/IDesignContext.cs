namespace Designer.Domain.ViewModels
{
    public interface IDesignContext
    {
        IDesignCommandsHost DesignCommandsHost { get; set; }
        Document Document { get; set; }
    }
}