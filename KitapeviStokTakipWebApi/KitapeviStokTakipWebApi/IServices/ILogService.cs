﻿using KitapeviStokTakipWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.IServices
{
    public interface ILogService
    {
        /*Category LOGS*/
        IEnumerable<CategoryLog> GetAllCategoryLogs();
        CategoryLog GetCategoryLogById(int id);
        int DeleteCategoryLog(int id);

        /*Book LOGS*/
        IEnumerable<BookLog> GetAllBookLogs();
        BookLog GetBookLogById(int id);
        int DeleteBookLog(int id);
    }
}
