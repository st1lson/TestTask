using TestTaskData.Models;

namespace TestTaskWebAPI.Data.Payloads
{
    /// <summary>
    /// Represents the payload for deleting a project
    /// </summary>
    public record DeleteProjectPayload(Project Project);
}
