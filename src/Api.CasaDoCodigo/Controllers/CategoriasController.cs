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
    [Route("api/v1/categorias")]
    public class CategoriasController : BaseController<Categoria>
    {

        private readonly ApiDbContext _apiDbContext;


        public CategoriasController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CategoriaViewModel>>> GetAll()
        {
            var categorias = await _apiDbContext.Categorias.ToListAsync();

            var categoriasViewModel = new List<CategoriaViewModel>();

            foreach (var categoria in categorias)
            {
                var categoriaViewModel = new CategoriaViewModel
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                };

                categoriasViewModel.Add(categoriaViewModel);
            }

            return Ok(categoriasViewModel);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> GetById(Guid id)
        {
            var categoria = await GetCategoriaById(id);
            if (categoria == null) return NotFound();

            var categoriaViewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
            };

            return Ok(categoriaViewModel);
        }

        [HttpPost("")]
        public async Task<ActionResult<CategoriaViewModel>> Add(CategoriaViewModel categoriaViewModel)
        {

            if (!ModelState.IsValid) return ModelErrors(ModelState);

            var categoria = new Categoria(categoriaViewModel.Nome);

            var resultValidation = await new CategoriaValidation().ValidateAsync(categoria);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            categoriaViewModel.Id = categoria.Id;

            //var checkCategoriaNomeDuplicated = await CategoriaNomeIsUnique(categoria.Nome, categoria.Id);
            //if (!checkCategoriaNomeDuplicated) return GetErrors();

            if (await IsDuplicate(o => o.Nome == categoriaViewModel.Nome && o.Id != categoriaViewModel.Id))
            {
                AddErrors("A categoria informada já possui cadastro no sistema!");
                return GetErrors();
            }

            _apiDbContext.Categorias.Add(categoria);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return CreatedAtAction(nameof(GetById), new { categoriaViewModel.Id }, categoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Update(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return ModelErrors(ModelState);
            if (id != categoriaViewModel.Id) return BadRequest();


            var categoria = await GetCategoriaById(id);
            if (categoria == null) return NotFound();

            categoria.Nome = categoriaViewModel.Nome;

            var resultValidation = await new CategoriaValidation().ValidateAsync(categoria);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            //var checkCategoriaNomeDuplicated = await CategoriaNomeIsUnique(categoriaViewModel.Nome, categoria.Id);
            //if (!checkCategoriaNomeDuplicated) return GetErrors();

            if (await IsDuplicate(o => o.Nome == categoriaViewModel.Nome && o.Id != categoriaViewModel.Id))
            {
                AddErrors("A categoria informada já possui cadastro no sistema!");
                return GetErrors();
            }

            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0 && categoria.Nome != categoriaViewModel.Nome) return StatusCode(StatusCodes.Status500InternalServerError,
                new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return Ok(categoriaViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var categoria = await GetCategoriaById(id);
            if (categoria == null) return NotFound();

            _apiDbContext.Categorias.Remove(categoria);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return Ok();
        }

        //[ApiExplorerSettings(IgnoreApi = true)]//por algum motivo que não tenho mais cabeça para pensar ele reconhece como rota
        //public async Task<bool> CategoriaNomeIsUnique(string nome, Guid id)
        //{

        //    var nomeExist = await _apiDbContext.Categorias.FirstOrDefaultAsync(parameter => parameter.Nome == nome && parameter.Id != id);
        //    if (nomeExist != null) AddErrors("A categoria informada já está cadastrada!");

        //    return nomeExist == null;
        //}


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Categoria> GetCategoriaById(Guid id)
        {

            var categoria = await _apiDbContext.Categorias.FirstOrDefaultAsync(parameter => parameter.Id == id);

            return categoria;
        }

    }
}
