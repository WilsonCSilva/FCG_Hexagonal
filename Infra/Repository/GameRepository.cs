using Infra.Infrastructure;
using Core.Interfaces;
using Domain.Models;

namespace Infra.Repository
{
    public class GameRepository : EFRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void CadastrarEmMassa()
        {
            var _games = new List<Game>() 
            {
                    new Game() { Nome = "Fortnite", Descricao = "Battle royale com eventos", DataLancamento = new DateTime(2017, 07, 25), Preco = 99.00M, Produtora = "Epic Games" },
                    new Game() { Nome = "League of Legends", Descricao = "MOBA competitivo online", DataLancamento = new DateTime(2009, 10, 27), Preco = 0.00M, Produtora = "Riot Games" },
                    new Game() { Nome = "Call of Duty: Warzone", Descricao = "FPS com modo battle royale", DataLancamento = new DateTime(2020, 03, 10), Preco = 38.90M, Produtora = "Activision" },
                    new Game() { Nome = "Minecraft", Descricao = "Criação e sobrevivência em blocos", DataLancamento = new DateTime(2011, 11, 18), Preco = 99.00M, Produtora = "Mojang Studios" },
                    new Game() { Nome = "Valorant", Descricao = "FPS tático com agentes únicos", DataLancamento = new DateTime(2020, 06, 02), Preco = 16.00M, Produtora = "Riot Games" },
                    new Game() { Nome = "Roblox", Descricao = "Plataforma de jogos e criação", DataLancamento = new DateTime(2006, 09, 01), Preco = 25.00M, Produtora = "Roblox Corporation" },
                    new Game() { Nome = "Apex Legends", Descricao = "Battle royale com heróis", DataLancamento = new DateTime(2019, 02, 04), Preco = 60.00M, Produtora = "Respawn Entertainment" },
                    new Game() { Nome = "FIFA 21", Descricao = "Simulador de futebol realista", DataLancamento = new DateTime(2020, 10, 09), Preco = 238.90M, Produtora = "EA Sports" },
                    new Game() { Nome = "PUBG", Descricao = "Battle royale realista", DataLancamento = new DateTime(2017, 12, 20), Preco = 50.00M, Produtora = "PUBG Corporation" },
                    new Game() { Nome = "Among Us", Descricao = "Dedução social em grupo", DataLancamento = new DateTime(2018, 06, 15), Preco = 24.50M, Produtora = "Innersloth" },
                    new Game() { Nome = "The Sims 4", Descricao = "Simulação de vida cotidiana", DataLancamento = new DateTime(2014, 09, 02), Preco = 99.90M, Produtora = "Maxis" },
                    new Game() { Nome = "Genshin Impact", Descricao = "Aventura RPG de mundo aberto", DataLancamento = new DateTime(2020, 09, 28), Preco = 0.00M, Produtora = "miHoYo" },
                    new Game() { Nome = "CS:GO", Descricao = "FPS competitivo em equipes", DataLancamento = new DateTime(2012, 08, 21), Preco = 0.00M, Produtora = "Valve" },
                    new Game() { Nome = "Rocket League", Descricao = "Futebol com carros turbo", DataLancamento = new DateTime(2015, 07, 07), Preco = 49.99M, Produtora = "Psyonix" },
                    new Game() { Nome = "Overwatch", Descricao = "FPS com heróis variados", DataLancamento = new DateTime(2016, 05, 24), Preco = 149.90M, Produtora = "Blizzard" },
                    new Game() { Nome = "Red Dead Redemption 2", Descricao = "Velho oeste em mundo aberto", DataLancamento = new DateTime(2018, 10, 26), Preco = 199.90M, Produtora = "Rockstar Games" },
                    new Game() { Nome = "Grand Theft Auto V", Descricao = "Ação em cidade fictícia", DataLancamento = new DateTime(2013, 09, 17), Preco = 99.00M, Produtora = "Rockstar Games" },
                    new Game() { Nome = "Elden Ring", Descricao = "RPG de ação em mundo sombrio", DataLancamento = new DateTime(2022, 02, 25), Preco = 299.00M, Produtora = "FromSoftware" },
                    new Game() { Nome = "Cyberpunk 2077", Descricao = "RPG futurista em Night City", DataLancamento = new DateTime(2020, 12, 10), Preco = 199.00M, Produtora = "CD Projekt" },
                    new Game() { Nome = "The Witcher 3", Descricao = "Aventura épica de Geralt", DataLancamento = new DateTime(2015, 05, 19), Preco = 89.90M, Produtora = "CD Projekt" },
                    new Game() { Nome = "Fall Guys", Descricao = "Corridas caóticas multiplayer", DataLancamento = new DateTime(2020, 08, 04), Preco = 37.99M, Produtora = "Mediatonic" },
                    new Game() { Nome = "Hogwarts Legacy", Descricao = "Magia no mundo de Harry Potter", DataLancamento = new DateTime(2023, 02, 10), Preco = 279.90M, Produtora = "Avalanche Software" },
                    new Game() { Nome = "Assassin's Creed Valhalla", Descricao = "Aventura viking épica", DataLancamento = new DateTime(2020, 11, 10), Preco = 199.90M, Produtora = "Ubisoft" },
                    new Game() { Nome = "Destiny 2", Descricao = "FPS online com loot e raids", DataLancamento = new DateTime(2017, 09, 06), Preco = 0.00M, Produtora = "Bungie" },
                    new Game() { Nome = "Sea of Thieves", Descricao = "Aventura pirata cooperativa", DataLancamento = new DateTime(2018, 03, 20), Preco = 129.90M, Produtora = "Rare" },
                    new Game() { Nome = "Dota 2", Descricao = "MOBA estratégico da Valve", DataLancamento = new DateTime(2013, 07, 09), Preco = 0.00M, Produtora = "Valve" },
                    new Game() { Nome = "Paladins", Descricao = "FPS com campeões únicos", DataLancamento = new DateTime(2018, 05, 08), Preco = 0.00M, Produtora = "Hi-Rez Studios" },
                    new Game() { Nome = "ARK: Survival Evolved", Descricao = "Sobrevivência com dinossauros", DataLancamento = new DateTime(2017, 08, 29), Preco = 89.99M, Produtora = "Studio Wildcard" },
                    new Game() { Nome = "Phasmophobia", Descricao = "Caça fantasmas cooperativa", DataLancamento = new DateTime(2020, 09, 18), Preco = 28.99M, Produtora = "Kinetic Games" },
                    new Game() { Nome = "Terraria", Descricao = "Aventura e construção 2D", DataLancamento = new DateTime(2011, 05, 16), Preco = 19.90M, Produtora = "Re-Logic" },
                    new Game() { Nome = "Hades", Descricao = "Roguelike no submundo grego", DataLancamento = new DateTime(2020, 09, 17), Preco = 47.99M, Produtora = "Supergiant Games" },
                    new Game() { Nome = "Stardew Valley", Descricao = "Fazendinha com RPG social", DataLancamento = new DateTime(2016, 02, 26), Preco = 24.99M, Produtora = "ConcernedApe" },
                    new Game() { Nome = "Dead by Daylight", Descricao = "Terror multiplayer assimétrico", DataLancamento = new DateTime(2016, 06, 14), Preco = 39.99M, Produtora = "Behaviour Interactive" },
                    new Game() { Nome = "F1 23", Descricao = "Simulador oficial da F1", DataLancamento = new DateTime(2023, 06, 16), Preco = 299.00M, Produtora = "Codemasters" },
                    new Game() { Nome = "NBA 2K24", Descricao = "Basquete com realismo extremo", DataLancamento = new DateTime(2023, 09, 08), Preco = 299.90M, Produtora = "Visual Concepts" },
                    new Game() { Nome = "Resident Evil 4 Remake", Descricao = "Terror em novo visual", DataLancamento = new DateTime(2023, 03, 24), Preco = 279.90M, Produtora = "Capcom" },
                    new Game() { Nome = "God of War Ragnarök", Descricao = "Ação mitológica intensa", DataLancamento = new DateTime(2022, 11, 09), Preco = 349.90M, Produtora = "Santa Monica Studio" },
                    new Game() { Nome = "The Last of Us Part I", Descricao = "Sobrevivência em remake", DataLancamento = new DateTime(2022, 09, 02), Preco = 349.90M, Produtora = "Naughty Dog" },
                    new Game() { Nome = "It Takes Two", Descricao = "Cooperação criativa e divertida", DataLancamento = new DateTime(2021, 03, 26), Preco = 149.90M, Produtora = "Hazelight Studios" },
                    new Game() { Nome = "Forza Horizon 5", Descricao = "Corridas em mundo aberto", DataLancamento = new DateTime(2021, 11, 09), Preco = 249.00M, Produtora = "Playground Games" },
                    new Game() { Nome = "Multiversus", Descricao = "Luta com personagens da Warner", DataLancamento = new DateTime(2022, 07, 19), Preco = 0.00M, Produtora = "Player First Games" },
                    new Game() { Nome = "Baldur's Gate 3", Descricao = "RPG baseado em D&D", DataLancamento = new DateTime(2023, 08, 03), Preco = 299.00M, Produtora = "Larian Studios" },
                    new Game() { Nome = "Diablo IV", Descricao = "RPG sombrio e intenso", DataLancamento = new DateTime(2023, 06, 05), Preco = 349.90M, Produtora = "Blizzard" },
                    new Game() { Nome = "Planet Zoo", Descricao = "Simulação de zoológico", DataLancamento = new DateTime(2019, 11, 05), Preco = 89.99M, Produtora = "Frontier Developments" },
                    new Game() { Nome = "RimWorld", Descricao = "Colônia espacial simulada", DataLancamento = new DateTime(2018, 10, 17), Preco = 55.99M, Produtora = "Ludeon Studios" },
                    new Game() { Nome = "Dark Souls III", Descricao = "Desafio e combate sombrio", DataLancamento = new DateTime(2016, 04, 12), Preco = 149.90M, Produtora = "FromSoftware" },
                    new Game() { Nome = "Monster Hunter: World", Descricao = "Caça cooperativa a monstros", DataLancamento = new DateTime(2018, 01, 26), Preco = 99.90M, Produtora = "Capcom" },
                    new Game() { Nome = "Halo Infinite", Descricao = "FPS sci-fi com Master Chief", DataLancamento = new DateTime(2021, 12, 08), Preco = 199.00M, Produtora = "343 Industries" },
            };
            _context.BulkInsert(_games);
        }
    }
}
