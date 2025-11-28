# POC-Synchronizer-Tool
-This solution contains two separate projects working together to synchronize
data between a local database and a remote server.  
The goal of the project is to create a **generic sync system** that can sync
**any entity/table** (e.g., User, Hobby, etc.) without writing separate sync logic.

**Update Connection Strings**

MyApp WPF Project: Update in App.xaml.cs and DbContextFactory.
MyApp.SyncAPI Project: Update DefaultConnection in appsettings.json.
