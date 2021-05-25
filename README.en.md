<p align="center"><img align="center" src="https://github.com/CacoCode/LBON/blob/master/logo.png"></p>

<p align="center"> 
    <a href="https://github.com/CacoCode/LBON/actions?query=workflow%3ALBON_MASTER+branch%3Amaster"><img src="https://github.com/CacoCode/LBON/workflows/LBON_MASTER/badge.svg?branch=master" alt="Github Build Status"></a>
    <a href="https://www.nuget.org/packages/LBON.Extensions"><img src="https://img.shields.io/nuget/v/LBON.Extensions.svg?style=flat-square" alt="nuget"></a>
    <a href="https://www.nuget.org/stats/packages/LBON.Extensions?groupby=Version"><img src="https://img.shields.io/nuget/dt/LBON.Extensions.svg?style=flat-square" alt="downloads"></a>
    <a href="https://github.com/CacoCode/LBON/blob/master/LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License"></a>
</p>
<p align="center"> 
    <a href="README.md">中文</a>|
    English
</p>

# Lu Ban Of .Net - .NET Luban Craftsman Auxiliary Library
. Net 's high available, efficient expansion library hopes to bring convenience to .NET developers and enthusiasts, farewell to 996 and away from ICU!!!

# Nuget

| **Name** |      **Nuget**      |
|----------|:-------------:|
| **LBON.Consts** | **[![NuGet](https://buildstats.info/nuget/LBON.Consts)](https://www.nuget.org/packages/LBON.Consts)** |
| **LBON.Extensions** | **[![NuGet](https://buildstats.info/nuget/LBON.Extensions)](https://www.nuget.org/packages/LBON.Extensions)** |
| **LBON.Helper** | **[![NuGet](https://buildstats.info/nuget/LBON.Helper)](https://www.nuget.org/packages/LBON.Helper)** |
| **LBON.DependencyInjection** | **[![NuGet](https://buildstats.info/nuget/LBON.DependencyInjection)](https://www.nuget.org/packages/LBON.DependencyInjection)** |
| **LBON.EntityFrameworkCore** | **[![NuGet](https://buildstats.info/nuget/LBON.EntityFrameworkCore)](https://www.nuget.org/packages/LBON.EntityFrameworkCore)** |

# Function Module
## LBON.EntityFrameworkCore
EntityFramework underlying implementation and extension classes, including the encapsulation of creating auditing fields, modifying auditing fields, deleting auditing fields, and extended fields

```Csharp
    public class FullAuditedEntity<TKey,TUser>:EntityBase<TKey>, ICreationAudited<TUser>, IModificationAudited<TUser>, IDeletionAudited<TUser>
    {
        public TUser CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public TUser LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public TUser DeleterId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
```
Also add the ExtendableObjectExtensions extension class to set the (SetData), get the (GetData) ExtendableObject field value. Enapsulate IEfRepository, for most EF operations
```Csharp
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity Find(TPrimaryKey id);

        Task<TEntity> FindAsync(TPrimaryKey id);

        TEntity Get(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] propertySelectors);

        void Insert(TEntity entity, bool autoSave = true);

        Task InsertAsync(TEntity entity, bool autoSave = true);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity, bool autoSave = true);

        void InsertList(List<TEntity> entities, bool autoSave = true);

        Task InsertListAsync(List<TEntity> entities, bool autoSave = true);

        void Update(TEntity entity, bool autoSave = true);

        Task UpdateAsync(TEntity entity, bool autoSave = true);

        void UpdateList(IEnumerable<TEntity> entities);

        Task UpdateListAsync(IEnumerable<TEntity> entities);

        void Delete(TPrimaryKey id, bool autoSave = true);

        Task DeleteAsync(TPrimaryKey id, bool autoSave = true);

        void Delete(TEntity entity, bool autoSave = true);

        Task DeleteAsync(TEntity entity, bool autoSave = true);

        void HardDelete(TPrimaryKey id, bool autoSave = true);

        Task HardDeleteAsync(TPrimaryKey id, bool autoSave = true);

        void HardDelete(TEntity entity, bool autoSave = true);

        Task HardDeleteAsync(TEntity entity, bool autoSave = true);
```
.When used in NET CORE , the **LBON.EntityFrameworkCore** library can be introduced that encapsulates the since - injection of IEfRepository and enables the automatic batch injection of the **IScopedDependency、ISingletonDependency、ITransientDependency** inheritance class to facilitate users to inject their own services. 
```Csharp
public void ConfigureServices(IServiceCollection services)
{
    services.ServiceRegister(Assembly.Load("AssemblyName"), Assembly.Load("AssemblyName"));
}
```
```Csharp
// Scoped
public interface IProductService: IScopedDependency
{
}
// Singleton
public interface IProductService: ISingletonDependency
{
}
// Transient
public interface IProductService: ITransientDependency
{
}
```
## LBON.Extensions
- ### [CollectionExtensions](Readmes/Extensions/COLLECTIONEXTENSIONS_README.md)
- ### [ComparableExtensions](Readmes/Extensions/COMPARABLEEXTENSIONS_README.md)
- ### [DataTableExtensions](Readmes/Extensions/DATATABLEEXTENSIONS_README.md)
- ### [DateTimeExtensions](Readmes/Extensions/DATETIMEEXTENSIONS_README.md)
- ### [DayOfWeekExtensions](Readmes/Extensions/DAYOFWEEKEXTENSIONS_README.md)
- ### [DecimalOrIntExtensions](Readmes/Extensions/DECIMALORINTEXTENSIONS_README.md)
- ### [DictionaryExtensions](Readmes/Extensions/DICTIONARYEXTENSIONS_README.md)
- ### [EnumerableExtensions](Readmes/Extensions/ENUMERABLEEXTENSIONS_README.md)
- ### [EnumExtensions](Readmes/Extensions/ENUMEXTENSIONS_README.md)
- ### [EventHandlerExtensions](Readmes/Extensions/EVENTHANDLEREXTENSIONS_README.md)
- ### [ExceptionExtensions](Readmes/Extensions/EXCEPTIONEXTENSIONS_README.md)
- ### [HttpResponseExtensions](Readmes/Extensions/HTTPRESPONSEEXTENSIONS_README.md)
- ### [ImageExtensions](Readmes/Extensions/IMAGEEXTENSIONS_README.md)
- ### [ListExtensions](Readmes/Extensions/LISTEXTENSIONS_README.md)
- ### [LockExtensions](Readmes/Extensions/LOCKEXTENSIONS_README.md)
- ### [ObjectExtensions](Readmes/Extensions/OBJECTEXTENSIONS_README.md)
- ### [StreamExtensions](Readmes/Extensions/STREAMEXTENSIONS_README.md)
- ### [StringExtensions](Readmes/Extensions/STRINGEXTENSIONS_README.md)
- ### [XmlExtensions](Readmes/Extensions/XMLEXTENSIONS_README.md)
## LBON.Helper
- ### [DirectoryHelper](Readmes/Helper/DIRECTORYHELPER_README.md)
- ### [FileHelper](Readmes/Helper/FILEHELPER_README.md)
- ### [RandomHelper](Readmes/Helper/RANDOMHELPER_README.md)
- ### [Utf8Helper](Readmes/Helper/UTF8HELPER_README.md)
## LBON.Consts
- ### [MediaType](LBON.Consts/MediaTypeConst.cs)
- ### [Regex](LBON.Consts/RegexConst.cs)

    
