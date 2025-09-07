using MunicipalServiceApp.Application.Abstractions;
using MunicipalServiceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp.Infrastructure.Repositories
{
    public sealed class InMemoryIssueRepository : IIssueRepository
    {
        private readonly List<Issue> _items = new();

        public void Add(Issue issue) => _items.Add(issue);

        public IReadOnlyList<Issue> GetAll() => _items.AsReadOnly();
    }
}
