using Core.DTOs;
using Infra.Helpers;
using Infra.Infrastructure;
using Core.Interfaces;
using Domain.Models;

namespace Infra.Repository
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context, CriptografiaHelper criptografiaHelper) : base(context)
        {
            _criptografiaHelper = criptografiaHelper;
        }

        public static CriptografiaHelper _criptografiaHelper { get; private set; }

        void IUsuarioRepository.CadastrarEmMassa()
        { 
            var _usuarios = new List<Usuario>()
            {
                new Usuario { Nome = "Wilson", Email = "wilson.carvalhais@gmail.com", Endereco = "Rua ABC, Rio de Janeiro, RJ", Senha= _criptografiaHelper.Criptografar("@Qwe123!@#"), DataNascimento = new DateTime(1981, 06, 24), TipoUsuario = 1 },
                new Usuario { Nome = "Camila", Email = "camila.rocha@example.com", Endereco = "Rua A, Rio de Janeiro, RJ", Senha= _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1992, 3, 15), TipoUsuario = 2 },
                new Usuario { Nome = "Lucas", Email = "lucas.silva@example.com", Endereco = "Av. Paulista, São Paulo, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1988, 7, 23), TipoUsuario = 1 },
                new Usuario { Nome = "Fernanda", Email = "fernanda.lima@example.com", Endereco = "Rua das Flores, Salvador, BA", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1995, 2, 10), TipoUsuario = 2 },
                new Usuario { Nome = "Rafael", Email = "rafael.souza@example.com", Endereco = "Rua 7 de Setembro, Curitiba, PR", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 12, 5), TipoUsuario = 1 },
                new Usuario { Nome = "Juliana", Email = "juliana.alves@example.com", Endereco = "Av. Brasil, Belo Horizonte, MG", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1993, 6, 18), TipoUsuario = 2 },
                new Usuario { Nome = "Bruno", Email = "bruno.costa@example.com", Endereco = "Rua Central, Fortaleza, CE", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1985, 4, 28), TipoUsuario = 1 },
                new Usuario { Nome = "Mariana", Email = "mariana.pereira@example.com", Endereco = "Av. Rio Branco, Recife, PE", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1991, 9, 12), TipoUsuario = 2 },
                new Usuario { Nome = "Thiago", Email = "thiago.gomes@example.com", Endereco = "Rua Nova, Porto Alegre, RS", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1989, 11, 3), TipoUsuario = 1 },
                new Usuario { Nome = "Patrícia", Email = "patricia.melo@example.com", Endereco = "Rua da Paz, Manaus, AM", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1994, 8, 7), TipoUsuario = 2 },
                new Usuario { Nome = "Carlos", Email = "carlos.fernandes@example.com", Endereco = "Av. Independência, Goiânia, GO", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1987, 1, 25), TipoUsuario = 1 },
                new Usuario { Nome = "Aline", Email = "aline.ramos@example.com", Endereco = "Rua Bela Vista, Florianópolis, SC", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1996, 10, 20), TipoUsuario = 2 },
                new Usuario { Nome = "Diego", Email = "diego.barros@example.com", Endereco = "Rua do Sol, Belém, PA", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1992, 5, 14), TipoUsuario = 1 },
                new Usuario { Nome = "Tatiane", Email = "tatiane.moura@example.com", Endereco = "Av. das Palmeiras, João Pessoa, PB", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 7, 2), TipoUsuario = 2 },
                new Usuario { Nome = "Eduardo", Email = "eduardo.nascimento@example.com", Endereco = "Rua Vitória, Natal, RN", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1986, 3, 9), TipoUsuario = 1 },
                new Usuario { Nome = "Isabela", Email = "isabela.teixeira@example.com", Endereco = "Rua Esperança, Campo Grande, MS", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1997, 6, 22), TipoUsuario = 2 },
                new Usuario { Nome = "Marcelo", Email = "marcelo.farias@example.com", Endereco = "Rua Alegre, Vitória, ES", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1984, 2, 16), TipoUsuario = 1 },
                new Usuario { Nome = "Larissa", Email = "larissa.freitas@example.com", Endereco = "Rua Horizonte, Aracaju, SE", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1993, 11, 30), TipoUsuario = 2 },
                new Usuario { Nome = "Henrique", Email = "henrique.campos@example.com", Endereco = "Rua Aurora, Teresina, PI", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1991, 1, 4), TipoUsuario = 1 },
                new Usuario { Nome = "Paula", Email = "paula.barbosa@example.com", Endereco = "Av. Leste-Oeste, Palmas, TO", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1995, 12, 17), TipoUsuario = 2 },
                new Usuario { Nome = "Rodrigo", Email = "rodrigo.machado@example.com", Endereco = "Rua Principal, Maceió, AL", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1989, 10, 13), TipoUsuario = 1 },
                new Usuario { Nome = "Vanessa", Email = "vanessa.cardoso@example.com", Endereco = "Rua da Liberdade, Porto Velho, RO", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 9, 6), TipoUsuario = 2 },
                new Usuario { Nome = "Fábio", Email = "fabio.santos@example.com", Endereco = "Rua do Comércio, Boa Vista, RR", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1987, 5, 1), TipoUsuario = 1 },
                new Usuario { Nome = "Simone", Email = "simone.goncalves@example.com", Endereco = "Av. Central, Macapá, AP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1994, 4, 19), TipoUsuario = 2 },
                new Usuario { Nome = "André", Email = "andre.lopes@example.com", Endereco = "Rua Aurora, Brasília, DF", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1985, 8, 11), TipoUsuario = 1 },
                new Usuario { Nome = "Beatriz", Email = "beatriz.oliveira@example.com", Endereco = "Rua das Acácias, São Luís, MA", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1992, 12, 29), TipoUsuario = 2 },
                new Usuario { Nome = "Pedro", Email = "pedro.martins@example.com", Endereco = "Rua Bela Vista, São Paulo, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 3, 8), TipoUsuario = 1 },
                new Usuario { Nome = "Renata", Email = "renata.araujo@example.com", Endereco = "Av. Getúlio Vargas, Belo Horizonte, MG", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1996, 7, 14), TipoUsuario = 2 },
                new Usuario { Nome = "Felipe", Email = "felipe.ribeiro@example.com", Endereco = "Rua São Pedro, Campinas, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1988, 1, 30), TipoUsuario = 1 },
                new Usuario { Nome = "Ana", Email = "ana.souza@example.com", Endereco = "Rua Dom Pedro, Salvador, BA", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1993, 6, 2), TipoUsuario = 2 },
                new Usuario { Nome = "João", Email = "joao.pinto@example.com", Endereco = "Av. Amazonas, Belo Horizonte, MG", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1985, 4, 10), TipoUsuario = 1 },
                new Usuario { Nome = "Letícia", Email = "leticia.camargo@example.com", Endereco = "Rua Tupinambás, São Paulo, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1997, 8, 19), TipoUsuario = 2 },
                new Usuario { Nome = "Gabriel", Email = "gabriel.novaes@example.com", Endereco = "Rua das Palmeiras, Rio de Janeiro, RJ", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1991, 11, 22), TipoUsuario = 1 },
                new Usuario { Nome = "Natália", Email = "natalia.silveira@example.com", Endereco = "Av. Ipiranga, Porto Alegre, RS", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1989, 9, 9), TipoUsuario = 2 },
                new Usuario { Nome = "Ricardo", Email = "ricardo.amaral@example.com", Endereco = "Rua do Comércio, Curitiba, PR", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1984, 12, 3), TipoUsuario = 1 },
                new Usuario { Nome = "Elaine", Email = "elaine.ferreira@example.com", Endereco = "Rua Laranjeiras, Recife, PE", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1992, 10, 6), TipoUsuario = 2 },
                new Usuario { Nome = "Alex", Email = "alex.souza@example.com", Endereco = "Rua XV de Novembro, Joinville, SC", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1986, 5, 28), TipoUsuario = 1 },
                new Usuario { Nome = "Carla", Email = "carla.mattos@example.com", Endereco = "Av. Boa Viagem, Recife, PE", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1994, 2, 13), TipoUsuario = 2 },
                new Usuario { Nome = "Douglas", Email = "douglas.menezes@example.com", Endereco = "Rua das Laranjeiras, Rio de Janeiro, RJ", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1987, 7, 5), TipoUsuario = 1 },
                new Usuario { Nome = "Mônica", Email = "monica.vieira@example.com", Endereco = "Rua Tiradentes, Teresina, PI", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 3, 17), TipoUsuario = 2 },
                new Usuario { Nome = "Vinícius", Email = "vinicius.azevedo@example.com", Endereco = "Rua Marechal Floriano, Porto Alegre, RS", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1988, 6, 21), TipoUsuario = 1 },
                new Usuario { Nome = "Roberta", Email = "roberta.dias@example.com", Endereco = "Av. dos Andradas, Belo Horizonte, MG", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1996, 1, 25), TipoUsuario = 2 },
                new Usuario { Nome = "Leandro", Email = "leandro.queiroz@example.com", Endereco = "Rua da Consolação, São Paulo, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1985, 8, 30), TipoUsuario = 1 },
                new Usuario { Nome = "Jéssica", Email = "jessica.martins@example.com", Endereco = "Rua Amapá, Cuiabá, MT", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1993, 5, 12), TipoUsuario = 2 },
                new Usuario { Nome = "Renan", Email = "renan.almeida@example.com", Endereco = "Av. das Nações, Brasília, DF", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1989, 12, 7), TipoUsuario = 1 },
                new Usuario { Nome = "Bruna", Email = "bruna.castro@example.com", Endereco = "Rua Itapoã, Goiânia, GO", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1991, 4, 26), TipoUsuario = 2 },
                new Usuario { Nome = "Igor", Email = "igor.barbosa@example.com", Endereco = "Rua Marechal Deodoro, Londrina, PR", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1987, 11, 10), TipoUsuario = 1 },
                new Usuario { Nome = "Débora", Email = "debora.nunes@example.com", Endereco = "Av. das Américas, Rio de Janeiro, RJ", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1994, 9, 1), TipoUsuario = 2 },
                new Usuario { Nome = "Matheus", Email = "matheus.bittencourt@example.com", Endereco = "Rua 13 de Maio, Ribeirão Preto, SP", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1990, 2, 18), TipoUsuario = 1 },
                new Usuario { Nome = "Sabrina", Email = "sabrina.macedo@example.com", Endereco = "Rua João Pessoa, Blumenau, SC", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1992, 7, 7), TipoUsuario = 2 },
                new Usuario { Nome = "Otávio", Email = "otavio.silva@example.com", Endereco = "Av. Carlos Gomes, Porto Alegre, RS", Senha = _criptografiaHelper.Criptografar("Senha@2024"), DataNascimento = new DateTime(1986, 6, 15), TipoUsuario = 1 },
            };
            _context.BulkInsert(_usuarios);
        }

        UsuarioDto IUsuarioRepository.ObterPedidosTodos(int id)
        {
            var _usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            return new UsuarioDto()
            {
                Id = _usuario.Id,
                DataCriacao = _usuario.DataCriacao.ToString("yyyy-MM-dd"),
                Nome = _usuario.Nome,
                DataNascimento = _usuario.DataNascimento.ToString("yyyy-MM-dd"),
                Email = _usuario.Email,
                TipoUsuario = _usuario.TipoUsuario,
                Pedidos = _usuario.Pedidos
                    .Select(Pedido => new PedidoDto()
                    {
                        Id = Pedido.Id,
                        DataCriacao = Pedido.DataCriacao.ToString("yyyy-MM-dd"),
                        UsuarioId = Pedido.UsuarioId,
                        GameId = Pedido.GameId,
                        Game = new GameDto()
                        {
                            Id = Pedido.Game.Id,
                            DataCriacao = Pedido.Game.DataCriacao.ToString("yyyy-MM-dd"),
                            Nome = Pedido.Game.Nome,
                            Produtora = Pedido.Game.Produtora,
                            Descricao = Pedido.Game.Descricao,
                            DataLancamento = Pedido.Game.DataLancamento.ToString("yyyy-MM-dd"),
                            Preco = Pedido.Game.Preco.ToString("F2"),
                        }
                    }).ToList(),
            };
        }

        public UsuarioDto ObterPedidosSeisMeses(int id)
        {
            var _usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);

            return new UsuarioDto()
            {
                Id = _usuario.Id,
                DataCriacao = _usuario.DataCriacao.ToString("yyyy-MM-dd"),
                Nome = _usuario.Nome,
                DataNascimento = _usuario.DataNascimento.ToString("yyyy-MM-dd"),
                Email = _usuario.Email,
                TipoUsuario = _usuario.TipoUsuario,
                Pedidos = _usuario.Pedidos
                    .Where(x => x.DataCriacao >= DateTime.Now.AddMonths(-6))
                    .Select(Pedido => new PedidoDto()
                    {
                        Id = Pedido.Id,
                        DataCriacao = Pedido.DataCriacao.ToString("yyyy-MM-dd"),
                        UsuarioId = Pedido.UsuarioId,
                        GameId = Pedido.GameId,
                        Game = new GameDto()
                        {
                            Id = Pedido.Game.Id,
                            DataCriacao = Pedido.Game.DataCriacao.ToString("yyyy-MM-dd"),
                            Nome = Pedido.Game.Nome,
                            Produtora = Pedido.Game.Produtora,
                            Descricao = Pedido.Game.Descricao,
                            DataLancamento = Pedido.Game.DataLancamento.ToString("yyyy-MM-dd"),
                            Preco = Pedido.Game.Preco.ToString("F2"),
                        }
                    }).ToList(),
            };
        }
    }
}
