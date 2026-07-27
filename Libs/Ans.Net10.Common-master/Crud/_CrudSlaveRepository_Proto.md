
---

## 2. `_CrudSlaveRepository_Proto<T>` – репозиторий для подчинённых сущностей

Файл `crud/_CrudSlaveRepository_Proto.md`

```markdown
# _CrudSlaveRepository_Proto\<T\>

**Пространство имён:** `Ans.Net10.Common.Crud`  
**Исходный код:** [_CrudSlaveRepository_Proto.cs](../../Libs/Ans.Net10.Common-master/Crud/_CrudSlaveRepository_Proto.cs)

---

## Назначение

Абстрактный класс-заготовка для репозиториев **подчинённых (slave) сущностей**, которые всегда принадлежат какой-либо родительской записи (связь через внешний ключ `MasterPtr`).

Наследует всю CRUD-логику от [`__CrudRepository_Base<T>`](common/crud/__CrudRepository_Base.md) и реализует интерфейс [`ICrudSlaveRepository<T>`](common/crud/ICrudSlaveRepository.md).

Ключевое отличие – все дополнительные методы чтения и подсчёта завязаны на идентификатор родителя (`masterPtr`).

## Отличия от родительского класса

- **Ограничение типа:** `T` должен реализовывать [`ISlaveEntity`](common/crud/ISlaveEntity.md) (наследует `IMasterEntity` и добавляет свойство `MasterPtr`).
- **Новый абстрактный метод** `GetNew(int masterPtr)` – создаёт новый экземпляр с уже проставленной ссылкой на родителя.
- **Новая перегрузка** `GetItemsAsQueryable(int masterPtr, Expression<Func<T, bool>> filter)` – всегда фильтрует по `masterPtr`, а дополнительный фильтр (если задан) комбинируется с ним через метод расширения `And`.
- **Новый метод** `GetItemsCount(int masterPtr)` – возвращает количество записей для конкретного родителя (использует унаследованный `GetItemsCount(filter)`).

> **Важно:** унаследованные методы `GetItem`, `GetItemsAsQueryable(filter)`, `GetItemsCount(filter)` и т.д. остаются доступны и могут применяться, но обычно для подчинённых сущностей удобнее использовать методы с параметром `masterPtr`.

## Собственные публичные методы

| Метод | Описание |
|-------|----------|
| `abstract T GetNew(int masterPtr)` | Создаёт новый экземпляр с установленным свойством `MasterPtr = masterPtr`. |
| `virtual IQueryable<T> GetItemsAsQueryable(int masterPtr, Expression<Func<T, bool>> filter)` | Возвращает запрос, отфильтрованный по `MasterPtr`. Если задан дополнительный `filter`, он объединяется с основным условием через `And`. |
| `virtual int GetItemsCount(int masterPtr)` | Возвращает количество записей, относящихся к указанному родителю. |

## Примечание о методе `And`

Для комбинации фильтров используется метод расширения `And` (предположительно из пространства имён `Ans.Net10.Common.Classes` или `~exts`). Он обеспечивает объединение выражений через `Expression.AndAlso`.

Если метод `And` отсутствует в вашем проекте, его легко реализовать самостоятельно (см. примеры в интернете).

## Связанные интерфейсы

- [`ISlaveEntity`](common/crud/ISlaveEntity.md) – интерфейс подчинённой сущности (включает `Id` и `MasterPtr`).
- [`ICrudSlaveRepository<T>`](common/crud/ICrudSlaveRepository.md) – полный контракт репозитория для подчинённых сущностей.

## Пример реализации

```csharp
public class OrderItemRepository : _CrudSlaveRepository_Proto<OrderItem>
{
    public OrderItemRepository(AppDbContext db) : base(db) { }

    public override OrderItem GetNew(int masterPtr)
        => new OrderItem { MasterPtr = masterPtr };
}

// Использование
var repo = new OrderItemRepository(context);

// Создать новый элемент для заказа с Id = 5
var newItem = repo.GetNew(5);

// Получить все элементы заказа 5, у которых цена > 100
var expensive = repo.GetItemsAsQueryable(5, x => x.Price > 100).ToList();

// Количество элементов заказа 5
int count = repo.GetItemsCount(5);
