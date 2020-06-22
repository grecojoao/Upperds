using System;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Infra;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/Tree")]
    [Authorize(Roles = "employee, manager")]
    public class TreeController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Tree>> GetById(
        int id,
        [FromServices] DataContext context)
        {
            var tree = await context.Trees.Include(x => x.Species).Include(x => x.GroupTrees).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (tree == null)
                tree = await context.Trees.Include(x => x.Species).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (tree == null)
                tree = await context.Trees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(tree);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IQueryable<Tree>>> GetAll(
        [FromServices] DataContext context)
        {
            var trees = await context.Trees.Include(x => x.Species).Include(x => x.GroupTrees).AsNoTracking().ToListAsync();
            if (!trees.Any())
                trees = await context.Trees.Include(x => x.Species).AsNoTracking().ToListAsync();

            if (!trees.Any())
                trees = await context.Trees.AsNoTracking().ToListAsync();
            return Ok(trees);
        }

        [HttpPost]
        [Route("")]

        public async Task<ActionResult<IId>> Post(
        [FromBody] Tree model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await SpeciesIsValid(model, context))
                return NotFound(new Error("Identificador de Espécie inválido", "Espécie não cadastrada!"));

            if (!await GroupTreesIsValid(model, context))
                return NotFound(new Error("Identificador do Grupo de Árvores inválido", "Grupo não cadastrado!"));

            try
            {
                context.Trees.Add(model);
                await UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao gravar no Banco de Dados", "Não foi possível criar a Árvore!"));
            }

            return Ok(model.Id);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult> Put(
        int id,
        [FromBody] Tree model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (id != model.Id)
                return BadRequest(new Error("Identificadores diferentes", "Id informado diferente do Id da Árvore!"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await SpeciesIsValid(model, context))
                return NotFound(new Error("Identificador de Espécie inválido", "Espécie não cadastrada!"));

            if (!await GroupTreesIsValid(model, context))
                return NotFound(new Error("Identificador do Grupo de Árvores inválido", "Grupo não cadastrado!"));

            try
            {
                context.Entry<Tree>(model).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Esta Árvore já foi atualizado!"));
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Não foi possível Atualizar a Árvore!"));
            }

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<ActionResult> Delete(
        int id,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            var tree = await context.Trees.FirstOrDefaultAsync(x => x.Id == id);
            if (tree == null)
                return NotFound(new Error("Id inválido", "Árvore não encontrada!"));

            try
            {
                context.Trees.Remove(tree);
                await UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao remover do Banco de Dados", "Não foi possível Remover a Árvore!"));
            }

            return Ok("Árvore removida com sucesso!");
        }

        private async Task<bool> SpeciesIsValid(Tree model, DataContext context)
        {
            var species = await context.Species.FirstOrDefaultAsync(x => x.Id == model.SpeciesId);
            if (species == null)
                return false;
            return true;
        }

        private async Task<bool> GroupTreesIsValid(Tree model, DataContext context)
        {
            var groupTrees = await context.GroupTrees.FirstOrDefaultAsync(x => x.Id == model.GroupTreesId);
            if (groupTrees == null)
                return false;
            return true;
        }
    }
}
