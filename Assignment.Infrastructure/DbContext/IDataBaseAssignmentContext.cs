using Assignment.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.DbContext
{
    public interface IDataBaseAssignmentContext
    {
        DbSet<AssignmentTable> AssignmentTable { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
