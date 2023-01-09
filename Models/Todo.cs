using System.ComponentModel.DataAnnotations;
using WebApiAuth.Models;

namespace TodoListAuthentication.Models
{
    public class Todo
    {
        [Key]
        public Guid TaskID { get; set; } = new Guid();
        public string TaskName { get; set; }
        public DateTime TaskCreatedDate { get; set; } = DateTime.Now;
        public DateTime TaskDueDate { get; set; }
        private Boolean isDone;
        public  Boolean IsDone
        {
            set
            {
                if(TaskDueDate == DateTime.Now)
                {
                   isDone = true;
                }
            }
          
        }
        List<User> Users { get; set; }




    }
}
