using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyToDoApi.Models;
using MyToDoApi.Repositories;

namespace MyToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyTodoController : ControllerBase
    {
        private readonly MyTodoRepository _todoRepository;

        public MyTodoController(MyTodoRepository todoRepository)
        {
            _todoRepository = todoRepository; 
        }

        [HttpGet]
        public IActionResult GetMyTodos(
            [FromQuery] string? nameFilter,
            [FromQuery] int? priorityFilter,
            [FromQuery] bool? statusFilter,
            [FromQuery] DateOnly? dueDateFilter,
            [FromQuery] string? nameSort,
            [FromQuery] string? prioritySort,
            [FromQuery] string? dueDateSort)
        {
            var todos = _todoRepository.GetAllTodos(nameFilter, priorityFilter, statusFilter, dueDateFilter, nameSort, prioritySort, dueDateSort);
            
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetMyTodoById(int id)
        {
            var currrentTodo = _todoRepository.GetTodoById(id);

            return currrentTodo == null ? NotFound() : Ok(currrentTodo);    
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateMyTodo([FromBody] MyTodoItem item)
        {
            var createdTodo = _todoRepository.AddTodo(item);

            return CreatedAtAction(nameof(GetMyTodoById), new { id =  createdTodo.Id }, createdTodo);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateMyTodo(int id,  [FromBody] MyTodoItem updateItem)
        {
            if (!_todoRepository.UpdateTodo(id, updateItem))
            {
                return NotFound();  
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteMyTodo(int id)
        {
            if (!_todoRepository.DeleteTodo(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
