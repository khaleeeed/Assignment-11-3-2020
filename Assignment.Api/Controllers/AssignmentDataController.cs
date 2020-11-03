using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Domain.Entity;
using Assignment.Domain.Models;
using Assignment.Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentDataController : ControllerBase
    {
        private readonly IDataBaseAssignmentContext _DBContext;

        public AssignmentDataController( IDataBaseAssignmentContext dbContext)
        {
            _DBContext = dbContext;
        }
      

        [HttpGet]
        public AssignmentTable Get (string key)
        {
            return _DBContext.AssignmentTable.FirstOrDefault(x => x.Key == key);
        }
    }
}
