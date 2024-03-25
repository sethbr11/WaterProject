using Microsoft.AspNetCore.Mvc;
using WaterProject.Models;

namespace WaterProject.Components;

public class ProjectTypesViewComponent : ViewComponent
{
    private IWaterRepository _waterRepository;
    
    // Constructor
    public ProjectTypesViewComponent(IWaterRepository temp)
    {
        _waterRepository = temp;
    }
    
    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedProjectType = RouteData?.Values["projectType"];
        
        var projectTypes = _waterRepository.Projects
            .Select(x => x.ProjectType)
            .Distinct()
            .OrderBy(x => x);
        
        return View(projectTypes);
    }
}