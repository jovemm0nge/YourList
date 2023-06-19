using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var DailyTasks = _Context.DailyTasks.Where(x => !x.Deletado)
                .Include(x => x.Passos)
                .ToList();
            return Ok(DailyTasks);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var DailyTask = _Context.DailyTasks
                                        .Include(x => x.Passos)
                                        .SingleOrDefault(x => x.Id == Id && !x.Deletado);
            
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
            _Context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = DailyTasks.Id }, DailyTasks);
        }

        [HttpPost("{Id}/Passos")]
        public IActionResult PostPassos(Guid Id, Passos Passos)
        {
            Passos.DailyTaskId = Id;
            var DailyTask = _Context.DailyTasks.Any(x => x.Id == Id && !x.Deletado);

            if (!DailyTask)
            {
                return NotFound();
            }

            _Context.Passos.Add(Passos);
            _Context.SaveChanges();

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
            _Context.DailyTasks.Update(DailyTask);
            _Context.SaveChanges();

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
            _Context.SaveChanges();

            return Ok(DailyTask);
        }
    }
}
