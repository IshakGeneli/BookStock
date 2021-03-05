using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitapeviStokTakipWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        ILogService logService;

        public LogsController(ILogService _logService)
        {
            this.logService = _logService;
        }

        [HttpGet]
        [Route("category")]
        public IEnumerable<CategoryLog> GetCategory()
        {
            IEnumerable<CategoryLog> logs = logService.GetAllCategoryLogs();

            return logs;
        }

        [HttpGet("{id}")]
        [Route("category")]
        public CategoryLog GetCategory(int id)
        {
            CategoryLog log = logService.GetCategoryLogById(id);

            return log;
        }

        [HttpDelete("{id}")]
        [Route("category")]
        public int DeleteCategory(int id)
        {
            return logService.DeleteCategoryLog(id);
        }

        [HttpGet]
        [Route("book")]
        public IEnumerable<BookLog> GetBook()
        {
            IEnumerable<BookLog> logs = logService.GetAllBookLogs();

            return logs;
        }

        [Route("book")]
        [HttpGet("{id}")]
        public BookLog GetBook(int id)
        {
            BookLog log = logService.GetBookLogById(id);

            return log;
        }

        [HttpDelete("{id}")]
        [Route("book")]
        public int DeleteBook(int id)
        {
            return logService.DeleteBookLog(id);
        }
    }
}