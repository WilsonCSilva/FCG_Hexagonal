using System;
using System.Collections.Generic;
using System.Linq;
using Core.DTOs;
using Core.Interfaces;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TEST_FCG._Tests_.Repository
{
    [TestClass]
    public class UsuarioRepositoryTest
    {
        private Mock<IUsuarioRepository> _usuarioRepoMock;
        private List<Usuario> _usuarios;
        private UsuarioDto _usuarioDtoSample;

        [TestInitialize]
        public void Setup()
        {
            _usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "Wilson",
                    Email = "wilson.carvalhais@gmail.com",
                    Endereco = "Rua ABC, Rio de Janeiro, RJ",
                    Senha = "encrypted",
                    DataNascimento = new DateTime(1981, 6, 24),
                    TipoUsuario = 1,
                    Pedidos = new List<Pedido>()
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Camila",
                    Email = "camila.rocha@example.com",
                    Endereco = "Rua A, Rio de Janeiro, RJ",
                    Senha = "encrypted",
                    DataNascimento = new DateTime(1992, 3, 15),
                    TipoUsuario = 2,
                    Pedidos = new List<Pedido>()
                }
            };

            _usuarioDtoSample = new UsuarioDto
            {
                Id = 1,
                Nome = "Wilson",
                Email = "wilson.carvalhais@gmail.com",
                Endereco = "Rua ABC, Rio de Janeiro, RJ",
                Senha = "encrypted",
                DataNascimento = "1981-06-24",
                TipoUsuario = 1,
                DataCriacao = DateTime.Now.ToString("yyyy-MM-dd"),
                Pedidos = new List<PedidoDto>()
            };

            _usuarioRepoMock = new Mock<IUsuarioRepository>();
        }

        [TestMethod]
        public void ObterTodosReturnsAllUsuarios()
        {
            _usuarioRepoMock.Setup(r => r.ObterTodos()).Returns(_usuarios);

            var result = _usuarioRepoMock.Object.ObterTodos();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(u => u.Nome == "Wilson"));
            Assert.IsTrue(result.Any(u => u.Nome == "Camila"));
        }

        [TestMethod]
        public void ObterPorIDReturnsCorrectUsuario()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorID(1)).Returns(_usuarios[0]);

            var result = _usuarioRepoMock.Object.ObterPorID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Wilson", result.Nome);
        }

        [TestMethod]
        public void ObterPorIDReturnsNullIfNotFound()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorID(99)).Returns((Usuario)null);

            var result = _usuarioRepoMock.Object.ObterPorID(99);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorNomeReturnsCorrectUsuario()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorNome("Camila")).Returns(_usuarios[1]);

            var result = _usuarioRepoMock.Object.ObterPorNome("Camila");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [TestMethod]
        public void ObterPorNomeReturnsNullIfNotFound()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorNome("NotFound")).Returns((Usuario)null);

            var result = _usuarioRepoMock.Object.ObterPorNome("NotFound");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorEmailReturnsCorrectUsuario()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorEmail("wilson.carvalhais@gmail.com")).Returns(_usuarios[0]);

            var result = _usuarioRepoMock.Object.ObterPorEmail("wilson.carvalhais@gmail.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Wilson", result.Nome);
        }

        [TestMethod]
        public void ObterPorEmailReturnsNullIfNotFound()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorEmail("notfound@email.com")).Returns((Usuario)null);

            var result = _usuarioRepoMock.Object.ObterPorEmail("notfound@email.com");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CadastrarAddsUsuario()
        {
            var newUsuario = new Usuario
            {
                Id = 3,
                Nome = "Lucas",
                Email = "lucas.silva@example.com",
                Endereco = "Av. Paulista, São Paulo, SP",
                Senha = "encrypted",
                DataNascimento = new DateTime(1988, 7, 23),
                TipoUsuario = 1,
                Pedidos = new List<Pedido>()
            };

            _usuarioRepoMock.Setup(r => r.Cadastrar(newUsuario)).Callback<Usuario>(u => _usuarios.Add(u));

            _usuarioRepoMock.Object.Cadastrar(newUsuario);

            Assert.IsTrue(_usuarios.Any(u => u.Id == 3));
        }

        [TestMethod]
        public void AlterarUpdatesUsuario()
        {
            var updatedUsuario = new Usuario
            {
                Id = 1,
                Nome = "Wilson Updated",
                Email = "wilson.carvalhais@gmail.com",
                Endereco = "Rua ABC, Rio de Janeiro, RJ",
                Senha = "encrypted",
                DataNascimento = new DateTime(1981, 6, 24),
                TipoUsuario = 1,
                Pedidos = new List<Pedido>()
            };

            _usuarioRepoMock.Setup(r => r.Alterar(updatedUsuario)).Callback<Usuario>(u =>
            {
                var idx = _usuarios.FindIndex(x => x.Id == u.Id);
                if (idx >= 0) _usuarios[idx] = u;
            });

            _usuarioRepoMock.Object.Alterar(updatedUsuario);

            var usuario = _usuarios.FirstOrDefault(u => u.Id == 1);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("Wilson Updated", usuario.Nome);
        }

        [TestMethod]
        public void DeletarRemovesUsuario()
        {
            _usuarioRepoMock.Setup(r => r.Deletar(2)).Callback<int>(id =>
            {
                var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
                if (usuario != null) _usuarios.Remove(usuario);
            });

            _usuarioRepoMock.Object.Deletar(2);

            Assert.IsFalse(_usuarios.Any(u => u.Id == 2));
        }

        [TestMethod]
        public void CadastrarEmMassaCallsMethod()
        {
            var called = false;
            _usuarioRepoMock.Setup(r => r.CadastrarEmMassa()).Callback(() => called = true);

            _usuarioRepoMock.Object.CadastrarEmMassa();

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ObterPedidosTodosReturnsUsuarioDto()
        {
            _usuarioRepoMock.Setup(r => r.ObterPedidosTodos(1)).Returns(_usuarioDtoSample);

            var result = _usuarioRepoMock.Object.ObterPedidosTodos(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Wilson", result.Nome);
        }

        [TestMethod]
        public void ObterPedidosSeisMesesReturnsUsuarioDto()
        {
            _usuarioRepoMock.Setup(r => r.ObterPedidosSeisMeses(1)).Returns(_usuarioDtoSample);

            var result = _usuarioRepoMock.Object.ObterPedidosSeisMeses(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Wilson", result.Nome);
        }
    }
}
