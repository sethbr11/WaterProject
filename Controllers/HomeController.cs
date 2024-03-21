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
    public IActionResult Index(int pageNum)
    {
        int pageSize = 2;
        
        var data = new ProjectsListViewModel
        {
            Projects = _waterRepository.Projects
                .OrderBy(x => x.ProjectName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = _waterRepository.Projects.Count()
            }
        };
        
        return View(data);
    }
}
