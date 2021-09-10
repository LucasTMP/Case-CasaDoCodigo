using Api.CasaDoCodigo.Data;
using Api.CasaDoCodigo.Models;
using Api.CasaDoCodigo.Models.Validations;
using Api.CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Controllers
{
    [Route("api/v1/paises")]
    public class PaisesController : BaseController<Pais>
    {
        private readonly ApiDbContext _apiDbContext;

        public PaisesController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Pais>>> GetAll()
        {
            return await _apiDbContext.Paises.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pais>> GetById(Guid id)
        {
            var pais = await GetPaisById(id);
            if (pais == null) return NotFound();

            return pais;
        }

        [HttpPost("")]
        public async Task<ActionResult<Pais>> Add(PaisAddViewModel paisAddViewModel)
        {
            if (!ModelState.IsValid) return ModelErrors(ModelState);
            if (await IsDuplicate(o => o.Nome == paisAddViewModel.Nome))
            {
                AddErrors("O País já possui cadastro no sistema.");
                return GetErrors();
            }

            var pais = new Pais(paisAddViewModel.Nome);

            var resultValidation = await new PaisValidation().ValidateAsync(pais);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            _apiDbContext.Paises.Add(pais);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            return CreatedAtAction(nameof(GetById), new { pais.Id }, pais);

        }

        //public async Task<> Update() {
        //}

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var pais = await GetPaisById(id);
            if (pais == null) return NotFound();

            if (await PaisHaveEstado(id)) return GetErrors();

            _apiDbContext.Paises.Remove(pais);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Pais> GetPaisById(Guid id)
        {

            var pais = await _apiDbContext.Paises.FirstOrDefaultAsync(o => o.Id == id);

            return pais;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PaisHaveEstado(Guid id)
        {

            var pais = await _apiDbContext.Estados.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.PaisId == id);
            if (pais != null) AddErrors("O País não pode ser removido, pois ele possui um ou mais estados.");

            return pais != null;
        }

    }
}
