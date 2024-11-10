namespace CP3.Domain.Interfaces.Dtos
{
    public interface IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        void Validate();
    }
}
