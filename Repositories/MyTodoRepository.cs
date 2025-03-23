using MyToDoApi.Models;

namespace MyToDoApi.Repositories
{
    public class MyTodoRepository
    {
        private readonly List<MyTodoItem> _todoItems = new();
        private int _currentId = 1;

        public IEnumerable<MyTodoItem> GetAllTodos(
           string? nameFilter = null,
           int? priorityFilter = null,
           bool? statusFilter = null,
           DateOnly? dueDateFilter = null,
           string? nameSort = null,
           string? prioritySort = null,
           string? dueDateSort = null)
        {
            var query = _todoItems.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(r => r.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (priorityFilter.HasValue)
            {
                query = query.Where(r => r.Priority == priorityFilter);
            }

            if (statusFilter.HasValue)
            {
                query = query.Where(r => r.IsCompleted == statusFilter);
            }

            if (dueDateFilter.HasValue)
            {
                query = query.Where(r => r.DueDate == dueDateFilter);
            }

            query = ApplySorting(query, nameSort, prioritySort, dueDateSort);

            return query.ToList();
        }

        private static IQueryable<MyTodoItem> ApplySorting(IQueryable<MyTodoItem> query, string? nameSort, string? prioritySort, string? dueDateSort)
        {
            query = nameSort?.ToLower() switch
            {
                "asc" => query.OrderBy(r => r.Name),
                "desc" => query.OrderByDescending(r => r.Name),
                _ => query
            };

            query = prioritySort?.ToLower() switch
            {
                "asc" => query.OrderBy(r => r.Priority),
                "desc" => query.OrderByDescending(r => r.Priority),
                _ => query
            };

            query = dueDateSort?.ToLower() switch
            {
                "asc" => query.OrderBy(r => r.DueDate),
                "desc" => query.OrderByDescending(r => r.DueDate),
                _ => query
            };

            return query;
        }

        public MyTodoItem? GetTodoById(int id)
        {
            return _todoItems.FirstOrDefault(r => r.Id == id);
        }

        public MyTodoItem AddTodo(MyTodoItem item)
        {
            item.Id = _currentId++;
            _todoItems.Add(item);
            return item;
        }

        public bool UpdateTodo(int id, MyTodoItem updateItem)
        {
            var currentItem = GetTodoById(id);

            if (currentItem == null)
            {
                return false; 
            }

            currentItem.Category = updateItem.Category;
            currentItem.Name = updateItem.Name;
            currentItem.Description = updateItem.Description;
            currentItem.Priority = updateItem.Priority;
            currentItem.DueDate = updateItem.DueDate;
            currentItem.IsCompleted = updateItem.IsCompleted;   

            return true;
        }

        public bool DeleteTodo(int id)
        {
            var currentItem = GetTodoById(id);

            if (currentItem == null)
            {
                return false;
            }

            _todoItems.Remove(currentItem);

            return true;
        }
    }
}
