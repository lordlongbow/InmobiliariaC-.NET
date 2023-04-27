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
            if (UsuarioAutenticado(model.Username, model.Contrasenia))
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Administrador")
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

        private bool UsuarioAutenticado(string username, string contrasenia)
        {
            var usuario = usuarioReositorio.GetUsuarioXUsername(username);
            if(usuario == null){
                return false;
            }else{
                if (usuario.Contrasenia == contrasenia && usuario.UsuarioId > 0){
                    return true;
                }
            }
            return false;
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


        [Authorize]
        public ActionResult CambioContrasenia(int id)
        {
            Usuario SuuestoUsuario = usuarioReositorio.GetUsuarioXUsername(User.Identity.Name);
            if (SuuestoUsuario.UsuarioId != id)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            
            return View(SuuestoUsuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CambioContrasenia(Usuario Usuario)
        {
            if (ModelState.IsValid)
                return View();
            try
            {
                usuarioReositorio.CambiarContrasenia(Usuario); 
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
               throw;
            }
            return View();
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
        public ActionResult Crear(Usuario Usuario, IFormFile foto)
        {
            try
            {
                //string contraseniaHash = usuarioReositorio.GenerarHash(Usuario.Contrasenia);


                string hashed = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: Usuario.Contrasenia,
                        salt: System.Text.Encoding.ASCII.GetBytes("ANTICONSTITUCIONALMENTE"),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    )
                );
                Usuario.Contrasenia = hashed;

                var nbreRnd = Guid.NewGuid(); //posible nombre aleatorio

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
                        return RedirectToAction(nameof(Index), "Home");
                }

                Usuario.UsuarioId = id;
                usuarioReositorio.Actualizar(id, Usuario);

                return View(Usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Usuario/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Borrar(int id)
        {
              if (!User.IsInRole("Administrador")){
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
                 if (!User.IsInRole("Administrador")){
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
    }
}
//INSERT INTO `usuario` (`UsuarioId`, `Username`, `password`, `Rolid`, `nombre`, `apellido`, `foto`) VALUES (NULL, 'Conejo', 'zanahoria', '1', 'Bugs', 'Bunny', ''), (NULL, 'Juan', 'soyJuan', '1', 'Juan', 'Castro', ''), (NULL, 'flash', 'force', '1', 'Barry', 'Allen', ''); 