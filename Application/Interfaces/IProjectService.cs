using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetProjectByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task AddProjectAsync(CreateProjectDto projectDto);
        Task UpdateProjectAsync(UpdateProjectDto projectDto);
        Task DeleteProjectAsync(int id);
    }
}
