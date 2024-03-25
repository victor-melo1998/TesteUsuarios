using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosTeste.Models;
using UsuariosTeste.Contexto;
using UsuariosTeste.Dominio;
using UsuariosTeste.Services.Interface.Service;

namespace UsuariosTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _UsuarioService;
        private readonly ContextoDB _context;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuariosService UsuarioService, ContextoDB context, IMapper mapper)
        {
            _UsuarioService = UsuarioService;
            _context = context;
            _mapper = mapper;
        }


        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginModel>>> GetUsuario()
        {
            var user = await _context.Usuarios.ToListAsync();
            var viewModel = _mapper.Map<List<LoginModel>>(user);
            return viewModel;
        }

        // GET: api/
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginModel>> GetUsuarioModel(long id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuModel = await _context.Usuarios.FindAsync(id);

            if (usuModel == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<LoginModel>(usuModel);
            return viewModel;
        }

        [HttpGet("BuscarUsuario/{nome}")]
        public async Task<ActionResult<IEnumerable<LoginModel>>> BuscarUsuario(string nome)
        {
            var user = await _context.Usuarios.ToListAsync();
            user = user.FindAll(x => x.Usuario.Contains(nome));
            if (user.Count == 0)
            {
                return BadRequest("Dados não encontrados");
            }
            var viewModel = _mapper.Map<List<LoginModel>>(user);
            return viewModel;
        }
        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuariosModel>> PostUsuarioModel(UsuariosModel usuarioModel)
        {
            var usu = await _UsuarioService.ConsultaUltimoId();
            var viewModel = _mapper.Map<Login>(usuarioModel);
            if (usu != 0)
            {
                viewModel.Id = usu + 1;
                _context.Usuarios.Add(viewModel);
                _context.SaveChanges();
            }
            else
            {
                viewModel.Id = 1;
                _context.Usuarios.Add(viewModel);
                _context.SaveChanges();
            }
            usuarioModel = _mapper.Map<UsuariosModel>(viewModel);

            //insere log
            var log = await _UsuarioService.ConsultaUltimoIdLog();

            if (log != 0)
            {
                LogUsuario l = new LogUsuario();
                l.Id = log + 1;
                l.idUsuario = viewModel.Id;
                l.dt_Log = DateTime.Now;
                l.acao = "Inclusão";
                _context.logAlteracoesUsuario.Add(l);
                _context.SaveChanges();
            }
            else
            {
                LogUsuario l = new LogUsuario();
                l.Id = 1;
                l.idUsuario = viewModel.Id;
                l.dt_Log = DateTime.Now;
                l.acao = "Inclusão";
                _context.logAlteracoesUsuario.Add(l);
                _context.SaveChanges();

            }
            return usuarioModel;
        }

        // PUT:
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartaoModel(long id, LoginModel usuarioModel)
        {
            try
            {
                var viewModel = _mapper.Map<Login>(usuarioModel);
                viewModel.Id = id;
                _context.Usuarios.Update(viewModel);
                _context.SaveChanges();

                //insere log
                var log = await _UsuarioService.ConsultaUltimoIdLog();

                if (log != 0)
                {
                    LogUsuario l = new LogUsuario();
                    l.Id = log + 1;
                    l.idUsuario = id;
                    l.dt_Log = DateTime.Now;
                    l.acao = "Alteração";
                    _context.logAlteracoesUsuario.Add(l);
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CustomResponse();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(long id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var cartaoModel = await _context.Usuarios.FindAsync(id);
            if (cartaoModel == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(cartaoModel);
            await _context.SaveChangesAsync();

            //insere log
            var log = await _UsuarioService.ConsultaUltimoIdLog();

            if (log != 0)
            {
                LogUsuario l = new LogUsuario();
                l.Id = log + 1;
                l.idUsuario = id;
                l.dt_Log = DateTime.Now;
                l.acao = "Exclusão";
                _context.logAlteracoesUsuario.Add(l);
                _context.SaveChanges();
            }

            return CustomResponse();
        }

        protected bool UsuarioModelExists(long id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });

        }
    }
}
