using MunicipalServiceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp.Application.Abstractions
{
    public interface IIssueRepository
    {
        void Add(Issue issue);
        IReadOnlyList<Issue> GetAll();
    }
}
