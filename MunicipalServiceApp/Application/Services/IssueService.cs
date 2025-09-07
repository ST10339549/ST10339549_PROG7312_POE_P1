using MunicipalServiceApp.Application.Abstractions;
using MunicipalServiceApp.Domain;
using MunicipalServiceApp.Domain.Validation;
using MunicipalServiceApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp.Application.Services
{
    public sealed class IssueService : IIssueService
    {
        private readonly IIssueRepository _repo;

        public IssueService(IIssueRepository repo) => _repo = repo;

        public OperationResult<string> CreateIssue(Issue issue)
        {
            var validation = IssueValidator.Validate(issue, Domain.Categories.All);
            if (!validation.Success)
                return OperationResult<string>.Fail(validation.ErrorMessage!);

            _repo.Add(issue);

            var token = TrackingNumberGenerator.NewToken();
            return OperationResult<string>.Ok(token);
        }
    }
}
