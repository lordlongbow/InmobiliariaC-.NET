/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class UsuarioController : Controller
    {


        
     
    }
}*/using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RolReositorio rolReositorio;
        private readonly UsuarioReositorio usuarioReositorio;
        private readonly IWebHostEnvironment environment;

        public UsuarioController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.rolReositorio = new RolReositorio();
            this.usuarioReositorio = new UsuarioReositorio();
        }

        [AllowAnonymous]
        public IActionResult Loggin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Loggin(Usuario model)
        {
            var user = UsuarioAutenticado(model.Username, model.Contrasenia);
            if (user != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Rol)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                );

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Usuario o Contraseña Incorrectos");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Loggin", "Usuario");
        }

        private Usuario UsuarioAutenticado(string username, string contrasenia)
        {
            var usuario = usuarioReositorio.GetUsuarioXUsername(username);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                string hashed = GenerarHash(contrasenia);
                if (usuario.Contrasenia == hashed)
                {
                    return usuario;
                }
            }
            return null;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.Roles = rolReositorio.GetRoles();
            // ViewBag.Rol = rolReositorio.GetRol(usuario.RolId);
            var lista = usuarioReositorio.GetUsuarios();

            return View(lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Detalles(int id)
        {
            Usuario usuario = usuarioReositorio.GetUsuario(id);
            return View(usuario);
        }

        public ActionResult MiPerfil(int id)
        {
            Usuario usuario = usuarioReositorio.GetUsuarioXUsername(User.Identity.Name);
            return View(usuario);
        }

        // GET: Usuario/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Crear()
        {
            ViewBag.Roles = rolReositorio.GetRoles();
            return View();
        }

        // POST: Usuario/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario Usuario)
        {
            try
            {
                string hashed = GenerarHash(Usuario.Contrasenia);
                Usuario.Contrasenia = hashed;

                var nbreRnd = Guid.NewGuid(); //posible nombre aleatorio
                int res = usuarioReositorio.Crear(Usuario);
                if (Usuario.Fotofisica != null && Usuario.UsuarioId > 0)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "imagenes");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName =
                        "avatar_"
                        + Usuario.UsuarioId
                        + Path.GetExtension(Usuario.Fotofisica.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    Usuario.foto = Path.Combine("/imagenes", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        Usuario.Fotofisica.CopyTo(stream);
                    }
                    usuarioReositorio.Actualizar(Usuario.UsuarioId, Usuario);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Usuario/Edit/5

        public ActionResult Editar(int id)
        {
            var usuario = usuarioReositorio.GetUsuario(id);
            ViewBag.Roles = rolReositorio.GetRoles();
            if (usuario.UsuarioId != id)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(usuario);
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Editar(int id, Usuario Usuario)
        {
            try
            {
                if (!User.IsInRole("Administrador"))
                {
                    var usuarioActual = usuarioReositorio.GetUsuarioXUsername(User.Identity.Name);
                    if (usuarioActual.UsuarioId != id)
                    {
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    Usuario.UsuarioId = id;
                if (Usuario.Contrasenia == null || Usuario.Contrasenia == "")
                {
                        Usuario.Contrasenia = usuarioActual.Contrasenia;
                }else{
                        string hashed = GenerarHash(Usuario.Contrasenia);
                        Usuario.Contrasenia = hashed;                  
                    }
                    
                  
                if (Usuario.Fotofisica != null)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "imagenes");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName =
                        "avatar_"
                        + Usuario.UsuarioId
                        + Path.GetExtension(Usuario.Fotofisica.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    Usuario.foto = Path.Combine("/imagenes", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                     Usuario.Fotofisica.CopyTo(stream);
                    }
                }else{
                    Usuario.foto = usuarioActual.foto;
                    }
                
                 usuarioReositorio.Actualizar(id, Usuario);
                            
                return RedirectToAction(nameof(Index),"Home");
                
            }
            }catch (Exception e)
            {
                throw;
            }
            return View(Usuario);
        }

        // GET: Usuario/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Borrar(int id)
        {
            if (User.IsInRole("Administrador"))
            {
                var usuario = usuarioReositorio.GetUsuario(id);
                return View(usuario);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Borrar(int id, Usuario Usuario)
        {
            try
            {
                if (User.IsInRole("Administrador"))
                {
                    Usuario.UsuarioId = id;
                    usuarioReositorio.Borrar(id);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string GenerarHash(string password)
        {
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: System.Text.Encoding.ASCII.GetBytes("ANTICONSTITUCIONALMENTE"),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );
            return hashed;
        }
    }
}
//INSERT INTO `usuario` (`UsuarioId`, `Username`, `password`, `Rolid`, `nombre`, `apellido`, `foto`) VALUES (NULL, 'Conejo', 'zanahoria', '1', 'Bugs', 'Bunny', ''), (NULL, 'Juan', 'soyJuan', '1', 'Juan', 'Castro', ''), (NULL, 'flash', 'force', '1', 'Barry', 'Allen', '');
