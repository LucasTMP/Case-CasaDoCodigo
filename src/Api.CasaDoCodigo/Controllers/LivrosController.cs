using Api.CasaDoCodigo.Data;
using Api.CasaDoCodigo.Models;
using Api.CasaDoCodigo.Models.Validations;
using Api.CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Controllers
{
    [Route("/api/v1/livros")]
    public class LivrosController : BaseController<Livro>
    {
        private readonly ApiDbContext _apiDbContext;

        public LivrosController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<LivroSimplesDetailsViewModel>>> GetAll()
        {
            var livros = await _apiDbContext.Livros.AsNoTrackingWithIdentityResolution().ToListAsync();

            var simpleDetailBooks = new List<LivroSimplesDetailsViewModel>();

            foreach (var livro in livros)
            {
                var livroView = new LivroSimplesDetailsViewModel()
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    SubTitulo = livro.SubTitulo
                };

                simpleDetailBooks.Add(livroView);
            }

            return Ok(simpleDetailBooks);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<LivroViewModel>>> GetById(Guid id)
        {

            var livro = await GetLivroAutorCategoriaById(id);
            if (livro == null) return NotFound();

            var livroView = new LivroViewModel
            {
                Id = livro.Id,
                Imagem = livro.Imagem,
                Titulo = livro.Titulo,
                SubTitulo = livro.SubTitulo,
                Resumo = livro.Resumo,
                Sumario = livro.Sumario,
                Valor = livro.Valor,
                TotalDePaginas = livro.TotalDePaginas,
                ISBN = livro.ISBN,
                DataPublicacao = livro.DataPublicacao,
                AutorNome = livro.Autor.Nome,
                AutorDescricao = livro.Autor.Descricao,
                CategoriaNome = livro.Categoria.Nome,
                DataCadastro = livro.CreatedAt,
                UltimaModificacao = livro.UpdatedAt
            };

            return Ok(livroView);
        }


        [HttpPost("")]
        public async Task<ActionResult<LivroAddViewModel>> Add(LivroAddViewModel livroAddViewModel)
        {
            Console.WriteLine();

            if (!ModelState.IsValid) return ModelErrors(ModelState);

            if (await IsDuplicate(o => o.ISBN == livroAddViewModel.ISBN || o.Titulo == livroAddViewModel.Titulo))
            {
                AddErrors("O ISBN ou o Titulo já possui cadastro no sistema!");
                return GetErrors();
            }

            if (await NotExistCategoriaAndAutor(livroAddViewModel.CategoriaId, livroAddViewModel.AutorId)) return GetErrors();

            var livro = new Livro(
                livroAddViewModel.Imagem,
                livroAddViewModel.Titulo,
                livroAddViewModel.SubTitulo,
                livroAddViewModel.Resumo,
                livroAddViewModel.Sumario,
                livroAddViewModel.Valor,
                livroAddViewModel.TotalDePaginas,
                livroAddViewModel.ISBN,
                livroAddViewModel.DataPublicacao,
                livroAddViewModel.CategoriaId,
                livroAddViewModel.AutorId
            );

            var resultValidation = await new LivroValidation().ValidateAsync(livro);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            livroAddViewModel.Id = livro.Id; //passar o id na viewmodel

            _apiDbContext.Livros.Add(livro);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return CreatedAtAction(nameof(GetById), new { livro.Id }, livroAddViewModel);
        }

        //[HttpPut("{id:guid}")]
        //public async Task<ActionResult<LivroViewModel>> Update(Guid id, LivroViewModel categoriaViewModel)
        //{
        //}

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var livro = await GetLivroById(id);
            if (livro == null) return NotFound();

            _apiDbContext.Livros.Remove(livro);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Livro> GetLivroById(Guid id)
        {
            var livros = await _apiDbContext.Livros.FirstOrDefaultAsync(parameter => parameter.Id == id);

            return livros;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Livro> GetLivroAutorCategoriaById(Guid id)
        {
            var livro = await _apiDbContext.Livros.AsNoTrackingWithIdentityResolution()
                                                  .Include(o => o.Autor)
                                                  .Include(o => o.Categoria)
                                                  .FirstOrDefaultAsync(o => o.Id == id);

            return livro;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> NotExistCategoriaAndAutor(Guid idCategoria, Guid idAutor)
        {
            var categoria = await _apiDbContext.Categorias.FirstOrDefaultAsync(parameter => parameter.Id == idCategoria);
            var autor = await _apiDbContext.Autores.FirstOrDefaultAsync(parameter => parameter.Id == idAutor);

            if (categoria == null) AddErrors("A categoria é invalida");
            if (autor == null) AddErrors("O autor é invalido");

            return categoria == null || autor == null;
        }
    }
}