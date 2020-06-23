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
    [Route("v1/HarvestTree")]
    [Authorize(Roles = "employee, manager")]
    public class HarvestTreeController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<HarvestTree>> GetById(
        int id,
        [FromServices] DataContext context)
        {
            var harvestTree = await context.HarvestTree.Include(x => x.Tree).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (harvestTree == null)
                harvestTree = await context.HarvestTree.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(harvestTree);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IQueryable<HarvestTree>>> GetAll(
        [FromServices] DataContext context)
        {
            var harvestTree = await context.HarvestTree.Include(x => x.Tree).AsNoTracking().ToListAsync();
            if (!harvestTree.Any())
                harvestTree = await context.HarvestTree.AsNoTracking().ToListAsync();
            return Ok(harvestTree);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<IId>> Post(
        [FromBody] HarvestTree model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await TreeIsValid(model, context))
                return NotFound(new Error("Identificador de Colheita inválido", "Colheita não cadastrada!"));

            try
            {
                context.HarvestTree.Add(model);
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
        [FromBody] HarvestTree model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (id != model.Id)
                return BadRequest(new Error("Identificadores diferentes", "Id informada difere da Colheita!"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await TreeIsValid(model, context))
                return NotFound(new Error("Identificador de Árvore inválido", "Árvore não cadastrada!"));

            try
            {
                context.Entry<HarvestTree>(model).State = EntityState.Modified;
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
            var harvestTrees = await context.HarvestTree.FirstOrDefaultAsync(x => x.Id == id);
            if (harvestTrees == null)
                return NotFound(new Error("Id inválido", "Colheita não encontrada!"));

            try
            {
                harvestTrees.Delete();
                context.Entry<HarvestTree>(harvestTrees).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (System.Exception)
            {
                return BadRequest(new Error("Erro ao remover do Banco de Dados", "Não foi possível Remover a Colheita!"));
            }
            return Ok();
        }

        private async Task<bool> TreeIsValid(HarvestTree model, DataContext context)
        {
            var species = await context.Trees.FirstOrDefaultAsync(x => x.Id == model.TreeId);
            if (species == null)
                return false;
            return true;
        }
    }
}
