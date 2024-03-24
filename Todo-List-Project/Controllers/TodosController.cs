using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Todo_List_Project.Data;
using Todo_List_Project.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly AppDbContext appDbContext;

        // Constructor to inject AppDbContext
        public TodosController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // POST: api/todos
        [HttpPost]
        public async Task<ActionResult<List<Todo>>> AddTodo(Todo newTodo)
        {
            // Check if newTodo is not null
            if (newTodo != null)
            {   
                //Add newTodo to db
                appDbContext.Todo.Add(newTodo);
                await appDbContext.SaveChangesAsync();

                //Return list of all todos
                return Ok(await appDbContext.Todo.ToListAsync());
            }
            return BadRequest("Object instance not set");
        }

        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAllTodos()
        {
            // Retrieve all todos from the database
            var todos = await appDbContext.Todo.ToListAsync();
            return Ok(todos);
        }

        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            // Retrieve todo by id from the database
            var todo = await appDbContext.Todo.FirstOrDefaultAsync(e => e.Id == id);
            if (todo != null)
            {
                return Ok(todo);
            }
            return NotFound("Todo is not available");
        }

        // PUT: api/todos
        [HttpPut]
        public async Task<ActionResult<Todo>> UpdateTodo(Todo updatedTodo)
        {
            // Check if updatedTodo is not null
            if (updatedTodo != null)
            {
                // Find existing todo by id
                var todo = await appDbContext.Todo.FirstOrDefaultAsync(e => e.Id == updatedTodo.Id);
                if (todo != null)
                {
                    //Update todo
                    todo.Title = updatedTodo.Title;
                    todo.Description = updatedTodo.Description;
                    todo.Status = updatedTodo.Status;
                    await appDbContext.SaveChangesAsync();
                    return Ok(todo);
                }
                else
                {
                    return NotFound("Todo not found");
                }
            }
            return BadRequest("Todo object instance not set");
        }

        // DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            // Find todo by id
            var todo = await appDbContext.Todo.FirstOrDefaultAsync(e => e.Id == id);
            if (todo != null)
            {
                // Remove todo from database
                appDbContext.Todo.Remove(todo);
                await appDbContext.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }

        // PUT: api/todos/{id}/completed
        [HttpPut("{id}/completed")]
        public async Task<ActionResult<Todo>> MarkTodoAsCompleted(int id)
        {
            // Find todo by id
            var todo = await appDbContext.Todo.FirstOrDefaultAsync(e => e.Id == id);
            if (todo != null)
            {
                // Mark todo as completed
                todo.Status = "Completed";
                await appDbContext.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }
        
    }
}
