# _CrudMasterRepository_Proto\<T\>

**Пространство имён:** `Ans.Net10.Common.Crud`  
**Исходный код:** [_CrudMasterRepository_Proto.cs](../../Libs/Ans.Net10.Common-master/Crud/_CrudMasterRepository_Proto.cs)

---

## Назначение

Абстрактный класс-заготовка для репозиториев справочных (master) сущностей.  
Наследует всю базовую CRUD-функциональность от [`__CrudRepository_Base<T>`](common/crud/__CrudRepository_Base.md) и реализует интерфейс [`ICrudMasterRepository<T>`](common/crud/ICrudMasterRepository.md).

Добавляет методы, характерные именно для справочников: создание нового экземпляра сущности и получение общего количества записей.

## Отличия от родительского класса

- **Ограничение типа:** `T` должен реализовывать [`IMasterEntity`](common/crud/IMasterEntity.md) (требует свойство `Id`).
- **Новый абстрактный метод** `GetNew()` – наследник обязан предоставить логику создания нового объекта.
- **Новый виртуальный метод** `GetItemsCount()` – возвращает количество **всех** записей (без фильтра). Может быть переопределён.
- Не меняет поведение унаследованных CRUD-операций.

## Собственные публичные методы

| Метод | Описание |
|-------|----------|
| `abstract T GetNew()` | Создаёт и возвращает новый экземпляр сущности с параметрами по умолчанию. |
| `virtual int GetItemsCount()` | Возвращает общее количество записей в таблице (через `DbSet.Count()`). |

## Связанные интерфейсы

- [`IMasterEntity`](common/crud/IMasterEntity.md) – контракт для сущности-справочника (обязательно свойство `Id`).
- [`ICrudMasterRepository<T>`](common/crud/ICrudMasterRepository.md) – интерфейс, объединяющий CRUD и дополнительные методы справочника.

## Пример реализации

```csharp
public class CountryRepository : _CrudMasterRepository_Proto<Country>
{
    public CountryRepository(AppDbContext db) : base(db) { }

    public override Country GetNew() => new Country { Name = "Новая страна" };
}

// Использование
var repo = new CountryRepository(context);
Country c = repo.GetNew();
int total = repo.GetItemsCount();
var all = repo.GetItemsAsQueryable(null).ToList();  // унаследованный метод
