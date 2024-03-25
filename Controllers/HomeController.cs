using Microsoft.AspNetCore.Mvc;
using WaterProject.Models;
using WaterProject.Models.ViewModels;

namespace WaterProject.Controllers;

public class HomeController : Controller
{
    private IWaterRepository _waterRepository;
    
    public HomeController(IWaterRepository temp)
    {
        _waterRepository = temp;
    }
    public IActionResult Index(int pageNum, string projectType)
    {
        int pageSize = 5;
        
        var data = new ProjectsListViewModel
        {
            Projects = _waterRepository.Projects
                .Where(x => x.ProjectType == projectType || projectType == null )
                .OrderBy(x => x.ProjectName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = projectType == null ? 
                                _waterRepository.Projects.Count() :
                                _waterRepository.Projects.Where(x => x.ProjectType == projectType).Count()
            },
            CurrentProjectType = projectType
        };
        
        return View(data);
    }
}
