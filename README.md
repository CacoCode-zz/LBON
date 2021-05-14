<p align="center"><img align="center" src="https://github.com/CacoCode/LBON/blob/master/logo.png"></p>

<p align="center"> 
    <a href="https://github.com/CacoCode/LBON/actions?query=workflow%3ALBON_MASTER+branch%3Amaster"><img src="https://github.com/CacoCode/LBON/workflows/LBON_MASTER/badge.svg?branch=master" alt="Github Build Status"></a>
    <a href="https://www.nuget.org/packages/LBON.Extensions"><img src="https://img.shields.io/nuget/v/LBON.Extensions.svg?style=flat-square" alt="nuget"></a>
    <a href="https://www.nuget.org/stats/packages/LBON.Extensions?groupby=Version"><img src="https://img.shields.io/nuget/dt/LBON.Extensions.svg?style=flat-square" alt="downloads"></a>
    <a href="https://github.com/CacoCode/LBON/blob/master/LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License"></a>
</p>

# Lu Ban Of .Net - .NET 鲁班工匠辅助库
.Net 高可用、高效率的扩展库，希望对 .NET 开发者及爱好者带来便捷，告别996，远离ICU！！！

# Nuget

| **名称** |      **Nuget**      |
|----------|:-------------:|
| **LBON.Consts** | **[![NuGet](https://buildstats.info/nuget/LBON.Consts)](https://www.nuget.org/packages/LBON.Consts)** |
| **LBON.Extensions** | **[![NuGet](https://buildstats.info/nuget/LBON.Extensions)](https://www.nuget.org/packages/LBON.Extensions)** |
| **LBON.Helper** | **[![NuGet](https://buildstats.info/nuget/LBON.Helper)](https://www.nuget.org/packages/LBON.Helper)** |
| **LBON.DependencyInjection** | **[![NuGet](https://buildstats.info/nuget/LBON.DependencyInjection)](https://www.nuget.org/packages/LBON.DependencyInjection)** |
| **LBON.EntityFrameworkCore** | **[![NuGet](https://buildstats.info/nuget/LBON.EntityFrameworkCore)](https://www.nuget.org/packages/LBON.EntityFrameworkCore)** |

# 功能模块
## LBON.EntityFrameworkCore
EntityFramework 底层实现和扩展类，包括创建审计字段、修改审计字段、删除审计字段和扩展字段的封装

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
另外还添加ExtendableObjectExtensions扩展类去设置（SetData）、获取(GetData) ExtendableObject字段的值。封装IEfRepository，满足绝大部分EF操作
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
在.NET CORE 中使用的话，可以引入**LBON.EntityFrameworkCore**库，里面封装了对IEfRepository的以来注入，并且实现了对**IScopedDependency、ISingletonDependency、ITransientDependency**继承类的自动批量注入，方便使用者注入自身服务。
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

    
