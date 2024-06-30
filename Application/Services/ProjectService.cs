using Application.DTOs;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces;


namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return new ProjectDto { Id = project.Id, Name = project.Name };
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => new ProjectDto { Id = p.Id, Name = p.Name });
        }

        public async Task AddProjectAsync(CreateProjectDto projectDto)
        {
            var project = new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name
            };
            await _projectRepository.AddAsync(project);
        }

        public async Task UpdateProjectAsync(UpdateProjectDto projectDto)
        {
            var project = await _projectRepository.GetByIdAsync(projectDto.Id);
            if (project != null)
            {
                project.Name = projectDto.Name;
                await _projectRepository.UpdateAsync(project);
            }
        }

        public async Task DeleteProjectAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}
