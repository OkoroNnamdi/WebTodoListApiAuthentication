using TodoListAuthentication.Models;
using WebApiAuth.Dto;

namespace TodoListAuthentication.Repository.Interface
{
    public interface ItodoRepository
    {
        Task <IEnumerable<Todo>> SearchBytaskName(string taskName);
        Task<Todo> GetTodo(Guid TodoId);
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoByDate(DateTime date);
        Task<Todo> AddTodo(TodoDto todo);
        Task DeleteTodo(Guid todoId);
        Task<Todo> UpdateTodo(Todo todo);
    }
}
