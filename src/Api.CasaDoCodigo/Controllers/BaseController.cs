using Api.CasaDoCodigo.Data;
using Api.CasaDoCodigo.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : Base
    {

        private readonly ApiDbContext _apiDbContext;
        public readonly DbSet<T> dbSet;
        protected readonly object MsgInternalErro = new { MsgErro = "Ooops! algo deu errado, tente mais tarde." };

        private List<string> _errors { get; set; }

        public BaseController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
            dbSet = _apiDbContext.Set<T>();
            _errors = new List<string>();
        }

        protected ActionResult ValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddErrors(error.ErrorMessage);
            }

            return GetErrors();
        }

        protected ActionResult ModelErrors(ModelStateDictionary modelstate)
        {
            var erros = modelstate.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AddErrors(errorMsg);
            }

            return GetErrors();
        }

        protected void AddErrors(string error)
        {
            _errors.Add(error);
        }

        protected ActionResult GetErrors()
        {
            return BadRequest(new
            {
                sucess = false,
                errors = _errors
            });
        }

        protected ActionResult GetErrors(string erroMsg)
        {
            AddErrors(erroMsg);

            return GetErrors();
        }

        protected bool ExistErrors()
        {
            return _errors.Any();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> IsDuplicate(Expression<Func<T, bool>> predicate)
        {
            var result = dbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync().Result.Any();

            return result;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> EmailExist(string email)
        {
            var autorEmailExist = await _apiDbContext.Autores.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.Email == email);

            var clientEmailExist = await _apiDbContext.Clientes.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.Email == email);

            if (autorEmailExist != null || clientEmailExist != null)
            {
                AddErrors("O Email informado já possui cadastro no sistema!");
                return true;
            }

            return false;
        }

    }
}
