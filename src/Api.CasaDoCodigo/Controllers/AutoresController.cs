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
    [Route("/api/v1/autores")]
    public class AutoresController : BaseController<Autor>
    {

        private readonly ApiDbContext _apiDbContext;

        public AutoresController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }




        [HttpGet("")]
        public async Task<ActionResult<List<AutorViewModel>>> GetAll()
        {
            var autores = await _apiDbContext.Autores.ToListAsync();

            var autoresViewModel = new List<AutorViewModel>();

            foreach (var autor in autores)
            {
                var autorViewModel = new AutorViewModel
                {
                    Id = autor.Id,
                    Nome = autor.Nome,
                    Email = autor.Email,
                    Descricao = autor.Descricao,
                    CreatedAt = autor.CreatedAt,
                    UpdatedAt = autor.UpdatedAt
                };

                autoresViewModel.Add(autorViewModel);
            }

            return Ok(autoresViewModel);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<AutorViewModel>>> GetById(Guid id)
        {

            var autor = await GetAutorById(id);

            if (autor == null) return NotFound();

            var autorViewModel = new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Email = autor.Email,
                Descricao = autor.Descricao,
                CreatedAt = autor.CreatedAt,
                UpdatedAt = autor.UpdatedAt
            };

            return Ok(autorViewModel);

        }

        [HttpPost("")]
        public async Task<ActionResult<AutorViewModel>> Add(AutorAddViewModel autorViewModel)
        {
            if (!ModelState.IsValid) return ModelErrors(ModelState);

            if (await EmailExist(autorViewModel.Email)) return GetErrors();

            var autor = new Autor(autorViewModel.Nome, autorViewModel.Email, autorViewModel.Descricao);

            //var checkEmailDuplicated = await EmailIsUnique(autor.Email, autor.Id);
            //if (!checkEmailDuplicated) return GetErrors();

            var resultValidation = await new AutorValidation().ValidateAsync(autor);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            _apiDbContext.Autores.Add(autor);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            var autorAutorViewModel = new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Email = autor.Email,
                Descricao = autor.Descricao,
                CreatedAt = autor.CreatedAt,
                UpdatedAt = autor.UpdatedAt,
            };

            return CreatedAtAction(nameof(GetById), new { autorAutorViewModel.Id }, autorAutorViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AutorViewModel>> Update(Guid id, AutorViewModel autorViewModel)
        {

            if (!ModelState.IsValid) return ModelErrors(ModelState);
            if (id != autorViewModel.Id) return BadRequest();

            var autor = await GetAutorById(id);
            if (autor == null) return NotFound();

            //var checkEmailDuplicated = await EmailIsUnique(autorViewModel.Email, autor.Id);
            //if (!checkEmailDuplicated) return GetErrors();

            if (await IsDuplicate(o => o.Email == autorViewModel.Email && o.Id != autorViewModel.Id))
            {
                AddErrors("O email informado já possui cadastro no sistema!");
                return GetErrors();
            }

            autor.Nome = autorViewModel.Nome;
            autor.Email = autorViewModel.Email;
            autor.Descricao = autorViewModel.Descricao;
            autor.UpdatedAt = DateTime.Now;

            autorViewModel.UpdatedAt = autor.UpdatedAt;
            autorViewModel.CreatedAt = autor.CreatedAt;

            var resultValidation = await new AutorValidation().ValidateAsync(autor);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);


            return Ok(autorViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var autor = await GetAutorById(id);
            if (autor == null) return NotFound();

            _apiDbContext.Autores.Remove(autor);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            return Ok();
        }


        private async Task<Autor> GetAutorById(Guid id)
        {
            var autor = await _apiDbContext.Autores.FirstOrDefaultAsync(o => o.Id == id);

            return autor;
        }

        //private async Task<bool> EmailIsUnique(string email, Guid id)
        //{

        //    var emailExist = await _apiDbContext.Autores.FirstOrDefaultAsync(o => o.Email == email && o.Id != id);
        //    if (emailExist != null) AddErrors("O email informado já possui cadastro no sistema!");

        //    return emailExist == null;
        //}

    }
}
