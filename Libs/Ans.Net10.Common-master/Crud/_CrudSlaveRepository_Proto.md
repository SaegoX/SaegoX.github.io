# _CrudSlaveRepository_Proto<T>

**Пространство имён:** `Ans.Net10.Common.Crud`  
**Исходный код:** `_CrudSlaveRepository_Proto.cs`

---

## Назначение

Абстрактный базовый класс репозитория для работы с подчинёнными (slave) сущностями, связанными с родительской записью. Предоставляет общий функционал CRUD-репозитория и методы для выборки данных по идентификатору родительской сущности (`MasterPtr`).

## Основные возможности

- Наследует базовый CRUD-репозиторий
- Работает только с сущностями, реализующими интерфейс `ISlaveEntity`
- Создаёт новый объект, связанный с указанной родительской записью
- Выполняет выборку данных по идентификатору родительской сущности
- Поддерживает дополнительную фильтрацию через LINQ-выражения
- Позволяет получить количество дочерних записей для выбранного родителя
- Использует `Entity Framework Core` для работы с базой данных

## Ограничения типа

Тип `T` должен:

- быть ссылочным типом (`class`)
- реализовывать интерфейс `ISlaveEntity`

## Публичные методы

| Метод | Описание |
|-------|----------|
| `GetNew(int masterPtr)` | Создаёт новый экземпляр дочерней сущности, связанный с указанным родителем. Метод должен быть реализован в производном классе. |
| `GetItemsAsQueryable(int masterPtr, Expression<Func<T, bool>> filter)` | Возвращает коллекцию дочерних сущностей в виде `IQueryable` с фильтрацией по `MasterPtr` и дополнительным пользовательским условием. |
| `GetItemsCount(int masterPtr)` | Возвращает количество дочерних записей, принадлежащих указанной родительской сущности. |

## Связанные интерфейсы

### ISlaveEntity

Интерфейс для сущностей, являющихся дочерними по отношению к другой сущности.

Наследует интерфейс `IMasterEntity`.

| Свойство | Описание |
|----------|----------|
| `Id` | Уникальный идентификатор сущности. |
| `MasterPtr` | Идентификатор родительской (master) сущности. |

### ICrudSlaveRepository<T>

Расширяет интерфейс `ICrudRepository<T>` и определяет дополнительные методы для работы с дочерними сущностями.

| Метод | Описание |
|-------|----------|
| `GetNew(int masterPtr)` | Создаёт новый экземпляр дочерней сущности для указанного родителя. |
| `GetItemsAsQueryable(int masterPtr, Expression<Func<T, bool>> filter)` | Возвращает коллекцию дочерних сущностей с фильтрацией по родителю и дополнительному условию. |
| `GetItemsCount(int masterPtr)` | Возвращает количество дочерних сущностей, связанных с указанным родителем. |

## Пример использования

```csharp
public class EmployeeTaskRepository
    : _CrudSlaveRepository_Proto<EmployeeTask>
{
    public EmployeeTaskRepository(AppDbContext db)
        : base(db)
    {
    }

    public override EmployeeTask GetNew(int masterPtr)
    {
        return new EmployeeTask
        {
            MasterPtr = masterPtr
        };
    }
}

var repository = new EmployeeTaskRepository(dbContext);

// Создание новой дочерней записи
EmployeeTask task = repository.GetNew(employeeId);

// Получение всех задач сотрудника
var tasks = repository.GetItemsAsQueryable(employeeId, null);

// Получение количества задач
int count = repository.GetItemsCount(employeeId);
```
