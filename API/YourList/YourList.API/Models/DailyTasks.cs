namespace YourList.API.Models
{
    public class DailyTasks
    {
        public DailyTasks()
        {
            Passos = new List<Passos>();
            Deletado = false;
        }

        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Classificacao { get; set; }
        public string Prioridade { get; set; }

        public bool? Concluida { get; set; }

        public List<Passos> Passos { get; set;}

        public bool Deletado { get; set; }

        public void Atualizar(DailyTasks DailyTasks)
        {
            Titulo = DailyTasks.Titulo;
            Descricao = DailyTasks.Descricao;
            Classificacao = DailyTasks.Classificacao;
            Prioridade = DailyTasks.Prioridade;
            Concluida = DailyTasks.Concluida; 
        }

        public void Deletar()
        {
            Deletado = true;
        }
    }
}
