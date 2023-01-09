using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAuthentication.Data;
using TodoListAuthentication.Models;
using TodoListAuthentication.Repository.Interface;
using WebApiAuth.Dto;
using WebApiAuth.Models;

namespace WebApiAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TodoController : ControllerBase
    {
        private readonly ItodoRepository repository;
        private readonly TodoContext todoContext;
        public TodoController(ItodoRepository repository, TodoContext todoContext)
        {
            this.repository = repository;
            this.todoContext = todoContext;
        }
        [HttpGet("GetTodos"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetTodos()
        {
         return Ok(await repository.GetTodos());
           
        }
        [HttpPost("Add"),Authorize(Roles ="Admin")]
        public async Task<ActionResult<Todo>> CreateTodo(TodoDto todo)
        {
            //try
            //{
            //    if (tod == null)
            //    {
            //        return BadRequest();
            //    }
            //    var stud = await studentRepository.GetStudentByEmail(student.Email);
            //    if (stud != null)
            //    {
            //        ModelState.AddModelError("Email", "Userid email already in use");
            //        return BadRequest(ModelState);

            //    }
            //    var createdStudent = await studentRepository.AddStudent(student);
            //    return Ok(CreatedAtAction(nameof(GetStudent),
            //        new { id = createdStudent.UserId }, createdStudent));
            //}
            //catch (System.Exception)
            //{

            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //       "Error creating new todo record ");

            //}
            try
            {
                var createdTodo = await repository.AddTodo(todo);
                return Ok(createdTodo);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error creating new todo record ");
            }
        }



        [HttpDelete("Delete"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTodo(Guid todoID)
        {
            try
            {
                var todo = await repository.GetTodo(todoID);
                if (todo == null)
                {

                    return BadRequest($"Todo with {todoID} not found");

                }
                await repository.DeleteTodo(todoID);
                return Ok($"Todo with {todoID} deleted Sucessfully");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error deleting todos record ");
            }
        }
        [HttpPut("Update"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Todo>> UpdateTodo(Guid id, Todo todo)
        {
            // check the matching Id

            if (id != todo.TaskID)
            {
                return BadRequest($"The ID:{id} do not exist");
            }


            var updateTodo = repository.GetTodo(id);
            if (updateTodo == null)
            {
                return NotFound($"student with Id ={id} not found");
            }
            return Ok(await repository.UpdateTodo(todo));
        }

        [HttpGet("SearchByCreatedDate"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetTodoByDate(DateTime date)
        {
            return Ok(await repository.GetTodoByDate(date));
        }
        //public async Task<ActionResult> SearchByTaskName(string taskName)
        //{
        //    return Ok(await repository.SearchBytaskName(taskName));
        //}

        [HttpGet("pages"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Todo>>> GetTodos(int page)
        {
            if (todoContext.Todos == null)
            {
                return NotFound();
            }
            var pageResults = 2f;
            var pageCount = Math.Ceiling(todoContext.Todos.Count() / pageResults);
            var todoss = await todoContext.Todos
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults).ToListAsync();
            var response = new TodoResponse
            {
                Todolist = todoss,
                CurrentPage = page,

                Pages = (int)pageCount
            };
            return Ok(response);

        }


    }
}

