using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.UpdateGithubProfileCommand
{
    public class UpdateGithubProfileCommandValidator : AbstractValidator<UpdateGitbubProfileCommand>
    {
        public UpdateGithubProfileCommandValidator()
        {
            RuleFor(c => c.NewGithubProfileLink).NotEmpty();
        }
    }
}
