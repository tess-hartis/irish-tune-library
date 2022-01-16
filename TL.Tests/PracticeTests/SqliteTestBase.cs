using Microsoft.EntityFrameworkCore;
using TL.Data;

namespace TL.Tests.PracticeTests;

public abstract class SqliteTestBase
{
    private bool _useSqlite;

    protected TuneLibraryContext SqLiteConnection()
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        if (_useSqlite)
        {
            builder.UseSqlite("DataSource=:memory:?cache=shared", x => { });
        }

        var dbContext = new TuneLibraryContext(builder.Options);

        return dbContext;
    }

    protected void UseSqlite()
    {
        _useSqlite = true;
    }
}