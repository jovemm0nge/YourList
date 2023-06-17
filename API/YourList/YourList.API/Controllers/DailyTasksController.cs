using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourList.API.Models;
using YourList.API.Persistence;

namespace YourList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyTasksController : ControllerBase
    {
        private readonly YourListDbContext _Context;
        public DailyTasksController(YourListDbContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var DailyTasks = _Context.DailyTasks.Where(x => !x.Deletado).ToList();
            return Ok(DailyTasks);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var DailyTask = _Context.DailyTasks.SingleOrDefault(x => x.Id == Id && !x.Deletado);
            
            if(DailyTask == null)
            {
                return NotFound();
            }

            return Ok(DailyTask);
        }

        [HttpPost]
        public IActionResult Post(DailyTasks DailyTasks)
        {
            _Context.DailyTasks.Add(DailyTasks);

            return CreatedAtAction(nameof(GetById), new { id = DailyTasks.Id }, DailyTasks);
        }

        [HttpPost("{Id}/Passos")]
        public IActionResult PostPassos(Guid Id, Passos Passos)
        {
            var DailyTask = _Context.DailyTasks.SingleOrDefault(x => x.Id == Id && !x.Deletado);

            if (DailyTask == null)
            {
                return NotFound();
            }

            DailyTask.Passos.Add(Passos);

            return NoContent();
        }


        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, DailyTasks DailyTasks)
        {
            var DailyTask = _Context.DailyTasks.SingleOrDefault(x => x.Id == Id);
           
            if (DailyTask == null)
            {
                return NotFound();
            }

            DailyTasks.Atualizar(DailyTasks);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            var DailyTask = _Context.DailyTasks.SingleOrDefault(x => x.Id == Id);
           
            if (DailyTask == null)
            {
                return NotFound();
            }

            DailyTask.Deletar();

            return Ok(DailyTask);
        }
    }
}
