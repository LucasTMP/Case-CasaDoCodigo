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
    [Route("api/v1/estados")]
    public class EstadosController : BaseController<Estado>
    {
        private readonly ApiDbContext _apiDbContext;

        public EstadosController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<EstadoDetailsViewModel>>> GetAll()
        {
            var estados = await _apiDbContext.Estados.Include(o => o.Pais).ToListAsync();

            var estadosDetailsViewModel = new List<EstadoDetailsViewModel>();

            foreach (var estado in estados)
            {
                var estadoDetailsViewModel = new EstadoDetailsViewModel
                {

                    Id = estado.Id,
                    Nome = estado.Nome,
                    Pais = estado.Pais.Nome
                };
                estadosDetailsViewModel.Add(estadoDetailsViewModel);
            }

            return estadosDetailsViewModel;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstadoDetailsViewModel>> GetById(Guid id)
        {
            var estado = await GetEstadoPaisById(id);
            if (estado == null) return NotFound();

            var estadoDetailsViewModel = new EstadoDetailsViewModel
            {
                Id = estado.Id,
                Nome = estado.Nome,
                Pais = estado.Pais.Nome,

            };

            return estadoDetailsViewModel;

        }

        [HttpPost("")]
        public async Task<ActionResult<EstadoDetailsViewModel>> Add(EstadoAddViewModel estadoAddViewModel)
        {
            if (!ModelState.IsValid) return ModelErrors(ModelState);
            if (await IsDuplicate(o => o.Nome == estadoAddViewModel.Nome && o.PaisId == estadoAddViewModel.PaisId))
            {
                AddErrors("O Estado já possui um cadastro para esse País no sistema.");
                return GetErrors();
            }

            if (await ExistPais(estadoAddViewModel.PaisId)) return GetErrors();

            var estado = new Estado(estadoAddViewModel.Nome, estadoAddViewModel.PaisId);

            var resultValidation = await new EstadoValidation().ValidateAsync(estado);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            _apiDbContext.Estados.Add(estado);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            var estadoPais = await GetEstadoPaisById(estado.Id);

            var estadoDetailsViewModel = new EstadoDetailsViewModel
            {
                Id = estado.Id,
                Nome = estado.Nome,
                Pais = estadoPais.Pais.Nome
            };

            return CreatedAtAction(nameof(GetById), new { estado.Id }, estadoDetailsViewModel);


        }

        //public async Task<> Update() {
        //}

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var estado = await GetEstadoById(id);
            if (estado == null) return NotFound();

            _apiDbContext.Estados.Remove(estado);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Estado> GetEstadoById(Guid id)
        {

            var estados = await _apiDbContext.Estados.FirstOrDefaultAsync(o => o.Id == id);

            return estados;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> ExistPais(Guid id)
        {

            var paisesExist = await _apiDbContext.Paises.FirstOrDefaultAsync(o => o.Id == id);
            if (paisesExist == null) AddErrors("O pais informado não possui cadastro no sistema.");

            return paisesExist == null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Estado> GetEstadoPaisById(Guid id)
        {

            var estado = await _apiDbContext.Estados.Include(o => o.Pais).FirstOrDefaultAsync(o => o.Id == id);

            return estado;
        }

    }
}
