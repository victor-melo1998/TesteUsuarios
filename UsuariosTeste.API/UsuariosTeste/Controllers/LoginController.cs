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

namespace UsuariosTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ContextoDB _context;
        private readonly IMapper _mapper;

        public LoginController(ContextoDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet("{usuario}/{pwd}")]
        public async Task<ActionResult<LoginModel>> GetCartaoModel(string usuario, string pwd)
        {
            bool acessa = false;
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            List<Login> listLogin = await _context.Usuarios.ToListAsync();
            Login login = new Login();
            foreach (var item in listLogin)
            {
                if (item.Usuario == usuario && item.Pwd == pwd)
                {
                    login.Usuario = usuario;
                    login.Pwd = pwd;    
                    acessa = true;
                    break;
                }
                else
                {
                    acessa = false;
                }
            }
            
            if (listLogin == null)
            {
                return NotFound();
            }
            if (acessa)
            {
                var viewModel = _mapper.Map<LoginModel>(login);
                return viewModel;
            }
            else
            {
                return BadRequest("Usuário ou senha incorretos!");
            }

            return CustomResponse();
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
