using System;
using System.Threading.Tasks;
using Domain.Infra;
using Domain.Models;
using Domain.Models.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/HarvestGroupTrees")]
    [Authorize(Roles = "employee, manager")]
    public class HarvestGroupTreesController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<HarvestGroupTrees>> GetById(
        int id,
        [FromServices] DataContext context)
        {
            var harvestGroupTrees = await context.HarvestGroupTrees.Include(x => x.Group).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (harvestGroupTrees == null)
                harvestGroupTrees = await context.HarvestGroupTrees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(harvestGroupTrees);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IQueryable<HarvestGroupTrees>>> GetAll(
        [FromServices] DataContext context)
        {
            var harvestGroupTrees = await context.HarvestGroupTrees.Include(x => x.Group).AsNoTracking().ToListAsync();
            if (!harvestGroupTrees.Any())
                harvestGroupTrees = await context.HarvestGroupTrees.AsNoTracking().ToListAsync();

            return Ok(harvestGroupTrees);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<IId>> Post(
        [FromBody] HarvestGroupTrees model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await GroupTreesIsValid(model, context))
                return NotFound(new Error("Identificador de Colheita inválido", "Colheita não cadastrada!"));

            try
            {
                context.HarvestGroupTrees.Add(model);
                await UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao gravar no Banco de Dados", "Não foi possível criar a Colheita!"));
            }

            return Ok(model.Id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
        int id,
        [FromBody] HarvestGroupTrees model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (id != model.Id)
                return BadRequest(new Error("Identificadores diferentes", "Id informada difere da Colheita!"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await GroupTreesIsValid(model, context))
                return NotFound(new Error("Identificador de Árvore inválido", "Árvore não cadastrada!"));

            try
            {
                context.Entry<HarvestGroupTrees>(model).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Esta Colheita já foi atualizado!"));
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Não foi possível Atualizar a Colheita!"));
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

            var harvestGroupTrees = await context.HarvestGroupTrees.FirstOrDefaultAsync(x => x.Id == id);
            if (harvestGroupTrees == null)
                return NotFound(new Error("Id inválido", "Colheita não encontrada!"));

            try
            {
                harvestGroupTrees.Delete();
                context.Entry<HarvestGroupTrees>(harvestGroupTrees).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (System.Exception)
            {
                return BadRequest(new Error("Erro ao remover do Banco de Dados", "Não foi possível Remover a Colheita!"));
            }

            return Ok();
        }

        private async Task<bool> GroupTreesIsValid(HarvestGroupTrees model, DataContext context)
        {
            var species = await context.GroupTrees.FirstOrDefaultAsync(x => x.Id == model.GroupId);
            if (species == null)
                return false;
            return true;
        }
    }
}
