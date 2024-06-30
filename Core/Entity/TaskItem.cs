using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool IsCompleted { get; set; }
    }
}
