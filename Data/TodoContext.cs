using Microsoft.EntityFrameworkCore;
using TodoListAuthentication.Models;
using WebApiAuth.Models;

namespace TodoListAuthentication.Data
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext>options):base(options)
        {

        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
