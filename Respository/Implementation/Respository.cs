using System;
using Microsoft.EntityFrameworkCore;
using TodoListAuthentication.Data;
using TodoListAuthentication.Models;
using TodoListAuthentication.Repository.Interface;
using WebApiAuth.Dto;
using WebApiAuth.Migrations;

namespace WebApiAuth.Respository.Services
{
    public class Respository : ItodoRepository


    {
        /// <summary>
        /// public readonly ItodoRepository _todoRepository;
        /// </summary>
        private readonly TodoContext _todoContext;

        public Respository(TodoContext todoContext)
        {
            _todoContext = todoContext;

        }


        public async Task<Todo> AddTodo(TodoDto todoDto)
        {
            var user = new Todo();
            user.TaskName = todoDto.TaskName;
            user.TaskDueDate = todoDto.TaskDueDate;

            var result = await _todoContext.AddAsync(user);
            await _todoContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task DeleteTodo(Guid todoId)
        {
            var todoResult = await _todoContext.Todos.FindAsync(todoId);
            if (todoResult != null)
            {
                _todoContext.Todos.Remove(todoResult);
                await _todoContext.SaveChangesAsync();
            }



        }

        public async Task<Todo> GetTodo(Guid TodoId)
        {
            var result = await _todoContext.Todos.FindAsync(TodoId);
            return result;
        }

        public async Task<Todo> GetTodoByDate(DateTime date)
        {
            return await _todoContext.Todos.FirstOrDefaultAsync(e => e.TaskCreatedDate == date);

        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            return await _todoContext.Todos.ToListAsync();
        }

        public async Task<IEnumerable<Todo>> SearchBytaskName(string taskName)
        {
            IQueryable<Todo> query = _todoContext.Todos;
            if (!string.IsNullOrEmpty(taskName))
            {
                query = query.Where(e => e.TaskName.Contains(taskName));


            }
            return await query.ToListAsync();


        }
        public async Task<Todo> UpdateTodo(Todo todo)
        {
            var result = await _todoContext.Todos.FirstOrDefaultAsync(e => e.TaskID == todo.TaskID);
            if (result != null)
            {
                result.TaskName = todo.TaskName;
                result.TaskID = todo.TaskID;
                result.TaskCreatedDate = todo.TaskCreatedDate;
                _todoContext.Update(result);
                await _todoContext.SaveChangesAsync();
                return result;

            }
            return null;
        }
    }
    }

