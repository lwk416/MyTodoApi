namespace MyToDoApi.Models
{
    public class MyTodoItem
    {
        public int Id { get; set; } 
        public string Category {  get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateOnly DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
