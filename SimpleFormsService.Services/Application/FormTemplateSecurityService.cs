﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions.Application;

namespace SimpleFormsService.Services.Application;

public class FormTemplateSecurityService : ServiceBase, IFormTemplateSecurityService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FormTemplateSecurityService(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Determines whether or not the current user is authorized to work with the given form template.
    /// </summary>
    /// <param name="templateId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> IsUserAuthorized(string templateId, CancellationToken cancellationToken = default)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var email = user.Identity?.Name;

        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
        Guard.AgainstNullEmptyOrWhiteSpace(email, nameof(email));

        var formTemplate = await _repositoryManager.FormTemplateRepository.FindByCondition(x => x.Id == Guid.Parse(templateId))
            .FirstOrDefaultAsync(cancellationToken);

        Guard.AgainstObjectNotFound(formTemplate, "form template", templateId, nameof(formTemplate));

        var authorizedUsers = formTemplate.Data.AuthorizedUsers;

        return authorizedUsers.Any(authorizedUser => email.Equals(authorizedUser.User));
    }
}