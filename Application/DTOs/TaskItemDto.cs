using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ProjectId { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CreateTaskItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ProjectId { get; set; }
    }

    public class UpdateTaskItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ProjectId { get; set; }
        public bool IsCompleted { get; set; }
    }

}
