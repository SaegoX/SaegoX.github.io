# _CrudMasterRepository_Proto<T>

## Назначение

Базовый репозиторий для работы со справочными (master) сущностями.

Наследует всю функциональность `__CrudRepository_Base<T>` и расширяет её методами создания новых объектов и получения количества записей.

---

## Дополнительные возможности

| Метод | Описание |
|--------|----------|
| GetNew() | Создаёт новый экземпляр сущности |
| GetItemsCount() | Возвращает количество записей |

---

## Используется совместно

- IMasterEntity
- ICrudMasterRepository<T>

---

## Наследование

```
ICrudRepository<T>
        │
__CrudRepository_Base<T>
        │
_CrudMasterRepository_Proto<T>
```
