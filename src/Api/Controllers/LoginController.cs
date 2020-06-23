using System.Linq;
using System.Threading.Tasks;
using Api.Service;
using Domain.Data;
using Domain.Infra;
using Domain.Models.Login;
using Domain.Models.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("v1/Login")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseLogin>> Authenticate([FromBody] User model, [FromServices] DataContext context)
        {
            var user = await context.Users.Where(x => x.UserName.ToLower() == model.UserName.ToLower() && x.Password.ToLower() == model.Password.ToLower()).FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou Senha inválidos!" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return Ok(new ResponseLogin(user.UserName, token));
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<IId>> Create([FromBody] User model, [FromServices] DataContext Context, [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Context.Users.AddAsync(model);
            await UnitOfWork.Commit();
            return Ok(model.Id);
        }
    }
}
