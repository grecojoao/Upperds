using System;
using System.Threading.Tasks;
using Domain.Infra;
using Domain.Models;
using Domain.Models.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/GroupTrees")]
    [Authorize(Roles = "employee, manager")]
    public class GroupTreesController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GroupTrees>> GetById(
        int id,
        [FromServices] DataContext context)
            => Ok(await context.GroupTrees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id));

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IQueryable<GroupTrees>>> GetAll(
        [FromServices] DataContext context)
            => Ok(await context.GroupTrees.AsNoTracking().ToListAsync());

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<IId>> Post(
        [FromBody] GroupTrees model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.GroupTrees.Add(model);
                await UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao gravar no Banco de Dados", "Não foi possível criar o Grupo de Árvores!"));
            }
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
        int id,
        [FromBody] GroupTrees model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (id != model.Id)
                return BadRequest(new Error("Identificadores diferentes", "Id informada difere do Grupo de Árvores!"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<GroupTrees>(model).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Este Grupo de Árvores já foi atualizado!"));
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Não foi possível Atualizar o Grupo de Árvores!"));
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
        int id,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            var groupOfTrees = await context.GroupTrees.FirstOrDefaultAsync(x => x.Id == id);
            if (groupOfTrees == null)
                return NotFound(new Error("Id inválido", "Grupo de Árvores não encontrada!"));

            try
            {
                groupOfTrees.Delete();
                context.Entry<GroupTrees>(groupOfTrees).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (System.Exception)
            {
                return BadRequest(new Error("Erro ao remover do Banco de Dados", "Não foi possível Remover o Grupo de Árvores!"));
            }
            return Ok();
        }
    }
}
