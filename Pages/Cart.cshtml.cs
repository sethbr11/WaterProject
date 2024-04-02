using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterProject.Infrastructure;
using WaterProject.Models;

namespace WaterProject.Pages;

public class CartModel : PageModel
{
    private IWaterRepository _waterRepository;
    public CartModel(IWaterRepository temp, Cart cartService)
    {
        _waterRepository = temp;
        Cart = cartService;
    }

    public Cart? Cart { get; set; }
    public string ReturnUrl { get; set; } = "/";
    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
        
        // After SessionCart is added we don't need this line
        //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
    }

    public IActionResult OnPost(int projectId, string returnUrl)
    {
        Project proj = _waterRepository.Projects
            .FirstOrDefault(x => x.ProjectId == projectId);

        if (proj != null)
        {
            // After SessionCart is added we don't need this line
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            
            Cart.AddItem(proj, 1);
            
            // Or this line
            //HttpContext.Session.SetJson("cart", Cart);
        }

        return RedirectToPage(new { returnUrl = returnUrl });
    }

    public IActionResult OnPostRemove(int projectId, string returnUrl)
    {
        Cart.RemoveLine(Cart.Lines.First(x => x.Project.ProjectId == projectId).Project);

        return RedirectToPage(new { returnUrl = returnUrl });
    }
}