using Application.Instrutores.InputModels;
using Application.Instrutores.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Notification;

namespace WebApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class InstrutorController : ControllerBase
    {

        private readonly IInstrutoresApplication _instrutoresApplication;
        
        public InstrutorController(IInstrutoresApplication instrutorAplication)
        {
            _instrutoresApplication = instrutorAplication;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IEnumerable<Notifiable>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarInstrutor([FromBody] InstrutorInputModel instrutor)
        {
            try
            {
                if (instrutor == null)
                {
                    Log.Error("InstrutorInputModel recebido é nulo.");
                    return UnprocessableEntity("O corpo da requisição não pode ser nulo.");
                }
                
                await _instrutoresApplication.AdicionarInstrutorAsync(instrutor);

               
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao processar a requisição de adicionar instrutor: {Mensagem}", ex?.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
