using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        public static void DeveloperShouldExistWhenRequested(Developer? developer)
        {
            if (developer == null) throw new BusinessException("Requested developer does not exist");
        }
        public static void RoleShouldExistWhenRequested(OperationClaim? operationClaim)
        {
            if (operationClaim == null) throw new BusinessException("Requested role does not exist");
        }

        public static void IdentityResultMustSucceededWhenRegister(IdentityResult identityResult)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            if (!identityResult.Succeeded)
                throw new BusinessException(JsonSerializer.Serialize(identityResult.Errors.Select(e => e.Description), options: jsonSerializerOptions));
        }
    }
}
