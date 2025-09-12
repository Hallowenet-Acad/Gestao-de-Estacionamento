namespace Gestao_de_Estacionamento.Core.Dominio.Compartilhado;

public abstract class EntidadeBase<Tipo>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }

    public abstract void AtualizarRegistro(Tipo registroEditado);
}
