# CRUD Framework

Библиотека `Ans.Net10.Common.Crud` предоставляет набор универсальных репозиториев для работы с базой данных через Entity Framework Core.

---

## Архитектура

```
                ICrudRepository<T>
                       │
                       │
             __CrudRepository_Base<T>
                  /               \
                 /                 \
CrudMasterRepository      CrudSlaveRepository

```

---

## Основные компоненты

| Компонент | Назначение |
|-----------|------------|
| `ICrudRepository<T>` | Общий контракт CRUD-репозитория |
| `__CrudRepository_Base<T>` | Базовая реализация CRUD |
| `_CrudMasterRepository_Proto<T>` | Репозиторий справочников |
| `_CrudSlaveRepository_Proto<T>` | Репозиторий дочерних сущностей |

---

## Иерархия

Все репозитории наследуются от `__CrudRepository_Base<T>` и получают базовую реализацию CRUD-операций.

Специализированные репозитории добавляют только функциональность, необходимую для конкретного типа сущностей.

- **Master** — независимые записи.
- **Slave** — записи, принадлежащие определённой master-сущности.
