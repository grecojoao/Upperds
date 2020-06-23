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
    [Route("v1/Species")]
    [Authorize(Roles = "employee, manager")]
    public class SpeciesController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Species>> GetById(
        int id,
        [FromServices] DataContext context)
            => Ok(await context.Species.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id));

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IQueryable<Species>>> GetAll(
        [FromServices] DataContext context)
            => Ok(await context.Species.AsNoTracking().ToListAsync());

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<IId>> Post(
        [FromBody] Species model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Species.Add(model);
                await UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao gravar no Banco de Dados", "Não foi possível criar a Espécie!"));
            }
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
        int id,
        [FromBody] Species model,
        [FromServices] DataContext context,
        [FromServices] IUnitOfWork UnitOfWork)
        {
            if (id != model.Id)
                return BadRequest(new Error("Identificadores diferentes", "Id informada difere da Espécie!"));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Species>(model).State = EntityState.Modified;
                await UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Esta Espécie já foi atualizado!"));
            }
            catch (Exception)
            {
                return BadRequest(new Error("Erro ao atualizar o Banco de Dados", "Não foi possível Atualizar a Espécie!"));
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

            var species = await context.Species.FirstOrDefaultAsync(x => x.Id == id);
            if (species == null)
                return NotFound(new Error("Id inválido", "Espécie não encontrada!"));

            try
            {
                context.Species.Remove(species);
                await UnitOfWork.Commit();
            }
            catch (System.Exception)
            {
                return BadRequest(new Error("Erro ao remover do Banco de Dados", "Não foi possível Remover a Espécie!"));
            }
            return Ok();
        }
    }
}
