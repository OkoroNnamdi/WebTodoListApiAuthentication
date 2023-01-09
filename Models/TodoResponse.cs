using TodoListAuthentication.Models;

namespace WebApiAuth.Models
{
    public class TodoResponse
    {
        
        
            public List<Todo> Todolist { get; set; } = new List<Todo>();
            public int Pages { get; set; }
            public int CurrentPage { get; set; }
        
    }
}
