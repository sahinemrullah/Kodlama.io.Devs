namespace Application.Features.Developers.Dtos
{
    public class DeletedGithubProfileDto
    {
        public int Id { get; set; }
        public string GithubLink { get; set; } = null!;
    }
}