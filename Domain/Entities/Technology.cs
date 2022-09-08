using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Technology : Entity
    {
        public string Name { get; set; } = null!;
        public int ProgrammingLanguageId { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
        public Technology()
        {

        }
        public Technology(string name, int programmingLanguageId) : this()
        {
            Name = name;
            ProgrammingLanguageId = programmingLanguageId;
        }
    }
}
