namespace Application.Features.Developers.Dtos
{
    public class UpdatedGithubProfileDto
    {
        public int Id { get; set; }
        public string GithubLink { get; set; } = null!;
    }
}