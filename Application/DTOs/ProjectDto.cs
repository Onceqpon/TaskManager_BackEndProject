using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
