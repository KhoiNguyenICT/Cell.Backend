using System;
using Cell.Helpers.Models;

namespace Cell.Helpers.Interfaces
{
    public interface IWriteProvider
    {
        OutputQuery InsertQuery(WriteModel model);
        OutputQuery UpdateQuery(WriteModel model);
        OutputQuery DeleteQuery(string tableName, Guid id);
    }
}