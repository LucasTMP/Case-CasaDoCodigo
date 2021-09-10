using Api.CasaDoCodigo.Data;
using Api.CasaDoCodigo.Models;
using Api.CasaDoCodigo.Models.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Controllers
{
    [Route("api/v1/clientes")]
    public class ClientesController : BaseController<Cliente>
    {

        private readonly ApiDbContext _apiDbContext;

        public ClientesController(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ClienteSimplesDetailsViewModel>>> GetAll()
        {

            var clientes = await GetClientesPaisEstado();

            var clienteSimplesDetailsViewModel = new List<ClienteSimplesDetailsViewModel>();

            foreach (var cliente in clientes)
            {
                var clienteview = new ClienteSimplesDetailsViewModel()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Documento = cliente.Documento,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone,
                    Pais = cliente.Pais.Nome,
                    Estado = cliente.Estado == null ? "Sem Estado" : cliente.Estado.Nome,
                    Cep = cliente.Cep
                };

                clienteSimplesDetailsViewModel.Add(clienteview);
            }

            return Ok(clienteSimplesDetailsViewModel);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteDetailsViewModel>> GetById(Guid id)
        {
            var clienteToView = await GetClientePaisEstado(id);
            if (clienteToView == null) return NotFound();

            var clienteDetailsViewModel = new ClienteDetailsViewModel
            {
                Id = clienteToView.Id,
                Nome = clienteToView.Nome,
                Sobrenome = clienteToView.Sobrenome,
                Documento = clienteToView.Documento,
                Email = clienteToView.Email,
                Telefone = clienteToView.Telefone,
                Endereco = clienteToView.Endereco,
                Complemento = clienteToView.Complemento,
                Cidade = clienteToView.Cidade,
                Cep = clienteToView.Cep,
                Estado = clienteToView.Estado == null ? "Sem Estado" : clienteToView.Estado.Nome,
                Pais = clienteToView.Pais.Nome,
                CriadoEm = clienteToView.CreatedAt,
            };


            return Ok(clienteDetailsViewModel);

        }

        [HttpPost("")]
        public async Task<ActionResult<ClienteDetailsViewModel>> Add(ClienteAddViewModel clienteAddViewModel)
        {
            if (!ModelState.IsValid) return ModelErrors(ModelState);

            if (await NotExistPais(clienteAddViewModel.PaisId)) return GetErrors("O País informado não possui cadastro!");

            if (clienteAddViewModel.EstadoId != Guid.Empty)
            {
                if (await PaisNotHaveEstado(clienteAddViewModel.PaisId, clienteAddViewModel.EstadoId)) return GetErrors();
            }
            else
            {
                if (await PaisHaveEstado(clienteAddViewModel.PaisId)) return GetErrors();
            }

            if (await EmailExist(clienteAddViewModel.Email)) return GetErrors();

            if (await IsDuplicate(o => o.Documento == clienteAddViewModel.Documento)) return GetErrors();

            var cliente = new Cliente(
                clienteAddViewModel.Nome,
                clienteAddViewModel.Sobrenome,
                clienteAddViewModel.Documento,
                clienteAddViewModel.Email,
                clienteAddViewModel.Endereco,
                clienteAddViewModel.Complemento,
                clienteAddViewModel.Cidade,
                clienteAddViewModel.PaisId,
                clienteAddViewModel.Telefone,
                clienteAddViewModel.Cep
                );

            if (clienteAddViewModel.EstadoId != Guid.Empty) cliente.EstadoId = clienteAddViewModel.EstadoId;

            var resultValidation = await new ClienteValidation().ValidateAsync(cliente);
            if (!resultValidation.IsValid) return ValidationErrors(resultValidation);

            _apiDbContext.Clientes.Add(cliente);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, MsgInternalErro);

            var clienteToView = await GetClientePaisEstado(cliente.Id);
            if (clienteToView == null) return GetErrors("O Cliente sofreu uma alteração inesperada!");

            var clienteDetailsViewModel = new ClienteDetailsViewModel
            {
                Id = clienteToView.Id,
                Nome = clienteToView.Nome,
                Sobrenome = clienteToView.Sobrenome,
                Documento = clienteToView.Documento,
                Email = clienteToView.Email,
                Telefone = clienteToView.Telefone,
                Endereco = clienteToView.Endereco,
                Complemento = clienteToView.Complemento,
                Cidade = clienteToView.Cidade,
                Cep = clienteToView.Cep,
                Estado = clienteToView.Estado == null ? "Sem Estado" : clienteToView.Estado.Nome,
                Pais = clienteToView.Pais.Nome,
                CriadoEm = clienteToView.CreatedAt,
            };

            return CreatedAtAction(nameof(GetById), new { cliente.Id }, clienteDetailsViewModel);
        }

        //public async Task<> Update()
        //{
        //}

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cliente = await GetClienteById(id);
            if (cliente == null) return NotFound();

            _apiDbContext.Clientes.Remove(cliente);
            var result = await _apiDbContext.SaveChangesAsync();
            if (result <= 0) return StatusCode(StatusCodes.Status500InternalServerError, new { MsgErro = "Ooops! algo deu errado, tente mais tarde." });

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<List<Cliente>> GetClientesPaisEstado()
        {
            var clientes = await _apiDbContext.Clientes.AsNoTrackingWithIdentityResolution().Include(o => o.Pais)
                                                                                            .Include(o => o.Estado)
                                                                                            .DefaultIfEmpty()
                                                                                            .ToListAsync();

            return clientes;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Cliente> GetClientePaisEstado(Guid id)
        {
            var cliente = await _apiDbContext.Clientes.AsNoTrackingWithIdentityResolution().Include(o => o.Pais)
                                                                                            .Include(o => o.Estado)
                                                                                            .DefaultIfEmpty()
                                                                                            .FirstOrDefaultAsync(o => o.Id == id);

            return cliente;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> NotExistPais(Guid id)
        {
            var existPais = await _apiDbContext.Paises.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.Id == id);
            if (existPais == null) AddErrors("O País informado não possui cadastro!");

            return existPais == null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PaisNotHaveEstado(Guid paisId, Guid estadoId)
        {
            var paisHaveEstado = await _apiDbContext.Estados.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.PaisId == paisId && o.Id == estadoId);
            if (paisHaveEstado == null) AddErrors("O Estado não possui cadastro para o País informado!");

            return paisHaveEstado == null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> PaisHaveEstado(Guid paisId)
        {
            var paisHaveEstado = await _apiDbContext.Estados.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(o => o.PaisId == paisId);
            if (paisHaveEstado != null) AddErrors("O País possui Estados, permitido cadastro apenas usando eles.");

            return paisHaveEstado != null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Cliente> GetClienteById(Guid id)
        {
            var cliente = await _apiDbContext.Clientes.FirstOrDefaultAsync(o => o.Id == id);

            return cliente;
        }

    }
}
