using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Datos
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
