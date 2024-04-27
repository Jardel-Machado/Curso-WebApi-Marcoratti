using APICatalogo.Models;
using APICatalogo.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AutenticacaoController : ControllerBase
{
    private readonly ITokenServico tokenServico;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration configuration;

    public AutenticacaoController(ITokenServico tokenServico, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.tokenServico = tokenServico;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.configuration = configuration;
    }
}
