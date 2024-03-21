namespace AmazonProject.Models;

public class EFWaterRepository : IWaterRepository
{
    private WaterProjectContext _waterContext;
    
    public EFWaterRepository(WaterProjectContext temp)
    {
        _waterContext = temp;
    }

    public IQueryable<Project> Projects => _waterContext.Projects;
}