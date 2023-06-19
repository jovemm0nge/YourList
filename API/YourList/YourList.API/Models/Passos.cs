namespace YourList.API.Models
{
    public class Passos
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Sequencia { get; set; }
        public Guid DailyTaskId { get; set; }
    }
}
