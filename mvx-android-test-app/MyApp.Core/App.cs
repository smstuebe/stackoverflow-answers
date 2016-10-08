using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net;

namespace MyApp.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }

    public class DataStorageService : IDataStorageService
    {
        private readonly SQLiteConnection _connection;

        public User UserData
        {
            get { return _connection.Table<User>().FirstOrDefault(); }
            set { _connection.InsertOrReplace(value); }
        }

        public DataStorageService(IMvxSqliteConnectionFactory factory)
        {
            _connection = factory.GetConnection("foo.db");
            _connection.CreateTable<User>();
        }
    }

    public interface IDataStorageService
    {
        User UserData { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
    }
}
